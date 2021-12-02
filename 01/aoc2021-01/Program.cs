using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace aoc2021_01
{
    class Program
    {
        static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }

        static List<int> GetData()
        {
            string text = System.IO.File.ReadAllText(@"T:\git_checkouts\personal\aoc2021\01\aoc2021-01\testdata.txt");
            string[] splits = text.Split("\n");
            var dataAsInts = new List<int>();
            foreach (var line in splits)
            {
                dataAsInts.Add(Int16.Parse(line));
            }

            return dataAsInts;
        }

        static void PartOne()
        {
            List<int> values = GetData();
            bool firstIter = true;
            int prevValue = 0;
            var incCount = 0;
            
            foreach (var number in values)
            {
                var thisValue = number;
                
                if (firstIter)
                {
                    firstIter = false;
                    prevValue = thisValue;
                    continue;
                }

                if (thisValue > prevValue)
                {
                    incCount++;
                }

                prevValue = thisValue;
            }
            Console.WriteLine($"Part One Answer: {incCount}");
        }

        static void PartTwo()
        {
            List<int> values = GetData();
            // values = GetTestData();
            int prevValue = values[0] + values[1] + values[2];
            int thisValue = 0;
            int incCount = 0;
                
            for (var i = 1; i < values.Count - 2; i++)
            {
                thisValue = values[i] + values[i + 1] + values [i + 2];
                Console.WriteLine($"comparing {i}: {values[i]} {values[i + 1]} {values[i + 2]} ({thisValue}) to prev Value: {prevValue}");
                if (thisValue > prevValue)
                {
                    incCount++;
                    Console.WriteLine("it's bigger");
                }

                prevValue = thisValue;
            }
            
            Console.WriteLine($"Part Two Answer: {incCount}");
        }

        static List<int> GetTestData()
        {
            return new List<int>
            {
                199,
                200,
                208,
                210,
                200,
                207,
                240,
                269,
                260,
                263
            };
        }
    }
}