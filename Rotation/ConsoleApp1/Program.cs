using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //testing the application
            Console.WriteLine("Input the number of elements to store in the array :");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.Write("Input {0} number of elements in the array :\n",n);
            int[] num1 = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write("element - {0} : ", i);
                num1[i] = Convert.ToInt32(Console.ReadLine());
            }

            int num2;
            Console.WriteLine("Type a number, and then press Enter");
            num2 = Convert.ToInt32(Console.ReadLine());
            int [] answer = rotleft(num1, num2);

            Console.WriteLine("Your result:");

            Console.WriteLine(string.Join(" ", answer));
            Console.ReadKey();
        }
        public static int[] rotleft(int[] a, int d)
        {

            if (a.Length == 0 || a.Length == 1)
            {
                return a;
            }
            if (d > a.Length)
            {
                d = d % a.Length;
            }

            //slice the first part
            int[] firstPart = a.Take(a.Length - d).ToArray();
            //slice the secondpart
            int[] secondPart = a.Skip(a.Length - d).ToArray();

            Array.Reverse(firstPart);
            Array.Reverse(secondPart);

            int[] newArray = firstPart.Concat(secondPart).ToArray();
            if (d != a.Length)
            {
                Array.Reverse(newArray);
            }
            return newArray;
        }
    }
}
