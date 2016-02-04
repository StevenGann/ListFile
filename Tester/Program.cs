using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Random RNG = new Random();
            ListFile<List<string>> bigList = new ListFile<List<string>>(0);
            for (int j = 0; j < 2000; j++)
            {
                List<string> tempList = new List<string>();
                for (int i = 0; i < 50000; i++)
                {
                    string tempString = "";
                    for (int k = 0; k < 20; k++)
                    {
                        tempString += Convert.ToString(RNG.Next(100000, 999999)) + " ";
                    }
                    tempList.Add(tempString);
                }
                bigList.Add(tempList);
                Console.WriteLine(j);
            }

            Console.WriteLine("Done generating");

            Console.ReadLine();
        }
    }
}
