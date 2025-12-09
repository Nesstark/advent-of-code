using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    static void Main()
    {
        // Read all lines from the input file
        string[] lines = File.ReadAllLines("inputs/day09.txt");

        // Store all points
        List<Point> points = new List<Point>();

        // Parse each line like "98139,50134"
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            points.Add(new Point(x, y));
        }

        long maxArea = 0;

        // Compare every point with every other point
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                long width = Math.Abs(points[i].X - points[j].X);
                long height = Math.Abs(points[i].Y - points[j].Y);
                long area = width * height;

                if (area > maxArea)
                    maxArea = area;
            }
        }

        Console.WriteLine("Largest rectangle area: " + maxArea);
    }
}