using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("inputs/day06.txt");
        
        int maxWidth = 0;
        foreach (string line in lines)
        {
            if (line.Length > maxWidth)
                maxWidth = line.Length;
        }
        
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].PadRight(maxWidth, ' ');
        }

        Console.WriteLine("Part One: " + PartOne(lines));
        Console.WriteLine("Part Two: " + PartTwo(lines));
    }

    // Part one
    static long PartOne(string[] lines)
    {
        long total = 0;
        int currentCol = 0;

        while (currentCol < lines[0].Length)
        {
            bool isEmpty = true;
            for (int row = 0; row < lines.Length; row++)
            {
                if (lines[row][currentCol] != ' ')
                {
                    isEmpty = false;
                    break;
                }
            }
            
            if (isEmpty)
            {
                currentCol++;
                continue;
            }

            int blockStart = currentCol;
            int blockEnd = currentCol;
            
            while (blockEnd < lines[0].Length)
            {
                bool isBlank = true;
                for (int row = 0; row < lines.Length; row++)
                {
                    if (lines[row][blockEnd] != ' ')
                    {
                        isBlank = false;
                        break;
                    }
                }
                
                if (isBlank)
                    break;
                    
                blockEnd++;
            }

            List<long> numbers = new List<long>();
            char operation = '+';

            for (int row = 0; row < lines.Length; row++)
            {
                string blockText = lines[row].Substring(blockStart, blockEnd - blockStart).Trim();
                
                if (blockText == "+")
                    operation = '+';
                else if (blockText == "*")
                    operation = '*';
                else if (blockText != "")
                    numbers.Add(long.Parse(blockText));
            }

            long answer = (operation == '+') ? 0 : 1;
            
            foreach (long num in numbers)
            {
                if (operation == '+')
                    answer += num;
                else
                    answer *= num;
            }

            total += answer;
            currentCol = blockEnd + 1;
        }

        return total;
    }

    // Part Two
    static long PartTwo(string[] lines)
    {
        long total = 0;
        int currentCol = 0;

        while (currentCol < lines[0].Length)
        {
            bool isEmpty = true;
            for (int row = 0; row < lines.Length; row++)
            {
                if (lines[row][currentCol] != ' ')
                {
                    isEmpty = false;
                    break;
                }
            }
            
            if (isEmpty)
            {
                currentCol++;
                continue;
            }

            int blockStart = currentCol;
            int blockEnd = currentCol;
            
            while (blockEnd < lines[0].Length)
            {
                bool isBlank = true;
                for (int row = 0; row < lines.Length; row++)
                {
                    if (lines[row][blockEnd] != ' ')
                    {
                        isBlank = false;
                        break;
                    }
                }
                
                if (isBlank)
                    break;
                    
                blockEnd++;
            }

            char operation = '+';
            for (int c = blockStart; c < blockEnd; c++)
            {
                if (lines[lines.Length - 1][c] == '+' || lines[lines.Length - 1][c] == '*')
                {
                    operation = lines[lines.Length - 1][c];
                    break;
                }
            }

            List<long> numbers = new List<long>();
            
            for (int c = blockEnd - 1; c >= blockStart; c--)
            {
                string digitString = "";
                bool hasDigits = false;
                
                for (int row = 0; row < lines.Length; row++)
                {
                    char ch = lines[row][c];
                    if (char.IsDigit(ch))
                    {
                        digitString += ch;
                        hasDigits = true;
                    }
                }
                
                if (hasDigits)
                    numbers.Add(long.Parse(digitString));
            }

            long answer = (operation == '+') ? 0 : 1;
            
            foreach (long num in numbers)
            {
                if (operation == '+')
                    answer += num;
                else
                    answer *= num;
            }

            total += answer;
            currentCol = blockEnd + 1;
        }

        return total;
    }
}