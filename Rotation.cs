using System;

public class Class1
{
	public Class1()
	{
         int[] rotleft(int[]a,int d)
        {

            if (a.Length == 0 || a.Length == 1)
            {
                return a;
            }

            if (d > a.Length )
            {
                d = d % a.Length;
            }

            int[] firstPart = a.Take(a.Length -d).ToArray();

            int[] secondPart = a.Skip(a.Length - d).ToArray();

            Array.Reverse(firstPart);
            Array.Reverse(secondPart);

            int[] newArray = firstPart.Concat(secondPart).ToArray();
            Array.Reverse(newArray);

            return newArray;
        }
	}
}
