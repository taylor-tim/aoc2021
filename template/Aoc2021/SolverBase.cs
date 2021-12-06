using System;
using System.Collections.Generic;

namespace Aoc2021
{
    public abstract class SolverBase
    {
        
        private bool enablePartOneTest = false;
        private bool enablePartOne = false;
        private bool enablePartTwoTest = false;
        private bool enablePartTwo = true;

        private enum parts
        {
            one,
            two
        };

        private IEnumerable<string> testData;
        private IEnumerable<string> puzzleData;

        protected SolverBase()
        {
            testData = FileHandler.GetTestData();
            puzzleData = FileHandler.GetPuzzleData();
        }

        protected abstract string PartOne(IEnumerable<string> inputData);

        protected abstract string PartTwo(IEnumerable<string> inputData);

        public void Solve()
        {
            if (enablePartOneTest)
            {
                RunTest(parts.one, true);
            }
            if (enablePartOne)
            {
                RunTest(parts.one, false);
            }
            if (enablePartTwoTest)
            {
                RunTest(parts.two, true);
            }
            if (enablePartTwo)
            {
                RunTest(parts.two, false);
            }
        }

        private void RunTest(parts part, bool isTest)
        {
            var dataSet = isTest ? testData : puzzleData;
            var testLine = isTest ? "Test" : "Puzzle";
            string answer;
            string partLine;
            
            Console.WriteLine($"Beginning Part {part} - {testLine} Data...");

            switch (part)
            {
                case parts.one:
                    answer = PartOne(dataSet);
                    break;
                
                case parts.two:
                    answer = PartTwo(dataSet);
                    break;
                
                default:
                    throw new NotImplementedException("No matching part!");
            }
            
            Console.WriteLine($"Part {part} - {testLine} Data Answer: {answer}");
        }
    }
}