using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static List<int[]> FindAllSolutions(int[][] equations, int[] targetValues)
    {
        int numEquations = equations.Length;
        int numVariables = equations[0].Length;
        
        int[][] matrix = equations.Select(row => row.ToArray()).ToArray();
        int[] results = targetValues.ToArray();
        
        int[] pivotPositions = new int[numVariables];
        for (int i = 0; i < numVariables; i++)
            pivotPositions[i] = -1;
        
        int currentRow = 0;
        
        for (int col = 0; col < numVariables && currentRow < numEquations; col++)
        {
            int pivotRow = FindPivotRow(matrix, currentRow, numEquations, col);
            
            if (pivotRow == -1) 
                continue;
            
            SwapRows(matrix, results, currentRow, pivotRow);
            pivotPositions[col] = currentRow;
            
            EliminateColumn(matrix, results, currentRow, col, numEquations, numVariables);
            
            currentRow++;
        }
        
        if (HasNoSolution(matrix, results, numEquations))
            return new List<int[]>();
        
        List<int> freeVariables = GetFreeVariables(pivotPositions, numVariables);
        
        return GenerateAllSolutions(matrix, results, pivotPositions, freeVariables, numVariables);
    }
    
    static int FindPivotRow(int[][] matrix, int startRow, int numEquations, int col)
    {
        for (int i = startRow; i < numEquations; i++)
        {
            if (matrix[i][col] == 1)
                return i;
        }
        return -1;
    }
    
    static void SwapRows(int[][] matrix, int[] results, int row1, int row2)
    {
        (matrix[row1], matrix[row2]) = (matrix[row2], matrix[row1]);
        (results[row1], results[row2]) = (results[row2], results[row1]);
    }
    
    static void EliminateColumn(int[][] matrix, int[] results, int pivotRow, int col, int numEquations, int numVariables)
    {
        for (int i = 0; i < numEquations; i++)
        {
            if (i == pivotRow || matrix[i][col] == 0)
                continue;
            
            for (int j = col; j < numVariables; j++)
                matrix[i][j] ^= matrix[pivotRow][j];
            
            results[i] ^= results[pivotRow];
        }
    }
    
    static bool HasNoSolution(int[][] matrix, int[] results, int numEquations)
    {
        for (int i = 0; i < numEquations; i++)
        {
            bool allZeros = matrix[i].All(x => x == 0);
            if (allZeros && results[i] == 1)
                return true;
        }
        return false;
    }
    
    static List<int> GetFreeVariables(int[] pivotPositions, int numVariables)
    {
        List<int> freeVars = new List<int>();
        for (int i = 0; i < numVariables; i++)
        {
            if (pivotPositions[i] == -1)
                freeVars.Add(i);
        }
        return freeVars;
    }
    
    static List<int[]> GenerateAllSolutions(int[][] matrix, int[] results, int[] pivotPositions, List<int> freeVariables, int numVariables)
    {
        List<int[]> allSolutions = new List<int[]>();
        int numCombinations = 1 << freeVariables.Count;
        
        for (int combination = 0; combination < numCombinations; combination++)
        {
            int[] solution = new int[numVariables];
            
            for (int i = 0; i < freeVariables.Count; i++)
            {
                int varIndex = freeVariables[i];
                solution[varIndex] = (combination >> i) & 1;
            }
            
            for (int varIndex = 0; varIndex < numVariables; varIndex++)
            {
                if (pivotPositions[varIndex] == -1)
                    continue;
                
                int row = pivotPositions[varIndex];
                int value = results[row];
                
                for (int j = varIndex + 1; j < numVariables; j++)
                    value ^= (matrix[row][j] & solution[j]);
                
                solution[varIndex] = value;
            }
            
            allSolutions.Add(solution);
        }
        
        return allSolutions;
    }
    
    static int SolveMachine(string targetState, List<int[]> buttons)
    {
        int numLights = targetState.Length;
        int numButtons = buttons.Count;
        
        int[][] equations = new int[numLights][];
        for (int i = 0; i < numLights; i++)
            equations[i] = new int[numButtons];
        
        for (int buttonIndex = 0; buttonIndex < numButtons; buttonIndex++)
        {
            foreach (int lightIndex in buttons[buttonIndex])
            {
                equations[lightIndex][buttonIndex] = 1;
            }
        }
        
        int[] targetValues = targetState.Select(c => c == '#' ? 1 : 0).ToArray();
        
        var solutions = FindAllSolutions(equations, targetValues);
        
        int minPresses = int.MaxValue;
        foreach (var solution in solutions)
        {
            int presses = solution.Sum();
            minPresses = Math.Min(minPresses, presses);
        }
        
        return minPresses;
    }
    
    static void Main()
    {
        int totalPresses = 0;
        
        foreach (var line in File.ReadAllLines("inputs/day10.txt"))
        {
            if (string.IsNullOrWhiteSpace(line)) 
                continue;
            
            string indicator = line.Split('[')[1].Split(']')[0];
            
            var buttonConfigs = line.Split('(')
                                    .Skip(1)
                                    .Select(x => x.Split(')')[0])
                                    .TakeWhile(x => !x.Contains("{"))
                                    .ToList();
            
            List<int[]> buttons = new List<int[]>();
            foreach (var config in buttonConfigs)
            {
                int[] affectedLights = config.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                             .Select(int.Parse)
                                             .ToArray();
                buttons.Add(affectedLights);
            }
            
            int result = SolveMachine(indicator, buttons);
            totalPresses += result;
        }
        
        Console.WriteLine("Part One: " + totalPresses);
    }
}