using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class Jail
    {

        //DAYS IN JAIL
        private int[] daysInJail;

        //IS IN JAIL
        private Boolean[] isInJail;

        public Jail()
        {

        }

        public Jail(int numberOfPlayers)
        {
            daysInJail = new int[numberOfPlayers];

            isInJail = new Boolean[numberOfPlayers];

            for (int index = 0; index < numberOfPlayers; index++)
            {
                daysInJail[index] = 0;

                isInJail[index] = false;
            }//END FOR
        }

        public void setIsInJail(int player, Boolean isInJail)
        {
            this.isInJail[player] = isInJail;

            daysInJail[player] = 0;
        }

        public Boolean getIsInJail(int player)
        {
            return isInJail[player];
        }

        public void addDayInJail(int player)
        {
            this.daysInJail[player]++;
        }

        public Boolean thirdDayInJail(int player)
        {
            return daysInJail[player] >= 3;
        }

    }
}
