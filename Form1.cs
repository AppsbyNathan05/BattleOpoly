using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleOpoly
{
    public partial class BattleOpolyForm : Form
    {

        private IndexConversions indexConversions = new IndexConversions();

        private TurnDirector turnDirector;

        private DisplayResources displayResources;

        private int numberOfPlayers = 2;

        public BattleOpolyForm()
        {
            InitializeComponent();

            setup.Visible = true;
            game.Visible = false;

            player3InputLabel.Visible = false;
            player4InputLabel.Visible = false;
            player5InputLabel.Visible = false;
            player6InputLabel.Visible = false;

            player3AbvInputTb.Visible = false;
            player4AbvInputTb.Visible = false;
            player5AbvInputTb.Visible = false;
            player6AbvInputTb.Visible = false;

            player3NameInputTb.Visible = false;
            player4NameInputTb.Visible = false;
            player5NameInputTb.Visible = false;
            player6NameInputTb.Visible = false;

            subtractPlayerBtn.Visible = false;

            numberOfPlayers = 2;
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //GAME SETUP-----------------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        private void addPlayerBtn_Click(object sender, EventArgs e)
        {
            if (numberOfPlayers < 6)
            {
                numberOfPlayers++; 
            }//END IF

            updateSetupButtons();
        }

        private void subtractPlayerBtn_Click(object sender, EventArgs e)
        {
            if (numberOfPlayers > 2)
            {
                numberOfPlayers--;
            }//END IF

            updateSetupButtons();
        }

        private void startGameBtn_Click(object sender, EventArgs e)
        {
            string[] playerNames = new string[numberOfPlayers];
            string[] playerAbvs = new string[numberOfPlayers];

            playerNames[0] = getValidNameString(player1NameInputTb.Text, 1);
            playerNames[1] = getValidNameString(player2NameInputTb.Text, 2);

            playerAbvs[0] = getValidAbvString(player1AbvInputTb.Text, 1);
            playerAbvs[1] = getValidAbvString(player2AbvInputTb.Text, 2);

            if (numberOfPlayers > 2)
            {
                playerAbvs[2] = getValidAbvString(player3AbvInputTb.Text, 3);
                playerNames[2] = getValidNameString(player3NameInputTb.Text, 3);
            }//END IF

            if (numberOfPlayers > 3)
            {
                playerAbvs[3] = getValidAbvString(player4AbvInputTb.Text, 4);
                playerNames[3] = getValidNameString(player4NameInputTb.Text, 4);
            }//END IF

            if (numberOfPlayers > 4)
            {
                playerAbvs[4] = getValidAbvString(player5AbvInputTb.Text, 5);
                playerNames[4] = getValidNameString(player5NameInputTb.Text, 5);
            }//END IF

            if (numberOfPlayers > 5)
            {
                playerAbvs[5] = getValidAbvString(player6AbvInputTb.Text, 6);
                playerNames[5] = getValidNameString(player6NameInputTb.Text, 6);
            }//END IF

            setup.Visible = false;
            game.Visible = true;

            displayResources = new DisplayResources(playerNames, playerAbvs);

            turnDirector = new TurnDirector(displayResources);

            initialize();
        }

        //---------------------------------------------------------------------

        private void updateSetupButtons()
        {
            addPlayerBtn.Visible = false;
            subtractPlayerBtn.Visible = false;

            if (numberOfPlayers > 5)
            {
                subtractPlayerBtn.Visible = true;
            }
            else if (numberOfPlayers < 3)
            {
                addPlayerBtn.Visible = true;
            }
            else
            {
                addPlayerBtn.Visible = true;
                subtractPlayerBtn.Visible = true;
            }//END IF

            player3InputLabel.Visible = false;
            player4InputLabel.Visible = false;
            player5InputLabel.Visible = false;
            player6InputLabel.Visible = false;

            player3AbvInputTb.Visible = false;
            player4AbvInputTb.Visible = false;
            player5AbvInputTb.Visible = false;
            player6AbvInputTb.Visible = false;

            player3NameInputTb.Visible = false;
            player4NameInputTb.Visible = false;
            player5NameInputTb.Visible = false;
            player6NameInputTb.Visible = false;

            if (numberOfPlayers > 2)
            {
                player3InputLabel.Visible = true;
                player3AbvInputTb.Visible = true;
                player3NameInputTb.Visible = true; 
            }//END IF

            if (numberOfPlayers > 3)
            {
                player4InputLabel.Visible = true;
                player4AbvInputTb.Visible = true;
                player4NameInputTb.Visible = true;
            }//END IF

            if (numberOfPlayers > 4)
            {
                player5InputLabel.Visible = true;
                player5AbvInputTb.Visible = true;
                player5NameInputTb.Visible = true;
            }//END IF

            if (numberOfPlayers > 5)
            {
                player6InputLabel.Visible = true;
                player6AbvInputTb.Visible = true;
                player6NameInputTb.Visible = true;
            }//END IF
        }

        //---------------------------------------------------------------------

        private String getValidAbvString(String abv, int player)
        {
            abv.Trim();

            if (String.IsNullOrEmpty(abv))
            {
                return player.ToString();
            }
            else
            {
                return abv;
            }//END IF
        }

        private String getValidNameString(String name, int player)
        {
            name.Trim();

            if (String.IsNullOrEmpty(name))
            {
                return "PLAYER " + player.ToString();
            }
            else
            {
                return name;
            }//END IF
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //INITIALIZE-----------------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        private void initialize()
        {
            if (turnDirector.isPlayer(0))
            {
                player1.Text = displayResources.getPlayerName(0);
                playerABV1.Text = displayResources.getPlayerAbv(0);
            }//END IF

            if (turnDirector.isPlayer(1))
            {
                player2.Text = displayResources.getPlayerName(1);
                playerABV2.Text = displayResources.getPlayerAbv(1);
            }//END IF

            if (turnDirector.isPlayer(2))
            {
                player3.Text = displayResources.getPlayerName(2);
                playerABV3.Text = displayResources.getPlayerAbv(2);
            }//END IF

            if (turnDirector.isPlayer(3))
            {
                player4.Text = displayResources.getPlayerName(3);
                playerABV4.Text = displayResources.getPlayerAbv(3);
            }//END IF

            if (turnDirector.isPlayer(4))
            {
                player5.Text = displayResources.getPlayerName(4);
                playerABV5.Text = displayResources.getPlayerAbv(4);
            }//END IF

            if (turnDirector.isPlayer(5))
            {
                player6.Text = displayResources.getPlayerName(5);
                playerABV6.Text = displayResources.getPlayerAbv(5);
            }//END IF

            updatePlayerCashDisplay();

            turnDirector.resetTempActionArrays();

            //SET DISPLAY
            setDisplay();
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //USER INTERCATIONS----------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        //PLAYER ACTIONS-------------------------------------------------------
        //---------------------------------------------------------------------

        //TRADE / BID----------------------------------------------------------

        private void tradeBidPlayer1_Click(object sender, EventArgs e)
        {
            turnDirector.tradePlayer = 0;
            tradeBidPlayer();
        }

        private void tradeBidPlayer2_Click(object sender, EventArgs e)
        {
            turnDirector.tradePlayer = 1;
            tradeBidPlayer();
        }

        private void tradeBidPlayer3_Click(object sender, EventArgs e)
        {
            turnDirector.tradePlayer = 2;
            tradeBidPlayer();
        }

        private void tradeBidPlayer4_Click(object sender, EventArgs e)
        {
            turnDirector.tradePlayer = 3;
            tradeBidPlayer();
        }

        private void tradeBidPlayer5_Click(object sender, EventArgs e)
        {
            turnDirector.tradePlayer = 4;
            tradeBidPlayer();
        }

        private void tradeBidPlayer6_Click(object sender, EventArgs e)
        {
            turnDirector.tradePlayer = 5;
            tradeBidPlayer();
        }

        private void tradeBidPlayer()
        {
            // 10 AUCTION
            //IF CURRENT STATE IS AUCTION
            if (turnDirector.getCurrentState() == 10)
            {
                //VALID BID
                Boolean validBid = false;

                //BID VALUE
                int bid = 0;

                //GET INPUT VALUE
                try
                {
                    //GET BID VALUE
                    bid = Convert.ToInt32(tbAuctionBid.Text);

                    //VALID BID
                    validBid = true;
                }
                catch (Exception)
                {
                    //INVALID BID
                    validBid = false;
                    System.Windows.Forms.MessageBox.Show("INVALID BID");
                }//END TRY CATCH

                if (validBid)
                {
                    //PURCHASE PROPERTY
                    turnDirector.purchaseProperty(
                        turnDirector.tradePlayer,
                        turnDirector.getPlayerLocationIndex(turnDirector.currentPlayer),
                        bid
                        );
                }//END IF

                //RESET TRADE PLAYER
                turnDirector.tradePlayer = -1;
            }
            else 
            {
                // 8 TRADE
                //SET CURRENT STATE TRADE
                turnDirector.setStateTrade();

                //RESET TEMP ACTION ARRAYS
                turnDirector.resetTempActionArrays();
            }//END IF

            //SET DISPLAY
            setDisplay();
        }

        //ROLL-----------------------------------------------------------------

        private void roll_Click(object sender, EventArgs e)
        {
            //ROLL
            turnDirector.roll();

            //SET DISPLAY
            setDisplay();
        }

        //GET OUT OF JAIL------------------------------------------------------

        private void getOutOfJail_Click(object sender, EventArgs e)
        {
            //GET OUT OF JAIL
            turnDirector.getOutOfJail();

            //SET PLAYER DISPLAY
            setPlayerDisplay();
        }

        //MORTGAGE-------------------------------------------------------------

        private void mortgage_Click(object sender, EventArgs e)
        {
            // 5 MORTGAGE
            //SET CURRENT STATE
            turnDirector.setStateMortgage();

            //RESET TEMP ACTION ARRAYS
            turnDirector.resetTempActionArrays();

            //SET DISPLAY
            setDisplay();
        }

        //BUY PROPERTY---------------------------------------------------------

        private void buyProperty_Click(object sender, EventArgs e)
        {
            //PURCHASE PROPERTY
            turnDirector.purchaseProperty();

            //SET DISPLAY
            setDisplay();
        }

        //BUY HOUSES-----------------------------------------------------------

        private void buyHouses_Click(object sender, EventArgs e)
        {
            // 6 BUY HOUSES
            //SET CURRENT STATE
            turnDirector.setStateBuyHouses();

            //RESET TEMP ACTION ARRAYS
            turnDirector.resetTempActionArrays();

            //SET DISPLAY
            setDisplay();
        }

        //SELL HOUSES----------------------------------------------------------

        private void sellHouses_Click(object sender, EventArgs e)
        {
            // 7 SELL HOUSES
            //SET CURRENT STATE
            turnDirector.setStateSellHouses();

            //RESET TEMP ACTION ARRAYS
            turnDirector.resetTempActionArrays();

            //SET DISPLAY
            setDisplay();
        }

        //DONE / MAKE TRADE----------------------------------------------------

        private void doneMakeTrade_Click(object sender, EventArgs e)
        {
            if (turnDirector.getCurrentState() == 5)
            {
                // 5 MORTGAGE
                turnDirector.mortgageUnmortgage();
            }
            else if (turnDirector.getCurrentState() == 6)
            {
                // 6 BUY HOUSES
                turnDirector.buyHouses();
            }
            else if (turnDirector.getCurrentState() == 7)
            {
                // 7 SELL HOUSES
                turnDirector.sellHouses();
            }
            else if (turnDirector.getCurrentState() == 8)
            {
                // 8 TRADE

                //VALID CASH TRADE
                Boolean validCashTrade = false;

                //TRADE CASH VALUES
                int cashToPlayer = 0;
                int cashToTradePlayer = 0;

                //CHECK IF STRING IS EMPTY
                if (String.IsNullOrEmpty(tbCashToPlayer1.Text))
                {
                    //SET CASH TO PLAYER 1
                    cashToPlayer = 0;

                    //VALID CASH TRADE
                    validCashTrade = true;
                }
                else
                {
                    //GET INPUT VALUE
                    try
                    {
                        //SET CASH TO PLAYER 1
                        cashToPlayer = Convert.ToInt32(tbCashToPlayer1.Text);

                        //VALID CASH TRADE
                        validCashTrade = true;
                    }
                    catch (Exception)
                    {
                        //INVALID CASH TRADE
                        validCashTrade = false;
                        System.Windows.Forms.MessageBox.Show("INVALID CASH TO " + displayResources.getPlayerName(turnDirector.currentPlayer));
                    }//END TRY CATCH
                }//END IF

                //CHECK IF STRING IS EMPTY
                if (String.IsNullOrEmpty(tbCashToPlayer2.Text))
                {
                    //SET CASH TO PLAYER 1
                    cashToTradePlayer = 0;

                    //VALID CASH TRADE
                    validCashTrade = true && validCashTrade;
                }
                else
                {
                    //GET INPUT VALUE
                    try
                    {
                        //SET CASH TO PLAYER 1
                        cashToTradePlayer = Convert.ToInt32(tbCashToPlayer2.Text);

                        //VALID CASH TRADE
                        validCashTrade = true && validCashTrade;
                    }
                    catch (Exception)
                    {
                        //INVALID CASH TRADE
                        validCashTrade = false;
                        System.Windows.Forms.MessageBox.Show("INVALID CASH TO " + displayResources.getPlayerName(turnDirector.tradePlayer));
                    }//END TRY CATCH
                }//END IF

                if (validCashTrade)
                {
                    // 8 TRADE
                    turnDirector.makeTrade(cashToPlayer, cashToTradePlayer);
                }//END IF
            }//END IF

            //SET DISPLAY
            setDisplay();
        }

        //NEXT TURN / SURRENDER------------------------------------------------

        private void nextTurnSurrender_Click(object sender, EventArgs e)
        {
            if (turnDirector.getCurrentState() == 9)
            {
                // 9 OWE MONEY
                turnDirector.surrender();
            }
            else
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
                // 10 AUCTION
                turnDirector.nextTurn();
            }//END IF

            //SET DISPLAY
            setDisplay();
        }

        //CANCEL---------------------------------------------------------------

        private void cancel_Click(object sender, EventArgs e)
        {
            //RESET TEMP ACTION ARRAYS
            turnDirector.resetTempActionArrays();

            //SET CURRENT STATE
            turnDirector.setState();

            //SET DISPLAY
            setDisplay();
        }

        //---------------------------------------------------------------------
        //PROPERTIES-----------------------------------------------------------
        //---------------------------------------------------------------------

        private void square1_Click(object sender, EventArgs e)
        {
            // 0 Mediterranean Avenue 
            squareClick(0);
        }

        private void square3_Click(object sender, EventArgs e)
        {
            // 1 Baltic Avenue 
            squareClick(1);
        }

        private void square5_Click(object sender, EventArgs e)
        {
            // 2 Reading Railroad 
            squareClick(2);
        }

        private void square6_Click(object sender, EventArgs e)
        {
            // 3 Oriental Avenue 
            squareClick(3);
        }

        private void square8_Click(object sender, EventArgs e)
        {
            // 4 Vermont Avenue 
            squareClick(4);
        }

        private void square9_Click(object sender, EventArgs e)
        {
            // 5 Connecticut Avenue 
            squareClick(5);
        }

        private void square11_Click(object sender, EventArgs e)
        {
            // 6 St. Charles Place 
            squareClick(6);
        }

        private void square12_Click(object sender, EventArgs e)
        {
            // 7 Electric Company 
            squareClick(7);
        }

        private void square13_Click(object sender, EventArgs e)
        {
            // 8 States Avenue 
            squareClick(8);
        }

        private void square14_Click(object sender, EventArgs e)
        {
            // 9 Virginia Avenue
            squareClick(9);
        }

        private void square15_Click(object sender, EventArgs e)
        {
            // 10 Pennsylvania Railroad 
            squareClick(10);
        }

        private void square16_Click(object sender, EventArgs e)
        {
            // 11 St. James Place 
            squareClick(11);
        }

        private void square18_Click(object sender, EventArgs e)
        {
            // 12 Tennessee Avenue 
            squareClick(12);
        }

        private void square19_Click(object sender, EventArgs e)
        {
            // 13 New York Avenue 
            squareClick(13);
        }

        private void square21_Click(object sender, EventArgs e)
        {
            // 14 Kentucky Avenue 
            squareClick(14);
        }

        private void square23_Click(object sender, EventArgs e)
        {
            // 15 Indiana Avenue 
            squareClick(15);
        }

        private void square24_Click(object sender, EventArgs e)
        {
            // 16 Illinois Avenue 
            squareClick(16);
        }

        private void square25_Click(object sender, EventArgs e)
        {
            // 17 B&O Railroad 
            squareClick(17);
        }

        private void square26_Click(object sender, EventArgs e)
        {
            // 18 Atlantic Avenue 
            squareClick(18);
        }

        private void square27_Click(object sender, EventArgs e)
        {
            // 19 Ventnor Avenue
            squareClick(19);
        }

        private void square28_Click(object sender, EventArgs e)
        {
            // 20 Water Works 
            squareClick(20);
        }

        private void square29_Click(object sender, EventArgs e)
        {
            // 21 Marvin Gardens 
            squareClick(21);
        }

        private void square31_Click(object sender, EventArgs e)
        {
            // 22 Pacific Avenue 
            squareClick(22);
        }

        private void square32_Click(object sender, EventArgs e)
        {
            // 23 North Carolina Avenue 
            squareClick(23);
        }

        private void square34_Click(object sender, EventArgs e)
        {
            // 24 Pennsylvania Avenue
            squareClick(24);
        }

        private void square35_Click(object sender, EventArgs e)
        {
            // 25 Short Line 
            squareClick(25);
        }

        private void square37_Click(object sender, EventArgs e)
        {
            // 26 Park Place 
            squareClick(26);
        }

        private void square39_Click(object sender, EventArgs e)
        {
            // 27 Boardwalk
            squareClick(27);
        }

        private void squareClick(int squareIndex28)
        {
            turnDirector.squareClick(squareIndex28);

            setDisplay();
        }

        //---------------------------------------------------------------------
        //MENU STRIP-----------------------------------------------------------
        //---------------------------------------------------------------------

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //EXIT APP
            Application.Exit();
        }

        //---------------------------------------------------------------------
        //ON CLOSE-------------------------------------------------------------
        //---------------------------------------------------------------------

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //EXIT APP
            Application.Exit();
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        //DISPLAY--------------------------------------------------------------
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        private void setDisplay()
        {
            setPlayerDisplay();

            setBoardDisplay();
        }

        private void setPlayerDisplay()
        {
            //UPDATE PLAYER CASH
            updatePlayerCashDisplay();

            //RESET PLAYER DISPLAY
            resetPlayerDisplay();

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

            //FIND CURRENT STATE
            if (turnDirector.getCurrentState() == 0)
            {
                // 0 PRE ROLL
                setPreRollDisplay();
                showTradeButtons();
            }
            else if (turnDirector.getCurrentState() == 1)
            {
                // 1 DOUBLES
                setDoublesRollDisplay();
                showTradeButtons();
            }
            else if (turnDirector.getCurrentState() == 2)
            {
                // 2 POST ROLL
                setPostRollDisplay();
                showTradeButtons();
            }
            else if (turnDirector.getCurrentState() == 3)
            {
                // 3 PRE ROLL IN JAIL
                setPreRollInJailDisplay();
                showTradeButtons();
            }
            else if (turnDirector.getCurrentState() == 4)
            {
                // 4 POST ROLL IN JAIL
                setPostRollInJailDisplay();
                showTradeButtons();
            }
            else if (turnDirector.getCurrentState() == 5)
            {
                // 5 MORTGAGE
                setMortgageDisplay();
                showTradeButtons();
            }
            else if (turnDirector.getCurrentState() == 6)
            {
                // 6 BUY HOUSES
                setBuyHousesDisplay();
                showTradeButtons();
            }
            else if (turnDirector.getCurrentState() == 7)
            {
                // 7 SELL HOUSES
                setSellHousesDisplay();
                showTradeButtons();
            }
            else if (turnDirector.getCurrentState() == 8)
            {
                // 8 TRADE
                setTradeDisplay();
                hideTradeButtons();
            }
            else if (turnDirector.getCurrentState() == 9)
            {
                // 9 OWE MONEY
                setOweMoneyDisplay();
                showTradeButtons();
            }
            else if (turnDirector.getCurrentState() == 10)
            {
                // 10 AUCTION
                setAuctionDisplay();
                showBidButtons();
            }//END IF
        }

        private void setBoardDisplay()
        {
            setBoardOwnersDisplay();

            setBoardHousesDisplay();

            setBoardSquareDisplay();

            freeParking.Text = "$" + turnDirector.getCashInFreeParking();
        }

        //---------------------------------------------------------------------
        //PLAYER DISPLAY-------------------------------------------------------
        //---------------------------------------------------------------------

        private void resetPlayerDisplay()
        {
            btnRoll.Visible = false;
            btnGetOutOfJail.Visible = false;
            btnMortgage.Visible = false;
            btnBuyProperty.Visible = false;
            btnBuyHouses.Visible = false;
            btnSellHouses.Visible = false;
            btnDoneMakeTrade.Visible = false;
            btnNextTurnSurrender.Visible = false;
            btnCancel.Visible = false;

            currentAction.Visible = false;
            playerTrade1.Visible = false;
            playerTrade2.Visible = false;
            propertyName.Visible = false;
            tradeCash1.Visible = false;
            tradeCash2.Visible = false;
            playerBid.Visible = false;

            tbCashToPlayer2.Visible = false;
            tbAuctionBid.Visible = false;
            tbCashToPlayer1.Visible = false;
        }

        //PLAYER CASH DISPLAY--------------------------------------------------

        private void updatePlayerCashDisplay()
        {
            if (turnDirector.isPlayer(0))
            {
                cash1.Text = "$" + turnDirector.getPlayerCash(0);
            }//END IF

            if (turnDirector.isPlayer(1))
            {
                cash2.Text = "$" + turnDirector.getPlayerCash(1);
            }//END IF

            if (turnDirector.isPlayer(2))
            {
                cash3.Text = "$" + turnDirector.getPlayerCash(2);
            }//END IF

            if (turnDirector.isPlayer(3))
            {
                cash4.Text = "$" + turnDirector.getPlayerCash(3);
            }//END IF

            if (turnDirector.isPlayer(4))
            {
                cash5.Text = "$" + turnDirector.getPlayerCash(4);
            }//END IF

            if (turnDirector.isPlayer(5))
            {
                cash6.Text = "$" + turnDirector.getPlayerCash(5);
            }//END IF
        }

        //AUCTION BUTTONS------------------------------------------------------

        private void showBidButtons()
        {
            btnTradeBidPlayer1.Text = "BID";
            btnTradeBidPlayer2.Text = "BID";
            btnTradeBidPlayer3.Text = "BID";
            btnTradeBidPlayer4.Text = "BID";
            btnTradeBidPlayer5.Text = "BID";
            btnTradeBidPlayer6.Text = "BID";

            btnTradeBidPlayer1.Visible = turnDirector.isPlayer(0);
            btnTradeBidPlayer2.Visible = turnDirector.isPlayer(1);
            btnTradeBidPlayer3.Visible = turnDirector.isPlayer(2);
            btnTradeBidPlayer4.Visible = turnDirector.isPlayer(3);
            btnTradeBidPlayer5.Visible = turnDirector.isPlayer(4);
            btnTradeBidPlayer6.Visible = turnDirector.isPlayer(5);
        }

        //TRADE BUTTONS--------------------------------------------------------

        private void hideTradeButtons()
        {
            btnTradeBidPlayer1.Visible = false;
            btnTradeBidPlayer2.Visible = false;
            btnTradeBidPlayer3.Visible = false;
            btnTradeBidPlayer4.Visible = false;
            btnTradeBidPlayer5.Visible = false;
            btnTradeBidPlayer6.Visible = false;
        }

        private void showTradeButtons()
        {
            btnTradeBidPlayer1.Text = "TRADE";
            btnTradeBidPlayer2.Text = "TRADE";
            btnTradeBidPlayer3.Text = "TRADE";
            btnTradeBidPlayer4.Text = "TRADE";
            btnTradeBidPlayer5.Text = "TRADE";
            btnTradeBidPlayer6.Text = "TRADE";

            btnTradeBidPlayer1.Visible = turnDirector.isPlayer(0);
            btnTradeBidPlayer2.Visible = turnDirector.isPlayer(1);
            btnTradeBidPlayer3.Visible = turnDirector.isPlayer(2);
            btnTradeBidPlayer4.Visible = turnDirector.isPlayer(3);
            btnTradeBidPlayer5.Visible = turnDirector.isPlayer(4);
            btnTradeBidPlayer6.Visible = turnDirector.isPlayer(5);

            if (turnDirector.currentPlayer == 0)
            {
                btnTradeBidPlayer1.Visible = false;
            }
            else if (turnDirector.currentPlayer == 1)
            {
                btnTradeBidPlayer2.Visible = false;
            }
            else if (turnDirector.currentPlayer == 2)
            {
                btnTradeBidPlayer3.Visible = false;
            }
            else if (turnDirector.currentPlayer == 3)
            {
                btnTradeBidPlayer4.Visible = false;
            }
            else if (turnDirector.currentPlayer == 4)
            {
                btnTradeBidPlayer5.Visible = false;
            }
            else if (turnDirector.currentPlayer == 5)
            {
                btnTradeBidPlayer6.Visible = false;
            }//END IF
        }

        // 0 PRE ROLL----------------------------------------------------------
        // 1 DOUBLES-----------------------------------------------------------
        // 2 POST ROLL---------------------------------------------------------
        // 3 PRE ROLL IN JAIL--------------------------------------------------
        // 4 POST ROLL IN JAIL-------------------------------------------------
        // 5 MORTGAGE----------------------------------------------------------
        // 6 BUY HOUSES--------------------------------------------------------
        // 7 SELL HOUSES-------------------------------------------------------
        // 8 TRADE-------------------------------------------------------------
        // 9 OWE MONEY---------------------------------------------------------
        // 10 AUCTION----------------------------------------------------------

        // 0 PRE ROLL----------------------------------------------------------

        private void setPreRollDisplay()
        {
            btnRoll.Visible = true;

            btnMortgage.Visible = turnDirector.canShowMortgageButton();
            btnBuyHouses.Visible = turnDirector.canShowBuyHousesButton();
            btnSellHouses.Visible = turnDirector.canShowSellHousesButton();

            currentAction.Text = displayResources.getPlayerName(turnDirector.currentPlayer) + "'S TURN";
            currentAction.Visible = true;
        }

        // 1 DOUBLES-----------------------------------------------------------

        private void setDoublesRollDisplay()
        {
            btnRoll.Visible = true;

            btnMortgage.Visible = turnDirector.canShowMortgageButton();
            btnBuyProperty.Visible = turnDirector.propertyForSale();
            btnBuyHouses.Visible = turnDirector.canShowBuyHousesButton();
            btnSellHouses.Visible = turnDirector.canShowSellHousesButton();

            currentAction.Text = displayResources.getPlayerName(turnDirector.currentPlayer) + "'S TURN";
            currentAction.Visible = true;
        }

        // 2 POST ROLL---------------------------------------------------------

        private void setPostRollDisplay()
        {
            btnMortgage.Visible = turnDirector.canShowMortgageButton();
            btnBuyProperty.Visible = turnDirector.propertyForSale();
            btnBuyHouses.Visible = turnDirector.canShowBuyHousesButton();
            btnSellHouses.Visible = turnDirector.canShowSellHousesButton();

            btnNextTurnSurrender.Text = "NEXT TURN";
            btnNextTurnSurrender.Visible = true;

            currentAction.Text = displayResources.getPlayerName(turnDirector.currentPlayer) + "'S TURN";
            currentAction.Visible = true;
        }

        // 3 PRE ROLL IN JAIL--------------------------------------------------

        private void setPreRollInJailDisplay()
        {
            btnRoll.Visible = true;

            btnGetOutOfJail.Visible = true;

            btnMortgage.Visible = turnDirector.canShowMortgageButton();
            btnBuyHouses.Visible = turnDirector.canShowBuyHousesButton();
            btnSellHouses.Visible = turnDirector.canShowSellHousesButton();

            currentAction.Text = displayResources.getPlayerName(turnDirector.currentPlayer) + "'S TURN";
            currentAction.Visible = true;
        }

        // 4 POST ROLL IN JAIL-------------------------------------------------

        private void setPostRollInJailDisplay()
        {
            btnMortgage.Visible = turnDirector.canShowMortgageButton();
            btnBuyHouses.Visible = turnDirector.canShowBuyHousesButton();
            btnSellHouses.Visible = turnDirector.canShowSellHousesButton();

            btnNextTurnSurrender.Text = "NEXT TURN";
            btnNextTurnSurrender.Visible = true;

            currentAction.Text = displayResources.getPlayerName(turnDirector.currentPlayer) + "'S TURN";
            currentAction.Visible = true;
        }

        // 5 MORTGAGE----------------------------------------------------------
        
        private void setMortgageDisplay()
        {
            btnDoneMakeTrade.Text = "DONE";
            btnDoneMakeTrade.Visible = true;

            btnCancel.Visible = true;

            currentAction.Text = "MORTGAGE / UNMORTGAGE";
            currentAction.Visible = true;
        }

        // 6 BUY HOUSES--------------------------------------------------------

        private void setBuyHousesDisplay()
        {
            btnDoneMakeTrade.Text = "DONE";
            btnDoneMakeTrade.Visible = true;

            btnCancel.Visible = true;

            currentAction.Text = "BUY HOUSES";
            currentAction.Visible = true;
        }

        // 7 SELL HOUSES-------------------------------------------------------
        
        private void setSellHousesDisplay()
        {
            btnDoneMakeTrade.Text = "DONE";
            btnDoneMakeTrade.Visible = true;

            btnCancel.Visible = true;

            currentAction.Text = "SELL HOUSES";
            currentAction.Visible = true;
        }

        // 8 TRADE-------------------------------------------------------------
        
        private void setTradeDisplay()
        {
            btnDoneMakeTrade.Text = "MAKE TRADE";
            btnDoneMakeTrade.Visible = true;

            btnCancel.Visible = true;

            currentAction.Text = "TRADE";
            currentAction.Visible = true;

            playerTrade1.Text = displayResources.getPlayerName(turnDirector.currentPlayer);
            playerTrade1.Visible = true;

            playerTrade2.Text = displayResources.getPlayerName(turnDirector.tradePlayer);
            playerTrade2.Visible = true;

            tradeCash1.Visible = true;
            tradeCash2.Visible = true;

            tbCashToPlayer2.Visible = true;
            tbCashToPlayer1.Visible = true;
        }

        // 9 OWE MONEY---------------------------------------------------------
        
        private void setOweMoneyDisplay()
        {
            btnMortgage.Visible = turnDirector.canShowMortgageButton();
            btnSellHouses.Visible = turnDirector.canShowSellHousesButton();

            btnNextTurnSurrender.Text = "SURRENDER";
            btnNextTurnSurrender.Visible = true;

            currentAction.Text = displayResources.getPlayerName(turnDirector.currentPlayer) + "'S TURN";
            currentAction.Visible = true;
        }

        // 10 AUCTION----------------------------------------------------------

        private void setAuctionDisplay()
        {
            int squareIndex40 = turnDirector.getPlayerLocationIndex(turnDirector.currentPlayer);
            int squareIndex28 = indexConversions.get28IndexFrom40Index(squareIndex40);

            btnCancel.Visible = true;

            currentAction.Text = "AUCTION";
            currentAction.Visible = true;

            propertyName.Text = displayResources.getPropertyName(squareIndex28);
            propertyName.Visible = true;

            playerBid.Visible = true;

            tbAuctionBid.Visible = true;
        }

        //---------------------------------------------------------------------
        //BOARD DISPLAY--------------------------------------------------------
        //---------------------------------------------------------------------

        //SQUARE DISPLAY
        //---------------------------------------------------------------------

        private void setBoardSquareDisplay()
        {
            square0.Text = getSquareDisplay(0);
            square0.Visible = getLabelVisible(square0.Text);

            // 0 Mediterranean Avenue
            square1.Text = getSquareDisplay(1);
            square1.Visible = getLabelVisible(square1.Text);

            square2.Text = getSquareDisplay(2);
            square2.Visible = getLabelVisible(square2.Text);

            // 1 Baltic Avenue
            square3.Text = getSquareDisplay(3);
            square3.Visible = getLabelVisible(square3.Text);

            square4.Text = getSquareDisplay(4);
            square4.Visible = getLabelVisible(square4.Text);

            // 2 Reading Railroad 
            square5.Text = getSquareDisplay(5);
            square5.Visible = getLabelVisible(square5.Text);

            // 3 Oriental Avenue 
            square6.Text = getSquareDisplay(6);
            square6.Visible = getLabelVisible(square6.Text);

            square7.Text = getSquareDisplay(7);
            square7.Visible = getLabelVisible(square7.Text);

            // 4 Vermont Avenue 
            square8.Text = getSquareDisplay(8);
            square8.Visible = getLabelVisible(square8.Text);

            // 5 Connecticut Avenue 
            square9.Text = getSquareDisplay(9);
            square9.Visible = getLabelVisible(square9.Text);

            square10.Text = getSquareDisplay(10);
            square10.Visible = getLabelVisible(square10.Text);

            // 6 St. Charles Place 
            square11.Text = getSquareDisplay(11);
            square11.Visible = getLabelVisible(square11.Text);

            // 7 Electric Company 
            square12.Text = getSquareDisplay(12);
            square12.Visible = getLabelVisible(square12.Text);

            // 8 States Avenue 
            square13.Text = getSquareDisplay(13);
            square13.Visible = getLabelVisible(square13.Text);

            // 9 Virginia Avenue
            square14.Text = getSquareDisplay(14);
            square14.Visible = getLabelVisible(square14.Text);

            // 10 Pennsylvania Railroad 
            square15.Text = getSquareDisplay(15);
            square15.Visible = getLabelVisible(square15.Text);

            // 11 St. James Place 
            square16.Text = getSquareDisplay(16);
            square16.Visible = getLabelVisible(square16.Text);

            square17.Text = getSquareDisplay(17);
            square17.Visible = getLabelVisible(square17.Text);

            // 12 Tennessee Avenue 
            square18.Text = getSquareDisplay(18);
            square18.Visible = getLabelVisible(square18.Text);

            // 13 New York Avenue 
            square19.Text = getSquareDisplay(19);
            square19.Visible = getLabelVisible(square19.Text);

            square20.Text = getSquareDisplay(20);
            square20.Visible = getLabelVisible(square20.Text);

            // 14 Kentucky Avenue 
            square21.Text = getSquareDisplay(21);
            square21.Visible = getLabelVisible(square21.Text);

            square22.Text = getSquareDisplay(22);
            square22.Visible = getLabelVisible(square22.Text);

            // 15 Indiana Avenue 
            square23.Text = getSquareDisplay(23);
            square23.Visible = getLabelVisible(square23.Text);

            // 16 Illinois Avenue 
            square24.Text = getSquareDisplay(24);
            square24.Visible = getLabelVisible(square24.Text);

            // 17 B&O Railroad 
            square25.Text = getSquareDisplay(25);
            square25.Visible = getLabelVisible(square25.Text);

            // 18 Atlantic Avenue 
            square26.Text = getSquareDisplay(26);
            square26.Visible = getLabelVisible(square26.Text);

            // 19 Ventnor Avenue
            square27.Text = getSquareDisplay(27);
            square27.Visible = getLabelVisible(square27.Text);

            // 20 Water Works 
            square28.Text = getSquareDisplay(28);
            square28.Visible = getLabelVisible(square28.Text);

            // 21 Marvin Gardens 
            square29.Text = getSquareDisplay(29);
            square29.Visible = getLabelVisible(square29.Text);

            square30.Text = getSquareDisplay(30);
            square30.Visible = getLabelVisible(square30.Text);

            // 22 Pacific Avenue 
            square31.Text = getSquareDisplay(31);
            square31.Visible = getLabelVisible(square31.Text);

            // 23 North Carolina Avenue 
            square32.Text = getSquareDisplay(32);
            square32.Visible = getLabelVisible(square32.Text);

            square33.Text = getSquareDisplay(33);
            square33.Visible = getLabelVisible(square33.Text);

            // 24 Pennsylvania Avenue
            square34.Text = getSquareDisplay(34);
            square34.Visible = getLabelVisible(square34.Text);

            // 25 Short Line 
            square35.Text = getSquareDisplay(35);
            square35.Visible = getLabelVisible(square35.Text);

            square36.Text = getSquareDisplay(36);
            square36.Visible = getLabelVisible(square36.Text);

            // 26 Park Place 
            square37.Text = getSquareDisplay(37);
            square37.Visible = getLabelVisible(square37.Text);

            square38.Text = getSquareDisplay(38);
            square38.Visible = getLabelVisible(square38.Text);

            // 27 Boardwalk
            square39.Text = getSquareDisplay(39);
            square39.Visible = getLabelVisible(square39.Text);
        }

        private string getSquareDisplay(int squareIndex40)
        {
            if (turnDirector.getCurrentState() == 5)
            {
                // 5 MORTGAGE
                return getSquareMortgageDisplay(squareIndex40);
            }
            else if (turnDirector.getCurrentState() == 6)
            {
                // 6 BUY HOUSES
                return getSquareBuyHousesDisplay(squareIndex40);
            }
            else if (turnDirector.getCurrentState() == 7)
            {
                // 7 SELL HOUSES
                return getSquareSellHousesDisplay(squareIndex40);
            }
            else if (turnDirector.getCurrentState() == 8)
            {
                // 8 TRADE
                return getSquareTradeDisplay(squareIndex40);
            }
            else
            {
                // 0 PRE ROLL
                // 1 DOUBLES
                // 2 POST ROLL
                // 3 PRE ROLL IN JAIL
                // 4 POST ROLL IN JAIL
                // 9 OWE MONEY
                // 10 AUCTION
                return getSquarePlayersDisplay(squareIndex40);
            }//END IF
        }

        private string getSquarePlayersDisplay(int squareIndex40)
        {
            int numberOfPlayers = turnDirector.getNumberOfPlayersOnSquare(squareIndex40);
            int[] playerIndexes = new int[numberOfPlayers];
            string stringPlayers = "";

            Array.Copy(turnDirector.getPlayersOnSquare(squareIndex40),
                playerIndexes,
                numberOfPlayers);

            for (int index = 0; index < numberOfPlayers; index++)
            {
                if ((index + 1) < numberOfPlayers)
                {
                    stringPlayers = stringPlayers + displayResources.getPlayerName(playerIndexes[index]) + "\r\n";
                }
                else
                {
                    stringPlayers = stringPlayers + displayResources.getPlayerName(playerIndexes[index]);
                }//END IF
            }//END FOR

            return stringPlayers;
        }

        private string getSquareMortgageDisplay(int squareIndex40)
        {
            //SQUARE STRING
            string stringMortgage = "";

            if (turnDirector.canPlayerMortgageProperty(squareIndex40))
            {
                stringMortgage = "MORTGAGE";
            }
            else if (turnDirector.canPlayerUnmortgageProperty(squareIndex40))
            {
                stringMortgage = "UNMORTGAGE";
            }
            else
            {
                stringMortgage = "";
            }//END IF

            return stringMortgage;
        }

        private string getSquareBuyHousesDisplay(int squareIndex40)
        {
            //SQUARE STRING
            string stringBuyHouses = "";

            if (turnDirector.canBuyHousesOnSquare(squareIndex40))
            {
                stringBuyHouses = "BUY HOUSE";
            }
            else
            {
                stringBuyHouses = "";
            }//END IF

            return stringBuyHouses;
        }

        private string getSquareSellHousesDisplay(int squareIndex40)
        {
            //SQUARE STRING
            string stringSellHouses = "";

            if (turnDirector.canSellHousesOnSquare(squareIndex40))
            {
                stringSellHouses = "SELL HOUSE";
            }
            else
            {
                stringSellHouses = "";
            }//END IF

            return stringSellHouses;
        }

        private string getSquareTradeDisplay(int squareIndex40)
        {
            //IN TRADE
            Boolean inTrade = turnDirector.propertyInTrade(squareIndex40);

            int squareIndex28 = indexConversions.get28IndexFrom40Index(squareIndex40);

            //OWNED BY TRADERS
            Boolean ownedByTraders = turnDirector.getOwnerOfProperty(squareIndex28) == turnDirector.currentPlayer || 
                turnDirector.getOwnerOfProperty(squareIndex28) == turnDirector.tradePlayer;

            //TRADE STRING
            string stringTrade = "";

            if (!inTrade && ownedByTraders)
            {
                stringTrade = "ADD TO TRADE";
            }
            else if(inTrade && ownedByTraders)
            {
                stringTrade = "REMOVE FROM TRADE";
            }
            else
            {
                stringTrade = "";
            }//END IF

            return stringTrade;
        }

        //HOUSE DISPLAY
        //---------------------------------------------------------------------

        private void setBoardHousesDisplay()
        {
            // 0 Mediterranean Avenue
            house1H.Text = getHorizontalHousesDisplay(0);
            house1H.Visible = getLabelVisible(house1H.Text);

            // 1 Baltic Avenue
            house3H.Text = getHorizontalHousesDisplay(1);
            house3H.Visible = getLabelVisible(house3H.Text);

            // 3 Oriental Avenue 
            house6H.Text = getHorizontalHousesDisplay(3);
            house6H.Visible = getLabelVisible(house6H.Text);

            // 4 Vermont Avenue 
            house8H.Text = getHorizontalHousesDisplay(4);
            house8H.Visible = getLabelVisible(house8H.Text);

            // 5 Connecticut Avenue 
            house9H.Text = getHorizontalHousesDisplay(5);
            house9H.Visible = getLabelVisible(house9H.Text);

            // 6 St. Charles Place 
            house11V.Text = getVerticalHousesDisplay(6);
            house11V.Visible = getLabelVisible(house11V.Text);

            // 8 States Avenue 
            house13V.Text = getVerticalHousesDisplay(8);
            house13V.Visible = getLabelVisible(house13V.Text);

            // 9 Virginia Avenue
            house14V.Text = getVerticalHousesDisplay(9);
            house14V.Visible = getLabelVisible(house14V.Text);

            // 11 St. James Place 
            house16V.Text = getVerticalHousesDisplay(11);
            house16V.Visible = getLabelVisible(house16V.Text);

            // 12 Tennessee Avenue 
            house18V.Text = getVerticalHousesDisplay(12);
            house18V.Visible = getLabelVisible(house18V.Text);

            // 13 New York Avenue 
            house19V.Text = getVerticalHousesDisplay(13);
            house19V.Visible = getLabelVisible(house19V.Text);

            // 14 Kentucky Avenue 
            house21H.Text = getHorizontalHousesDisplay(14);
            house21H.Visible = getLabelVisible(house21H.Text);

            // 15 Indiana Avenue 
            house23H.Text = getHorizontalHousesDisplay(15);
            house23H.Visible = getLabelVisible(house23H.Text);

            // 16 Illinois Avenue 
            house24H.Text = getHorizontalHousesDisplay(16);
            house24H.Visible = getLabelVisible(house24H.Text);

            // 18 Atlantic Avenue 
            house26H.Text = getHorizontalHousesDisplay(18);
            house26H.Visible = getLabelVisible(house26H.Text);

            // 19 Ventnor Avenue
            house27H.Text = getHorizontalHousesDisplay(19);
            house27H.Visible = getLabelVisible(house27H.Text);

            // 21 Marvin Gardens 
            house29H.Text = getHorizontalHousesDisplay(21);
            house29H.Visible = getLabelVisible(house29H.Text);

            // 22 Pacific Avenue 
            house31V.Text = getVerticalHousesDisplay(22);
            house31V.Visible = getLabelVisible(house31V.Text);

            // 23 North Carolina Avenue 
            house32V.Text = getVerticalHousesDisplay(23);
            house32V.Visible = getLabelVisible(house32V.Text);

            // 24 Pennsylvania Avenue
            house34V.Text = getVerticalHousesDisplay(24);
            house34V.Visible = getLabelVisible(house34V.Text);

            // 26 Park Place 
            house37V.Text = getVerticalHousesDisplay(26);
            house37V.Visible = getLabelVisible(house37V.Text);

            // 27 Boardwalk
            house39V.Text = getVerticalHousesDisplay(27);
            house39V.Visible = getLabelVisible(house39V.Text);
        }

        private string getHorizontalHousesDisplay(int squareIndex28)
        {
            //GET NUMBER OF HOUSES
            int numberOfHouses = turnDirector.getNumberOfHouses(squareIndex28);

            //HOUSES STRING
            string stringHouses = "";

            for (int index = 0; index < numberOfHouses; index++)
            {
                stringHouses = stringHouses + "H";
            }//END FOR

            return stringHouses;
        }

        private string getVerticalHousesDisplay(int squareIndex28)
        {
            //GET NUMBER OF HOUSES
            int numberOfHouses = turnDirector.getNumberOfHouses(squareIndex28);

            //HOUSES STRING
            string stringHouses = "";

            for (int index = 0; index < numberOfHouses; index++)
            {
                if ((index + 1) < numberOfHouses)
                {
                    stringHouses = stringHouses + "H\r\n";
                }
                else
                {
                    stringHouses = stringHouses + "H";
                }//END IF
            }//END FOR

            return stringHouses;
        }

        //OWNER DISPLAY
        //---------------------------------------------------------------------

        private void setBoardOwnersDisplay()
        {
            // 0 Mediterranean Avenue
            owner1H.Text = getHorizontalOwnerDisplay(0);
            owner1H.Visible = getLabelVisible(owner1H.Text);
            owner1H.BackColor = Color.FromName(getOwnerColorName(1));

            // 1 Baltic Avenue
            owner3H.Text = getHorizontalOwnerDisplay(1);
            owner3H.Visible = getLabelVisible(owner3H.Text);
            owner3H.BackColor = Color.FromName(getOwnerColorName(3));

            // 2 Reading Railroad 
            owner5H.Text = getHorizontalOwnerDisplay(2);
            owner5H.Visible = getLabelVisible(owner5H.Text);
            owner5H.BackColor = Color.FromName(getOwnerColorName(5));

            // 3 Oriental Avenue 
            owner6H.Text = getHorizontalOwnerDisplay(3);
            owner6H.Visible = getLabelVisible(owner6H.Text);
            owner6H.BackColor = Color.FromName(getOwnerColorName(6));

            // 4 Vermont Avenue 
            owner8H.Text = getHorizontalOwnerDisplay(4);
            owner8H.Visible = getLabelVisible(owner8H.Text);
            owner8H.BackColor = Color.FromName(getOwnerColorName(8));

            // 5 Connecticut Avenue 
            owner9H.Text = getHorizontalOwnerDisplay(5);
            owner9H.Visible = getLabelVisible(owner9H.Text);
            owner9H.BackColor = Color.FromName(getOwnerColorName(9));

            // 6 St. Charles Place 
            owner11V.Text = getVerticalOwnerDisplay(6);
            owner11V.Visible = getLabelVisible(owner11V.Text);
            owner11V.BackColor = Color.FromName(getOwnerColorName(11));

            // 7 Electric Company 
            owner12V.Text = getVerticalOwnerDisplay(7);
            owner12V.Visible = getLabelVisible(owner12V.Text);
            owner12V.BackColor = Color.FromName(getOwnerColorName(12));

            // 8 States Avenue 
            owner13V.Text = getVerticalOwnerDisplay(8);
            owner13V.Visible = getLabelVisible(owner13V.Text);
            owner13V.BackColor = Color.FromName(getOwnerColorName(13));

            // 9 Virginia Avenue
            owner14V.Text = getVerticalOwnerDisplay(9);
            owner14V.Visible = getLabelVisible(owner14V.Text);
            owner14V.BackColor = Color.FromName(getOwnerColorName(14));

            // 10 Pennsylvania Railroad 
            owner15V.Text = getVerticalOwnerDisplay(10);
            owner15V.Visible = getLabelVisible(owner15V.Text);
            owner15V.BackColor = Color.FromName(getOwnerColorName(15));

            // 11 St. James Place 
            owner16V.Text = getVerticalOwnerDisplay(11);
            owner16V.Visible = getLabelVisible(owner16V.Text);
            owner16V.BackColor = Color.FromName(getOwnerColorName(16));

            // 12 Tennessee Avenue 
            owner18V.Text = getVerticalOwnerDisplay(12);
            owner18V.Visible = getLabelVisible(owner18V.Text);
            owner18V.BackColor = Color.FromName(getOwnerColorName(18));

            // 13 New York Avenue 
            owner19V.Text = getVerticalOwnerDisplay(13);
            owner19V.Visible = getLabelVisible(owner19V.Text);
            owner19V.BackColor = Color.FromName(getOwnerColorName(19));

            // 14 Kentucky Avenue 
            owner21H.Text = getHorizontalOwnerDisplay(14);
            owner21H.Visible = getLabelVisible(owner21H.Text);
            owner21H.BackColor = Color.FromName(getOwnerColorName(21));

            // 15 Indiana Avenue 
            owner23H.Text = getHorizontalOwnerDisplay(15);
            owner23H.Visible = getLabelVisible(owner23H.Text);
            owner23H.BackColor = Color.FromName(getOwnerColorName(23));

            // 16 Illinois Avenue 
            owner24H.Text = getHorizontalOwnerDisplay(16);
            owner24H.Visible = getLabelVisible(owner24H.Text);
            owner24H.BackColor = Color.FromName(getOwnerColorName(24));

            // 17 B&O Railroad 
            owner25H.Text = getHorizontalOwnerDisplay(17);
            owner25H.Visible = getLabelVisible(owner25H.Text);
            owner25H.BackColor = Color.FromName(getOwnerColorName(25));

            // 18 Atlantic Avenue 
            owner26H.Text = getHorizontalOwnerDisplay(18);
            owner26H.Visible = getLabelVisible(owner26H.Text);
            owner26H.BackColor = Color.FromName(getOwnerColorName(26));

            // 19 Ventnor Avenue
            owner27H.Text = getHorizontalOwnerDisplay(19);
            owner27H.Visible = getLabelVisible(owner27H.Text);
            owner27H.BackColor = Color.FromName(getOwnerColorName(27));

            // 20 Water Works 
            owner28H.Text = getHorizontalOwnerDisplay(20);
            owner28H.Visible = getLabelVisible(owner28H.Text);
            owner28H.BackColor = Color.FromName(getOwnerColorName(28));

            // 21 Marvin Gardens 
            owner29H.Text = getHorizontalOwnerDisplay(21);
            owner29H.Visible = getLabelVisible(owner29H.Text);
            owner29H.BackColor = Color.FromName(getOwnerColorName(29));

            // 22 Pacific Avenue 
            owner31V.Text = getVerticalOwnerDisplay(22);
            owner31V.Visible = getLabelVisible(owner31V.Text);
            owner31V.BackColor = Color.FromName(getOwnerColorName(31));

            // 23 North Carolina Avenue 
            owner32V.Text = getVerticalOwnerDisplay(23);
            owner32V.Visible = getLabelVisible(owner32V.Text);
            owner32V.BackColor = Color.FromName(getOwnerColorName(32));

            // 24 Pennsylvania Avenue
            owner34V.Text = getVerticalOwnerDisplay(24);
            owner34V.Visible = getLabelVisible(owner34V.Text);
            owner34V.BackColor = Color.FromName(getOwnerColorName(34));

            // 25 Short Line 
            owner35V.Text = getVerticalOwnerDisplay(25);
            owner35V.Visible = getLabelVisible(owner35V.Text);
            owner35V.BackColor = Color.FromName(getOwnerColorName(35));

            // 26 Park Place 
            owner37V.Text = getVerticalOwnerDisplay(26);
            owner37V.Visible = getLabelVisible(owner37V.Text);
            owner37V.BackColor = Color.FromName(getOwnerColorName(37));

            // 27 Boardwalk
            owner39V.Text = getVerticalOwnerDisplay(27);
            owner39V.Visible = getLabelVisible(owner39V.Text);
            owner39V.BackColor = Color.FromName(getOwnerColorName(39));
        }

        private string getHorizontalOwnerDisplay(int squareIndex28)
        {
            int propertyOwner = turnDirector.getOwnerOfProperty(squareIndex28);

            if (propertyOwner > -1)
            {
                return displayResources.getPlayerAbv(propertyOwner);
            }
            else 
            {
                return "";
            }
        }

        private string getVerticalOwnerDisplay(int squareIndex28)
        {
            //OWNER STRING
            string stringOwner = "";

            string stringVerticalOwner = "";

            int propertyOwner = turnDirector.getOwnerOfProperty(squareIndex28);

            if (propertyOwner > -1)
            {
                stringOwner = displayResources.getPlayerAbv(turnDirector.getOwnerOfProperty(squareIndex28));

                for (int index = 0; index < stringOwner.Length; index++)
                {
                    if ((index + 1) < stringOwner.Length)
                    {
                        stringVerticalOwner = stringVerticalOwner + stringOwner[index] + "\r\n";
                    }
                    else
                    {
                        stringVerticalOwner = stringVerticalOwner + stringOwner[index];
                    }//END IF
                }//END FOR

                return stringVerticalOwner;
            }
            else
            {
                return "";
            }
        }

        private string getOwnerColorName(int squareIndex40)
        {
            //CHECK IF CAN BE MORTGAGED
            if (turnDirector.canMortgageProperty(squareIndex40))
            {
                return "Lime";
            }
            else
            {
                return "Red";
            }//END IF
        }

        //---------------------------------------------------------------------

        private Boolean getLabelVisible(string displayText)
        {
            if (String.Compare(displayText, "") == 0)
            {
                return false;
            }
            else
            {
                return true;
            }//END IF
        }

    }
}
