using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = Path.GetFullPath("inputs/day02.txt");
        string content = File.ReadAllText(inputPath).Trim();
        string[] ranges = content.Split(',', StringSplitOptions.RemoveEmptyEntries);

        long totalSumPart1 = 0;
        long totalSumPart2 = 0;

        foreach (string range in ranges)
        {
            var parts = range.Split('-');
            long start = long.Parse(parts[0]);
            long end = long.Parse(parts[1]);

            for (long id = start; id <= end; id++)
            {
                if (IsInvalidId_Part1(id))
                    totalSumPart1 += id;

                if (IsInvalidId_Part2(id))
                    totalSumPart2 += id;
            }
        }

        Console.WriteLine("Sum of invalid IDs (Part 1): " + totalSumPart1);
        Console.WriteLine("Sum of invalid IDs (Part 2): " + totalSumPart2);
    }

    // Part 1
    static bool IsInvalidId_Part1(long n)
    {
        string s = n.ToString();
        int len = s.Length;

        if (len % 2 != 0)
            return false;

        int half = len / 2;

        for (int i = 0; i < half; i++)
            if (s[i] != s[i + half])
                return false;

        return true;
    }

    // Part 2
    static bool IsInvalidId_Part2(long n)
    {
        string s = n.ToString();
        int len = s.Length;

        for (int subLen = 1; subLen <= len / 2; subLen++)
        {
            if (len % subLen != 0) continue;

            bool match = true;

            for (int start = subLen; start < len && match; start += subLen)
            {
                for (int i = 0; i < subLen; i++)
                {
                    if (s[i] != s[start + i])
                    {
                        match = false;
                        break;
                    }
                }
            }

            if (match)
                return true;
        }

        return false;
    }
}