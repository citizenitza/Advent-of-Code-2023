﻿namespace _2023_day_17 {
    internal class Program {
        static void Main(string[] args) {

            Solution solution = new Solution();
            solution.PartOne();
            Console.WriteLine("Part one result: " + solution.Result_PartOne.ToString());
            Console.WriteLine("Cycle Cnt: " + solution.CycleCnt.ToString());
            solution.PartTwo();
            Console.WriteLine("Part two result: " + solution.Result_PartTwo.ToString());
        }
    }
}
