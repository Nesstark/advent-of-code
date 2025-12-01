using System;
using System.IO;
class Program
{
    static void Main()
    {
        string inputPath = Path.GetFullPath("inputs/day01.txt");
        string[] lines = File.ReadAllLines(inputPath);
        Console.WriteLine($"Loaded {lines.Length} lines from day01.txt.");
        
        // ----------------
        // Part 1
        // ----------------
        int dial1 = 50;
        int zeroCount1 = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            char direction = line[0];                 
            int amount = int.Parse(line.Substring(1)); 
            if (direction == 'L')
            {
                dial1 = (dial1 - amount) % 100;
                if (dial1 < 0) dial1 += 100;
            }
            else
            {
                dial1 = (dial1 + amount) % 100;
            }
            if (dial1 == 0)
                zeroCount1++;
        }
        Console.WriteLine($"Part One Password: {zeroCount1}");
        
        // ----------------
        // Part 2
        // ----------------
        int dial2 = 50;
        int zeroCount2 = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            char direction = line[0];
            int amount = int.Parse(line.Substring(1));
            int oldDial = dial2;
            
            if (direction == 'L')
            {
                dial2 = (oldDial - amount) % 100;
                if (dial2 < 0) dial2 += 100;
                
                if (oldDial == 0)
                {
                    zeroCount2 += amount / 100;
                }
                else
                {
                    if (amount >= oldDial)
                    {
                        zeroCount2 += 1 + (amount - oldDial) / 100;
                    }
                }
            }
            else
            {
                dial2 = (oldDial + amount) % 100;
                
                if (oldDial == 0)
                {
                    zeroCount2 += amount / 100;
                }
                else
                {
                    int stepsToZero = 100 - oldDial;
                    if (amount >= stepsToZero)
                    {
                        zeroCount2 += 1 + (amount - stepsToZero) / 100;
                    }
                }
            }
        }
        Console.WriteLine($"Part Two Password: {zeroCount2}");
    }
}