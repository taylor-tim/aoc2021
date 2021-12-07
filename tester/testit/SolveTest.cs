using System.Collections.Generic;
using Aoc2021;

namespace testit
{
    public class SolveTest : SolverBase
    {
        public override bool enablePartOneTest { get { return false; } }
        public override bool enablePartOne { get { return false; } }
        public override bool enablePartTwoTest { get { return true; } }
        public override bool enablePartTwo { get { return true; } }
        protected override string PartOne(IEnumerable<string> inputData)
        {
            return "hi";
        }
        
        protected override string PartTwo(IEnumerable<string> inputData)
        {
            return "hi";
        }
    }
}