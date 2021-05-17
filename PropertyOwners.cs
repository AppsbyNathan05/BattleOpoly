using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class PropertyOwners
    {

        private IndexConversions indexConversions = new IndexConversions();

        private Boolean[][] boardPropertyOwners = new Boolean[28][];

        private Boolean[] boardPropertyMortgaged = new Boolean[28];

        private int[] boardPropertyHouses = new int[22];

        private int[] playerHouses;

        private int[] playerHotels;

        public PropertyOwners()
        {

        }

        public PropertyOwners(int numberOfPlayers)
        {
            for (int row28 = 0; row28 < boardPropertyOwners.Length; row28++)
            {
                boardPropertyOwners[row28] = new Boolean[numberOfPlayers];

                for (int column = 0; column < numberOfPlayers; column++)
                {
                    boardPropertyOwners[row28][column] = false;
                }//END FOR
            }//END FOR

            for (int index22 = 0; index22 < boardPropertyHouses.Length; index22++)
            {
                boardPropertyHouses[index22] = 0;
            }//END FOR

            for (int index28 = 0; index28 < boardPropertyMortgaged.Length; index28++)
            {
                boardPropertyMortgaged[index28] = false;
            }//END FOR

            playerHouses = new int[numberOfPlayers];
            playerHotels = new int[numberOfPlayers];

            for (int index = 0; index < numberOfPlayers; index++)
            {
                playerHouses[index] = 0;
                playerHotels[index] = 0;
            }//END FOR
        }

        //---------------------------------------------------------------------
        //PROPERTY OWNER-------------------------------------------------------
        //---------------------------------------------------------------------

        public int getOwnerOfProperty(int squareIndex28)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28
            //RETURN INDEX OF PLAYER WHO OWNS PROPERTY
            //RETURN -1 IF NO OWNER

            for (int index = 0; index < boardPropertyOwners[squareIndex28].Length; index++)
            {
                if (boardPropertyOwners[squareIndex28][index])
                {
                    return index; 
                }//END IF
            }//END FOR

            return -1;
        }

        public Boolean getOwnsProperty(int squareIndex28, int player)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28
            //RETURN TRUE IF PLAYER OWNS PROPERTY
            return boardPropertyOwners[squareIndex28][player];
        }

        //---------------------------------------------------------------------

        public void purchaseProperty(int squareIndex28, int player)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28
            boardPropertyOwners[squareIndex28][player] = true;
        }

        //---------------------------------------------------------------------

        public void freePlayerProperties(int player)
        {
            for (int index28 = 0; index28 < boardPropertyOwners.Length; index28++)
            {
                boardPropertyOwners[index28][player] = false;
            }//END FOR
        }

        public void transferPlayerProperties(int fromPlayer, int toPlayer)
        {
            for (int index28 = 0; index28 < boardPropertyOwners.Length; index28++)
            {
                if (boardPropertyOwners[index28][fromPlayer])
                {
                    boardPropertyOwners[index28][fromPlayer] = false;
                    boardPropertyOwners[index28][toPlayer] = true;
                }//END IF
            }//END FOR
        }

        //---------------------------------------------------------------------
        //PROPERTY MORTGAGED---------------------------------------------------
        //---------------------------------------------------------------------

        public Boolean propertyMortgaged(int squareIndex28)
        {
            //SQUARE IS FROM ALL PROPERTY SQUARES 28
            //RETURN TRUE IF PROPERTY IS MORTGAGED
            return boardPropertyMortgaged[squareIndex28];
        }

        public Boolean canShowMortgageButton(int player)
        {
            //RETURN TRUE IF PLAYER OWNS PROPERTY
            for (int index28 = 0; index28 < boardPropertyOwners.Length; index28++)
            {
                if (boardPropertyOwners[index28][player])
                {
                    return true;  
                }//END IF
            }//END FOR

            return false;
        }

        public void mortgageUnmortgage(Boolean[] mortgageProperties)
        {
            //FLIP ALL PROPERTIES THAT ARE TRUE
            for (int index28 = 0; index28 < boardPropertyMortgaged.Length; index28++)
            {
                if (mortgageProperties[index28])
                {
                    if (boardPropertyMortgaged[index28])
                    {
                        boardPropertyMortgaged[index28] = false;
                    }
                    else if (!boardPropertyMortgaged[index28])
                    {
                        boardPropertyMortgaged[index28] = true;
                    }//END IF
                }//END IF
            }//END FOR
        }

        //---------------------------------------------------------------------
        //PROPERTY HOUSES------------------------------------------------------
        //---------------------------------------------------------------------

        public int getHousesOnProperty(int squareIndex22)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            //RETURN NUMBER OF HOUSES
            return boardPropertyHouses[squareIndex22];
        }

        public int getHousesOnSet(int squareIndex22)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            //RETURN NUMBER OF HOUSES
            if (squareIndex22 == 0 || squareIndex22 == 1)
            {
                // 0 Mediterranean Avenue
                // 1 Baltic Avenue
                return boardPropertyHouses[0] + boardPropertyHouses[1];
            }
            else if (squareIndex22 == 2 || squareIndex22 == 3 || squareIndex22 == 4)
            {
                // 2 Oriental Avenue 
                // 3 Vermont Avenue 
                // 4 Connecticut Avenue 
                return boardPropertyHouses[2] +
                    boardPropertyHouses[3] +
                    boardPropertyHouses[4];

            }
            else if (squareIndex22 == 5 || squareIndex22 == 6 || squareIndex22 == 7)
            {
                // 5 St. Charles Place 
                // 6 States Avenue 
                // 7 Virginia Avenue
                return boardPropertyHouses[5] +
                    boardPropertyHouses[6] +
                    boardPropertyHouses[7];
            }
            else if (squareIndex22 == 8 || squareIndex22 == 9 || squareIndex22 == 10)
            {
                // 8 St. James Place 
                // 9 Tennessee Avenue 
                // 10 New York Avenue 
                return boardPropertyHouses[8] +
                    boardPropertyHouses[9] +
                    boardPropertyHouses[10];
            }
            else if (squareIndex22 == 11 || squareIndex22 == 12 || squareIndex22 == 13)
            {
                // 11 Kentucky Avenue 
                // 12 Indiana Avenue 
                // 13 Illinois Avenue 
                return boardPropertyHouses[11] +
                    boardPropertyHouses[12] +
                    boardPropertyHouses[13];
            }
            else if (squareIndex22 == 14 || squareIndex22 == 15 || squareIndex22 == 16)
            {
                // 14 Atlantic Avenue 
                // 15 Ventnor Avenue
                // 16 Marvin Gardens 
                return boardPropertyHouses[14] +
                    boardPropertyHouses[15] +
                    boardPropertyHouses[16];
            }
            else if (squareIndex22 == 17 || squareIndex22 == 18 || squareIndex22 == 19)
            {
                // 17 Pacific Avenue 
                // 18 North Carolina Avenue 
                // 19 Pennsylvania Avenue
                return boardPropertyHouses[17] +
                    boardPropertyHouses[18] +
                    boardPropertyHouses[19];
            }
            else if (squareIndex22 == 20 || squareIndex22 == 21)
            {
                // 20 Park Place 
                // 21 Boardwalk
                return boardPropertyHouses[20] + boardPropertyHouses[21];
            }//END IF

            return 0;
        }

        //---------------------------------------------------------------------

        public int getPlayerOwnedHotels(int player)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            //RETURN NUMBER OF HOTELS
            return playerHotels[player];
        }

        public int getPlayerOwnedHouses(int player)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            //RETURN NUMBER OF HOUSES
            return playerHouses[player];
        }

        //---------------------------------------------------------------------

        public Boolean squareCanBuyHouses(int player, int squareIndex22, int[] houses)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            return (boardPropertyHouses[squareIndex22] + houses[squareIndex22]) < 5 && 
                isSet(player, squareIndex22) && buyBalance(squareIndex22, houses);
        }

        public Boolean squareCanSellHouses(int player, int squareIndex22, int[] houses)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            return (boardPropertyHouses[squareIndex22] + houses[squareIndex22]) > 0 && 
                isSet(player, squareIndex22) && sellBalance(squareIndex22, houses);
        }

        //---------------------------------------------------------------------

        public Boolean playerCanBuyHouses(int player)
        {
            // 0 0 Mediterranean Avenue
            // 1 1 Baltic Avenue
            Boolean set1 = (boardPropertyOwners[0][player] &&
                    boardPropertyOwners[1][player])
                    &&
                    (buyBalance(0) ||
                    buyBalance(1));

            // 3 2 Oriental Avenue
            // 4 3 Vermont Avenue
            // 5 4 Connecticut Avenue
            Boolean set2 = (boardPropertyOwners[3][player] &&
                    boardPropertyOwners[4][player] &&
                    boardPropertyOwners[5][player])
                    &&
                    (buyBalance(2) ||
                    buyBalance(3) ||
                    buyBalance(4));

            // 6 5 St. Charles Place
            // 8 6 States Avenue
            // 9 7 Virginia Avenue
            Boolean set3 = (boardPropertyOwners[6][player] &&
                    boardPropertyOwners[8][player] &&
                    boardPropertyOwners[9][player])
                    &&
                    (buyBalance(5) ||
                    buyBalance(6) ||
                    buyBalance(7));

            // 11 8 St. James Place
            // 12 9 Tennessee Avenue
            // 13 10 New York Avenue
            Boolean set4 = (boardPropertyOwners[11][player] &&
                    boardPropertyOwners[12][player] &&
                    boardPropertyOwners[13][player])
                    &&
                    (buyBalance(8) ||
                    buyBalance(9) ||
                    buyBalance(10));

            // 14 11 Kentucky Avenue
            // 15 12 Indiana Avenue
            // 16 13 Illinois Avenue
            Boolean set5 = (boardPropertyOwners[14][player] &&
                    boardPropertyOwners[15][player] &&
                    boardPropertyOwners[16][player])
                    &&
                    (buyBalance(11) ||
                    buyBalance(12) ||
                    buyBalance(13));

            // 18 14 Atlantic Avenue
            // 19 15 Ventnor Avenue
            // 21 16 Marvin Gardens
            Boolean set6 = (boardPropertyOwners[18][player] &&
                    boardPropertyOwners[19][player] &&
                    boardPropertyOwners[21][player])
                    &&
                    (buyBalance(14) ||
                    buyBalance(15) ||
                    buyBalance(16));

            // 22 17 Pacific Avenue
            // 23 18 North Carolina Avenue
            // 24 19 Pennsylvania Avenue
            Boolean set7 = (boardPropertyOwners[22][player] &&
                    boardPropertyOwners[23][player] &&
                    boardPropertyOwners[24][player])
                    &&
                    (buyBalance(17) ||
                    buyBalance(18) ||
                    buyBalance(19));

            // 26 20 Park Place
            // 27 21 Boardwalk
            Boolean set8 = (boardPropertyOwners[26][player] &&
                    boardPropertyOwners[27][player])
                    &&
                    (buyBalance(20) ||
                    buyBalance(21));

            return set1 || set2 || set3 || set4 || set5 || set6 || set7 || set8;
        }

        public Boolean playerCanSellHouses(int player)
        {
            return playerHouses[player] > 0 || playerHotels[player] > 0;
        }

        //---------------------------------------------------------------------

        public Boolean isSet(int player, int squareIndex22)
        {
            if (squareIndex22 == 0 || squareIndex22 == 1)
            {
                // 0 Mediterranean Avenue
                // 1 Baltic Avenue
                return boardPropertyOwners[0][player] && boardPropertyOwners[1][player];
            }
            else if (squareIndex22 == 2 || squareIndex22 == 3 || squareIndex22 == 4)
            {
                // 3 Oriental Avenue 
                // 4 Vermont Avenue 
                // 5 Connecticut Avenue 
                return boardPropertyOwners[3][player] && 
                    boardPropertyOwners[4][player] &&
                    boardPropertyOwners[5][player];

            }
            else if (squareIndex22 == 5 || squareIndex22 == 6 || squareIndex22 == 7)
            {
                // 6 St. Charles Place 
                // 8 States Avenue 
                // 9 Virginia Avenue
                return boardPropertyOwners[6][player] &&
                    boardPropertyOwners[8][player] &&
                    boardPropertyOwners[9][player];
            }
            else if (squareIndex22 == 8 || squareIndex22 == 9 || squareIndex22 == 10)
            {
                // 11 St. James Place 
                // 12 Tennessee Avenue 
                // 13 New York Avenue 
                return boardPropertyOwners[11][player] &&
                    boardPropertyOwners[12][player] &&
                    boardPropertyOwners[13][player];
            }
            else if (squareIndex22 == 11 || squareIndex22 == 12 || squareIndex22 == 13)
            {
                // 14 Kentucky Avenue 
                // 15 Indiana Avenue 
                // 16 Illinois Avenue 
                return boardPropertyOwners[14][player] &&
                    boardPropertyOwners[15][player] &&
                    boardPropertyOwners[16][player];
            }
            else if (squareIndex22 == 14 || squareIndex22 == 15 || squareIndex22 == 16)
            {
                // 18 Atlantic Avenue 
                // 19 Ventnor Avenue
                // 21 Marvin Gardens 
                return boardPropertyOwners[18][player] &&
                    boardPropertyOwners[19][player] &&
                    boardPropertyOwners[21][player];
            }
            else if (squareIndex22 == 17 || squareIndex22 == 18 || squareIndex22 == 19)
            {
                // 22 Pacific Avenue 
                // 23 North Carolina Avenue 
                // 24 Pennsylvania Avenue
                return boardPropertyOwners[22][player] &&
                    boardPropertyOwners[23][player] &&
                    boardPropertyOwners[24][player];
            }
            else if (squareIndex22 == 20 || squareIndex22 == 21)
            {
                // 26 Park Place 
                // 27 Boardwalk
                return boardPropertyOwners[26][player] && boardPropertyOwners[27][player];
            }//END IF

            return false;
        }

        private Boolean buyBalance(int squareIndex22)
        {
            int[] houses = new int[22];

            for (int index = 0; index < houses.Length; index++)
            {
                houses[index] = 0;
            }//END FOR

            return buyBalance(squareIndex22, houses);
        }

        private Boolean buyBalance(int squareIndex22, int[] houses)
        {
            int[] totalHouses = new int[22];

            for (int index22 = 0; index22 < totalHouses.Length; index22++)
            {
                totalHouses[index22] = houses[index22] + boardPropertyHouses[index22];
            }//END FOR

            if (squareIndex22 == 0 || squareIndex22 == 1)
            {
                // 0 Mediterranean Avenue
                // 1 Baltic Avenue
                return (totalHouses[squareIndex22] * 2) <= 
                    (totalHouses[0] + totalHouses[1]);
            }
            else if (squareIndex22 == 2 || squareIndex22 == 3 || squareIndex22 == 4)
            {
                // 2 Oriental Avenue 
                // 3 Vermont Avenue 
                // 4 Connecticut Avenue 
                return (totalHouses[squareIndex22] * 3) <=
                    (totalHouses[2] + totalHouses[3] + totalHouses[4]);

            }
            else if (squareIndex22 == 5 || squareIndex22 == 6 || squareIndex22 == 7)
            {
                // 5 St. Charles Place 
                // 6 States Avenue 
                // 7 Virginia Avenue
                return (totalHouses[squareIndex22] * 3) <=
                    (totalHouses[5] + totalHouses[6] + totalHouses[7]);
            }
            else if (squareIndex22 == 8 || squareIndex22 == 9 || squareIndex22 == 10)
            {
                // 8 St. James Place 
                // 9 Tennessee Avenue 
                // 10 New York Avenue 
                return (totalHouses[squareIndex22] * 3) <=
                    (totalHouses[8] + totalHouses[9] + totalHouses[10]);
            }
            else if (squareIndex22 == 11 || squareIndex22 == 12 || squareIndex22 == 13)
            {
                // 11 Kentucky Avenue 
                // 12 Indiana Avenue 
                // 13 Illinois Avenue 
                return (totalHouses[squareIndex22] * 3) <=
                    (totalHouses[11] + totalHouses[12] + totalHouses[13]);
            }
            else if (squareIndex22 == 14 || squareIndex22 == 15 || squareIndex22 == 16)
            {
                // 14 Atlantic Avenue 
                // 15 Ventnor Avenue
                // 16 Marvin Gardens 
                return (totalHouses[squareIndex22] * 3) <=
                    (totalHouses[14] + totalHouses[15] + totalHouses[16]);
            }
            else if (squareIndex22 == 17 || squareIndex22 == 18 || squareIndex22 == 19)
            {
                // 17 Pacific Avenue 
                // 18 North Carolina Avenue 
                // 19 Pennsylvania Avenue
                return (totalHouses[squareIndex22] * 3) <=
                    (totalHouses[17] + totalHouses[18] + totalHouses[19]);
            }
            else if (squareIndex22 == 20 || squareIndex22 == 21)
            {
                // 20 Park Place 
                // 21 Boardwalk
                return (totalHouses[squareIndex22] * 2) <=
                    (totalHouses[20] + totalHouses[21]);
            }//END IF

            return false;
        }

        private Boolean sellBalance(int squareIndex22)
        {
            int[] houses = new int[22];

            for (int index = 0; index < houses.Length; index++)
            {
                houses[index] = 0;
            }//END FOR

            return sellBalance(squareIndex22, houses);
        }

        private Boolean sellBalance(int squareIndex22, int[] houses)
        {
            int[] totalHouses = new int[22];

            for (int index22 = 0; index22 < totalHouses.Length; index22++)
            {
                totalHouses[index22] = houses[index22] + boardPropertyHouses[index22];
            }//END FOR

            if (squareIndex22 == 0 || squareIndex22 == 1)
            {
                // 0 Mediterranean Avenue
                // 1 Baltic Avenue
                return (totalHouses[squareIndex22] * 2) >=
                    (totalHouses[0] + totalHouses[1]);
            }
            else if (squareIndex22 == 2 || squareIndex22 == 3 || squareIndex22 == 4)
            {
                // 2 Oriental Avenue 
                // 3 Vermont Avenue 
                // 4 Connecticut Avenue 
                return (totalHouses[squareIndex22] * 3) >=
                    (totalHouses[2] + totalHouses[3] + totalHouses[4]);

            }
            else if (squareIndex22 == 5 || squareIndex22 == 6 || squareIndex22 == 7)
            {
                // 5 St. Charles Place 
                // 6 States Avenue 
                // 7 Virginia Avenue
                return (totalHouses[squareIndex22] * 3) >=
                    (totalHouses[5] + totalHouses[6] + totalHouses[7]);
            }
            else if (squareIndex22 == 8 || squareIndex22 == 9 || squareIndex22 == 10)
            {
                // 8 St. James Place 
                // 9 Tennessee Avenue 
                // 10 New York Avenue 
                return (totalHouses[squareIndex22] * 3) >=
                    (totalHouses[8] + totalHouses[9] + totalHouses[10]);
            }
            else if (squareIndex22 == 11 || squareIndex22 == 12 || squareIndex22 == 13)
            {
                // 11 Kentucky Avenue 
                // 12 Indiana Avenue 
                // 13 Illinois Avenue 
                return (totalHouses[squareIndex22] * 3) >=
                    (totalHouses[11] + totalHouses[12] + totalHouses[13]);
            }
            else if (squareIndex22 == 14 || squareIndex22 == 15 || squareIndex22 == 16)
            {
                // 14 Atlantic Avenue 
                // 15 Ventnor Avenue
                // 16 Marvin Gardens 
                return (totalHouses[squareIndex22] * 3) >=
                    (totalHouses[14] + totalHouses[15] + totalHouses[16]);
            }
            else if (squareIndex22 == 17 || squareIndex22 == 18 || squareIndex22 == 19)
            {
                // 17 Pacific Avenue 
                // 18 North Carolina Avenue 
                // 19 Pennsylvania Avenue
                return (totalHouses[squareIndex22] * 3) >=
                    (totalHouses[17] + totalHouses[18] + totalHouses[19]);
            }
            else if (squareIndex22 == 20 || squareIndex22 == 21)
            {
                // 20 Park Place 
                // 21 Boardwalk
                return (totalHouses[squareIndex22] * 2) >=
                    (totalHouses[20] + totalHouses[21]);
            }//END IF

            return false;
        }

        //ADD HOUSES-----------------------------------------------------------

        public void addHouses(int player, int[] houses)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            //ADD HOUSES TO TOTAL
            playerHotels[player] = 0;
            playerHouses[player] = 0;

            for (int index22 = 0; index22 < boardPropertyHouses.Length; index22++)
            {
                boardPropertyHouses[index22] += houses[index22];

                playerHotels[player] += (boardPropertyHouses[index22] / 5);
                playerHouses[player] += (boardPropertyHouses[index22] % 5);
            }//END FOR

        }

        //REMOVE ALL HOUSES IN SET---------------------------------------------

        public void removeAllHousesInSet(int player, int squareIndex22)
        {
            int[] houses = new int[22];

            for (int index22 = 0; index22 < houses.Length; index22++)
            {
                houses[index22] = 0;
            }//END FOR

            //REMOVES ALL HOUSES ON SET
            if (squareIndex22 == 0 || squareIndex22 == 1)
            {
                // 0 Mediterranean Avenue
                // 1 Baltic Avenue
                houses[0] = getHousesOnProperty(0) * -1;
                houses[1] = getHousesOnProperty(1) * -1;

            }
            else if (squareIndex22 == 2 || squareIndex22 == 3 || squareIndex22 == 4)
            {
                // 2 Oriental Avenue 
                // 3 Vermont Avenue 
                // 4 Connecticut Avenue 
                houses[2] = getHousesOnProperty(2) * -1;
                houses[3] = getHousesOnProperty(3) * -1;
                houses[4]= getHousesOnProperty(4) * -1;

            }
            else if (squareIndex22 == 5 || squareIndex22 == 6 || squareIndex22 == 7)
            {
                // 5 St. Charles Place 
                // 6 States Avenue 
                // 7 Virginia Avenue
                houses[5] = getHousesOnProperty(5) * -1;
                houses[6] = getHousesOnProperty(6) * -1;
                houses[7]= getHousesOnProperty(7) * -1;
            }
            else if (squareIndex22 == 8 || squareIndex22 == 9 || squareIndex22 == 10)
            {
                // 8 St. James Place 
                // 9 Tennessee Avenue 
                // 10 New York Avenue 
                houses[8] = getHousesOnProperty(8) * -1;
                houses[9] = getHousesOnProperty(9) * -1;
                houses[10]= getHousesOnProperty(10) * -1;
            }
            else if (squareIndex22 == 11 || squareIndex22 == 12 || squareIndex22 == 13)
            {
                // 11 Kentucky Avenue 
                // 12 Indiana Avenue 
                // 13 Illinois Avenue 
                houses[11] = getHousesOnProperty(11) * -1;
                houses[12] = getHousesOnProperty(12) * -1;
                houses[13]= getHousesOnProperty(13) * -1;
            }
            else if (squareIndex22 == 14 || squareIndex22 == 15 || squareIndex22 == 16)
            {
                // 14 Atlantic Avenue 
                // 15 Ventnor Avenue
                // 16 Marvin Gardens 
                houses[14] = getHousesOnProperty(14) * -1;
                houses[15] = getHousesOnProperty(15) * -1;
                houses[16]= getHousesOnProperty(16) * -1;
            }
            else if (squareIndex22 == 17 || squareIndex22 == 18 || squareIndex22 == 19)
            {
                // 17 Pacific Avenue 
                // 18 North Carolina Avenue 
                // 19 Pennsylvania Avenue
                houses[17] = getHousesOnProperty(17) * -1;
                houses[18] = getHousesOnProperty(18) * -1;
                houses[19]= getHousesOnProperty(19) * -1;
            }
            else if (squareIndex22 == 20 || squareIndex22 == 21)
            {
                // 20 Park Place 
                // 21 Boardwalk
                houses[20] = getHousesOnProperty(20) * -1; 
                houses[21]= getHousesOnProperty(21) * -1;
            }//END IF

            addHouses(player, houses);
        }

        //---------------------------------------------------------------------
        //TRADE----------------------------------------------------------------
        //---------------------------------------------------------------------

        public void makeTrade(
            int currentPlayer,
            int tradePlayer,
            Boolean[] propertiesToPlayer,
            Boolean[] propertiesToTradePlayer)
        {
            for (int index28 = 0; index28 < boardPropertyOwners.Length; index28++)
            {
                if (propertiesToPlayer[index28])
                {
                    boardPropertyOwners[index28][currentPlayer] = true;
                    boardPropertyOwners[index28][tradePlayer] = false;
                }//END IF

                if (propertiesToTradePlayer[index28])
                {
                    boardPropertyOwners[index28][currentPlayer] = false;
                    boardPropertyOwners[index28][tradePlayer] = true;
                }//END IF
            }//END FOR
        }
    }
}
