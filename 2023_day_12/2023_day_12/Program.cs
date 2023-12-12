namespace _2023_day_12 {
    internal class Program {
        static void Main(string[] args) {
            Solution newSolution = new Solution();
            //newSolution.PartOne();
            //Console.WriteLine("Part one brute force solution: " + newSolution.Result_PartOne.ToString());
            newSolution.PartTwo();
            Console.WriteLine("Part one recursive solution: " + newSolution.Result_PartOne_Method_2.ToString());
            Console.WriteLine("Part two recursive solution: " + newSolution.Result_PartTwo.ToString());
        }
    }
}