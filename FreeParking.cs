using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class FreeParking
    {

        //CASH
        public int cash { get; set; }

        public FreeParking()
        {
            resetFreeParking();
        }

        public void resetFreeParking()
        {
            cash = 500;
        }

    }
}
