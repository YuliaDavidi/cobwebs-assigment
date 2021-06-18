namespace FBgame
{

    public sealed class GameTracker
    {
        private static readonly GameTracker instance = new GameTracker();

        private readonly bool[] previousGuesses = new bool[100];

        public bool[] PreviousGuesses
        {
            get
            {
                return previousGuesses;
            }
        }

        public void RecordGuess(int guess)
        {
            previousGuesses[guess - 40] = true;
        }

        static GameTracker()
        {
        }
        private GameTracker()
        {
            
        }
        public static GameTracker Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
