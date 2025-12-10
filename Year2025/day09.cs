using System;
using System.Collections.Generic;
using System.IO;

class day09
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

    static void MainDay09()
    {
        string[] lines = File.ReadAllLines("inputs/day09.txt");
        List<Point> points = new List<Point>();

        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            points.Add(new Point(x, y));
        }

        long maxArea = 0;

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