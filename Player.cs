using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class Player
    {

        //LOCATION
        public int location { get; set; }

        //CASH
        public int cash { get; set; }

        //GET OUT OF JAIL FREE CARDS
        private int getOutOfJailFreeCards = 0;

        //STILL PLAYING
        public Boolean stillPlaying { get; set; }

        public Player()
        {
            intializeValues();
        }

        private void intializeValues()
        {
            location = 0;

            cash = 1500;

            getOutOfJailFreeCards = 0;

            stillPlaying = true;
        }

        public void addGetOutOfJailFreeCards(int numberOfCards)
        {
            getOutOfJailFreeCards += numberOfCards;
        }

        public void getOutOfJail()
        {
            if (getOutOfJailFreeCards > 0)
            {
                getOutOfJailFreeCards--;
            }
            else 
            {
                cash = cash - 50;
            }//END IF
        }
    }
}
