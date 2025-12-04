using System;
using System.IO;
using System.Collections.Generic;

class Day03
{
    static void MainDay03(string[] args)
    {
        string inputPath = Path.GetFullPath("inputs/day03.txt");
        string[] lines = File.ReadAllLines(inputPath);
        
        long totalPartOne = 0;
        long totalPartTwo = 0;
        
        foreach (string line in lines)
        {
            totalPartOne += GetMaxJoltagePartOne(line);
            totalPartTwo += GetMaxJoltagePartTwo(line, 12);
        }
        
        Console.WriteLine("Part One total joltage: " + totalPartOne);
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
            
            if (a == 9)
            {
                for (int j = i + 1; j < bank.Length; j++)
                {
                    int b = bank[j] - '0';
                    if (b == 9)
                        return 99;
                    maxPair = Math.Max(maxPair, a * 10 + b);
                }
            }
            else
            {
                for (int j = i + 1; j < bank.Length; j++)
                {
                    int b = bank[j] - '0';
                    int value = a * 10 + b;
                    if (value > maxPair)
                        maxPair = value;
                }
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
        var stack = new Stack<char>(bank.Length);
        
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
        
        return long.Parse(result.AsSpan(0, k));
    }
}