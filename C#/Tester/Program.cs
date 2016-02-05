using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            testBasic();
            testPath();
            testNested();
            //testLargeList();
            Console.ReadLine();
        }

        static private void testBasic()
        {
            Console.WriteLine("=testBasic()=");
            Random RNG = new Random();
            ListFile<List<string>> bigList = new ListFile<List<string>>(0);
            for (int j = 0; j < 5; j++)
            {
                List<string> tempList = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    string tempString = "";
                    for (int k = 0; k < 5; k++)
                    {
                        tempString += Convert.ToString(RNG.Next(100000, 999999)) + " ";
                    }
                    tempList.Add(tempString);
                }
                bigList.Add(tempList);
                Console.WriteLine(j);
            }

            Console.WriteLine("Done generating");

            foreach (List<string> smallList in bigList)
            {
                Console.WriteLine(smallList[0]);
            }
            bigList.Destroy();
        }

        static private void testNested()
        {
            Console.WriteLine("=testNested()=");
            Random RNG = new Random();
            ListFile<ListFile<string>> bigList = new ListFile<ListFile<string>>(0);
            for (int j = 0; j < 5; j++)
            {
                ListFile<string> tempList = new ListFile<string>(j);
                for (int i = 0; i < 10; i++)
                {
                    string tempString = "";
                    for (int k = 0; k < 5; k++)
                    {
                        tempString += Convert.ToString(RNG.Next(100000, 999999)) + " ";
                    }
                    tempList.Add(tempString);
                }
                bigList.Add(tempList);
                Console.WriteLine(j);
            }

            Console.WriteLine("Done generating");

            foreach (ListFile<string> smallList in bigList)
            {
                Console.WriteLine(smallList[0]);
                smallList.Destroy();
            }
            bigList.Destroy();
        }

        static private void testLargeList()
        {
            Console.WriteLine("=testLargeList()=");
            Random RNG = new Random();
            ListFile<List<string>> bigList = new ListFile<List<string>>(0);
            for (int j = 0; j < 5000; j++)
            {
                List<string> tempList = new List<string>();
                for (int i = 0; i < 2000; i++)
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

            foreach (List<string> smallList in bigList)
            {
                Console.WriteLine(smallList[0]);
            }
            bigList.Destroy();
        }

        static private void testPath()
        {
            Console.WriteLine("=testPath()=");
            Random RNG = new Random();
            ListFile<List<string>> bigList = new ListFile<List<string>>(0, Path.GetTempPath() + "ListFile Cache\\");
            Console.WriteLine(bigList.Path);
            for (int j = 0; j < 5; j++)
            {
                List<string> tempList = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    string tempString = "";
                    for (int k = 0; k < 5; k++)
                    {
                        tempString += Convert.ToString(RNG.Next(100000, 999999)) + " ";
                    }
                    tempList.Add(tempString);
                }
                bigList.Add(tempList);
                Console.WriteLine(j);
            }

            Console.WriteLine("Done generating");

            foreach (List<string> smallList in bigList)
            {
                Console.WriteLine(smallList[0]);
            }
            bigList.Destroy();
        }
    }
}
