using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day08
{
    struct Point
    {
        public int X, Y, Z;
        public Point(int x, int y, int z) { X = x; Y = y; Z = z; }
    }

    struct Connection
    {
        public int FirstPoint, SecondPoint;
        public double Distance;
        public Connection(int first, int second, double distance) 
        { 
            FirstPoint = first; 
            SecondPoint = second; 
            Distance = distance; 
        }
    }

    class UnionFind
    {
        private int[] parent;
        private int[] groupSize;

        public UnionFind(int numberOfPoints)
        {
            parent = new int[numberOfPoints];
            groupSize = new int[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
            {
                parent[i] = i;
                groupSize[i] = 1;
            }
        }

        public int FindRoot(int point)
        {
            if (parent[point] != point)
                parent[point] = FindRoot(parent[point]);
            return parent[point];
        }

        public bool ConnectPoints(int pointA, int pointB)
        {
            pointA = FindRoot(pointA);
            pointB = FindRoot(pointB);
            if (pointA == pointB) return false;

            if (groupSize[pointA] < groupSize[pointB])
            {
                parent[pointA] = pointB;
                groupSize[pointB] += groupSize[pointA];
            }
            else
            {
                parent[pointB] = pointA;
                groupSize[pointA] += groupSize[pointB];
            }
            return true;
        }

        public int GetGroupSize(int point)
        {
            return groupSize[FindRoot(point)];
        }

        public int CountSeparateGroups()
        {
            HashSet<int> uniqueRoots = new HashSet<int>();
            for (int i = 0; i < parent.Length; i++)
            {
                uniqueRoots.Add(FindRoot(i));
            }
            return uniqueRoots.Count;
        }
    }

    static void MainDay08()
    {
        string[] lines = File.ReadAllLines("inputs/day08.txt");
        List<Point> points = new List<Point>();

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            points.Add(new Point(
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                int.Parse(parts[2])
            ));
        }

        int totalPoints = points.Count;
        List<Connection> allConnections = new List<Connection>();

        for (int i = 0; i < totalPoints; i++)
        {
            for (int j = i + 1; j < totalPoints; j++)
            {
                double deltaX = points[i].X - points[j].X;
                double deltaY = points[i].Y - points[j].Y;
                double deltaZ = points[i].Z - points[j].Z;
                double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
                allConnections.Add(new Connection(i, j, distance));
            }
        }

        allConnections.Sort((a, b) => a.Distance.CompareTo(b.Distance));

        // PART ONE

        UnionFind groupTracker = new UnionFind(totalPoints);
        for (int i = 0; i < 1000; i++)
        {
            groupTracker.ConnectPoints(allConnections[i].FirstPoint, allConnections[i].SecondPoint);
        }

        Dictionary<int, int> groupSizes = new Dictionary<int, int>();
        for (int i = 0; i < totalPoints; i++)
        {
            int root = groupTracker.FindRoot(i);
            if (!groupSizes.ContainsKey(root))
                groupSizes[root] = 0;
            groupSizes[root]++;
        }

        var largestThreeGroups = groupSizes.Values.OrderByDescending(size => size).Take(3).ToArray();
        long partOneAnswer = 1;
        foreach (var size in largestThreeGroups)
            partOneAnswer *= size;

        Console.WriteLine("Part One: " + partOneAnswer);

        // PART TWO

        UnionFind groupTracker2 = new UnionFind(totalPoints);
        Connection finalConnection = new Connection();

        foreach (var connection in allConnections)
        {
            if (groupTracker2.ConnectPoints(connection.FirstPoint, connection.SecondPoint))
            {
                finalConnection = connection;
                if (groupTracker2.CountSeparateGroups() == 1)
                {
                    break;
                }
            }
        }

        int xCoord1 = points[finalConnection.FirstPoint].X;
        int xCoord2 = points[finalConnection.SecondPoint].X;
        long partTwoAnswer = (long)xCoord1 * xCoord2;

        Console.WriteLine("Part Two: " + partTwoAnswer);
    }
}