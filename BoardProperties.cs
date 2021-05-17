using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class BoardProperties
    {

        private PropertyInfo propertyInfo = new PropertyInfo();

        private PropertyOwners propertyOwners;

        private ChanceAndCommunityChest chanceAndCommunityChest = new ChanceAndCommunityChest();

        private IndexConversions indexConversions = new IndexConversions();

        public BoardProperties()
        {

        }

        public BoardProperties(int numberOfPlayers)
        {
            propertyOwners = new PropertyOwners(numberOfPlayers);
        }

        //---------------------------------------------------------------------
        //PROPERTY PRICES------------------------------------------------------
        //---------------------------------------------------------------------

        //Price
        public int getPropertyPrice(int squareIndex28)
        {
            return propertyInfo.getPropertyPrice(squareIndex28);
        }

        //---------------------------------------------------------------------
        //PROPERTY HOUSE PRICES------------------------------------------------
        //---------------------------------------------------------------------

        //Price per house
        public int getHousePropertyHouseCost(int squareIndex22)
        {
            return propertyInfo.getHousePropertyHouseCost(squareIndex22);
        }

        //---------------------------------------------------------------------
        //HOUSE PROPERTY RENTS-------------------------------------------------
        //---------------------------------------------------------------------

        //Rent
        public int getHousePropertyRent(int owner, int squareIndex22, int numberOf)
        {
            int rent = 0;

            if (squareIndex22 >= 0)
            {
                //HOUSE PROPERTY RENTS
                rent = propertyInfo.getHousePropertyRent(squareIndex22, numberOf);

                if (propertyOwners.isSet(owner, squareIndex22) && numberOf < 1)
                {
                    rent = rent * 2;
                }//END IF

                return rent;
            }
            else
            {
                return 0;
            }//END IF
        }

        //---------------------------------------------------------------------
        //PROPERTY OWNER-------------------------------------------------------
        //---------------------------------------------------------------------

        public int getOwnerOfProperty(int squareIndex28)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28
            //RETURN INDEX OF PLAYER WHO OWNS PROPERTY
            //RETURN -1 IF NO OWNER

            return propertyOwners.getOwnerOfProperty(squareIndex28);
        }

        public void purchaseProperty(int squareIndex28, int player)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28
            propertyOwners.purchaseProperty(squareIndex28, player);
        }

        //---------------------------------------------------------------------
        //PROPERTY MORTGAGED---------------------------------------------------
        //---------------------------------------------------------------------

        public Boolean propertyMortgaged(int squareIndex28)
        {
            //SQUARE IS FROM ALL PROPERTY SQUARES 28
            //RETURN TRUE IF PROPERTY IS MORTGAGED
            return propertyOwners.propertyMortgaged(squareIndex28);
        }

        public int getMortgagePropertyValue(int squareIndex28)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28
            //RETURN PROPERTY MORTGAGE VALUE
            int mortgageValue = getPropertyPrice(squareIndex28);

            mortgageValue = mortgageValue / 2;

            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28
            //RETURN PROPERTY MORTGAGE VALUE
            return mortgageValue;
        }

        public Boolean canShowMortgageButton(int player)
        {
            //RETURN TRUE IF PLAYER OWNS PROPERTY
            return propertyOwners.canShowMortgageButton(player);
        }

        public TurnResults mortgageUnmortgage(Boolean[] mortgageProperties, TurnResults turnResults, int player)
        {
            int mortgageValue = 0;

            //FLIP ALL PROPERTIES THAT ARE TRUE
            for (int index28 = 0; index28 < mortgageProperties.Length; index28++)
            {
                if (mortgageProperties[index28])
                {
                    if (propertyOwners.propertyMortgaged(index28))
                    {
                        mortgageValue = getMortgagePropertyValue(index28) * -1;

                        turnResults.addPlayerCash(player, mortgageValue);
                    }
                    else if (!propertyOwners.propertyMortgaged(index28))
                    {
                        mortgageValue = getMortgagePropertyValue(index28);

                        turnResults.addPlayerCash(player, mortgageValue);
                    }//END IF
                }//END IF
            }//END FOR

            propertyOwners.mortgageUnmortgage(mortgageProperties);

            return turnResults;
        }

        //---------------------------------------------------------------------
        //PROPERTY HOUSES------------------------------------------------------
        //---------------------------------------------------------------------

        public Boolean squareCanBuyHouses(int player, int square22, int[] houses)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            return propertyOwners.squareCanBuyHouses(player, square22, houses);
        }

        public Boolean squareCanSellHouses(int player, int square22, int[] houses)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            return propertyOwners.squareCanSellHouses(player, square22, houses);
        }

        //---------------------------------------------------------------------

        public Boolean playerCanBuyHouses(int player)
        {
            return propertyOwners.playerCanBuyHouses(player);
        }

        public Boolean playerCanSellHouses(int player)
        {
            return propertyOwners.playerCanSellHouses(player);
        }

        //---------------------------------------------------------------------

        public int getHousesOnProperty(int squareIndex22)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            //RETURN NUMBER OF HOUSES
            return propertyOwners.getHousesOnProperty(squareIndex22);
        }

        public TurnResults addHouses(int player, int[] houses, TurnResults turnResults)
        {
            int costOfHouses = 0;

            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            //ADD HOUSES TO TOTAL
            //ONLY NEGITIVE NUMBERS AND ZEROS IN HOUSES
            propertyOwners.addHouses(player, houses);

            for (int index22 = 0; index22 < houses.Length; index22++)
            {
                costOfHouses = propertyInfo.getHousePropertyHouseCost(index22);
                costOfHouses = costOfHouses * -1 * houses[index22];
                turnResults.addPlayerCash(player, costOfHouses);
            }//END FOR

            return turnResults;
        }

        //---------------------------------------------------------------------
        //MAKE TRADE-----------------------------------------------------------
        //---------------------------------------------------------------------

        public TurnResults makeTrade(
            int currentPlayer,
            int tradePlayer,
            Boolean[] propertiesToPlayer,
            Boolean[] propertiesToTradePlayer,
            TurnResults turnResults)
        {
            int squareIndex22 = 0;

            int housesOnSet = 0;

            int totalHousesCost = 0;

            for (int index28 = 0; index28 < propertiesToPlayer.Length; index28++)
            {
                if (index28 == 0 || index28 == 1 ||
                    index28 == 3 || index28 == 4 ||
                    index28 == 5 || index28 == 6 ||
                    index28 == 8 || index28 == 9 ||
                    index28 == 11 || index28 == 12 ||
                    index28 == 13 || index28 == 14 ||
                    index28 == 15 || index28 == 16 ||
                    index28 == 18 || index28 == 19 ||
                    index28 == 21 || index28 == 22 ||
                    index28 == 23 || index28 == 24 ||
                    index28 == 26 || index28 == 27)
                {
                    // 0 0 Mediterranean Avenue
                    // 1 1 Baltic Avenue
                    // 3 2 Oriental Avenue
                    // 4 3 Vermont Avenue
                    // 5 4 Connecticut Avenue
                    // 6 5 St. Charles Place
                    // 8 6 States Avenue
                    // 9 7 Virginia Avenue
                    // 11 8 St. James Place
                    // 12 9 Tennessee Avenue
                    // 13 10 New York Avenue
                    // 14 11 Kentucky Avenue
                    // 15 12 Indiana Avenue
                    // 16 13 Illinois Avenue
                    // 18 14 Atlantic Avenue
                    // 19 15 Ventnor Avenue
                    // 21 16 Marvin Gardens
                    // 22 17 Pacific Avenue
                    // 23 18 North Carolina Avenue
                    // 24 19 Pennsylvania Avenue
                    // 26 20 Park Place
                    // 27 21 Boardwalk

                    squareIndex22 = indexConversions.get22IndexFrom28Index(index28);

                    housesOnSet = propertyOwners.getHousesOnSet(squareIndex22);

                    if (propertiesToPlayer[index28] &&
                        housesOnSet > 0)
                    {
                        totalHousesCost = propertyInfo.getHousePropertyHouseCost(squareIndex22);

                        totalHousesCost = totalHousesCost * housesOnSet;

                        turnResults.addPlayerCash(tradePlayer, totalHousesCost);

                        propertyOwners.removeAllHousesInSet(tradePlayer, squareIndex22);
                    }//END IF

                    if (propertiesToTradePlayer[index28] &&
                        housesOnSet > 0)
                    {
                        totalHousesCost = propertyInfo.getHousePropertyHouseCost(squareIndex22);

                        totalHousesCost = totalHousesCost * housesOnSet;

                        turnResults.addPlayerCash(currentPlayer, totalHousesCost);

                        propertyOwners.removeAllHousesInSet(currentPlayer, squareIndex22);
                    }//END IF

                }//END IF
            }//END FOR

            propertyOwners.makeTrade(
                            currentPlayer,
                            tradePlayer,
                            propertiesToPlayer,
                            propertiesToTradePlayer);

            return turnResults;
        }

        //---------------------------------------------------------------------
        //SURRENDER------------------------------------------------------------
        //---------------------------------------------------------------------

        public TurnResults freePlayerProperties(int player, TurnResults turnResults)
        {
            int squareIndex28 = 0;

            int housesOnSet = 0;

            int totalHousesCost = 0;

            Boolean ownsProperty = false;

            for (int index22 = 0; index22 < 22; index22++)
            {
                squareIndex28 = indexConversions.get28IndexFrom22Index(index22);

                ownsProperty = propertyOwners.getOwnsProperty(squareIndex28, player);

                housesOnSet = propertyOwners.getHousesOnSet(index22);

                if (ownsProperty &&
                    housesOnSet > 0)
                {
                    totalHousesCost = propertyInfo.getHousePropertyHouseCost(index22);

                    totalHousesCost = totalHousesCost * housesOnSet;

                    turnResults.freeParkingMoney += totalHousesCost;

                    propertyOwners.removeAllHousesInSet(player, index22);
                }//END IF

            }//END FOR

            propertyOwners.freePlayerProperties(player);

            return turnResults;
        }

        public void transferPlayerProperties(int fromPlayer, int toPlayer)
        {
            propertyOwners.transferPlayerProperties(fromPlayer, toPlayer);
        }

        //---------------------------------------------------------------------
        //CHANCE AND COMMUNITY CHEST-------------------------------------------
        //---------------------------------------------------------------------

        //CHANCE---------------------------------------------------------------

        public TurnResults processChanceCard(int player, TurnResults turnResults)
        {
            return chanceAndCommunityChest.processChanceCard(
                player, 
                propertyOwners.getPlayerOwnedHouses(player), 
                propertyOwners.getPlayerOwnedHotels(player), 
                turnResults);
        }

        //COMMUNITY CHEST------------------------------------------------------

        public TurnResults processCommunityChestCard(int player, TurnResults turnResults)
        {
            return chanceAndCommunityChest.processCommunityChestCard(
                player,
                propertyOwners.getPlayerOwnedHouses(player),
                propertyOwners.getPlayerOwnedHotels(player),
                turnResults);
        }

    }
}
