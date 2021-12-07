using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aoc2021;

namespace Aoc07
{
    public class SolveTest : SolverBase
    {
        public override bool enablePartOneTest { get { return true; } }
        public override bool enablePartOne { get { return true; } }
        public override bool enablePartTwoTest { get { return true; } }
        public override bool enablePartTwo { get { return true; } }

        protected override string PartOne(IEnumerable<string> inputData)
        {
            return SolveIt(inputData, true);
        }
        
        protected override string PartTwo(IEnumerable<string> inputData)
        {
            return SolveIt(inputData, false);
        }

        private string SolveIt(IEnumerable<string>inputData, bool partOne)
        {
            var minFuel = 0;
            var crabPositions = new List<int>();
            foreach (var num in inputData.ToList()[0].Split(","))
            {
                crabPositions.Add(Int32.Parse(num));
            }
            var maxPosition = crabPositions.Max();
            
            for (var i = 0; i <= maxPosition; i++)
            {
                var fuelUsed = 0;
                for (var j = 0; j < crabPositions.Count; j++)
                {
                    var checkPosition = crabPositions[j];
                    var newFuel = Math.Abs(i - checkPosition);
                    if (partOne)
                    {
                        fuelUsed += newFuel;
                    }
                    else
                    {
                        var adjustedFuel = Enumerable.Range(0, newFuel + 1).Aggregate(0, (acc, x) => acc + x);
                        fuelUsed += adjustedFuel;
                    }
                }

                minFuel = (minFuel == 0 || fuelUsed < minFuel) ? fuelUsed : minFuel;
            }
            return minFuel.ToString();
        }
    }
}