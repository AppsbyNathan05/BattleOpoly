using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class TurnResults
    {

        //LOCATION
        public int location { get; set; }

        //PLAYER CASH
        private int[] playerCash;

        //IN JAIL
        public Boolean inJail { get; set; }

        //GET OUT OF JAIL FREE CARDS
        public int getOutOfJailFreeCards { get; set; }

        //FREE PARKING MONEY
        public int freeParkingMoney { get; set; }

        //STILL PLAYING
        private Boolean[] stillPlaying;

        //RENT MULTIPLIER
        public int rentMultiplier { get; set; }

        public TurnResults()
        {

        }

        public TurnResults(int players)
        {
            playerCash = new int[players];

            stillPlaying = new Boolean[players];

            intializeValues();
        }

        public void intializeValues()
        {
            //SET
            for (int index = 0; index < this.stillPlaying.Length; index++)
            {
                this.stillPlaying[index] = true;
            }//END FOR

            intializeValues(500);
        }

        public void intializeValues(int freeParkingMoney)
        {
            location = 0;

            //SET
            for (int index = 0; index < playerCash.Length; index++)
            {
                playerCash[index] = 0;
            }//END FOR

            inJail = false;

            getOutOfJailFreeCards = 0;

            rentMultiplier = 1;

            this.freeParkingMoney = freeParkingMoney;
        }

        public void intializeValues(int freeParkingMoney, Boolean[] stillPlaying)
        {
            //SET
            for (int index = 0; index < this.stillPlaying.Length; index++)
            {
                this.stillPlaying[index] = stillPlaying[index];
            }//END FOR

            intializeValues(freeParkingMoney);
        }

        public void setValues(TurnResults turnResults)
        {
            this.location = turnResults.location;

            //SET
            for (int index = 0; index < playerCash.Length; index++)
            {
                this.playerCash[index] = turnResults.getPlayerCash(index);
                this.stillPlaying[index] = turnResults.getPlayerStillPlaying(index);
            }//END FOR

            this.inJail = turnResults.inJail;

            this.getOutOfJailFreeCards = turnResults.getOutOfJailFreeCards;

            this.freeParkingMoney = turnResults.freeParkingMoney;

            this.rentMultiplier = turnResults.rentMultiplier;
        }

        public int getNumberOfPlayers()
        {
            return playerCash.Length;
        }

        public void addPlayerCash(int player, int cash)
        {
            playerCash[player] = playerCash[player] + cash;
        }

        public int getPlayerCash(int player)
        {
            return playerCash[player];
        }

        public void setPlayerNotPlaying(int player)
        {
            stillPlaying[player] = false;
        }

        public Boolean getPlayerStillPlaying(int player)
        {
            return stillPlaying[player];
        }

    }
}
