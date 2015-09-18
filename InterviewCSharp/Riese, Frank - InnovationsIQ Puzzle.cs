// Candidate: Frank Riese (riese8@gmail.com)
// Blog: http://frankriesecodingblog.blogspot.com        
// Resume: https://drive.google.com/file/d/0BwoFg75FgI-MYThYZ0UzR09YdkE/view?usp=sharing
//
// Submission date: 3/18/2015
// Recruiter: Sai Charan (sai.charan@innovationsiq.com)
using System;
using System.Collections.Generic;
using System.Linq;

public class InnovationsIQPuzzle
{
    /// <summary>
    /// Given a list of strings (single words or not), write a method to return 
    /// the longest, n-th string contained in the list. If more than one string     /// is of the same length and is the n-th longest, return a collection.    /// </summary>
    /// <param name="nTh">
    /// A one-based index to choosing the n-th largest set of strings to return.
    /// TODO: As a developer, I would clarify the requirement of it being one-based.
    /// </param>
    /// <param name="input">
    /// A list of strings.
    /// </param>
    /// <returns>
    /// See summary.
    /// </returns>
    public static IList<string> getLongestString(int nTh, List<string> input)
    {
        // In this implementation, I have prioritized simplicity. There are solutions
        // with a better runtime efficiency, for example using a max-heap. Floyd's 
        // heap construction can build a heap in O(n) (n being the number of strings 
        // here) and then extract the currently smallest element in O(1). Elements
        // in the heap would need to have a key with the length of a string and a 
        // reference to the original key. That would take a lot more code and there is 
        // no builtin collection in .NET with those properties that I am aware of, so 
        // I chose this simpler, more maintainable, and more readable solution.
        if (nTh < 1) throw new ArgumentException("nTh needs to be 1 or greater.");
        var query = (from s in input 
                     group s by s.Length into g 
                     orderby g.Key descending
                     select g.ToList())
                    .ToArray();
        return nTh < query.Length ? query[nTh - 1] : new List<string>() { };
    }

    /// <summary>
    /// Given a list of numbers between some start and end value (i.e. in a specific range), 
    /// where all the numbers in that range are present except for one, find the missing     /// number.    /// Ex:
    /// [1,5,2,3]
    /// Range is between 1 and 5
    /// Missing number to be returned is 4    /// NOTE: 
    /// There might be an issue with the specification of this problem. Without making
    /// the range explicit (e.g. by passing the upper and lower number as parameters)
    /// given a consecutive range of numbers like "4, 5, 6" it is ambiguous whether the
    /// missing number is "3", "7", or the input is malformed and we should consider no
    /// number to be missing.
    /// 
    /// This is a reuquirement I would clarify in a job setting. For the sake of simplicity,
    /// I am assuming that the lowest number is always the start and the highest number always
    /// the end of the range and that there is exactly one number missing. That seems to be
    /// in the spirit of this question.
    /// </summary>
    /// <param name="input">
    /// A range of numbers as described in the summary.
    /// </param>
    /// <returns>
    /// The missing number.
    /// </returns>
    public static int findMissing(int[] input)
    {
        int min = int.MaxValue, max = int.MinValue, sum = 0;
        foreach (int i in input)
        {
            min = Math.Min(min, i);
            max = Math.Max(max, i);
            sum += i;
        }
        return sum_range(max) - sum_range(min - 1) - sum;
    }
    private static int sum_range(int n) { return n * (n + 1) / 2; }

    /// <summary>
    /// Given a string, write a method to return all permutations of 
    /// that string (i.e. same length permutations).
    /// 
    /// NOTE: I am assuming that the order (such as it being lexicographic)
    /// does not matter. Also, it says to "return" them, so I chose to put
    /// them into a list, instead of just printing them.
    /// 
    /// This approach uses the "QuickPerm" algorithm, which generally performs
    /// far better than other approaches, uses no backtracking, requires no 
    /// recursion and performs no unnecessary exchanges. 
    /// 
    /// I had blogged about it here:
    /// http://frankriesecodingblog.blogspot.com/2015/01/shorter-quickperm-implementation.html
    /// 
    /// I had been in e-mail contact with the original inventor of this algorithm
    /// (Phillip Fuchs) at the beginning of this year and had informed him of my 
    /// use of his algorithm, and have significantly shortened its implementation.
    /// </summary>
    /// <param name="s">
    /// The string to be permuted.
    /// </param>
    /// <returns>
    /// An enumeration of all permutations of the given string.
    /// </returns>
    public static IEnumerable<string> permuteString(string s)
    {
        List<string> permutations = new List<string>() { s };
        char[] letters = s.ToCharArray();
        // Initialize swap indexes. The initial state encodes
        // the current ordering in the original string.
        int[] p = Enumerable.Range(0, s.Length + 1).ToArray();
	    for (int i = 1; i < s.Length; ) {
            // Exchange odd indexes, growing inwards towards
            // the beginning of the string.
            swap(ref letters[i], ref letters[(i % 2) * --p[i]]);
            permutations.Add(new string(letters));
            // Restore all "used up" exchanges to their initial
            // state before the loop, thereby repeating the 
            // pattern of exchanges on an increasingly wider 
            // range.
            for (i = 1; p[i] == 0; ++i) p[i] = i;
	    }
        return permutations;
    }
    private static void swap<T>(ref T x, ref T y) 
    {
        T t = x;
        x = y;
        y = t;
    }
}