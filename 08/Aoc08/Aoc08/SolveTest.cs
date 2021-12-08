using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Aoc2021;

namespace Aoc08
{
    public class SolveTest : SolverBase
    {
        public override bool enablePartOneTest { get { return false; } }
        public override bool enablePartOne { get { return false; } }
        public override bool enablePartTwoTest { get { return true; } }
        public override bool enablePartTwo { get { return true; } }
        protected override string PartOne(IEnumerable<string> inputData)
        {
            int count = 0;
            var uniqueLengths = new List<int>() {2, 3, 4, 7};
            
            foreach (var line in inputData)
            {
                List<string> signals;
                List<string> display;
                ParseLine(line, out signals, out display);
                count += display.Select(i => uniqueLengths.Contains(i.Length) ? 1 : 0).Sum();
            }
            return count.ToString();
        }
        
        protected override string PartTwo(IEnumerable<string> inputData)
        {
            int total = 0;
            foreach (var line in inputData)
            {
                List<string> signals;
                List<string> display;
                ParseLine(line, out signals, out display);
                var sig = new Signal(signals);
                sig.SolveSignals();
                var displayNum = sig.GetDisplayValue(display);
                total += displayNum;
            }
            return total.ToString();
        }

        private void ParseLine(string line, out List<string> signals, out List<string> display)
        {
            int dividerIndex = line.IndexOf("|");
            string signalString = line.Substring(0, dividerIndex - 1);
            string displayString = line.Substring(dividerIndex + 2);
            var signalList = signalString.Split().ToList();
            var displayList = displayString.Split().ToList();
            signals = SortListOfStrings(signalList);
            display = SortListOfStrings(displayList);
        }

        private List<string> SortListOfStrings(List<string> sortList)
        {
            var sorted = new List<string>();
            foreach (var fragment in sortList)
            {
                var fragmentSplits = fragment.Select(x => x.ToString()).ToList();
                fragmentSplits.Sort();
                var sortedFragment = string.Join("", fragmentSplits);
                sorted.Add(sortedFragment);
            }

            return sorted;
        }
    }

    public class Signal
    {
        private List<string> signals;
        private string zero;
        private string one;
        private string two;
        private string three;
        private string four;
        private string five;
        private string six;
        private string seven;
        private string eight;
        private string nine;

        private char middle;
        
        public Signal(List<string> signals)
        {
            this.signals = signals;
        }

        public void SolveSignals()
        {
            one = signals.Find(x => x.Length == 2);
            four = signals.Find(x => x.Length == 4);
            seven = signals.Find(x => x.Length == 3);
            eight = signals.Find(x => x.Length == 7);
            FindMiddle();

            zero = signals.Find(x => x.Length == 6 && !x.Contains(middle));
            three = signals.Find(x => x.Length == 5 && OverlapCount(one, x) == 2);
            six = signals.Find(x => x.Length == 6 && x != zero && OverlapCount(one, x) == 1);
            nine = signals.Find(x =>x.Length == 6 &&  x != zero && OverlapCount(one, x) == 2);
            five = signals.Find(x => x.Length == 5 && OverlapCount(six, x) == 5);
            two = signals.Find(x => x.Length == 5 && OverlapCount(three, x) == 4 && x != five && x != six);
        }

        public int GetDisplayValue(List<string> display)
        {
            var numbers = "";
            foreach (var num in display)
            {
                numbers += FindValue(num).ToString();
            }
            return Int32.Parse(numbers);
        }

        private int FindValue(string num)
        {
            var nums = new List<string> {zero, one, two, three, four, five, six, seven, eight, nine};
            signals.Sort();
            for (var i = 0; i < 10; i++)
            {
                if (nums[i] == num)
                {
                    return i;
                }
            }
            var numStr = string.Join(" ", nums);
            var parsed = string.Join(" ", signals);
            throw new Exception($"couldn't find {num} in \n{numStr}\n with signals\n{parsed}");
        }

        private int OverlapCount(string first, string second)
        {
            var count = 0;
            foreach (var i in first)
            {
                if (second.Contains(i))
                {
                    count++;
                }
            }
            return count;
        }

        private int CountPerSignal(char character)
        {
            var count = 0;
            foreach (var sig in signals)
            {
                if (sig.Contains(character))
                {
                    count++;
                }
            }
            return count;
        }

        private void FindMiddle()
        {
            foreach (var character in four)
            {
                var count = CountPerSignal(character);
                var overlap = OverlapCount(character.ToString(), one);
                if (count == 7 && overlap == 0)
                {
                    middle = character;
                    break;
                }
            }
        }
    }
}