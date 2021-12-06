using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aoc2021;

namespace Aoc04
{
    public class SolveTest : SolverBase
    {
        private bool enablePartOneTest = true;
        private bool enablePartOne = false;
        private bool enablePartTwoTest = false;
        private bool enablePartTwo = false;

        private List<int> DrawNums = new List<int>();
        private List<BingoBoard> Boards = new List<BingoBoard>();

        protected override string PartOne(IEnumerable<string> inputData)
        {
            int winSum = 0;
            ParseInputData(inputData.ToList());

            foreach (var board in Boards)
            {
                // board.PrintBoard();
            }

            foreach (var num in DrawNums)
            {
                foreach (var board in Boards)
                {
                    board.CheckNum(num);
                    if (board.HasWon())
                    {
                        var total = board.UnmarkedSum();
                        winSum = total * num;
                        Console.WriteLine("Winning board:");
                        board.PrintBoard();
                        return winSum.ToString();
                    }
                }
            }
            return winSum.ToString();
        }
        
        protected override string PartTwo(IEnumerable<string> inputData)
        {
            int winSum = 0;
            ParseInputData(inputData.ToList());

            foreach (var board in Boards)
            {
                // board.PrintBoard();
            }

            var count = 0;
            var max = 0;
            var resline = "";
            foreach (var board in Boards)
            {
                var numcount = 0;
                foreach (var num in DrawNums)
                {
                    numcount++;
                    board.CheckNum(num);
                    if (board.HasWon())
                    {
                        var total = board.UnmarkedSum();
                        winSum = total * num;
                        if (numcount > max)
                        {
                            max = numcount;
                            resline = $"{count}: sum: {winSum} won in {numcount} turns";
                        }
                        break;
                    }
                }
                count++;
                numcount = 0;
            }
            Console.WriteLine(resline);
            return winSum.ToString();
        }

        protected void ParseInputData(List<string> inputData)
        {
            ParseDrawNums(inputData[0]);

            var board = new List<string>();
            for (var i = 2; i < inputData.Count; i++)
            {
                var line = inputData[i];
                if (line == "")
                {
                    Boards.Add(new BingoBoard(board));
                    board = new List<string>();
                }
                else
                {
                    board.Add(line);
                }

                if (i == inputData.Count - 1)
                {
                    Boards.Add(new BingoBoard(board));
                }
            }
        }
        

        protected void ParseDrawNums(string toParse)
        {
            foreach (var num in toParse.Split(","))
            {
                DrawNums.Add(Int32.Parse(num));
            }
        }
    }
}