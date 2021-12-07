using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Aoc2021;

namespace Aoc06
{
    public class SolveTest : SolverBase
    {
        public override bool enablePartOneTest { get { return false; } }
        public override bool enablePartOne { get { return false; } }
        public override bool enablePartTwoTest { get { return true; } }
        public override bool enablePartTwo { get { return true; } }

        protected override string PartOne(IEnumerable<string> inputData)
        {
            var parsed = inputData.ToList()[0].Split(",");
            List<int> fish = parsed.Select(x => Int32.Parse(x)).ToList();
            
            for (var i = 1; i <= 80; i++)
            {
                fish = PartOneIterate(fish);
            }
            return fish.Count.ToString();
        }
        
        protected override string PartTwo(IEnumerable<string> inputData)
        {
            var parsed = inputData.ToList()[0].Split(",");
            List<int> fish = parsed.Select(x => Int32.Parse(x)).ToList();
            var newFishCounter = new List<long>();
            newFishCounter.AddRange(Enumerable.Repeat(0L, 9));

            foreach (var fishy in fish)
            {
                newFishCounter[fishy] += 1;
            }
            
            for (var i = 1; i <= 256; i++)
            {
                newFishCounter = PartTwoIterate(newFishCounter);
            }

            var result = newFishCounter.Sum();
            
            return result.ToString();
        }

        private List<long> PartTwoIterate(List<long> fish)
        {
            var result = new List<long>();
            result.AddRange(Enumerable.Repeat(0L, 9));
            
            for(var i = 0; i < 9; i++)
            {
                var fishCount = fish[i];
                switch (i)
                {
                    case 0:
                        result[8] += fishCount;
                        result[6] += fishCount;
                        break;
                    default:
                        result[i - 1] += fishCount;
                        break;
                }
            }

            return result;
        }
        
        private List<int> PartOneIterate(List<int> fishList)
        {
            var newFish = new List<int>();
            
            for(var i = 0; i < fishList.Count; i++)
            {
                var fish = fishList[i];
                switch (fish)
                {
                    case 0:
                        newFish.Add(8);
                        fishList[i] = 6;
                        break;
                    default:
                        fishList[i] = fish - 1;
                        break;
                }
            }

            fishList.AddRange(newFish);
            return fishList;
        }
    }
}