using System.Linq;

public class Combinatorics
{
    public static int GiveChange(int change, int[] coins)
    {
        var combinations = new int[change];
        // all zero

        for (int i = 0; i < coins.Length; i++)
        {
            for (int j = coins[i]; j < combinations.Length; j += coins[i])
            {
                combinations[j]++;
            }
        }
        return combinations.Last();
    }
    public static void TestGiveChange()
    {

    }
}