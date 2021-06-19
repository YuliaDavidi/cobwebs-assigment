namespace FBgame
{

    public sealed class GameTracker
    {
        private static readonly GameTracker instance = new GameTracker();

        private readonly bool[] previousGuesses = new bool[Range];

        const int MIN_VALUE= 40;
        const int MAX_VALUE= 140;
      
        public bool[] PreviousGuesses
        {
            get
            {
                return previousGuesses;
            }
        }
        public static int MinRange
        {
            get
            {
                return MIN_VALUE;
            }
        }
        public static int MaxRange
        {
            get
            {
                return MAX_VALUE;
            }
        }
        public static int Range
        {
            get
            {
                return MaxRange - MinRange;
            }
        }
        public void RecordGuess(int guess)
        {
            previousGuesses[guess - MinRange] = true; //shift 40 
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
