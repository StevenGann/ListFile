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
            ListFile<int> testList1 = new ListFile<int>(1);
            testList1.Add(1);
            testList1.Add(2);
            testList1.Add(3);
            testList1.Add(4);
            testList1.Add(5);

            ListFile<string> testList2 = new ListFile<string>(1);
            testList2.Add("one");
            testList2.Add("two");
            testList2.Add("three");
            testList2.Add("four");
            testList2.Add("five");
            testList2.Insert(0, "Definitely not one");

            ListFile<int> testList3 = new ListFile<int>(2);
            testList3.Add(1);
            testList3.Add(2);
            testList3.Add(3);
            testList3.Add(4);
            testList3.Add(5);

            Console.WriteLine(testList1[0]);
            Console.WriteLine(testList2[1]);
            Console.WriteLine(testList3[2]);

            Console.ReadLine();
        }
    }
}
