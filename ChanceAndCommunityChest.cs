using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class ChanceAndCommunityChest
    {

        //https://monopoly.fandom.com/wiki/Chance
        //https://monopoly.fandom.com/wiki/Community_Chest

        //RANDOM NUMBER GENERATOR
        private Random random = new Random();

        public ChanceAndCommunityChest()
        {

        }

        // 0 GO
        // 1 Mediterranean Avenue
        // 2 Community Chest 1
        // 3 Baltic Avenue
        // 4 Income Tax
        // 5 Reading Railroad
        // 6 Oriental Avenue
        // 7 Chance 1
        // 8 Vermont Avenue
        // 9 Connecticut Avenue
        // 10 Jail
        // 11 St. Charles Place
        // 12 Electric Company
        // 13 States Avenue
        // 14 Virginia Avenue
        // 15 Pennsylvania Railroad
        // 16 St. James Place
        // 17 Community Chest 2
        // 18 Tennessee Avenue
        // 19 New York Avenue
        // 20 Free Parking
        // 21 Kentucky Avenue
        // 22 Chance 2
        // 23 Indiana Avenue
        // 24 Illinois Avenue
        // 25 B&O Railroad
        // 26 Atlantic Avenue
        // 27 Ventnor Avenue
        // 28 Water Works
        // 29 Marvin Gardens
        // 30 Go To Jail
        // 31 Pacific Avenue
        // 32 North Carolina Avenue
        // 33 Community Chest 3
        // 34 Pennsylvania Avenue
        // 35 Short Line
        // 36 Chance 3
        // 37 Park Place
        // 38 Luxury Tax
        // 39 Boardwalk

        //---------------------------------------------------------------------

        public TurnResults processChanceCard(int player, int houses, int hotels, TurnResults turnResults)
        {
            int chance = random.Next(0, 17);

            int cost = 0;

            if (chance == 0)
            {
                System.Windows.Forms.MessageBox.Show("Advance to Go! Collect $200!");

                // 0 GO
                turnResults.location = 0;

                turnResults.addPlayerCash(player, 200);

                return turnResults; 
            }
            else if (chance == 1)
            {
                System.Windows.Forms.MessageBox.Show("Advance to Illinois Ave!");

                //CHECK IF PASSED GO
                if (turnResults.location == 36)
                {
                    // 36 Chance 3
                    System.Windows.Forms.MessageBox.Show("Passed Go!");

                    // 24 Illinois Avenue

                    //ADD PASS GO MONEY
                    turnResults.addPlayerCash(player, 200);
                }//END IF

                // 24 Illinois Avenue
                turnResults.location = 24;

                return turnResults;
            }
            else if (chance == 2)
            {
                System.Windows.Forms.MessageBox.Show("Advance to St. Charles Place!");

                //CHECK IF PASSED GO
                if (turnResults.location == 22 || turnResults.location == 36)
                {
                    // 22 Chance 2
                    // 36 Chance 3
                    System.Windows.Forms.MessageBox.Show("Passed Go!");

                    // 11 St. Charles Place

                    //ADD PASS GO MONEY
                    turnResults.addPlayerCash(player, 200);
                }//END IF

                // 11 St. Charles Place
                turnResults.location = 11;

                return turnResults;
            }
            else if (chance == 3)
            {
                System.Windows.Forms.MessageBox.Show("Advance token to the nearest Utility! If unowned, you may buy it from the Bank. If owned, throw dice and pay owner a total 10 times the amount thrown.");

                //CALCULATE NEAREST UTILITY
                if (turnResults.location == 7)
                {
                    // 7 Chance 1
                    // 12 Electric Company 
                    turnResults.location = 12;
                }
                else if (turnResults.location == 22)
                {
                    // 22 Chance 2
                    // 28 Water Works 
                    turnResults.location = 28;
                }
                else if (turnResults.location == 36)
                {
                    // 36 Chance 3
                    System.Windows.Forms.MessageBox.Show("Passed Go!");

                    //ADD PASS GO MONEY
                    turnResults.addPlayerCash(player, 200);

                    // 12 Electric Company 
                    turnResults.location = 12;
                }//END IF

                turnResults.rentMultiplier = 10;

                return turnResults;
            }
            else if (chance == 4 || chance == 5)
            {
                System.Windows.Forms.MessageBox.Show("Advance to the nearest Railroad! If unowned, you may buy it from the Bank. If owned, pay owner twice the rent to which they are otherwise entitled.");

                //CALCULATE NEAREST RAILROAD
                if (turnResults.location == 7)
                {
                    // 7 Chance 1
                    // 15 Pennsylvania Railroad
                    turnResults.location = 15;
                }
                else if (turnResults.location == 22)
                {
                    // 22 Chance 2
                    // 25 B&O Railroad
                    turnResults.location = 25;
                }
                else if (turnResults.location == 36)
                {
                    // 36 Chance 3
                    System.Windows.Forms.MessageBox.Show("Passed Go!");

                    //ADD PASS GO MONEY
                    turnResults.addPlayerCash(player, 200);

                    // 5 Reading Railroad
                    turnResults.location = 5;
                }//END IF

                turnResults.rentMultiplier = 2;

                return turnResults;
            }
            else if (chance == 6)
            {
                System.Windows.Forms.MessageBox.Show("Bank pays you dividend of $50!");

                turnResults.addPlayerCash(player, 50);

                return turnResults;
            }
            else if (chance == 7)
            {
                System.Windows.Forms.MessageBox.Show("Get out of Jail Free! This card will automatically be used when paying to get out of jail.");

                turnResults.getOutOfJailFreeCards += 1;

                return turnResults;
            }
            else if (chance == 8)
            {
                System.Windows.Forms.MessageBox.Show("Go Back Three 3 Spaces.");

                //HARD CODE LOCATIONS
                if (turnResults.location == 7)
                {
                    // 7 Chance 1
                    // 4 Income Tax
                    turnResults.location = 4;
                }
                else if (turnResults.location == 22)
                {
                    // 22 Chance 2
                    // 19 New York Avenue
                    turnResults.location = 19;
                }
                else if (turnResults.location == 36)
                {
                    // 36 Chance 3
                    // 33 Community Chest 3
                    turnResults.location = 33;
                }//END IF

                return turnResults;
            }
            else if (chance == 9)
            {
                System.Windows.Forms.MessageBox.Show("Go to Jail. Go directly to Jail. Do not pass GO, do not collect $200.");

                // 10 Jail
                turnResults.location = 10;

                turnResults.inJail = true;

                return turnResults;
            }
            else if (chance == 10)
            {
                System.Windows.Forms.MessageBox.Show("Make general repairs on all your property: For each house pay $25, For each hotel pay $100.");

                cost = (houses * -25) + (hotels * -100);

                turnResults.addPlayerCash(player, cost);

                turnResults.freeParkingMoney -= cost;

                return turnResults;
            }
            else if (chance == 11)
            {
                System.Windows.Forms.MessageBox.Show("Pay poor tax of $15.");

                turnResults.addPlayerCash(player, -15);

                turnResults.freeParkingMoney += 15;

                return turnResults;
            }
            else if (chance == 12)
            {
                System.Windows.Forms.MessageBox.Show("Take a trip to Reading Railroad!");

                //CHECK IF PASSED GO
                if (turnResults.location == 7 || turnResults.location == 22 || turnResults.location == 36)
                {
                    // 7 Chance 1
                    // 22 Chance 2
                    // 36 Chance 3
                    System.Windows.Forms.MessageBox.Show("Passed Go!");

                    // 5 Reading Railroad

                    //ADD PASS GO MONEY
                    turnResults.addPlayerCash(player, 200);
                }//END IF

                // 5 Reading Railroad
                turnResults.location = 5;

                return turnResults;
            }
            else if (chance == 13)
            {
                System.Windows.Forms.MessageBox.Show("Take a walk on the Boardwalk! Advance token to Boardwalk!");

                // 39 Boardwalk
                turnResults.location = 39;

                return turnResults;
            }
            else if (chance == 14)
            {
                System.Windows.Forms.MessageBox.Show("You have been elected Chairman of the Board. Pay each player $50.");

                for (int index = 0; index < turnResults.getNumberOfPlayers(); index++)
                {
                    if (turnResults.getPlayerStillPlaying(index))
                    {
                        turnResults.addPlayerCash(index, 50);
                        turnResults.addPlayerCash(player, -50);
                    }//END IF
                }//END FOR

                return turnResults;
            }
            else if (chance == 15)
            {
                System.Windows.Forms.MessageBox.Show("Your building and loan matures! Collect $150!");

                turnResults.addPlayerCash(player, 150);

                return turnResults;
            }
            else if (chance == 16)
            {
                System.Windows.Forms.MessageBox.Show("You have won a crossword competition. Collect $100!");

                turnResults.addPlayerCash(player, 100);

                return turnResults;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("ERROR NO CARD DRAWN");

                return turnResults;
            }//END IF
        }

        //---------------------------------------------------------------------

        public TurnResults processCommunityChestCard(int player, int houses, int hotels, TurnResults turnResults)
        {
            int communityChest = random.Next(0, 17);

            int cost = 0;

            if (communityChest == 0)
            {
                System.Windows.Forms.MessageBox.Show("Advance to Go! Collect $200!");

                // 0 GO
                turnResults.location = 0;

                turnResults.addPlayerCash(player, 200);

                return turnResults;
            }
            else if (communityChest == 1)
            {
                System.Windows.Forms.MessageBox.Show("Bank error in your favor. Collect $200!");

                turnResults.addPlayerCash(player, 200);

                return turnResults;
            }
            else if (communityChest == 2)
            {
                System.Windows.Forms.MessageBox.Show("Doctor's fees. Pay $50.");

                turnResults.addPlayerCash(player, -50);

                turnResults.freeParkingMoney += 50;

                return turnResults;
            }
            else if (communityChest == 3)
            {
                System.Windows.Forms.MessageBox.Show("From sale of stock you get $50!");

                turnResults.addPlayerCash(player, 50);

                return turnResults;
            }
            else if (communityChest == 4)
            {
                System.Windows.Forms.MessageBox.Show("Get Out of Jail Free! This card will automatically be used when paying to get out of jail.");

                turnResults.getOutOfJailFreeCards += 1;

                return turnResults;
            }
            else if (communityChest == 5)
            {
                System.Windows.Forms.MessageBox.Show("Go to Jail. Go directly to jail. Do not pass Go, Do not collect $200.");

                // 10 Jail
                turnResults.location = 10;

                turnResults.inJail = true;

                return turnResults;
            }
            else if (communityChest == 6)
            {
                System.Windows.Forms.MessageBox.Show("Grand Opera Opening. Collect $50 from every player for opening night seats!");

                for (int index = 0; index < turnResults.getNumberOfPlayers(); index++)
                {
                    if (turnResults.getPlayerStillPlaying(index))
                    {
                        turnResults.addPlayerCash(index, -50);
                        turnResults.addPlayerCash(player, 50); 
                    }//END IF
                }//END FOR

                return turnResults;
            }
            else if (communityChest == 7)
            {
                System.Windows.Forms.MessageBox.Show("Xmas Fund matures. Collect $100!");

                turnResults.addPlayerCash(player, 100);

                return turnResults;
            }
            else if (communityChest == 8)
            {
                System.Windows.Forms.MessageBox.Show("Income tax refund. Collect $20!");

                turnResults.addPlayerCash(player, 20);

                return turnResults;
            }
            else if (communityChest == 9)
            {
                System.Windows.Forms.MessageBox.Show("It'syour birthday. Collect $10 from every player!");

                for (int index = 0; index < turnResults.getNumberOfPlayers(); index++)
                {
                    if (turnResults.getPlayerStillPlaying(index))
                    {
                        turnResults.addPlayerCash(index, -10);
                        turnResults.addPlayerCash(player, 10);
                    }//END IF
                }//END FOR

                return turnResults;
            }
            else if (communityChest == 10)
            {
                System.Windows.Forms.MessageBox.Show("Life insurance matures – Collect $100!");

                turnResults.addPlayerCash(player, 100);

                return turnResults;
            }
            else if (communityChest == 11)
            {
                System.Windows.Forms.MessageBox.Show("Hospital Fees. Pay $100.");

                turnResults.addPlayerCash(player, -100);

                turnResults.freeParkingMoney += 100;

                return turnResults;
            }
            else if (communityChest == 12)
            {
                System.Windows.Forms.MessageBox.Show("School fees. Pay $50.");

                turnResults.addPlayerCash(player, -50);

                turnResults.freeParkingMoney += 50;

                return turnResults;
            }
            else if (communityChest == 13)
            {
                System.Windows.Forms.MessageBox.Show("Receive $25 consultancy fee!");

                turnResults.addPlayerCash(player, 25);

                return turnResults;
            }
            else if (communityChest == 14)
            {
                System.Windows.Forms.MessageBox.Show("You are assessed for street repairs: Pay $40 per house and $115 per hotel you own.");

                cost = (houses * -40) + (hotels * -115);

                turnResults.addPlayerCash(player, cost);

                turnResults.freeParkingMoney -= cost;

                return turnResults;
            }
            else if (communityChest == 15)
            {
                System.Windows.Forms.MessageBox.Show("You have won second prize in a beauty contest. Collect $10!");

                turnResults.addPlayerCash(player, 10);

                return turnResults;
            }
            else if (communityChest == 16)
            {
                System.Windows.Forms.MessageBox.Show("You inherit $100!");

                turnResults.addPlayerCash(player, 100);

                return turnResults;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("ERROR NO CARD DRAWN");

                return turnResults;
            }//END IF
        }

    }
}
