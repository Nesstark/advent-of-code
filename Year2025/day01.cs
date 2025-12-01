using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Load all instructions from file
        string inputPath = Path.GetFullPath("inputs/day01.txt");
        string[] lines = File.ReadAllLines(inputPath);

        // Debug print
        Console.WriteLine($"Loaded {lines.Length} lines from day01.txt.");

        int dial = 50;       // Starting position
        int zeroCount = 0;   // Count of times dial ends up at 0

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            char direction = line[0];                 // 'L' or 'R'
            int amount = int.Parse(line.Substring(1)); // number after direction

            if (direction == 'L')
            {
                dial = (dial - amount) % 100;
                if (dial < 0) dial += 100;
            }
            else // direction == 'R'
            {
                dial = (dial + amount) % 100;
            }

            if (dial == 0)
                zeroCount++;
        }

        Console.WriteLine($"Password: {zeroCount}");
    }
}
