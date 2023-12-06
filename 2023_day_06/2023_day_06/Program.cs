namespace _2023_day_06 {
    internal class Program {
        static void Main(string[] args) {
            Solution newSolution = new Solution();
            newSolution.PartOne();
            Console.WriteLine("Part one solution: " + newSolution.Result_PartOne.ToString());
            newSolution.BetterSolution();
            Console.WriteLine("Part one solution: " + newSolution.Result_PartOne.ToString());

        }
    }
}