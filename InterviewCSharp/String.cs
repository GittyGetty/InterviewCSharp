using System.Collections.Generic;

namespace Interview
{
    public class String
    {
        public static string LongestCommonSubstring(string a, string b)
        {
            int[,] suffixes = new int[a.Length, b.Length];
            string longest = string.Empty;

            for (int ai = 0; ai < a.Length; ai++)
            {
                for (int bi = 0; bi < b.Length; bi++)
                {
                    if (a[ai] != b[bi]) suffixes[ai, bi] = 0;
                    else if (ai == 0 || bi == 0) suffixes[ai, bi] = 1;
                    else suffixes[ai, bi] = suffixes[ai - 1, bi - 1] + 1;

                    if (suffixes[ai, bi] <= longest.Length) continue;

                    longest = a.Substring(ai - suffixes[ai, bi] + 1, suffixes[ai, bi]);
                }
            }

            return longest;
        }

        /*********************************************************************/

        public static string LongestCommonSubstring(string a, string b, string c)
        {
            string longest = string.Empty;

            int[, ,] suffixes = new int[a.Length, b.Length, c.Length];

            for (int ai = 0; ai < a.Length; ai++)
                for (int bi = 0; bi < b.Length; bi++)
                    for (int ci = 0; ci < c.Length; ci++)
                    {
                        if (!(a[ai] == b[bi] && b[bi] == c[ci])) suffixes[ai, bi, ci] = 0;
                        else if (ai == 0 || bi == 0 || ci == 0) suffixes[ai, bi, ci] = 1;
                        else suffixes[ai, bi, ci] = suffixes[ai - 1, bi - 1, ci - 1] + 1;

                        if (suffixes[ai, bi, ci] <= longest.Length) continue;

                        longest = a.Substring(ai - suffixes[ai, bi, ci] + 1, suffixes[ai, bi, ci]);
                    }

            return longest;
        }
        /*********************************************************************/
        public static string GetLongestPalindrome(string s)
        {
            if (s.Length == 0) return string.Empty;

            int n = s.Length;
            var isPalindrome = new bool[n, n];

            int longest_start = 0;
            int longest_length = 1;
            for (int i = 0; i < n; i++)
            {
                isPalindrome[i, i] = true;
                if (i < n - 1 && s[i] == s[i + 1])
                {
                    longest_start = i;
                    longest_length = 2;
                    isPalindrome[i, i + 1] = true;
                }
            }

            for (int pl = 3; pl <= n; pl++)
            {
                for (int ps = 0; ps < n - pl; ps++)
                {
                    int pe = ps + pl - 1;
                    if (isPalindrome[ps + 1, pe - 1] && s[ps] == s[pe])
                    {
                        longest_length = pl;
                        longest_start = ps;
                        isPalindrome[ps, pe] = true;
                    }
                }
            }

            return s.Substring(longest_start, longest_length);
        }
        /*********************************************************************/
        // Two maps are needed, because different characters in one string
        // could map to the same character otherwise.
        public static bool IsIsomorphic(string s1, string s2)
        {
            if (s1.Length != s2.Length) return false;

            var charMap1 = new Dictionary<char, char>();
            var charMap2 = new Dictionary<char, char>();

            for (int i = 0; i < s1.Length; i++)
            {
                if (charMap1.ContainsKey(s1[i]))
                {
                    if (charMap1[s1[i]] != s2[i]) return false;
                }
                charMap1[s1[i]] = s2[i];

                if (charMap2.ContainsKey(s2[i]))
                {
                    if (charMap2[s2[i]] != s1[i]) return false;
                }
                charMap2[s2[i]] = s1[i];
            }
            return true;
        }
        public static void IsIsomorphicTest()
        {
            bool result;
            result = IsIsomorphic("aabbbccaab", "AABBBCCAAB");
            result = IsIsomorphic("aabbbccaab", "AABBBCCAAD");
            result = IsIsomorphic("aaa", "aaa");
            result = IsIsomorphic("aaa", "aax");
            result = IsIsomorphic("aaa", "xxx");
            result = IsIsomorphic("a", "aa");
        } 
        /*********************************************************************/
    }
}