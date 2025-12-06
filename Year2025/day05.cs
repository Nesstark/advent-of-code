using System;
using System.Collections.Generic;
using System.IO;

class Day05
{
    struct Range
    {
        public long Start;
        public long End;
        public Range(long start, long end) { Start = start; End = end; }
    }

    static void MainDay05()
    {
        string inputPath = Path.GetFullPath("inputs/day05.txt");

        ParseInput(inputPath, out List<Range> ranges, out List<long> ids);

        ranges.Sort((a, b) => a.Start.CompareTo(b.Start));
        List<Range> merged = MergeRanges(ranges);

        // Part one
        int freshCount = 0;
        foreach (var id in ids)
            if (IsFresh(id, merged))
                freshCount++;

        // Part two
        long totalFreshIDs = 0;
        foreach (var r in merged)
            totalFreshIDs += (r.End - r.Start + 1);

        Console.WriteLine(
            $"Part One: Fresh ingredient IDs in list = {freshCount}\n" +
            $"Part Two: Total fresh ingredient IDs = {totalFreshIDs}"
        );
    }

    static void ParseInput(string path, out List<Range> ranges, out List<long> ids)
    {
        ranges = new List<Range>();
        ids = new List<long>();

        bool readingRanges = true;
        foreach (var line in File.ReadAllLines(path))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                readingRanges = false;
                continue;
            }

            if (readingRanges)
            {
                var parts = line.Split('-');
                long s = long.Parse(parts[0]);
                long e = long.Parse(parts[1]);
                ranges.Add(new Range(s, e));
            }
            else
            {
                ids.Add(long.Parse(line));
            }
        }
    }

    static List<Range> MergeRanges(List<Range> input)
    {
        var merged = new List<Range>();
        if (input.Count == 0) return merged;

        Range current = input[0];

        for (int i = 1; i < input.Count; i++)
        {
            var r = input[i];

            if (r.Start <= current.End + 1)
            {
                current.End = Math.Max(current.End, r.End);
            }
            else
            {
                merged.Add(current);
                current = r;
            }
        }

        merged.Add(current);
        return merged;
    }

    static bool IsFresh(long id, List<Range> ranges)
    {
        int left = 0;
        int right = ranges.Count - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            var r = ranges[mid];

            if (id < r.Start) right = mid - 1;
            else if (id > r.End) left = mid + 1;
            else return true;
        }

        return false;
    }
}