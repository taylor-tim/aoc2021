using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc2021
{
    public class FileHandler
    {
        private static readonly string RootDir = Path.GetFullPath($"../../../");
        private static readonly string TestPuzzleData = $"{RootDir}\\testPuzzleData.txt";
        private static readonly string PuzzleData = $"{RootDir}\\puzzleData.txt";

        public static IEnumerable<string> GetPuzzleData()
        {
            return ReadFromFile(PuzzleData);
        }

        public static IEnumerable<string> GetTestData()
        {
            return ReadFromFile(TestPuzzleData);
        }

        private static List<string> ReadFromFile(string path)
        {
            IEnumerable<string> outputData;
            try
            {
                outputData = File.ReadAllLines(path);
            }
            catch (Exception e)
            {
                Console.WriteLine($"No file found at {path}, returning empty.");
                outputData = new List<string>();
            }
            return outputData.ToList();
        }
    }
}