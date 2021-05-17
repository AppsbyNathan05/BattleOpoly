using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class TurnDirector
    {
        private IndexConversions indexConversions = new IndexConversions();

        private FreeParking freeParking = new FreeParking();

        private Dice dice = new Dice();

        private Jail jail;

        private TurnResults turnResults;

        private BoardDirector boardDirector;

        private DisplayResources displayResources;

        private Player[] players;

        // 0 PRE ROLL
        // 1 DOUBLES
        // 2 POST ROLL
        // 3 PRE ROLL IN JAIL
        // 4 POST ROLL IN JAIL
        // 5 MORTGAGE
        // 6 BUY HOUSES
        // 7 SELL HOUSES
        // 8 TRADE
        // 9 OWE MONEY
        // 10 AUCTION
        private int currentState = 0;

        //PLAYER WHO'S TURN IT IS
        public int currentPlayer { get; set; }

        //SELECTED TRADE PLAYER
        public int tradePlayer { get; set; }

        //MORTGAGE PROPERTIES
        Boolean[] mortgageProperties = new Boolean[28];

        //TRADE PROPERTIES
        Boolean[] propertiesToPlayer = new Boolean[28];
        Boolean[] propertiesToTradePlayer = new Boolean[28];

        //HOUSE PROPERTIES
        int[] houses = new int[22];

        //TEMP PLAYER CASH
        int[] tempPlayerCash = new int[6];

        public TurnDirector()
        {
            intializeValues();
        }

        public TurnDirector(DisplayResources displayResources)
        {
            boardDirector = new BoardDirector(displayResources);

            players = new Player[displayResources.getNumberOfPlayers()];

            for (int index = 0; index < displayResources.getNumberOfPlayers(); index++)
            {
                players[index] = new Player();
            }//END FOR

            jail = new Jail(displayResources.getNumberOfPlayers());

            turnResults = new TurnResults(displayResources.getNumberOfPlayers());

            this.displayResources = new DisplayResources(
                            displayResources.getPlayerNames(),
                            displayResources.getPlayerAbvs());

            intializeValues();
        }

        //---------------------------------------------------------------------

        private void intializeValues()
        {
            //PLAYER WHO'S TURN IT IS
            currentPlayer = 0;

            //SELECTED TRADE PLAYER
            tradePlayer = -1;

            //INITIALIZE FREE PARKING
            freeParking.resetFreeParking();

            //INITIALIZE TURN RESULTS
            turnResults.intializeValues(freeParking.cash);

            resetTempActionArrays();

            currentState = 0;
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //CURRENT STATE--------------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        //GET------------------------------------------------------------------
        //---------------------------------------------------------------------

        public int getCurrentState()
        {
            //RETURN INDEX OF THE PROPERTY PLAYER IS ON
            return currentState;
        }

        //---------------------------------------------------------------------
        //SET------------------------------------------------------------------
        //---------------------------------------------------------------------

        public void setState()
        {
            // 0 PRE ROLL
            // 1 DOUBLES
            // 2 POST ROLL
            // 3 PRE ROLL IN JAIL
            // 4 POST ROLL IN JAIL
            // 5 MORTGAGE
            // 6 BUY HOUSES
            // 7 SELL HOUSES
            // 8 TRADE
            // 9 OWE MONEY
            // 10 AUCTION

            currentState = 0;

            if (players[currentPlayer].cash < 0)
            {
                // 9 OWE MONEY 
                currentState = 9;
                //System.Windows.Forms.MessageBox.Show("CURRENT STATE OWE MONEY");
            }
            if (jail.getIsInJail(currentPlayer) && dice.getCanRoll())
            {
                // 3 PRE ROLL IN JAIL
                currentState = 3;
                //System.Windows.Forms.MessageBox.Show("CURRENT STATE PRE ROLL IN JAIL");
            }
            else if (jail.getIsInJail(currentPlayer) && !dice.getCanRoll())
            {
                // 4 POST ROLL IN JAIL
                currentState = 4;
                //System.Windows.Forms.MessageBox.Show("CURRENT STATE POST ROLL IN JAIL");
            }
            else if (!dice.getCanRoll())
            {
                // 2 POST ROLL
                currentState = 2;
                //System.Windows.Forms.MessageBox.Show("CURRENT STATE POST ROLL");
            }
            else if (dice.rolledDoubles())
            {
                // 1 DOUBLES
                currentState = 1;
                //System.Windows.Forms.MessageBox.Show("CURRENT STATE DOUBLES");
            }
            else if (dice.getCanRoll())
            {
                // 0 PRE ROLL
                currentState = 0;
                //System.Windows.Forms.MessageBox.Show("CURRENT STATE PRE ROLL");
            }//END IF

        }

        // 0 PRE ROLL

        public void setStatePreRoll()
        {
            currentState = 0;
            //System.Windows.Forms.MessageBox.Show("CURRENT STATE PRE ROLL");
        }

        // 1 DOUBLES

        public void setStateDoubles()
        {
            currentState = 1;
            //System.Windows.Forms.MessageBox.Show("CURRENT STATE DOUBLES");
        }

        // 2 POST ROLL

        public void setStatePostRoll()
        {
            currentState = 2;
            //System.Windows.Forms.MessageBox.Show("CURRENT STATE POST ROLL");
        }

        // 3 PRE ROLL IN JAIL

        public void setStatePreRollInJail()
        {
            currentState = 3;
            //System.Windows.Forms.MessageBox.Show("CURRENT STATE PRE ROLL IN JAIL");
        }

        // 4 POST ROLL IN JAIL

        public void setStatePostRollInJail()
        {
            currentState = 4;
            //System.Windows.Forms.MessageBox.Show("CURRENT STATE POST ROLL IN JAIL");
        }

        // 5 MORTGAGE

        public void setStateMortgage()
        {
            currentState = 5;
            //System.Windows.Forms.MessageBox.Show("CURRENT STATE MORTGAGE");
        }

        // 6 BUY HOUSES

        public void setStateBuyHouses()
        {
            currentState = 6;
            //System.Windows.Forms.MessageBox.Show("CURRENT STATE BUY HOUSES");
        }

        // 7 SELL HOUSES

        public void setStateSellHouses()
        {
            currentState = 7;
            //System.Windows.Forms.MessageBox.Show("CURRENT STATE SELL HOUSES");
        }

        // 8 TRADE

        public void setStateTrade()
        {
            currentState = 8;
            //System.Windows.Forms.MessageBox.Show("CURRENT STATE TRADE");
        }

        // 9 OWE MONEY

        public void setStateOweMoney()
        {
            currentState = 9;
            //System.Windows.Forms.MessageBox.Show("CURRENT STATE OWE MONEY");
        }

        // 10 AUCTION

        public void setStateAuction()
        {
            currentState = 10;
            //System.Windows.Forms.MessageBox.Show("CURRENT STATE AUCTION");
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //PLAYERS--------------------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        //QUERIES--------------------------------------------------------------
        //---------------------------------------------------------------------

        //VALIDATION-----------------------------------------------------------

        public Boolean isPlayer(int player)
        {
            //RETURN TRUE IF PLAYER IS PLAYING
            return player < players.Length && players[player].stillPlaying;
        }
        
        //LOCATION-------------------------------------------------------------

        public int getPlayerLocationIndex(int player)
        {
            //RETURN INDEX OF THE PROPERTY PLAYER IS ON
            return boardDirector.getPlayerLocationIndex(player);
        }
        
        //CASH-----------------------------------------------------------------

        public int getPlayerCash(int player)
        {
            return players[player].cash + tempPlayerCash[player];
        }

        //MORTGAGE-------------------------------------------------------------

        public Boolean canShowMortgageButton()
        {
            //RETURN TRUE IF PLAYER OWNS PROPERTY
            return boardDirector.canShowMortgageButton(currentPlayer);
        }

        //HOUSES---------------------------------------------------------------

        public Boolean canShowBuyHousesButton()
        {
            //RETURN TRUE IF PLAYER OWNS PROPERTY WHERE THEY CAN BUY HOUSES
            return boardDirector.playerCanBuyHouses(currentPlayer);
        }

        public Boolean canShowSellHousesButton()
        {
            //RETURN TRUE IF PLAYER HAS ANY HOUSES
            return boardDirector.playerCanSellHouses(currentPlayer);
        }

        //---------------------------------------------------------------------
        //ACTIONS--------------------------------------------------------------
        //---------------------------------------------------------------------

        //ROLL-----------------------------------------------------------------

        public void roll()
        {
            int playerLocation40 = players[currentPlayer].location;

            int squareIndex28 = indexConversions.get28IndexFrom40Index(playerLocation40);

            //IF PLAYER ON HOUSE FOR SALE AUCTION
            if (boardDirector.getOwnerOfProperty(squareIndex28) == -1 &&
                squareIndex28 > -1 &&
                players[currentPlayer].stillPlaying)
            {
                setStateAuction();
            }
            else
            {
                dice.rollDice();

                if (dice.thirdDoublesRoll())
                {
                    goToJail();
                }
                else if (dice.rolledDoubles())
                {
                    if (jail.getIsInJail(currentPlayer))
                    {
                        jail.setIsInJail(currentPlayer, false);

                        setStatePostRoll();

                        //MOVE
                        move(dice.getRoll());

                        dice.resetDice(false);
                    }
                    else if (jail.getIsInJail(currentPlayer) == false)
                    {
                        setStateDoubles();

                        //MOVE
                        move(dice.getRoll());
                    }//END IF
                }
                else
                {
                    if (jail.getIsInJail(currentPlayer))
                    {
                        jail.addDayInJail(currentPlayer);

                        if (jail.thirdDayInJail(currentPlayer))
                        {
                            getOutOfJail();

                            //MOVE
                            move(dice.getRoll());
                        }
                    }
                    else if (jail.getIsInJail(currentPlayer) == false)
                    {
                        //MOVE
                        move(dice.getRoll());
                    }//END IF

                    setStatePostRoll();
                }//END IF
            }//END IF
 
        }

        //GET OUT OF JAIL------------------------------------------------------

        public void getOutOfJail()
        {
            int playerCash = players[currentPlayer].cash;

            jail.setIsInJail(currentPlayer, false);

            players[currentPlayer].getOutOfJail();

            freeParking.cash = freeParking.cash + (playerCash - players[currentPlayer].cash);

            setState();
        }

        //PURCHASE PROPERTY----------------------------------------------------

        public void purchaseProperty()
        {
            //SQUARE IS FROM ALL SQUARES 40
            purchaseProperty(
                currentPlayer,
                players[currentPlayer].location,
                boardDirector.getPriceOfProperty(players[currentPlayer].location));
        }

        public void purchaseProperty(int player, int squareIndex40, int bid)
        {
            //SQUARE IS FROM ALL SQUARES 40
            int squareIndex28 = indexConversions.get28IndexFrom40Index(squareIndex40);

            if (squareIndex28 > -1)
            {
                players[player].cash -= bid;

                boardDirector.purchaseProperty(squareIndex28, player);

                setState();
            }//END IF
        }

        //MORTGAGE AND UNMORTGAGE----------------------------------------------

        public void mortgageUnmortgage()
        {
            //FLIP ALL PROPERTIES THAT ARE TRUE
            turnResults.setValues(boardDirector.mortgageUnmortgage(mortgageProperties, turnResults, currentPlayer));

            resetTempActionArrays();

            applyTurnResults();

            setState();
        }

        //BUY HOUSES-----------------------------------------------------------

        public void buyHouses()
        {
            //ADD HOUSES TO TOTAL
            turnResults.setValues(boardDirector.buyHouses(currentPlayer, houses, turnResults));

            resetTempActionArrays();

            applyTurnResults();

            setState();
        }

        //SELL HOUSES----------------------------------------------------------

        public void sellHouses()
        {
            //ADD HOUSES TO TOTAL
            //ONLY NEGITIVE NUMBERS AND ZEROS IN HOUSES
            turnResults.setValues(boardDirector.sellHouses(currentPlayer, houses, turnResults));

            resetTempActionArrays();

            applyTurnResults();

            setState();
        }

        //MAKE TRADE-----------------------------------------------------------

        public void makeTrade(int cashToPlayer, int cashToTradePlayer)
        {
            turnResults.setValues(boardDirector.makeTrade(
                                        currentPlayer,
                                        tradePlayer,
                                        propertiesToPlayer,
                                        propertiesToTradePlayer,
                                        turnResults));

            players[currentPlayer].cash += cashToPlayer;

            players[tradePlayer].cash += cashToTradePlayer;

            players[currentPlayer].cash -= cashToTradePlayer;

            players[tradePlayer].cash -= cashToPlayer;

            resetTempActionArrays();

            applyTurnResults();

            setState();
        }

        //NEXT TURN------------------------------------------------------------

        public void nextTurn()
        {
            int playerLocation40 = players[currentPlayer].location;

            int squareIndex28 = indexConversions.get28IndexFrom40Index(playerLocation40);

            //IF PLAYER ON HOUSE FOR SALE AUCTION
            if (boardDirector.getOwnerOfProperty(squareIndex28) == -1 && 
                squareIndex28 > -1 && 
                players[currentPlayer].stillPlaying)
            {
                setStateAuction();
            }
            else
            {
                do
                {
                    currentPlayer = (currentPlayer + 1) % players.Length;
                } while (players[currentPlayer].stillPlaying == false);

                turnResults.location = players[currentPlayer].location;

                dice.resetDice(true);

                setState();
            }//END IF
        }

        //SURRENDER------------------------------------------------------------

        public void surrender()
        {
            //TRANSFER MONEY AND RESOURCES TO OWNER OF SQUARE

            int squareIndex40 = players[currentPlayer].location;

            int squareIndex28 = indexConversions.get28IndexFrom40Index(squareIndex40);

            int toPlayer = boardDirector.getOwnerOfProperty(squareIndex28);

            if (toPlayer == -1 || squareIndex28 == -1)
            {
                turnResults.setValues(boardDirector.SurrenderFreePlayer(currentPlayer, turnResults));

                applyTurnResults();

                freeParking.cash = freeParking.cash + players[currentPlayer].cash;
            }
            else
            {
                boardDirector.SurrenderTransferPlayer(currentPlayer, toPlayer);

                players[toPlayer].cash = players[toPlayer].cash + players[currentPlayer].cash;
            }

            players[currentPlayer].stillPlaying = false;
            turnResults.setPlayerNotPlaying(currentPlayer);

            nextTurn();
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //TEMP VARIABLES-------------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        public void resetTempActionArrays()
        {
            for (int index = 0; index < mortgageProperties.Length; index++)
            {
                mortgageProperties[index] = false;

                propertiesToPlayer[index] = false;
                propertiesToTradePlayer[index] = false;
            }//END FOR

            for (int index = 0; index < houses.Length; index++)
            {
                houses[index] = 0;
            }//END FOR

            for (int index = 0; index < tempPlayerCash.Length; index++)
            {
                tempPlayerCash[index] = 0;
            }//END FOR
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //TURN RESULTS---------------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        private void applyTurnResults()
        {
            Boolean[] stillPlaying = new Boolean[players.Length];

            players[currentPlayer].location = turnResults.location;

            for (int index = 0; index < players.Length; index++)
            {
                if (players[index].stillPlaying)
                {
                    players[index].cash += turnResults.getPlayerCash(index);
                    stillPlaying[index] = true;
                }
                else
                {
                    stillPlaying[index] = false;
                }//END IF
            }//END FOR

            if (turnResults.inJail)
            {
                jail.setIsInJail(currentPlayer, true);
            }//END IF

            players[currentPlayer].addGetOutOfJailFreeCards(turnResults.getOutOfJailFreeCards);

            freeParking.cash = turnResults.freeParkingMoney;

            turnResults.intializeValues(freeParking.cash, stillPlaying);
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //BOARD----------------------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        //QUERIES--------------------------------------------------------------
        //---------------------------------------------------------------------

        //BATTLE---------------------------------------------------------------

        public int getNumberOfPlayersOnSquare(int squareIndex40)
        {
            //SQUARE IS FROM ALL SQUARES 40
            //RETURN NUMBER OF PLAYERS ON SQUARE
            return boardDirector.getNumberOfPlayersOnSquare(squareIndex40);
        }

        public int[] getPlayersOnSquare(int squareIndex40)
        {
            //SQUARE IS FROM ALL SQUARES 40
            //RETURN ARRAY OF PLAYER INDEXES ON SQUARE
            return boardDirector.getPlayersOnSquare(squareIndex40);
        }
        
        //BUY OR PAY RENT------------------------------------------------------

        public int getOwnerOfProperty(int squareIndex28)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28
            //RETURN INDEX OF PLAYER WHO OWNS PROPERTY
            //RETURN -1 IF NO OWNER

            if (squareIndex28 > -1)
            {
                if (propertiesToPlayer[squareIndex28])
                {
                    //OWNER STRING
                    return currentPlayer;
                }
                else if (propertiesToTradePlayer[squareIndex28])
                {
                    //OWNER STRING
                    return tradePlayer;
                }
                else
                {
                    //OWNER STRING
                    return boardDirector.getOwnerOfProperty(squareIndex28);
                }//END IF 
            }
            else
            {
                return -1;
            }//END IF
        }

        public Boolean propertyForSale()
        {
            int squareIndex28 = indexConversions.get28IndexFrom40Index(players[currentPlayer].location);

            if (squareIndex28 > -1)
            {
                //RETURN TRUE IF PROPERTY PLAYER ON IS FOR SALE
                return boardDirector.propertyForSale(squareIndex28);
            }
            else
            {
                return false;
            }//END IF
        }

        //MORTGAGE-------------------------------------------------------------

        public int getMortgagePropertyValue(int squareIndex28)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28
            //RETURN PROPERTY MORTGAGE VALUE
            return boardDirector.getMortgagePropertyValue(squareIndex28);
        }

        public Boolean canMortgageProperty(int squareIndex40)
        {
            //SQUARE IS FROM ALL SQUARES 40

            //CONVERT INDEX FROM 40 TO 28
            int squareIndex28 = indexConversions.get28IndexFrom40Index(squareIndex40);

            if (squareIndex28 > -1)
            {
                //RETURN TRUE IF PLAYER CAN MORTGAGE PROPERTY
                return boardDirector.canMortgageProperty(squareIndex28) ^ mortgageProperties[squareIndex28];
            }
            else
            {
                return false;
            }//END IF
        }

        public Boolean canPlayerMortgageProperty(int squareIndex40)
        {
            //SQUARE IS FROM ALL SQUARES 40

            //CONVERT INDEX FROM 40 TO 28
            int squareIndex28 = indexConversions.get28IndexFrom40Index(squareIndex40);

            if (squareIndex28 > -1)
            {
                //RETURN TRUE IF PLAYER CAN MORTGAGE PROPERTY
                return (boardDirector.canMortgageProperty(squareIndex28) ^
                    mortgageProperties[squareIndex28]) &&
                    boardDirector.getOwnerOfProperty(squareIndex28) == currentPlayer;
            }
            else
            {
                return false;
            }//END IF
        }

        public Boolean canPlayerUnmortgageProperty(int squareIndex40)
        {
            //SQUARE IS FROM ALL SQUARES 40

            //CONVERT INDEX FROM 40 TO 28
            int squareIndex28 = indexConversions.get28IndexFrom40Index(squareIndex40);

            if (squareIndex28 > -1)
            {
                //RETURN TRUE IF PLAYER CAN UNMORTGAGE PROPERTY
                return (boardDirector.canUnmortgageProperty(squareIndex28) ^
                    mortgageProperties[squareIndex28]) &&
                    boardDirector.getOwnerOfProperty(squareIndex28) == currentPlayer;
            }
            else
            {
                return false;
            }//END IF
        }

        //HOUSES---------------------------------------------------------------

        public int getHousePropertyCost(int squareIndex22)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN HAVE HOUSES 22
            //RETURN PROPERTY HOUSE COST
            return boardDirector.getHousePropertyCost(squareIndex22);
        }

        public int getNumberOfHouses(int squareIndex28)
        {
            //SQUARE IS FROM PROPERTIES THAT CAN BE OWNED 28

            //CONVERT INDEX FROM 28 TO 22
            int squareIndex22 = indexConversions.get22IndexFrom28Index(squareIndex28);

            if (squareIndex22 > -1)
            {
                //RETURN NUMBER OF HOUSES
                return boardDirector.getNumberOfHouses(squareIndex22) + houses[squareIndex22];
            }
            else
            {
                return 0;
            }//END IF
        }

        public Boolean canBuyHousesOnSquare(int squareIndex40)
        {
            //SQUARE IS FROM ALL SQUARES 40

            //CONVERT INDEX FROM 40 TO 28
            int squareIndex22 = indexConversions.get22IndexFrom40Index(squareIndex40);

            if (squareIndex22 > -1)
            {
                //RETURN TRUE IF PLAYER CAN PURCHASE HOUSE ON SQUARE
                return boardDirector.squareCanBuyHouses(currentPlayer, squareIndex22, houses);
            }
            else
            {
                return false;
            }//END IF
        }

        public Boolean canSellHousesOnSquare(int squareIndex40)
        {
            //SQUARE IS FROM ALL SQUARES 40

            //CONVERT INDEX FROM 40 TO 28
            int squareIndex22 = indexConversions.get22IndexFrom40Index(squareIndex40);

            if (squareIndex22 > -1)
            {
                //RETURN TRUE IF PLAYERCAN SELL HOUSE ON SQUARE
                return boardDirector.squareCanSellHouses(currentPlayer, squareIndex22, houses);
            }
            else
            {
                return false;
            }//END IF
        }

        //TRADE----------------------------------------------------------------

        public Boolean propertyInTrade(int squareIndex40)
        {
            //SQUARE IS FROM ALL SQUARES ON BOARD 40
            
            //CONVERT INDEX FROM 40 TO 28
            int squareIndex28 = indexConversions.get28IndexFrom40Index(squareIndex40);

            if (squareIndex28 > -1)
            {
                //RETURN TRUE IF PLAYER OWNS PROPERTY
                return propertiesToPlayer[squareIndex28] || propertiesToTradePlayer[squareIndex28];
            }
            else
            {
                return false;
            }//END IF
        }

        //FREE PARKING---------------------------------------------------------

        public int getCashInFreeParking()
        {
            return freeParking.cash;
        }

        //---------------------------------------------------------------------
        //ACTIONS--------------------------------------------------------------
        //---------------------------------------------------------------------

        public void squareClick(int squareIndex28)
        {
            //INDEX 28
            int squareIndex40 = 0;

            //INDEX 22
            int squareIndex22 = 0;

            if (currentState == 5)
            {
                squareIndex40 = indexConversions.get40IndexFrom28Index(squareIndex28);

                // 5 MORTGAGE
                if (canPlayerMortgageProperty(squareIndex40))
                {
                    tempPlayerCash[currentPlayer] =
                        tempPlayerCash[currentPlayer] +
                        getMortgagePropertyValue(squareIndex28);
                }
                else if (canPlayerUnmortgageProperty(squareIndex40))
                {
                    tempPlayerCash[currentPlayer] =
                        tempPlayerCash[currentPlayer] -
                        getMortgagePropertyValue(squareIndex28);
                }//END IF

                if (mortgageProperties[squareIndex28] == true)
                {
                    mortgageProperties[squareIndex28] = false;
                }
                else
                {
                    mortgageProperties[squareIndex28] = true;
                }//END IF
            }
            else if (currentState == 6)
            {
                squareIndex22 = indexConversions.get22IndexFrom28Index(squareIndex28);
                Console.WriteLine("BEFORE " + houses[squareIndex22]);
                // 6 BUY HOUSES
                houses[squareIndex22] = houses[squareIndex22] + 1;
                Console.WriteLine("AFTER " + houses[squareIndex22]);
                tempPlayerCash[currentPlayer] =
                        tempPlayerCash[currentPlayer] -
                        getHousePropertyCost(squareIndex22);
            }
            else if (currentState == 7)
            {
                squareIndex22 = indexConversions.get22IndexFrom28Index(squareIndex28);
                Console.WriteLine("BEFORE " + houses[squareIndex22]);
                // 7 SELL HOUSES
                houses[squareIndex22] = houses[squareIndex22] - 1;
                Console.WriteLine("AFTER " + houses[squareIndex22]);
                tempPlayerCash[currentPlayer] =
                        tempPlayerCash[currentPlayer] +
                        getHousePropertyCost(squareIndex22);
            }
            else if (currentState == 8)
            {
                // 8 TRADE
                if (getOwnerOfProperty(squareIndex28) == currentPlayer ||
                    propertiesToPlayer[squareIndex28])
                {
                    if (getOwnerOfProperty(squareIndex28) == tradePlayer)
                    {
                        propertiesToTradePlayer[squareIndex28] = false;
                    }
                    else
                    {
                        propertiesToTradePlayer[squareIndex28] = true;
                    }//END IF
                    propertiesToPlayer[squareIndex28] = false;
                }
                else if (getOwnerOfProperty(squareIndex28) == tradePlayer ||
                    propertiesToTradePlayer[squareIndex28])
                {
                    if (getOwnerOfProperty(squareIndex28) == currentPlayer)
                    {
                        propertiesToPlayer[squareIndex28] = false;
                    }
                    else
                    {
                        propertiesToPlayer[squareIndex28] = true;
                    }//END IF
                    propertiesToTradePlayer[squareIndex28] = false;
                }//END IF
            }//END IF
        }

        private void goToJail()
        {
            System.Windows.Forms.MessageBox.Show("Go to Jail. Go directly to Jail. Do not pass GO, do not collect $200.");

            jail.setIsInJail(currentPlayer, true);

            //SET LOCATION TO JAIL 
            //WITH PLAYER
            players[currentPlayer].location = 10;
            //ON BOARD
            boardDirector.goDirectlyToJail(currentPlayer);

            dice.resetDice(false);

            setStatePostRollInJail();
        }

        private void move(int spaces)
        {
            turnResults.location = players[currentPlayer].location;

            turnResults.setValues(
                boardDirector.move(
                    currentPlayer, 
                    spaces, 
                    turnResults, 
                    players[currentPlayer].cash,
                    jail));

            applyTurnResults();
        }
    }
}
