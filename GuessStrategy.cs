using System;
namespace FBgame
{

    public interface IGuessStrategy
    {
        public int MakeGuess();
    }
    
    public class RandomST: IGuessStrategy
    {
        public int MakeGuess()
        {
            Random random = new Random();
            return random.Next(GameTracker.MinRange, GameTracker.MaxRange);
        }
    }

    public class Memory : IGuessStrategy
    {
        private bool[] previeousGuesses = new bool[GameTracker.Range];
        public int MakeGuess()
        {
            Random random = new Random();
            int guess = random.Next(GameTracker.MinRange, GameTracker.MaxRange);
            while (previeousGuesses[guess - GameTracker.MinRange])
            {
                guess = random.Next(GameTracker.MinRange, GameTracker.MaxRange);
            }
            previeousGuesses[guess - GameTracker.MinRange] = true;
            return guess;
        }
    }

    public class Thorough : IGuessStrategy
    {
        private int prevGuess = GameTracker.MinRange-1;
        public int MakeGuess()
        {          
            prevGuess += 1;
            return prevGuess;
        }
    }

    public class Cheater : IGuessStrategy
    {

        public int MakeGuess()
        {

            Random random = new Random();
            int guess = random.Next(GameTracker.MinRange, GameTracker.MaxRange);
            while (GameTracker.Instance.PreviousGuesses[guess - GameTracker.MinRange])
            {
                guess = random.Next(GameTracker.MinRange, GameTracker.MaxRange);
            }

            return guess;            
        }
    }

    public class ThoroughCheater : IGuessStrategy
    {
        private int prevGuess = GameTracker.MinRange;


        public int MakeGuess()
        {
            int guess = prevGuess;
            while (GameTracker.Instance.PreviousGuesses[guess - GameTracker.MinRange])
            {
                guess += 1;
            }
            prevGuess = guess;

            return guess;

        }
    }
    
}