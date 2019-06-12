using System;

public class Class1
{
	public Class1()
	{
         int rotleft(int[]a,int d)
        {

            if (a.Length == 0 || a.Length == 1)
            {
                return a;
            }
            int lastElement;

            //int[] newArray =  a.s
            //int[] newArray1 = a[a.Length:]

            //Array.Reverse(a)
            int[] newArray = new int[a.Length];

            List<int> numbers = new List<int>();

            for (int i = 1; i < d + 1; i++)
            {

                lastElement = a[a.Length - 1];
                newArray = a.Take(a.Length - 1).ToArray();
                numbers = newArray.ToList<int>();
                numbers.Insert(0, lastElement);

                a = numbers.ToArray();
                newArray = a;

            }
            return newArray;
        }
	}
}
