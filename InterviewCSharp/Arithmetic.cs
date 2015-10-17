using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

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
    public static int[] PrimeFactors(int n)
    {
        var primeFactors = new List<int>();
        for (int i = 2; i * i <= n; i++)
        {
            while (n%i == 0)
            {
                primeFactors.Add(i);
                n /= i;
            }
        }
        if (n > 2) primeFactors.Add(n); // If n is a prime number.

        return primeFactors.ToArray();
    }
    public static void TestPrimeFactors()
    {
        var result = PrimeFactors(100);
        result = PrimeFactors(123903);
    }
    /*********************************************************************/
    public static bool IsPrime(int n)
    {
        if (n < 2) return false;

        int m = n;
        for (int i = 2; i * i <= n; i++)
        {
            while (n%i == 0)
            {
                n /= i;
            }
        }
        return m == n;
    }
    public static void TestIsPrime()
    {
        var result = IsPrime(1);
        result = IsPrime(2);
        result = IsPrime(3);
        result = IsPrime(4);
        result = IsPrime(5);
        result = IsPrime(6);
        result = IsPrime(101);
    }
    /*********************************************************************/
}

