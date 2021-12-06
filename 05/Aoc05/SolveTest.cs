using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Aoc2021;
using Microsoft.VisualBasic.CompilerServices;

namespace Aoc05
{
    public class SolveTest : SolverBase
    {
        private bool enablePartOneTest = true;
        private bool enablePartOne = false;
        private bool enablePartTwoTest = false;
        private bool enablePartTwo = false;

        private int rowCount;
        private int colCount;

        private List<ArrayNode> lineList;
        private int dangerAreas = 0;
        private List<ArrayNode> dangerZones;

        protected override string PartOne(IEnumerable<string> inputData)
        {
            lineList = new List<ArrayNode>();
            dangerZones = new List<ArrayNode>();
            /*
            var one = new ArrayNode(3, 2);
            var two = new ArrayNode(3, 2);
            // var wtf = one.Equals(two);
            var wtf = one == two;
            Console.WriteLine($"equals? {wtf}");
            var sigh = new List<ArrayNode>();
            sigh.Add(one);
            Console.WriteLine($"contains? {sigh.Contains(two)}");
            */
            
            foreach (var line in inputData)
            {
                ParseLine(line);
            }
            // DrawMap();
            // Console.WriteLine($"checking {lineList.Count()} nodes");
            var count = lineList.FindAll(x => x.concentration > 1).Count();
            return count.ToString();
            return dangerAreas.ToString();
        }
        
        protected override string PartTwo(IEnumerable<string> inputData)
        {
            lineList = new List<ArrayNode>();
            dangerZones = new List<ArrayNode>();
            foreach (var line in inputData)
            {
                ParseLine(line);
            }
            var count = lineList.FindAll(x => x.concentration > 1).Count();
            // DrawMap();
            return count.ToString();
        }

        private void DrawMap()
        {
            for (var i = 0; i <= colCount; i++)
            {
                var row = "";
                for (var j = 0; j <= rowCount; j++)
                {
                    var count = 0;
                    foreach (var node in lineList)
                    {
                        if (i == node.col && j == node.row)
                        {
                            count++;
                        }
                    }

                    switch (count)
                    {
                        case 0:
                            row += ".";
                            break;
                        default:
                            row += count.ToString();
                            break;
                    }

                }
                Console.WriteLine(row);
            }
        }

        private void ParseLine(string line)
        {
            var firstSpace = line.IndexOf(" ");
            var secondSpace = line.LastIndexOf(" ");
            var firstString = line.Substring(0, firstSpace);
            var secondString = line.Substring(secondSpace + 1);

            var firstNode = NodeFromString(firstString);
            var secondNode = NodeFromString(secondString);

            var nodeList = firstNode.FindPathToNode(secondNode);
            foreach (var node in nodeList)
            {
                if (lineList.Contains(node))
                {
                    var index = lineList.IndexOf(node);
                    lineList[index].AddConcentration();
                }
                else
                {
                    lineList.Add(node);
                }
            }
        }

        private ArrayNode NodeFromString(string line)
        {
            
            var splits = line.Split(",").ToList();
            var node = new ArrayNode(Int32.Parse(splits[0]), Int32.Parse(splits[1]));

            rowCount = node.row > rowCount ? node.row : rowCount;
            colCount = node.col > colCount ? node.col : colCount;

            return node;
        }
        
    }
    

    public class ArrayNode : IEquatable<ArrayNode>
    {
        public int row;
        public int col;
        public int concentration = 1;
        
        public ArrayNode(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public List<ArrayNode> FindPathToNode(ArrayNode otherNode)
        {
            var line = new List<ArrayNode>();
            
            if (row == otherNode.row)
            {
                var cols = FindInBetweens(col, otherNode.col);
                foreach (var num in cols)
                {
                    var newNode = new ArrayNode(row, num);
                    line.Add(newNode);
                }

            }
            else if (col == otherNode.col)
            {
                var rows = FindInBetweens(row, otherNode.row);
                foreach (var num in rows)
                {
                    var newNode = new ArrayNode(num, col);
                    line.Add(newNode);
                }

            }
            else
            {
                line = CheckDiag(this, otherNode);
                // Console.WriteLine($"found {line.Count()} diagonals");
            }

            if (line.Count() > 0)
            {
                // Console.WriteLine($"finding path between {this.ToString()} and {otherNode.ToString()}");
                var pathString = string.Join(" -> ", line);
                // Console.WriteLine($"path found: {pathString}");
            }

            return line;
        }

        private List<ArrayNode> CheckDiag(ArrayNode node1, ArrayNode node2)
        {
            var startNode = node1.row < node2.row ? node1 : node2;
            var endNode = startNode == node1 ? node2 : node1;

            var colDiff = startNode.col - endNode.col;
            var rowDiff = startNode.row - endNode.row;
            
            var diagList = new List<ArrayNode>();
            // Console.WriteLine($"diff between {colDiff} and {rowDiff} is abs diff is same {Math.Abs(rowDiff) == Math.Abs(colDiff)}");

            if (Math.Abs(rowDiff) != Math.Abs(colDiff))
            {
                return diagList;
            }

            // Console.WriteLine("moving on...");
            var newCol = startNode.col;
            var newRow = startNode.row;

            var rows = FindInBetweens(startNode.row, endNode.row);
            var cols = FindInBetweens(startNode.col, endNode.col);

            if (rows[0] != startNode.row)
            {
                rows.Reverse();
            }

            if (cols[0] != startNode.col)
            {
                cols.Reverse();
            }
            
            for(var i = 0; i < rows.Count(); i++)
            {
                diagList.Add(new ArrayNode(rows[i], cols[i]));
            }

            if (diagList.Count() > 0)
            {
                var diagLine = string.Join(" -> ", diagList);
                // Console.WriteLine(diagLine);
            }

            return diagList;
        }

        public bool Equals(ArrayNode otherNode)
        {
            // Console.WriteLine($"does this row {row} == {otherNode.row}? {row == otherNode.row}");
            // Console.WriteLine($"does this col {col} == {otherNode.col}? {col == otherNode.col}");
            var same = row == otherNode.row && col == otherNode.col;
            // Console.WriteLine($"returning {same}");
            return same;
        }

        public void AddConcentration()
        {
            concentration++;
        }

        private List<int> FindInBetweens(int firstInt, int secondInt)
        {
            // Console.WriteLine($"finding inbetweens for {firstInt} to {secondInt}");
            var result = new List<int>();
            var start = firstInt < secondInt ? firstInt : secondInt;
            var finish = start == firstInt ? secondInt : firstInt;
            // Console.WriteLine($"going to range from {start} to {finish + 1}");
            for (var i = start; i <= finish; i++)
            {
                // Console.WriteLine(i.ToString());
                result.Add(i);
            }

            return result;
        }

        public override string ToString()
        {
            return $"{row}, {col}";
        }
    }
}