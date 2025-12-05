using System;
using System.IO;

class Day04
{
    static readonly int[] DX = { -1, -1, -1, 0, 0, 1, 1, 1 };
    static readonly int[] DY = { -1,  0,  1, -1, 1, -1, 0, 1 };

    static void MainDay04(string[] args)
    {
        string inputPath = Path.GetFullPath("inputs/day04.txt");

        string[] lines = File.ReadAllLines(inputPath);
        Console.WriteLine("Part one: " + PartOne(lines));
        Console.WriteLine("Part two: " + PartTwo(lines));
    }

    // -----------------------------------------------------------
    // PART ONE
    // -----------------------------------------------------------
    static int PartOne(string[] lines)
    {
        int rows = lines.Length;
        int cols = lines[0].Length;
        int count = 0;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (lines[r][c] != '@') 
                    continue;

                int neighbors = CountNeighbors(lines, r, c);

                if (neighbors < 4)
                    count++;
            }
        }

        return count;
    }

    // -----------------------------------------------------------
    // PART TWO
    // -----------------------------------------------------------
    static int PartTwo(string[] lines)
    {
        int rows = lines.Length;
        int cols = lines[0].Length;

        // Copy into mutable grid
        char[,] grid = new char[rows, cols];
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                grid[r, c] = lines[r][c];

        int totalRemoved = 0;

        while (true)
        {
            bool removedThisRound = false;
            bool[,] toRemove = new bool[rows, cols];
            int roundRemoved = 0;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (grid[r, c] != '@') 
                        continue;

                    int neighbors = CountNeighbors(grid, r, c);

                    if (neighbors < 4)
                    {
                        toRemove[r, c] = true;
                        removedThisRound = true;
                        roundRemoved++;
                    }
                }
            }

            if (!removedThisRound)
                break;

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (toRemove[r, c])
                        grid[r, c] = '.';

            totalRemoved += roundRemoved;
        }

        return totalRemoved;
    }

    static int CountNeighbors(string[] lines, int r, int c)
    {
        int rows = lines.Length;
        int cols = lines[0].Length;
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            int nr = r + DX[i];
            int nc = c + DY[i];

            if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && lines[nr][nc] == '@')
                count++;
        }

        return count;
    }

    static int CountNeighbors(char[,] grid, int r, int c)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            int nr = r + DX[i];
            int nc = c + DY[i];

            if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && grid[nr, nc] == '@')
                count++;
        }

        return count;
    }
}