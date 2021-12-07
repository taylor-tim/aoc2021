using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2021
{
    public class Utils
    {
        public List<int> CommaStringToInts(string inputString)
        {
            var splits = inputString.Split(",").ToList();
            var asInts = splits.Select(x => Int32.Parse(x));

            return asInts.ToList();
        }
    }
}