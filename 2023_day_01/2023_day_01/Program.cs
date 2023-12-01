namespace _2023_day_01 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Solution for 2023 Day 1");

            Solution newSolution = new Solution();
            newSolution.PartOne();
            Console.WriteLine("Part one solution: " + newSolution.Result_PartOne.ToString());

            newSolution.PartTwo();
            Console.WriteLine("Part two solution: " + newSolution.Result_PartTwo.ToString());
        }
    }
}