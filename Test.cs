using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOpoly
{
    class Test
    {
        private Battle battle = new Battle();

        private IndexConversions indexConversions = new IndexConversions();

        static void Main(string[] args)
        {

            Test test = new Test();

            //test.runTest1();

            test.runTest2();

        }

        public void runTest1()
        {
            int[] order = new int[20];

            battle.setOrder(order);

        }

        public void runTest2()
        {
            Console.WriteLine();

            for (int index40 = 0; index40 < 40; index40++)
            {
                //Console.WriteLine("40: " + index40 + " 28: " + indexConversions.get28IndexFrom40Index(index40));
                Console.WriteLine(indexConversions.get28IndexFrom40Index(index40));
            }//END FOR

            Console.WriteLine();

            for (int index40 = 0; index40 < 40; index40++)
            {
                //Console.WriteLine("40: " + index40 + " 22: " + indexConversions.get22IndexFrom40Index(index40));
                Console.WriteLine(indexConversions.get22IndexFrom40Index(index40));
            }//END FOR

            Console.WriteLine();

            for (int index28 = 0; index28 < 28; index28++)
            {
                //Console.WriteLine("28: " + index28 + " 40: " + indexConversions.get40IndexFrom28Index(index28));
                Console.WriteLine(indexConversions.get40IndexFrom28Index(index28));
            }//END FOR

            Console.WriteLine();

            for (int index28 = 0; index28 < 28; index28++)
            {
                //Console.WriteLine("28: " + index28 + " 22: " + indexConversions.get22IndexFrom28Index(index28));
                Console.WriteLine(indexConversions.get22IndexFrom28Index(index28));
            }//END FOR

            Console.WriteLine();

            for (int index22 = 0; index22 < 22; index22++)
            {
                //Console.WriteLine("22: " + index22 + " 40: " + indexConversions.get40IndexFrom22Index(index22));
                Console.WriteLine(indexConversions.get40IndexFrom22Index(index22));
            }//END FOR

            Console.WriteLine();

            for (int index22 = 0; index22 < 22; index22++)
            {
                //Console.WriteLine("22: " + index22 + " 28: " + indexConversions.get28IndexFrom22Index(index22));
                Console.WriteLine(indexConversions.get28IndexFrom22Index(index22));
            }//END FOR

        }

    }
}
