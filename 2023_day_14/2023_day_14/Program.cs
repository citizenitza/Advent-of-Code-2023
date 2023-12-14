namespace _2023_day_14 {
    internal class Program {
        static void Main(string[] args) {
            Solution newSolution = new Solution();
            newSolution.PartOne();
            Console.WriteLine("Part one solution: " + newSolution.Result_PartOne.ToString());
            Visualize(newSolution);
            VisualizeOriginal(newSolution);
            newSolution.PartTwo();
            Console.WriteLine("Part two solution: " + newSolution.Result_PartTwo.ToString()); ;


        }


        public static void Visualize(Solution newSolution) {
            string line = "";
            for (int row = 0; row < newSolution.MatrixSize; row++) {
                bool Inner = false;
                bool CornerDirectionUp = false;
                bool CornenrInProgress = false;
                var originalColor = Console.ForegroundColor;
                for (int col = 0; col < newSolution.MatrixSize; col++) {
                        Console.Write(newSolution.Input[row, col]);

                    
                }
                Console.Write("\r\n");
                
            }
            Console.Write("\r\n"); Console.Write("\r\n");
        }
        public static void VisualizeOriginal(Solution newSolution) {
            string line = "";
            for (int row = 0; row < newSolution.MatrixSize; row++) {
                bool Inner = false;
                bool CornerDirectionUp = false;
                bool CornenrInProgress = false;
                var originalColor = Console.ForegroundColor;
                for (int col = 0; col < newSolution.MatrixSize; col++) {
                    Console.Write(newSolution.InputOriginal[row, col]);


                }
                Console.Write("\r\n");

            }
            Console.Write("\r\n"); Console.Write("\r\n");
        }
    }
}