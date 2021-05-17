using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class PropertyInfo
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
        private static readonly int[] arrayIntPropertyPrices = new int[]
        {
            60, 60, 200, 100, 100, 120, 140,
            150, 140, 160, 200, 180, 180, 200,
            220, 220, 240, 200, 260, 260, 150,
            280, 300, 300, 320, 200, 350, 400
        };

        // 0 Mediterranean Avenue
        // 1 Baltic Avenue
        // 2 Oriental Avenue
        // 3 Vermont Avenue
        // 4 Connecticut Avenue
        // 5 St. Charles Place
        // 6 States Avenue
        // 7 Virginia Avenue
        // 8 St. James Place
        // 9 Tennessee Avenue
        // 10 New York Avenue
        // 11 Kentucky Avenue
        // 12 Indiana Avenue
        // 13 Illinois Avenue
        // 14 Atlantic Avenue
        // 15 Ventnor Avenue
        // 16 Marvin Gardens
        // 17 Pacific Avenue
        // 18 North Carolina Avenue
        // 19 Pennsylvania Avenue
        // 20 Park Place
        // 21 Boardwalk
        private static readonly int[] arrayIntPropertyHousePrices = new int[]
        {
            50, 50, 50, 50, 50,
            100, 100, 100, 100, 100, 100,
            150, 150, 150, 150, 150, 150,
            200, 200, 200, 200, 200
        };

        // 0 Mediterranean Avenue
        // 1 Baltic Avenue
        // 2 Oriental Avenue
        // 3 Vermont Avenue
        // 4 Connecticut Avenue
        // 5 St. Charles Place
        // 6 States Avenue
        // 7 Virginia Avenue
        // 8 St. James Place
        // 9 Tennessee Avenue
        // 10 New York Avenue
        // 11 Kentucky Avenue
        // 12 Indiana Avenue
        // 13 Illinois Avenue
        // 14 Atlantic Avenue
        // 15 Ventnor Avenue
        // 16 Marvin Gardens
        // 17 Pacific Avenue
        // 18 North Carolina Avenue
        // 19 Pennsylvania Avenue
        // 20 Park Place
        // 21 Boardwalk

        //Rent
        //Rent 1 house
        //Rent 2 houses
        //Rent 3 houses
        //Rent 4 houses
        //Rent 5 houses
        private static readonly int[][] arrayIntHousePropertiesRents = new int[][]
        {
            new int[] {2, 10, 30, 90, 160, 250},
            new int[] {4, 20, 60, 180, 320, 450},
            new int[] {6, 30, 90, 270, 400, 550},
            new int[] {6, 30, 90, 270, 400, 550},
            new int[] {8, 40, 100, 300, 450, 600},
            new int[] {10, 50, 150, 450, 625, 750},
            new int[] {10, 50, 150, 450, 625, 750},
            new int[] {12, 60, 180, 500, 700, 900},
            new int[] {14, 70, 200, 550, 750, 950},
            new int[] {14, 70, 200, 550, 750, 950},
            new int[] {16, 80, 220, 600, 800, 1000},
            new int[] {18, 90, 250, 700, 875, 1050},
            new int[] {18, 90, 250, 700, 875, 1050},
            new int[] {20, 100, 300, 750, 925, 1100},
            new int[] {22, 110, 330, 800, 975, 1150},
            new int[] {22, 110, 330, 800, 975, 1150},
            new int[] {24, 120, 360, 850, 1025, 1200},
            new int[] {26, 130, 390, 900, 1100, 1275},
            new int[] {26, 130, 390, 900, 1100, 1275},
            new int[] {28, 150, 450, 1000, 1200, 1400},
            new int[] {35, 175, 500, 1100, 1300, 1500},
            new int[] {50, 200, 600, 1400, 1700, 2000}
        };

        public PropertyInfo()
        {

        }

        //---------------------------------------------------------------------
        //PROPERTY PRICES------------------------------------------------------
        //---------------------------------------------------------------------

        //Price
        public int getPropertyPrice(int squareIndex28)
        {
            return arrayIntPropertyPrices[squareIndex28];
        }

        //---------------------------------------------------------------------
        //PROPERTY HOUSE PRICES------------------------------------------------
        //---------------------------------------------------------------------

        //Price per house
        public int getHousePropertyHouseCost(int squareIndex22)
        {
            return arrayIntPropertyHousePrices[squareIndex22];
        }

        //---------------------------------------------------------------------
        //HOUSE PROPERTY RENTS-------------------------------------------------
        //---------------------------------------------------------------------

        //Rent
        //Rent 1 house
        //Rent 2 houses
        //Rent 3 houses
        //Rent 4 houses
        //Rent 5 houses
        public int getHousePropertyRent(int squareIndex22 ,int numberOfHouses)
        {
            return arrayIntHousePropertiesRents[squareIndex22][numberOfHouses];
        }

    }
}
