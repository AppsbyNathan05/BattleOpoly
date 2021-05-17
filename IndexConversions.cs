using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class IndexConversions
    {
        private static readonly int[] index40To28 = new int[] 
        {
            -1, //GO
            0, //Mediterranean Avenue
            -1, //Community Chest 1
            1, //Baltic Avenue
            -1, //Income Tax
            2, //Reading Railroad
            3, //Oriental Avenue
            -1, //Chance 1
            4, //Vermont Avenue
            5, //Connecticut Avenue
            -1, //Jail
            6, //St. Charles Place
            7, //Electric Company
            8, //States Avenue
            9, //Virginia Avenue
            10, //Pennsylvania Railroad
            11, //St. James Place
            -1, //Community Chest 2
            12, //Tennessee Avenue
            13, //New York Avenue
            -1, //Free Parking
            14, //Kentucky Avenue
            -1, //Chance 2
            15, //Indiana Avenue
            16, //Illinois Avenue
            17, //B&O Railroad
            18, //Atlantic Avenue
            19, //Ventnor Avenue
            20, //Water Works
            21, //Marvin Gardens
            -1, //Go To Jail
            22, //Pacific Avenue
            23, //North Carolina Avenue
            -1, //Community Chest 3
            24, //Pennsylvania Avenue
            25, //Short Line
            -1, //Chance 3
            26, //Park Place
            -1, //Luxury Tax
            27 //Boardwalk
        };

        private static readonly int[] index40To22 = new int[] 
        {
            -1, //GO
            0, //Mediterranean Avenue
            -1, //Community Chest 1
            1, //Baltic Avenue
            -1, //Income Tax
            -1, //Reading Railroad
            2, //Oriental Avenue
            -1, //Chance 1
            3, //Vermont Avenue
            4, //Connecticut Avenue
            -1, //Jail
            5, //St. Charles Place
            -1, //Electric Company
            6, //States Avenue
            7, //Virginia Avenue
            -1, //Pennsylvania Railroad
            8, //St. James Place
            -1, //Community Chest 2
            9, //Tennessee Avenue
            10, //New York Avenue
            -1, //Free Parking
            11, //Kentucky Avenue
            -1, //Chance 2
            12, //Indiana Avenue
            13, //Illinois Avenue
            -1, //B&O Railroad
            14, //Atlantic Avenue
            15, //Ventnor Avenue
            -1, //Water Works
            16, //Marvin Gardens
            -1, //Go To Jail
            17, //Pacific Avenue
            18, //North Carolina Avenue
            -1, //Community Chest 3
            19, //Pennsylvania Avenue
            -1, //Short Line
            -1, //Chance 3
            20, //Park Place
            -1, //Luxury Tax
            21 //Boardwalk
        };

        private static readonly int[] index28To22 = new int[] 
        {
            0, //Mediterranean Avenue
            1, //Baltic Avenue
            -1, //Reading Railroad
            2, //Oriental Avenue
            3, //Vermont Avenue
            4, //Connecticut Avenue
            5, //St. Charles Place
            -1, //Electric Company
            6, //States Avenue
            7, //Virginia Avenue
            -1, //Pennsylvania Railroad
            8, //St. James Place
            9, //Tennessee Avenue
            10, //New York Avenue
            11, //Kentucky Avenue
            12, //Indiana Avenue
            13, //Illinois Avenue
            -1, //B&O Railroad
            14, //Atlantic Avenue
            15, //Ventnor Avenue
            -1, //Water Works
            16, //Marvin Gardens
            17, //Pacific Avenue
            18, //North Carolina Avenue
            19, //Pennsylvania Avenue
            -1, //Short Line
            20, //Park Place
            21 //Boardwalk
        };

        public IndexConversions()
        {

        }

        public int get40IndexFrom28Index(int index28)
        {
            return Array.IndexOf(index40To28, index28);
        }

        public int get40IndexFrom22Index(int index22)
        {
            return Array.IndexOf(index40To22, index22);
        }

        public int get28IndexFrom40Index(int index40)
        {
            return index40To28[index40];
        }

        public int get28IndexFrom22Index(int index22)
        {
            return Array.IndexOf(index28To22, index22);
        }

        public int get22IndexFrom40Index(int index40)
        {
            return index40To22[index40];
        }

        public int get22IndexFrom28Index(int index28)
        {
            return index28To22[index28];
        }

    }
}
