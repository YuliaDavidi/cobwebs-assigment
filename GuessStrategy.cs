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
            return random.Next(40, 140);
        }
    }

    public class Memory : IGuessStrategy
    {
        private bool[] previeousGuesses = new bool[100];
        public int MakeGuess()
        {
            Random random = new Random();
            int guess = random.Next(40, 140);
            while (previeousGuesses[guess - 40])
            {
                guess = random.Next(40, 140);
            }
            previeousGuesses[guess - 40] = true;
            return guess;
        }
    }

    public class Thorough : IGuessStrategy
    {
        private int prevGuess = 39;
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
            int guess = random.Next(40, 140);
            while (GameTracker.Instance.PreviousGuesses[guess - 40])
            {
                guess = random.Next(40, 140);
            }

            return guess;            
        }
    }

    public class ThoroughCheater : IGuessStrategy
    {
        private int prevGuess = 40;


        public int MakeGuess()
        {
            int guess = prevGuess;
            while (GameTracker.Instance.PreviousGuesses[guess - 40])
            {
                guess += 1;
            }
            prevGuess = guess;

            return guess;

        }
    }
    
}