using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Aoc2021;
using Microsoft.VisualBasic.FileIO;

namespace Aoc03
{
    public class SolveTest : SolverBase
    {
        private bool enablePartOneTest = true;
        private bool enablePartOne = false;
        private bool enablePartTwoTest = false;
        private bool enablePartTwo = false;

        protected override string PartOne(IEnumerable<string> inputData)
        {
            var report = new DiagReport(inputData.ToList());
            report.ProcessReport();
            
            return report.result.ToString();
        }
        
        protected override string PartTwo(IEnumerable<string> inputData)
        {
            var report = new DiagReport(inputData.ToList());
            var result = report.O2Report();
            
            return result.ToString();
        }
        
    }

    public class DiagReport
    {
        private int[,] reportArray;
        private int rowCount;
        private int colCount;
        public string gamma = "";
        public string epsilon = "";
        public int result;
        private List<string> inputReport;
        
        public DiagReport(List<string> inputReport)
        {
            this.inputReport = inputReport;
            rowCount = inputReport.Count();
            colCount = inputReport.First().Length;
            reportArray = new int[rowCount,colCount];

            for (var i = 0; i < rowCount; i++)
            {
                var row = inputReport[i];
                for (var j = 0; j < row.Length; j++)
                {
                    int col = Int32.Parse(row[j].ToString());
                    reportArray[i, j] = col;
                }
            }
        }

        public void ProcessReport()
        {
            SetGamma();
            SetEpsilon();
            var gammaInt = BinaryToDecimal(gamma);
            // Console.WriteLine($"gammaInt is {gammaInt}");
            var epsilonInt = BinaryToDecimal(epsilon);
            // Console.WriteLine($"epsilonInt is {epsilonInt}");
            result = gammaInt * epsilonInt;
        }

        public int GetMostSignificant(int column)
        {
            var nums = new List<int>();
            for (var i = 0; i < rowCount; i++)
            {
                nums.Add(reportArray[i,column]);
            }

            var significant = (double)nums.Sum() / nums.Count() > .5 ? 1 : 0;
            return significant;
        }

        // this booster shot is killing me, i should just not dupe code, but i'm so... tired...
        public int GetMostSignificant(int column, IEnumerable<string> inputIterable)
        {
            var nums = new List<int>();
            // Console.WriteLine($"checking {column}:");
            var arrayString = string.Join("\n", inputIterable);
            // Console.WriteLine(arrayString);
            foreach (var line in inputIterable)
            {
                // this is so gross, i'm so sorry... i just am too tired to separate this out 
                nums.Add(Int32.Parse(line[column].ToString()));
            }

            string numString = string.Join("", nums);
            var significant = (double)nums.Sum() / nums.Count() > .5 ? 1 : 0;
            if (nums.Count(x => x == 0) == nums.Count(x => x == 1))
            {
                significant = 1;
            }
            // Console.WriteLine($"significant for {numString} is {significant}");
            return significant;
        }

        public void SetGamma()
        {
            for (var i = 0; i < colCount; i++)
            {
                var mostSig = GetMostSignificant(i);
                gamma += GetMostSignificant(i).ToString();
            }
            // Console.WriteLine($"gamma set to {gamma}");
        }

        public void SetEpsilon()
        {
            if (gamma == "")
            {
                SetGamma();
            }
            foreach (var i in gamma)
            {
                epsilon += i == '0' ? '1' : '0';
            }
            // Console.WriteLine($"epsilon set to {epsilon}");
        }
        
        public int BinaryToDecimal(string inputString)
        {
            return Convert.ToInt32(inputString, 2);
        }

        public int O2Report()
        {
            var o2Rating = ReduceReport();
            var o2Decimal = BinaryToDecimal(o2Rating);
            var co2Rating = ReduceReport(false);
            var co2Decimal = BinaryToDecimal(co2Rating);
            Console.WriteLine($"o2: {o2Rating}({o2Decimal}), co2: {co2Rating}({co2Decimal})");
            var o2Result = o2Decimal * co2Decimal;
            return o2Result;
        }

        public string ReduceReport(bool mostSignificant = true)
        {
            var validValues = new List<string>();
            var colCheck = 0;
            while (validValues.Count != 1)
            {
                var iterable = validValues.Count == 0 ? inputReport : validValues;
                var mostSig = GetMostSignificant(colCheck, iterable);
                var leastSig = mostSig == 0 ? 1 : 0;
                var validBit = mostSignificant ? mostSig : leastSig;
                var removeValues = new List<string>();

                foreach (var line in iterable)
                {
                    if (Int16.Parse(line[colCheck].ToString()) == validBit)
                    {
                        if (colCheck == 0)
                        {
                            validValues.Add(line);
                        }
                    }
                    else
                    {
                        if (colCheck != 0)
                        {
                            removeValues.Add(line);
                        }
                    }
                }

                foreach (var line in removeValues)
                {
                    Console.WriteLine($"removing {line} because col {colCheck} != {validBit}");
                    validValues.Remove(line);
                }

                colCheck++;
            }

            return validValues[0];
        }
    }
}