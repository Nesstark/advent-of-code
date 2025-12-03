using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = Path.GetFullPath("inputs/day03.txt");

        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Input file not found: " + inputPath);
            return;
        }

        string[] lines = File.ReadAllLines(inputPath);

        // ------------------------------
        // Part One
        // ------------------------------
        long totalPartOne = 0;

        foreach (string line in lines)
        {
            int bestTwoDigit = GetMaxJoltagePartOne(line);
            totalPartOne += bestTwoDigit;
        }

        Console.WriteLine("Part One total joltage: " + totalPartOne);

        // ------------------------------
        // Part Two
        // ------------------------------
        long totalPartTwo = 0;

        foreach (string line in lines)
        {
            long bestTwelveDigit = GetMaxJoltagePartTwo(line, 12); 
            totalPartTwo += bestTwelveDigit;
        }

        Console.WriteLine("Part Two total joltage: " + totalPartTwo);
    }

    // --------------------------------------------------
    // Part one
    // --------------------------------------------------
    static int GetMaxJoltagePartOne(string bank)
    {
        int maxPair = -1;

        for (int i = 0; i < bank.Length - 1; i++)
        {
            int a = bank[i] - '0';

            for (int j = i + 1; j < bank.Length; j++)
            {
                int b = bank[j] - '0';
                int value = a * 10 + b;

                if (value > maxPair)
                    maxPair = value;
            }
        }

        return maxPair;
    }

    // --------------------------------------------------
    // Part two
    // --------------------------------------------------
    static long GetMaxJoltagePartTwo(string bank, int k)
    {
        int removeCount = bank.Length - k;
        var stack = new Stack<char>();

        foreach (char digit in bank)
        {
            while (stack.Count > 0 && removeCount > 0 && stack.Peek() < digit)
            {
                stack.Pop();
                removeCount--;
            }
            stack.Push(digit);
        }

        while (removeCount > 0)
        {
            stack.Pop();
            removeCount--;
        }

        char[] result = stack.ToArray();
        Array.Reverse(result);

        return long.Parse(new string(result, 0, k));
    }
}