using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class Battle
    {

        //RANDOM NUMBER GENERATOR
        private Random random = new Random();

        private static readonly int[][] arrayPropertyMultipliers = new int[][]
        {
            new int[] {1, 0},
            new int[] {2, 1, 0},
            new int[] {2, 1, 0, 0},
            new int[] {3, 2, 1, 0, 0},
            new int[] {3, 2, 1, 0, 0, 0}
        };

        private static readonly int[][] arrayDrinkMultipliers = new int[][]
        {
            new int[] {0, 1},
            new int[] {0, 1, 2},
            new int[] {0, 0, 1, 2},
            new int[] {0, 0, 1, 2, 3},
            new int[] {0, 0, 0, 1, 2, 3}
        };

        public Battle()
        {

        }

        public TurnResults battle(
            int[] players, 
            int propertyValue, 
            TurnResults turnResults, 
            DisplayResources displayResources)
        {
            int[] order = new int[players.Length];

            int cash = 0;

            int player = 0;

            int place = 0;

            Array.Copy(setOrder(order),
                order,
                order.Length);

            for (int index = 0; index < order.Length; index++)
            {
                cash = propertyValue * arrayPropertyMultipliers[players.Length - 2][index];

                player = players[order[index]];

                place = index + 1; 

                turnResults.addPlayerCash(player, cash);

                System.Windows.Forms.MessageBox.Show(
                            "Battle " + 
                            Convert.ToString(index) + 
                            " place " +
                            displayResources.getPlayerName(player) + 
                            " cash " + 
                            Convert.ToString(cash) + 
                            " drinks " +
                            arrayDrinkMultipliers[players.Length - 2][index]);
            }//END FOR

            return turnResults;
        }

        public int[] setOrder(int[] order)
        {
            int[] nextOrder = new int[order.Length - 1];

            int last = random.Next(0, order.Length);

            if (order.Length <= 1)
            {
                order[0] = 0;

                return order;
            }
            else
            {
                order[last] = order.Length - 1;

                Array.Copy(setOrder(nextOrder),
                    nextOrder,
                    nextOrder.Length);

                for (int index = 0; index < nextOrder.Length; index++)
                {
                    if (index >= last)
                    {
                        order[index + 1] = nextOrder[index];
                    }
                    else
                    {
                        order[index] = nextOrder[index];
                    }//END IF
                }//END FOR
            }//END IF

            return order;
        }

    }
}
