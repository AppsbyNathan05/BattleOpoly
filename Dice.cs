using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class Dice
    {
        //RANDOM NUMBER GENERATOR
        private Random random = new Random();

        //DICE
        private int dieOne = 0;
        private int dieTwo = 0;

        //NUMBER OF DOUBLES
        private int numberOfDoubles = 0;

        //CAN ROLL
        Boolean canRoll = true;

        public Dice()
        {
            resetDice(true);
        }

        public void resetDice(Boolean canRoll)
        {
            dieOne = 0;
            dieTwo = 0;

            numberOfDoubles = 0;

            this.canRoll = canRoll;
        }

        public void rollDice()
        {
            dieOne = random.Next(1, 7);
            dieTwo = random.Next(1, 7);

            

            if (dieOne == dieTwo)
            {
                System.Windows.Forms.MessageBox.Show("Doubles: " + dieOne + " " + dieTwo);
                numberOfDoubles++;
                canRoll = true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(dieOne + " " + dieTwo);
                canRoll = false;
            }//END IF
        }

        public Boolean rolledDoubles()
        {
            return dieOne == dieTwo && dieOne != 0 && dieTwo != 0;
        }

        public Boolean thirdDoublesRoll()
        {
            return numberOfDoubles >= 3;
        }

        public int getRoll()
        {
            return dieOne + dieTwo;
        }

        public Boolean getCanRoll()
        {
            return canRoll;
        }

    }
}
