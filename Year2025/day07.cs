using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

class Day07
{
    static void MainDay07()
    {
        string[] mapLines = File.ReadAllLines("inputs/day07.txt");
        
        Console.WriteLine("Part One: " + CountSplits(mapLines));
        Console.WriteLine("Part Two: " + CountTotalPaths(mapLines));
    }

    // Part one
    static int CountSplits(string[] map)
    {
        int totalRows = map.Length;
        int totalColumns = map[0].Length;
        
        int startRow = -1;
        int startColumn = -1;
        
        for (int row = 0; row < totalRows; row++)
        {
            startColumn = map[row].IndexOf('S');
            if (startColumn != -1)
            {
                startRow = row;
                break;
            }
        }
        
        Queue<(int row, int col)> positionsToCheck = new Queue<(int, int)>();
        positionsToCheck.Enqueue((startRow + 1, startColumn));
        
        HashSet<(int, int)> visitedPositions = new HashSet<(int, int)>();
        
        int splitCount = 0;
        
        while (positionsToCheck.Count > 0)
        {
            var (currentRow, currentCol) = positionsToCheck.Dequeue();
            
            if (currentRow < 0 || currentRow >= totalRows || 
                currentCol < 0 || currentCol >= totalColumns)
            {
                continue;
            }
            
            if (!visitedPositions.Add((currentRow, currentCol)))
            {
                continue;
            }
            
            char currentChar = map[currentRow][currentCol];
            
            if (currentChar == '^')
            {
                splitCount++;
                positionsToCheck.Enqueue((currentRow, currentCol - 1));
                positionsToCheck.Enqueue((currentRow, currentCol + 1));
            }
            else
            {
                positionsToCheck.Enqueue((currentRow + 1, currentCol));
            }
        }
        
        return splitCount;
    }

    // Part two
    static BigInteger CountTotalPaths(string[] map)
    {
        int totalRows = map.Length;
        int totalColumns = map[0].Length;
        
        int startRow = -1;
        int startColumn = -1;
        
        for (int row = 0; row < totalRows; row++)
        {
            startColumn = map[row].IndexOf('S');
            if (startColumn != -1)
            {
                startRow = row;
                break;
            }
        }
        
        int firstRow = startRow + 1;
        
        if (firstRow < 0 || firstRow >= totalRows)
        {
            return BigInteger.One;
        }
        
        Dictionary<(int, int), BigInteger> savedResults = new Dictionary<(int, int), BigInteger>();
        
        HashSet<(int, int)> currentlyVisiting = new HashSet<(int, int)>();
        
        BigInteger CountPathsFrom(int row, int col)
        {
            if (row < 0 || row >= totalRows || col < 0 || col >= totalColumns)
            {
                return BigInteger.One;
            }
            
            var position = (row, col);
            
            if (savedResults.ContainsKey(position))
            {
                return savedResults[position];
            }
            
            if (!currentlyVisiting.Add(position))
            {
                throw new InvalidOperationException("Cycle detected - infinite loop in the map!");
            }
            
            char currentChar = map[row][col];
            BigInteger pathCount;
            
            if (currentChar == '^')
            {
                BigInteger leftPaths = CountPathsFrom(row, col - 1);
                BigInteger rightPaths = CountPathsFrom(row, col + 1);
                pathCount = leftPaths + rightPaths;
            }
            else
            {
                pathCount = CountPathsFrom(row + 1, col);
            }
            
            currentlyVisiting.Remove(position);
            
            savedResults[position] = pathCount;
            
            return pathCount;
        }
        
        return CountPathsFrom(firstRow, startColumn);
    }
}