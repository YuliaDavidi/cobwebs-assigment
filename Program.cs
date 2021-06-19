using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace FBgame
{
    class Program
    {
        static void Main(string[] args)
        {      
            List<Player> players = InitPlayers();
            if (players.Count > 0)
            {
                StartGame(players);
            }
        }

        private static List<Player> InitPlayers()
        {
            List<Player> players = new List<Player>();
            int numberOfplayers = 0;
            while (!(numberOfplayers >= 2 && numberOfplayers <= 8))
            {
                Console.WriteLine($"Please enter number of players (2-8): ");
                var input = Console.ReadLine();
                if (!int.TryParse(input, out numberOfplayers))
                {
                    Console.WriteLine($"Wrong number, try again");
                }
                else
                {
                    if (!(numberOfplayers >= 2 && numberOfplayers <= 8))
                    {
                        Console.WriteLine($"Number is not in range");
                    }
                }
            }

            for (int i = 0; i < numberOfplayers; i++)
            {
                int type = 0;
                while (!(type >= 1 && type <= 5))
                {
                    Console.WriteLine($"Please enter Player{i + 1} type 1=Random, 2=Memory, 3=Thorough, 4=Cheater, 5=ThoroughCheater (1-5): ");
                    var input = Console.ReadLine();
                    if (!int.TryParse(input, out type))
                    {
                        Console.WriteLine($"Wrong number, try again");
                    }
                    else
                    {
                        if (!(type >= 1 && type <= 5))
                        {
                            Console.WriteLine($"Number is not in range");
                        }
                        else
                        {
                            Console.WriteLine($"Please enter the player's name");
                            var name = Console.ReadLine();
                            switch (type)
                            {
                                case 1:
                                    {
                                        players.Add(new Player($"{name}(Random)", new RandomST()));
                                        break;
                                    }
                                case 2:
                                    {
                                        players.Add(new Player($"{name}(Memory)", new Memory()));
                                        break;
                                    }
                                case 3:
                                    {
                                        players.Add(new Player($"{name}(Thorough)", new Thorough()));
                                        break;
                                    }
                                case 4:
                                    {
                                        players.Add(new Player($"{name}(Cheater)", new Cheater()));
                                        break;
                                    }
                                case 5:
                                    {
                                        players.Add(new Player($"{name}(ThoroughCheater)", new ThoroughCheater()));
                                        break;
                                    }
                            }

                        }
                    }
                }
            }
            return players;
        }

        private static void StartGame(List<Player> players)
        {          
            const int TOTAL_ALLOWED_MOVES = 100;
            const int TIMEOUT = 1500; //For bonus

            Random random = new Random();
            int basketWeight = random.Next(GameTracker.MinRange, GameTracker.MaxRange);
            bool hasWinner = false;
            int nextPlayerIndex = 0;
            int totalMoves = 0;
            Player closestPlayer = players[0];
            Stopwatch stopWatch = new Stopwatch();
            Console.WriteLine($"Lets see who will guess first that number ({basketWeight})");           
            stopWatch.Start();
            while (!hasWinner //this loop will synchronously and circulary let each player that is not waiting make his guess
                   && totalMoves < TOTAL_ALLOWED_MOVES 
                   && stopWatch.ElapsedMilliseconds < TIMEOUT)
            {
                var player = players[nextPlayerIndex];
               
                if (!player.InSleep)
                {
                    totalMoves++;
                    int guess = player.MakeGuess();
                    GameTracker.Instance.RecordGuess(guess);
                    Console.WriteLine($"{player.Name} : {guess}");

                    if (guess == basketWeight)
                    {
                        hasWinner = true;
                        Console.WriteLine($"The winner is {player.Name} with {player.Attempts} attempts!!!");
                    }
                    else
                    {
                        var gap = Math.Abs(basketWeight - guess);
                        player.GotoSleep(gap);

                        #region track closes guess
                        if (player.ClosestGuess == 0)
                        {
                            player.ClosestGuess = guess;
                        }

                        player.ClosestGuess = gap < Math.Abs(basketWeight - player.ClosestGuess) ? guess : player.ClosestGuess;
                        closestPlayer = gap < Math.Abs(basketWeight - closestPlayer.ClosestGuess) ? player : closestPlayer;
                        #endregion
                    }

                }
                nextPlayerIndex++;
                if (nextPlayerIndex == players.Count) nextPlayerIndex = 0;

             
            }
            stopWatch.Stop();
            if (stopWatch.ElapsedMilliseconds >= TIMEOUT)
            {
                Console.WriteLine($"Reached timeout. GAME OVER!!!");
            }
            else if(totalMoves>= TOTAL_ALLOWED_MOVES)
            {
                Console.WriteLine($"No more Guesses. GAME OVER!!! : {stopWatch.Elapsed.TotalMilliseconds}");
            }

            if (!hasWinner)
            {
                
                Console.WriteLine($"The winner is {closestPlayer.Name} with closest guess {closestPlayer.ClosestGuess}!!!");
            }
        }

        
    }
}
