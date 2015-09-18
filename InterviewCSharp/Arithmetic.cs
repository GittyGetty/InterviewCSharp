using System;
using System.Linq;
using System.Text;
public class Arithmetic
{
    /*********************************************************************/
    public static void PrintPrimeNumbers(int max)
    {
        bool[] isPrime = Enumerable.Repeat(true, max).ToArray();

        for (int n = 2; n <= max; n++)
        {
            if (isPrime[n - 1] == false) continue;
            System.Console.WriteLine("{0} is a prime number.", n);
            for (int m = n * n; m <= max; m += n) isPrime[m - 1] = false;
        }
    }
    /*********************************************************************/
    public static void Count(int digits, int b)
    {
        StringBuilder number = new StringBuilder("0");
        for (int i = 0; i < digits; i++)
        {
            for (int j = 0; j < b; j++)
            {
                Console.WriteLine(number);
                int carry = 1;
                for (int pos = number.Length - 1; carry == 1; pos--)
                {
                    if (pos == -1)
                    {
                        number.Insert(0, '0');
                        pos = 0;
                    }
                    int digit = (number[pos] - '0' + carry) % b;
                    number[pos] = (char)('0' + digit);

                    carry = 0;
                    if (number[pos] == '0') carry = 1;
                }
            }
        }
    }
    /*********************************************************************/
}
