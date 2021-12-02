using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices;
using Aoc2021;

namespace Aoc02
{
    public class SolveTest : SolverBase
    {
        private bool enablePartOneTest = true;
        private bool enablePartOne = true;
        private bool enablePartTwoTest = true;
        private bool enablePartTwo = true;

        protected override string PartOne(IEnumerable<string> inputData)
        {
            var position = new Position();
            var parsedLines = ParseLines(inputData);
            foreach (var mover in parsedLines)
            {
                // Console.WriteLine($"Current position: {position.GetPosition()}");
                // Console.WriteLine($"Applying move: {mover.GetMovement()}");
                position.ApplyMovement(mover);
                // Console.WriteLine(($"New position: {position.GetPosition()}"));
                
            }

            var result = position.GetResult();
            return $"{result}";
        }
        
        protected override string PartTwo(IEnumerable<string> inputData)
        {
            var position = new Position();
            var parsedLines = ParseLines(inputData);
            foreach (var mover in parsedLines)
            {
                // Console.WriteLine($"Current position: {position.GetPosition()}");
                // Console.WriteLine($"Applying move: {mover.GetMovement()}");
                position.ApplyAimMovement(mover);
                // Console.WriteLine(($"New position: {position.GetPosition()}"));
                
            }

            var result = position.GetResult();
            return $"{result}";
        }

        private IEnumerable<Movement> ParseLines(IEnumerable<string> inputData)
        {
            var result = new List<Movement>();
            foreach (var line in inputData)
            {
                var splits = line.Split();
                var direction = splits[0];
                var number = Int32.Parse(splits[1]);
                var mover = new Movement(direction, number);
                result.Add(mover);
            }

            return result;
        }

        private class Movement
        {
            public string direction;
            public int number;
            
            public Movement(string direction, int number)
            {
                this.direction = direction;
                this.number = number;
            }

            public string GetMovement()
            {
                return $"{direction} {number}";
            }
        }

        private class Position
        {
            private int horizontal = 0;
            private int vertical = 0;
            private int aim = 0;

            public void ApplyAimMovement(Movement mover)
            {
                switch (mover.direction)
                {
                    case "forward":
                        horizontal += mover.number;
                        vertical += (mover.number * aim);
                        break;
                    case "down":
                        aim += mover.number;
                        break;
                    case "up":
                        aim -= mover.number;
                        break;
                }
            }
            
            public void ApplyMovement(Movement mover)
            {
                switch (mover.direction)
                {
                    case "forward":
                        horizontal += mover.number;
                        break;
                    case "down":
                        vertical += mover.number;
                        break;
                    case "up":
                        vertical -= mover.number;
                        break;
                }
            }

            public string GetPosition()
            {
                return $"horizontal: {horizontal}, depth: {vertical}, aim: {aim}";
            }

            public int GetResult()
            {
                return horizontal * vertical;
            }
        }
        
    }
}