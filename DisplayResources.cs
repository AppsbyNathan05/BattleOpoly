using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class DisplayResources
    {

        // 0 Mediterranean Avenue
        // 1 Baltic Avenue
        // 2 Reading Railroad 
        // 3 Oriental Avenue 
        // 4 Vermont Avenue 
        // 5 Connecticut Avenue 
        // 6 St. Charles Place 
        // 7 Electric Company 
        // 8 States Avenue 
        // 9 Virginia Avenue
        // 10 Pennsylvania Railroad 
        // 11 St. James Place 
        // 12 Tennessee Avenue 
        // 13 New York Avenue 
        // 14 Kentucky Avenue 
        // 15 Indiana Avenue 
        // 16 Illinois Avenue 
        // 17 B&O Railroad 
        // 18 Atlantic Avenue 
        // 19 Ventnor Avenue
        // 20 Water Works 
        // 21 Marvin Gardens 
        // 22 Pacific Avenue 
        // 23 North Carolina Avenue 
        // 24 Pennsylvania Avenue
        // 25 Short Line 
        // 26 Park Place 
        // 27 Boardwalk
        private static readonly string[] arrayStringPropertyNames = new string[]
        {
            "Mediterranean Avenue", "Baltic Avenue", "Reading Railroad", "Oriental Avenue",
            "Vermont Avenue", "Connecticut Avenue", "St. Charles Place", "Electric Company", "States Avenue", "Virginia Avenue",
            "Pennsylvania Railroad", "St. James Place", "Tennessee Avenue", "New York Avenue",
            "Kentucky Avenue", "Indiana Avenue", "Illinois Avenue", "B&O Railroad", "Atlantic Avenue", "Ventnor Avenue",
            "Water Works", "Marvin Gardens", "Pacific Avenue", "North Carolina Avenue", "Pennsylvania Avenue",
            "Short Line", "Park Place", "Boardwalk"
        };

        private string[] playerNames;
        private string[] playerAbvs;

        public DisplayResources()
        {

        }

        public DisplayResources(string[] playerNames, string[] playerAbvs) 
        {
            this.playerNames = new string[playerNames.Length];
            this.playerAbvs = new string[playerAbvs.Length];

            for (int index = 0; index < this.playerNames.Length; index++)
            {
                this.playerNames[index] = playerNames[index];
                this.playerAbvs[index] = playerAbvs[index];
            }//END FOR
        }

        //---------------------------------------------------------------------
        //PROPERTY NAMES-------------------------------------------------------
        //---------------------------------------------------------------------

        public string getPropertyName(int index)
        {
            return arrayStringPropertyNames[index];
        }

        //---------------------------------------------------------------------
        //PLAYER INFORMATION---------------------------------------------------
        //---------------------------------------------------------------------

        public string getPlayerName(int playerIndex)
        {
            return playerNames[playerIndex];
        }

        public string getPlayerAbv(int playerIndex)
        {
            return playerAbvs[playerIndex];
        }

        public string[] getPlayerNames()
        {
            return playerNames;
        }

        public string[] getPlayerAbvs()
        {
            return playerAbvs;
        }

        public int getNumberOfPlayers()
        {
            return playerNames.Length;
        }

    }
}
