using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class BoardDirector
    {

        private IndexConversions indexConversions = new IndexConversions();

        private Battle battle = new Battle();

        private BoardProperties boardProperties;

        private DisplayResources displayResources;

        private Boolean[][] boardPlayerLocations = new Boolean[40][];

        private int[] playerBoardLocations;

        public BoardDirector()
        {

        }

        public BoardDirector(DisplayResources displayResources)
        {
            boardProperties = new BoardProperties(displayResources.getNumberOfPlayers());

            this.displayResources = new DisplayResources(
                            displayResources.getPlayerNames(),
                            displayResources.getPlayerAbvs());

            for (int square40 = 0; square40 < boardPlayerLocations.Length; square40++)
            {
                boardPlayerLocations[square40] = new Boolean[displayResources.getNumberOfPlayers()];

                for (int playerIndex = 0; playerIndex < displayResources.getNumberOfPlayers(); playerIndex++)
                {
                    boardPlayerLocations[square40][playerIndex] = false;
                }//END FOR
            }//END FOR

            playerBoardLocations = new int[displayResources.getNumberOfPlayers()];

            for (int playerIndex = 0; playerIndex < displayResources.getNumberOfPlayers(); playerIndex++)
            {
                playerBoardLocations[playerIndex] = 0;
            }//END FOR

            for (int playerIndex = 0; playerIndex < displayResources.getNumberOfPlayers(); playerIndex++)
            {
                boardPlayerLocations[0][playerIndex] = true;
            }//END FOR
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //MOVE-----------------------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        //MOVE-----------------------------------------------------------------
        //---------------------------------------------------------------------

        public TurnResults move(int player, int spaces, TurnResults turnResults, int playerCash, Jail jail)
        {
            int currentLocation40 = playerBoardLocations[player];

            boardPlayerLocations[currentLocation40][player] = false;

            currentLocation40 = (currentLocation40 + spaces) % 40;

            //CHECK IF PASSED GO
            if (turnResults.location > currentLocation40)
            {
                System.Windows.Forms.MessageBox.Show("Passed Go!");
                turnResults.addPlayerCash(player, 200);
            }//END IF

            turnResults.location = currentLocation40;

            return processMove(player, spaces, turnResults, playerCash, jail);
        }

        //---------------------------------------------------------------------

        private TurnResults processMove(int player, int spaces, TurnResults turnResults, int playerCash, Jail jail)
        {
            int currentLocation40 = turnResults.location;

            int squareIndex28 = 0;

            int squareIndex22 = 0;

            int cashAssets = 0;

            int rent = 0;

            int propertyOwner = 0;

            int railRoads = 0;

            boardPlayerLocations[currentLocation40][player] = true;

            playerBoardLocations[player] = currentLocation40;

            if (currentLocation40 == 0)
            {
                // 0 GO

                processBattle(turnResults, currentLocation40, jail);

                turnResults.addPlayerCash(player, 200);
            }
            else if (currentLocation40 == 10)
            {
                // 10 Jail

                processBattle(turnResults, currentLocation40, jail);

                System.Windows.Forms.MessageBox.Show("Just Visiting");
            }
            else if (currentLocation40 == 20)
            {
                // 20 Free Parking

                processBattle(turnResults, currentLocation40, jail);

                System.Windows.Forms.MessageBox.Show("Free Parking!");

                turnResults.addPlayerCash(player, turnResults.freeParkingMoney);

                turnResults.freeParkingMoney = 500;
            }
            else if (currentLocation40 == 30)
            {
                // 30 Go To Jail
                System.Windows.Forms.MessageBox.Show("Go to Jail. Go directly to Jail. Do not pass GO, do not collect $200.");

                turnResults.location = 10;

                boardPlayerLocations[currentLocation40][player] = false;

                boardPlayerLocations[turnResults.location][player] = true;

                playerBoardLocations[player] = turnResults.location;

                turnResults.inJail = true;
            }
            else if (currentLocation40 == 4)
            {
                // 4 Income Tax

                processBattle(turnResults, currentLocation40, jail);

                cashAssets = playerCash + turnResults.getPlayerCash(player);

                if (canShowMortgageButton(player) == false && cashAssets < 2000)
                {
                    cashAssets = cashAssets / 10;

                    System.Windows.Forms.MessageBox.Show("Income Tax: " + cashAssets);

                    cashAssets = cashAssets * -1;

                    turnResults.addPlayerCash(player, cashAssets);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Income Tax: 200");

                    turnResults.addPlayerCash(player, -200);
                }//END IF
            }
            else if (currentLocation40 == 38)
            {
                // 38 Luxury Tax

                processBattle(turnResults, currentLocation40, jail);

                System.Windows.Forms.MessageBox.Show("Luxury Tax: 75");

                turnResults.addPlayerCash(player, -75);
            }
            else if (currentLocation40 == 7 ||
                    currentLocation40 == 22 ||
                    currentLocation40 == 36)
            {
                // 7 Chance 1
                // 22 Chance 2
                // 36 Chance 3

                processBattle(turnResults, currentLocation40, jail);

                turnResults.setValues(boardProperties.processChanceCard(player, turnResults));

                if (currentLocation40 != turnResults.location)
                {
                    boardPlayerLocations[currentLocation40][player] = false;

                    turnResults.setValues(processMove(player, spaces, turnResults, playerCash, jail));
                }//END IF

            }
            else if (currentLocation40 == 2 ||
                    currentLocation40 == 17 ||
                    currentLocation40 == 33)
            {
                // 2 Community Chest 1
                // 17 Community Chest 2
                // 33 Community Chest 3

                processBattle(turnResults, currentLocation40, jail);

                turnResults.setValues(boardProperties.processCommunityChestCard(player, turnResults));

                if (currentLocation40 != turnResults.location)
                {
                    boardPlayerLocations[currentLocation40][player] = false;

                    turnResults.setValues(processMove(player, spaces, turnResults, playerCash, jail));
                }//END IF
            }
            else if (currentLocation40 == 12 ||
                    currentLocation40 == 28)
            {
                // 12 Electric Company
                // 28 Water Works

                processBattle(turnResults, currentLocation40, jail);

                squareIndex28 = indexConversions.get28IndexFrom40Index(currentLocation40);

                propertyOwner = boardProperties.getOwnerOfProperty(squareIndex28);

                if ((turnResults.rentMultiplier == 10 ||
                    boardProperties.getOwnerOfProperty(7) == boardProperties.getOwnerOfProperty(20)) &&
                    propertyOwner >= 0 &&
                    !boardProperties.propertyMortgaged(squareIndex28))
                {
                    rent = spaces * 10;

                    turnResults.addPlayerCash(propertyOwner, rent);

                    rent = rent * -1;

                    turnResults.addPlayerCash(player, rent);
                }
                else if (propertyOwner >= 0 &&
                    !boardProperties.propertyMortgaged(squareIndex28))
                {
                    rent = spaces * 4;

                    turnResults.addPlayerCash(propertyOwner, rent);

                    rent = rent * -1;

                    turnResults.addPlayerCash(player, rent);
                }//END IF

                turnResults.rentMultiplier = 1;
            }
            else if (currentLocation40 == 5 ||
                    currentLocation40 == 15 ||
                    currentLocation40 == 25 ||
                    currentLocation40 == 35)
            {
                // 5 Reading Railroad
                // 15 Pennsylvania Railroad
                // 25 B&O Railroad
                // 35 Short Line

                processBattle(turnResults, currentLocation40, jail);

                squareIndex28 = indexConversions.get28IndexFrom40Index(currentLocation40);

                propertyOwner = boardProperties.getOwnerOfProperty(squareIndex28);

                railRoads = 0;

                if (propertyOwner >= 0 &&
                    !boardProperties.propertyMortgaged(squareIndex28))
                {
                    if (propertyOwner == boardProperties.getOwnerOfProperty(2))
                    {
                        railRoads++;
                    }//END IF

                    if (propertyOwner == boardProperties.getOwnerOfProperty(10))
                    {
                        railRoads++;
                    }//END IF

                    if (propertyOwner == boardProperties.getOwnerOfProperty(17))
                    {
                        railRoads++;
                    }//END IF

                    if (propertyOwner == boardProperties.getOwnerOfProperty(25))
                    {
                        railRoads++;
                    }//END IF

                    if (railRoads == 1)
                    {
                        rent = 25 * turnResults.rentMultiplier;
                    }
                    else if (railRoads == 2)
                    {
                        rent = 50 * turnResults.rentMultiplier;
                    }
                    else if (railRoads == 3)
                    {
                        rent = 100 * turnResults.rentMultiplier;
                    }
                    else if (railRoads == 4)
                    {
                        rent = 200 * turnResults.rentMultiplier;
                    }//END IF

                    turnResults.addPlayerCash(propertyOwner, rent);

                    rent = rent * -1;

                    turnResults.addPlayerCash(player, rent);
                }//END IF

                turnResults.rentMultiplier = 1;
            }
            else if (currentLocation40 == 1 || currentLocation40 == 3 ||
                    currentLocation40 == 6 || currentLocation40 == 8 ||
                    currentLocation40 == 9 || currentLocation40 == 11 ||
                    currentLocation40 == 13 || currentLocation40 == 14 ||
                    currentLocation40 == 16 || currentLocation40 == 18 ||
                    currentLocation40 == 19 || currentLocation40 == 21 ||
                    currentLocation40 == 23 || currentLocation40 == 24 ||
                    currentLocation40 == 26 || currentLocation40 == 27 ||
                    currentLocation40 == 29 || currentLocation40 == 31 ||
                    currentLocation40 == 32 || currentLocation40 == 34 ||
                    currentLocation40 == 37 || currentLocation40 == 39)
            {
                // 1 Mediterranean Avenue
                // 3 Baltic Avenue
                // 6 Oriental Avenue
                // 8 Vermont Avenue
                // 9 Connecticut Avenue
                // 11 St. Charles Place
                // 13 States Avenue
                // 14 Virginia Avenue
                // 16 St. James Place
                // 18 Tennessee Avenue
                // 19 New York Avenue
                // 21 Kentucky Avenue
                // 23 Indiana Avenue
                // 24 Illinois Avenue
                // 26 Atlantic Avenue
                // 27 Ventnor Avenue
                // 29 Marvin Gardens
                // 31 Pacific Avenue
                // 32 North Carolina Avenue
                // 34 Pennsylvania Avenue
                // 37 Park Place
                // 39 Boardwalk

                processBattle(turnResults, currentLocation40, jail);

                squareIndex28 = indexConversions.get28IndexFrom40Index(currentLocation40);

                squareIndex22 = indexConversions.get22IndexFrom40Index(currentLocation40);

                propertyOwner = boardProperties.getOwnerOfProperty(squareIndex28);

                if (propertyOwner >= 0 &&
                    !boardProperties.propertyMortgaged(squareIndex28))
                {
                    rent = boardProperties.getHousePropertyRent(
                        propertyOwner,
                        squareIndex22,
                        boardProperties.getHousesOnProperty(squareIndex22));

                    turnResults.addPlayerCash(propertyOwner, rent);

                    rent = rent * -1;

                    turnResults.addPlayerCash(player, rent);
                }//END IF
            }//END IF

            return turnResults;
        }

        //---------------------------------------------------------------------

        private TurnResults processBattle(TurnResults turnResults, int squareIndex40, Jail jail)
        {
            int[] battlePlayers;

            int numberOfBattlePlayers;

            int propertyValue = 0;

            int squareIndex28 = 0;

            numberOfBattlePlayers = getNumberOfBattlePlayersOnSquare(squareIndex40, jail);

            if (numberOfBattlePlayers >= 2)
            {
                battlePlayers = new int[numberOfBattlePlayers];

                Array.Copy(getBattlePlayersOnSquare(squareIndex40, jail),
                        battlePlayers,
                        battlePlayers.Length);

                if (squareIndex40 == 0)
                {
                    // 0 GO
                    propertyValue = 400;
                }
                else if (squareIndex40 == 10)
                {
                    // 10 Jail
                    propertyValue = 200;
                }
                else if (squareIndex40 == 20)
                {
                    // 20 Free Parking
                    propertyValue = 500;
                }
                else if (squareIndex40 == 4)
                {
                    // 4 Income Tax
                    propertyValue = 200;
                }
                else if (squareIndex40 == 38)
                {
                    // 38 Luxury Tax
                    propertyValue = 75;
                }
                else if (squareIndex40 == 7 ||
                        squareIndex40 == 22 ||
                        squareIndex40 == 36)
                {
                    // 7 Chance 1
                    // 22 Chance 2
                    // 36 Chance 3
                    propertyValue = 200;
                }
                else if (squareIndex40 == 2 ||
                        squareIndex40 == 17 ||
                        squareIndex40 == 33)
                {
                    // 2 Community Chest 1
                    // 17 Community Chest 2
                    // 33 Community Chest 3
                    propertyValue = 200;
                }
                else if (squareIndex40 == 12 ||
                        squareIndex40 == 28)
                {
                    // 12 Electric Company
                    // 28 Water Works
                    squareIndex28 = indexConversions.get28IndexFrom40Index(squareIndex40);

                    propertyValue = boardProperties.getPropertyPrice(squareIndex28);
                }
                else if (squareIndex40 == 5 ||
                        squareIndex40 == 15 ||
                        squareIndex40 == 25 ||
                        squareIndex40 == 35)
                {
                    // 5 Reading Railroad
                    // 15 Pennsylvania Railroad
                    // 25 B&O Railroad
                    // 35 Short Line
                    squareIndex28 = indexConversions.get28IndexFrom40Index(squareIndex40);

                    propertyValue = boardProperties.getPropertyPrice(squareIndex28);
                }
                else if (squareIndex40 == 1 || squareIndex40 == 3 ||
                        squareIndex40 == 6 || squareIndex40 == 8 ||
                        squareIndex40 == 9 || squareIndex40 == 11 ||
                        squareIndex40 == 13 || squareIndex40 == 14 ||
                        squareIndex40 == 16 || squareIndex40 == 18 ||
                        squareIndex40 == 19 || squareIndex40 == 21 ||
                        squareIndex40 == 23 || squareIndex40 == 24 ||
                        squareIndex40 == 26 || squareIndex40 == 27 ||
                        squareIndex40 == 29 || squareIndex40 == 31 ||
                        squareIndex40 == 32 || squareIndex40 == 34 ||
                        squareIndex40 == 37 || squareIndex40 == 39)
                {
                    // 1 Mediterranean Avenue
                    // 3 Baltic Avenue
                    // 6 Oriental Avenue
                    // 8 Vermont Avenue
                    // 9 Connecticut Avenue
                    // 11 St. Charles Place
                    // 13 States Avenue
                    // 14 Virginia Avenue
                    // 16 St. James Place
                    // 18 Tennessee Avenue
                    // 19 New York Avenue
                    // 21 Kentucky Avenue
                    // 23 Indiana Avenue
                    // 24 Illinois Avenue
                    // 26 Atlantic Avenue
                    // 27 Ventnor Avenue
                    // 29 Marvin Gardens
                    // 31 Pacific Avenue
                    // 32 North Carolina Avenue
                    // 34 Pennsylvania Avenue
                    // 37 Park Place
                    // 39 Boardwalk
                    squareIndex28 = indexConversions.get28IndexFrom40Index(squareIndex40);

                    propertyValue = boardProperties.getPropertyPrice(squareIndex28);
                }//END IF

                turnResults.setValues(battle.battle(battlePlayers, propertyValue, turnResults, displayResources));
            }//END IF

            return turnResults;
        }

        private int getNumberOfBattlePlayersOnSquare(int squareIndex40, Jail jail)
        {
            int battlePlayerCount = getNumberOfPlayersOnSquare(squareIndex40);

            if (squareIndex40 == 10)
            {
                int[] battlePlayers = new int[battlePlayerCount];

                Array.Copy(getPlayersOnSquare(squareIndex40),
                            battlePlayers,
                            battlePlayers.Length);

                for (int index = 0; index < battlePlayers.Length; index++)
                {
                    if (jail.getIsInJail(battlePlayers[index]))
                    {
                        battlePlayerCount--;
                    } 
                }//END FOR

                return battlePlayerCount;
            }
            else 
            {
                return battlePlayerCount;
            }
        }

        private int[] getBattlePlayersOnSquare(int squareIndex40, Jail jail)
        {
            if (squareIndex40 == 10)
            {
                int battlePlayerCount = getNumberOfBattlePlayersOnSquare(squareIndex40, jail);

                int[] playersOnSquare = new int[getNumberOfPlayersOnSquare(squareIndex40)];

                int[] battlePlayers = new int[battlePlayerCount];

                int battleIndex = 0;

                Array.Copy(getPlayersOnSquare(squareIndex40),
                            playersOnSquare,
                            playersOnSquare.Length);

                for (int index = 0; index < playersOnSquare.Length; index++)
                {
                    if (!jail.getIsInJail(battlePlayers[index]))
                    {
                        battlePlayers[battleIndex] = playersOnSquare[index];

                        battleIndex++;
                    }//END IF
                }//END FOR

                return battlePlayers;
            }
            else
            {
                return getPlayersOnSquare(squareIndex40);
            }
        }

        //---------------------------------------------------------------------
        //JAIL-----------------------------------------------------------------
        //---------------------------------------------------------------------

        public void goDirectlyToJail(int player)
        {
            int currentLocation = playerBoardLocations[player];

            boardPlayerLocations[currentLocation][player] = false;

            currentLocation = 10;

            boardPlayerLocations[currentLocation][player] = true;

            playerBoardLocations[player] = currentLocation;
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //PLAYER QUERIES-------------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        public int getPlayerLocationIndex(int player)
        {
            //RETURN INDEX OF THE PROPERTY PLAYER IS ON
            return playerBoardLocations[player];
        }

        //BATTLE---------------------------------------------------------------

        public int getNumberOfPlayersOnSquare(int squareIndex40)
        {
            int playerCount = 0;

            //SQUARE IS FROM ALL SQUARES 40
            //RETURN NUMBER OF PLAYERS ON SQUARE

            for (int index = 0; index < boardPlayerLocations[squareIndex40].Length; index++)
            {
                if (boardPlayerLocations[squareIndex40][index])
                {
                    playerCount++;
                }//END IF

            }//END FOR

            return playerCount;
        }

        public int[] getPlayersOnSquare(int squareIndex40)
        {
            int[] arrayPlayerIndexes = new int[getNumberOfPlayersOnSquare(squareIndex40)];

            int returnIndex = 0;

            //SQUARE IS FROM ALL SQUARES 40
            //RETURN ARRAY OF PLAYER INDEXES ON SQUARE

            for (int index = 0; index < boardPlayerLocations[squareIndex40].Length; index++)
            {
                if (boardPlayerLocations[squareIndex40][index])
                {
                    arrayPlayerIndexes[returnIndex] = index;

                    returnIndex++;
                }//END IF

            }//END FOR

            return arrayPlayerIndexes;
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //PROPERTY QUERIES-----------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        public int getOwnerOfProperty(int squareIndex28)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28
            //RETURN INDEX OF PLAYER WHO OWNS PROPERTY
            //RETURN -1 IF NO OWNER

            if (squareIndex28 > -1)
            {
                return boardProperties.getOwnerOfProperty(squareIndex28);
            }
            else 
            {
                return -1;
            }//END IF
        }

        public int getPriceOfProperty(int squareIndex40)
        {
            int squareIndex28 = 0;

            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28
            //RETURN INDEX OF PLAYER WHO OWNS PROPERTY
            //RETURN -1 IF NO OWNER

            squareIndex28 = indexConversions.get28IndexFrom40Index(squareIndex40);

            if (squareIndex28 > -1)
            {
                return boardProperties.getPropertyPrice(squareIndex28); 
            }
            else
            {
                return 0;
            }//END IF
        }

        public Boolean propertyForSale(int squareIndex28)
        {
            //RETURN TRUE IF PROPERTY PLAYER ON IS FOR SALE

            return boardProperties.getOwnerOfProperty(squareIndex28) < 0;
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //PURCHASE PROPERTY----------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        public void purchaseProperty(int squareIndex28, int player)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28
            boardProperties.purchaseProperty(squareIndex28, player);
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //MORTGAGE AND UNMORTGAGE----------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        public Boolean canMortgageProperty(int squareIndex28)
        {
            //SQUARE IS FROM ALL PROPERTY SQUARES 28
            //RETURN TRUE IF PROPERTY ISN'T MORTGAGED
            return !boardProperties.propertyMortgaged(squareIndex28);
        }

        public Boolean canUnmortgageProperty(int squareIndex28)
        {
            //SQUARE IS FROM ALL PROPERTY SQUARES 28
            //RETURN TRUE IF PROPERTY IS MORTGAGED
            return boardProperties.propertyMortgaged(squareIndex28);
        }

        //---------------------------------------------------------------------

        public int getMortgagePropertyValue(int squareIndex28)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28
            //RETURN PROPERTY MORTGAGE VALUE
            return boardProperties.getMortgagePropertyValue(squareIndex28);
        }

        //---------------------------------------------------------------------

        public Boolean canShowMortgageButton(int player)
        {
            //RETURN TRUE IF PLAYER OWNS PROPERTY
            return boardProperties.canShowMortgageButton(player);
        }

        //---------------------------------------------------------------------

        public TurnResults mortgageUnmortgage(Boolean[] mortgageProperties, TurnResults turnResults, int player)
        {
            //FLIP ALL PROPERTIES THAT ARE TRUE
            return boardProperties.mortgageUnmortgage(mortgageProperties, turnResults, player);
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //HOUSES---------------------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        public int getNumberOfHouses(int squareIndex22)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            //RETURN NUMBER OF HOUSES
            return boardProperties.getHousesOnProperty(squareIndex22);
        }

        //---------------------------------------------------------------------

        public int getHousePropertyCost(int squareIndex22)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN HAVE HOUSES 22
            //RETURN PROPERTY HOUSE COST
            return boardProperties.getHousePropertyHouseCost(squareIndex22);
        }

        //---------------------------------------------------------------------

        public Boolean squareCanBuyHouses(int player, int squareIndex22, int[] houses)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            return boardProperties.squareCanBuyHouses(player, squareIndex22, houses);
        }

        public Boolean squareCanSellHouses(int player, int squareIndex22, int[] houses)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            return boardProperties.squareCanSellHouses(player, squareIndex22, houses);
        }

        //---------------------------------------------------------------------

        public Boolean playerCanBuyHouses(int player)
        {
            return boardProperties.playerCanBuyHouses(player);
        }

        public Boolean playerCanSellHouses(int player)
        {
            return boardProperties.playerCanSellHouses(player);
        }

        //---------------------------------------------------------------------
        //BUY HOUSES-----------------------------------------------------------
        //---------------------------------------------------------------------

        public TurnResults buyHouses(int player, int[] houses, TurnResults turnResults)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            //ADD HOUSES TO TOTAL
            return boardProperties.addHouses(player, houses, turnResults);

        }

        //---------------------------------------------------------------------
        //SELL HOUSES----------------------------------------------------------
        //---------------------------------------------------------------------

        public TurnResults sellHouses(int player, int[] houses, TurnResults turnResults)
        {
            //SQUARE IS FROM PROPERTIES THAT BUY HOUSES 22
            //ADD HOUSES TO TOTAL
            //ONLY NEGITIVE NUMBERS AND ZEROS IN HOUSES
            return boardProperties.addHouses(player, houses, turnResults);

        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //MAKE TRADE-----------------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        public TurnResults makeTrade(
            int currentPlayer, 
            int tradePlayer, 
            Boolean[] propertiesToPlayer,
            Boolean[] propertiesToTradePlayer,
            TurnResults turnResults)
        {
            return boardProperties.makeTrade(
                                    currentPlayer,
                                    tradePlayer,
                                    propertiesToPlayer,
                                    propertiesToTradePlayer,
                                    turnResults);
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //SURRENDER------------------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        public TurnResults SurrenderFreePlayer(int player, TurnResults turnResults)
        {
            boardPlayerLocations[playerBoardLocations[player]][player] = false;

            playerBoardLocations[player] = -1;

            return boardProperties.freePlayerProperties(player, turnResults);
        }

        public void SurrenderTransferPlayer(int fromPlayer, int toPlayer)
        {
            boardPlayerLocations[playerBoardLocations[fromPlayer]][fromPlayer] = false;

            playerBoardLocations[fromPlayer] = -1;

            boardProperties.transferPlayerProperties(fromPlayer, toPlayer);
        }

    }
}
