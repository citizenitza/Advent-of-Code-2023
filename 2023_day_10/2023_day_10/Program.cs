namespace _2023_day_10 {
    internal class Program {
        static void Main(string[] args) {
            Solution newSolution = new Solution();
            newSolution.PartOne();
            Console.WriteLine("Part one solution: " + newSolution.Result_PartOne.ToString());
            newSolution.PartTwo();
            Console.WriteLine("Part two solution: " + newSolution.Result_PartTwo.ToString());
            Console.BufferHeight = 3000;
            Visualize(newSolution);
        }
        public static void Visualize(Solution newSolution) {
            string line = "";
            for (int row = 0; row < newSolution.MatrixSize; row++) {
                bool Inner = false;
                bool CornerDirectionUp = false;
                bool CornenrInProgress = false;
                var originalColor = Console.ForegroundColor;
                for (int col = 0; col < newSolution.MatrixSize; col++) {
                    if (newSolution.PipeMap[row, col].MainPath) {

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(newSolution.PipeMap[row, col].VisualizeChar());
                        Console.ForegroundColor = originalColor;
                    } else if (newSolution.PipeMap[row, col].InnerLoopTile) {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("~");
                        Console.ForegroundColor = originalColor;
                    } else {
                        Console.Write(newSolution.PipeMap[row, col].Char);

                    }
                } 
                    Console.Write("\r\n");
            } 
        }
    }
}