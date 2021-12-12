using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Xml.Linq;
using Aoc2021;
using Microsoft.VisualBasic;

namespace Aoc11
{
    public class SolveTest : SolverBase
    {
        public override bool enablePartOneTest { get { return false; } }
        public override bool enablePartOne { get { return false; } }
        public override bool enablePartTwoTest { get { return true; } }
        public override bool enablePartTwo { get { return true; } }

        private List<List<Octopus>> grid;
        protected override string PartOne(IEnumerable<string> inputData)
        {
            int turns = 100;
            ParseGrid(inputData.ToList());

            int totalFlashes = 0;
            
            for (var i = 0; i < turns; i++)
            {
                // Console.WriteLine();
                // PrintGrid();
                foreach (var row in grid)
                {
                    foreach (var col in row)
                    {
                        col.locked = false;
                        col.IncrementEnergy();
                    }
                }

                totalFlashes += ProcessSteps();
            }
            return totalFlashes.ToString();
        }
        
        protected override string PartTwo(IEnumerable<string> inputData)
        {
            int turns = 0;
            ParseGrid(inputData.ToList());

            
            while (true)
            {
                turns++;
                int totalFlashes = 0;
                foreach (var row in grid)
                {
                    foreach (var col in row)
                    {
                        col.locked = false;
                        col.IncrementEnergy();
                    }
                }

                totalFlashes += ProcessSteps();
                if (totalFlashes == grid.Count * grid[0].Count)
                {
                    break;
                }
            }
            return turns.ToString();
        }

        private int ProcessSteps()
        {
            var neighborsToIncr = new List<Cell>();
            var firstRun = true;
            var flashedThisRound = new List<Cell>();
            var flashes = 0;
            
            while (neighborsToIncr.Count > 0 || firstRun)
            {
                firstRun = false;
                foreach (var neighbor in neighborsToIncr)
                { 
                    grid[neighbor.row][neighbor.col].IncrementEnergy();
                }
                neighborsToIncr = new List<Cell>();
                
                foreach (var row in grid)
                {
                    foreach (var col in row)
                    {
                        var neighborDict = col.Step();
                        
                        if (neighborDict.Count > 0)
                        {
                            flashes++;
                            flashedThisRound.Add(col.cell);
                        }
                        
                        foreach (var neighbor in neighborDict.Values)
                        {
                            neighborsToIncr.Add(neighbor);
                        }
                    }
                }
            }

            return flashes;
        }

        private void ParseGrid(List<string> inputData)
        {
            int rowMax = inputData.Count - 1;
            int colMax = inputData[0].Length - 1;
            grid = new List<List<Octopus>>();

            for (var i = 0; i <= rowMax; i++)
            {
                string row = inputData[i];
                grid.Add(new List<Octopus>());
                
                for (var j = 0; j <= colMax; j++)
                {
                    int col = j;
                    int energy = Int32.Parse(row[j].ToString());
                    grid[i].Add(new Octopus(i, j, energy, rowMax, colMax));
                }
            }
        }

        public void PrintGrid()
        {
            foreach (var row in grid)
            {
                foreach (var col in row)
                {
                    Console.Write(col.ToString());
                }
                Console.WriteLine();
            }
        }
    }

    public class Octopus : Node
    {
        private int energy;
        public bool locked = false;

        public Octopus(int row, int col, int energy, int rowMax, int colMax) :
            base(row, col, rowMax, colMax, true)
        {
            this.energy = energy;
        }

        public Dictionary<direction, Cell> Step()
        {
            if (energy > 9)
            {
                Flash();
                return GetNodeNeighbors();
            }

            return new Dictionary<direction, Cell>();
        }

        public void Flash()
        {
            energy = 0;
            locked = true;
        }

        public void IncrementEnergy()
        {
            if (!locked)
            {
                energy++;
            }
        }

        public override string ToString()
        {
            return energy.ToString();
        }
    }

    public class Node
    {
        public int row;
        public int col;
        internal int rowMax;
        internal int colMax;
        public Cell cell;

        public enum direction
        {
            up,
            down,
            left,
            right,
            upLeft,
            upRight,
            downLeft,
            downRight
        }
        internal Dictionary<direction, Cell> neighbors;
        
        public bool diagonalNeighbors { get; set; }

        public Node(int row, int col, int rowMax, int colMax, bool diagonalNeighbors = false)
        {
            this.row = row;
            this.col = col;
            this.rowMax = rowMax;
            this.colMax = colMax;
            this.diagonalNeighbors = diagonalNeighbors;
            this.cell = new Cell(row, col);
            neighbors = new Dictionary<direction, Cell>();
        }

        public virtual Dictionary<direction, Cell> GetNodeNeighbors()
        {
            if (neighbors.Count > 0)
            {
                return neighbors;
            }

            bool firstRow = row == 0;
            bool lastRow = row == rowMax;
            bool firstCol = col == 0;
            bool lastCol = col == colMax;

            if (!firstRow)
            {
                neighbors[direction.up] = new Cell(row - 1, col);
                if (diagonalNeighbors)
                {
                    if (!firstCol)
                    {
                        neighbors[direction.upLeft] = new Cell(row - 1, col - 1);
                    }

                    if (!lastCol)
                    {
                        neighbors[direction.upRight] = new Cell(row - 1, col + 1);
                    }
                }

            }
            
            if (!firstCol)
            {
                neighbors[direction.left] = new Cell(row, col - 1);
            }

            if (!lastCol)
            {
                
                neighbors[direction.right] = new Cell(row, col + 1);
            }
            
            if (!lastRow)
            {
                neighbors[direction.down] = new Cell(row + 1, col);
                if (diagonalNeighbors)
                {
                    if (!firstCol)
                    {
                        neighbors[direction.downLeft] = new Cell(row + 1, col - 1);
                    }

                    if (!lastCol)
                    {
                        neighbors[direction.downRight] = new Cell(row + 1, col + 1);
                    }
                }

            }

            return neighbors;
        }
    }

    public class Cell : IEquatable<Cell>
    {
        public int row;
        public int col;
        
        public Cell(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public bool Equals(Cell other)
        {
            return row == other.row && col == other.col;
        }
    }
}