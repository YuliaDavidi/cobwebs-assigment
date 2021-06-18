using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FBgame
{
    public class Player
    {
        private IGuessStrategy _strategy;
        private int _attempts;
      

        public string Name { get; set; }

        public bool InSleep { get; set; }

        public void GotoSleep(int delay)
        {
            InSleep = true;
            Task.Run(async delegate
            {
                await Task.Delay(delay);
                InSleep = false;
            });
        }

        public int Attempts
        {
            get {
                return _attempts;
            }
        }
        public int ClosestGuess
        {
            get;set;
        }
        public int MakeGuess()
        {
            _attempts += 1;
            return _strategy.MakeGuess();
        }

        public Player(string name, IGuessStrategy guessStrategy)
        {
            this.Name = name;
            _strategy = guessStrategy;

        }
    }
}

