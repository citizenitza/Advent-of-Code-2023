namespace _2023_day_17_2 {
    public struct Tile {
        public Location Location;
        public uint HeatLossValue;
        public uint HeatDistance;
        public int StraightCount;
        public Direction LastDirection;
        //public bool Start;
        //public bool End;
        public bool Visited;
        public string MainPath;

    }
    public struct Location {
        public int row;
        public int col;
    }
    public enum Direction {
        Down = 0,
        Left = 2,
        Up = 1,
        Right = 3,
        Nodirection = 4,
    }

    internal class Solution {
        public uint Result_PartOne;
        public uint Result_PartTwo;
        public Tile[,] Map;
        public int MatrixSize;

        public Solution() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            int lineIndex = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            string[] lineArray;
            bool firstline = true;
            int Row = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
                if (firstline) {
                    firstline = false;
                    MatrixSize = lineOfText.Count();
                    //initialize input
                    Map = new Tile[MatrixSize, MatrixSize];
                }
                int Col = 0;
                foreach (char c in lineOfText) {
                    Map[Row, Col] = new Tile();
                    Map[Row, Col].Location = new Location();
                    Map[Row, Col].Location.row = Row;
                    Map[Row, Col].Location.col = Col;
                    Map[Row, Col].HeatLossValue = Convert.ToUInt32(c.ToString());
                    Col++;
                }
                Row++;
            }

        }

        public void PartOne() {
            PriorityQueue<Location, uint> queue = new PriorityQueue<Location, uint>();
            //initialize-> remove for aoc
            Map[0, 0].HeatDistance = Map[0, 0].HeatLossValue;
            Map[0, 0].Visited= true;
            queue.Enqueue(Map[0, 0].Location, Map[0, 0].HeatDistance);//enqueue start
            uint cyclecnt = 0;
            while (queue.Count > 0) {
                //Console.Clear();
                //DrawHeatDistanceMap();
                cyclecnt++;
                var current = queue.Dequeue(); // dequeue minimal
                //check Right
                if (current.col != (MatrixSize - 1)) { // boundary check
                    int newcol = current.col + 1;
                    int newrow = current.row;
                    //if (!Map[newrow, newcol].Visited && Map[newrow, newcol].HeatDistance <= Map[current.row, current.col].HeatDistance) {
                    if (!Map[newrow, newcol].Visited) {
                        Map[newrow, newcol].HeatDistance = Map[current.row, current.col].HeatDistance + Map[newrow, newcol].HeatLossValue; // update heatdistance
                        Map[newrow, newcol].Visited = true;
                        queue.Enqueue(Map[newrow, newcol].Location, Map[newrow, newcol].HeatDistance);//enqueue
                    }
                }
                //check Down
                if (current.row != (MatrixSize - 1)) {// boundary check
                    int newcol = current.col;
                    int newrow = current.row + 1;
                    if (!Map[newrow, newcol].Visited) {
                        Map[newrow, newcol].HeatDistance = Map[current.row, current.col].HeatDistance + Map[newrow, newcol].HeatLossValue; // update heatdistance
                        Map[newrow, newcol].Visited = true;
                        queue.Enqueue(Map[newrow, newcol].Location, Map[newrow, newcol].HeatDistance);//enqueue
                    }
                }
                //check Left
                if (current.col != 0) { // boundary check
                    int newcol = current.col - 1;
                    int newrow = current.row;
                    if (!Map[newrow, newcol].Visited) {
                        Map[newrow, newcol].HeatDistance = Map[current.row, current.col].HeatDistance + Map[newrow, newcol].HeatLossValue; // update heatdistance
                        Map[newrow, newcol].Visited = true;
                        queue.Enqueue(Map[newrow, newcol].Location, Map[newrow, newcol].HeatDistance);//enqueue
                    }
                }
                //check Up
                if (current.row != 0) {// boundary check
                    int newcol = current.col;
                    int newrow = current.row - 1;
                    if (!Map[newrow, newcol].Visited) {
                        Map[newrow, newcol].HeatDistance = Map[current.row, current.col].HeatDistance + Map[newrow, newcol].HeatLossValue; // update heatdistance
                        Map[newrow, newcol].Visited = true;
                        queue.Enqueue(Map[newrow, newcol].Location, Map[newrow, newcol].HeatDistance);//enqueue
                    }
                }

                if (current.row == (MatrixSize - 1) && current.col == (MatrixSize - 1)) {
                    break;
                }
            }
            DrawHeatDistanceMap();
            Console.WriteLine("Cycle count: " + cyclecnt.ToString());
            Result_PartOne = Map[MatrixSize - 1, MatrixSize - 1].HeatDistance;
        }
        public void PartTwo() {
        }


        private void DrawHeatDistanceMap() {
            for (int Row = 0; Row < MatrixSize; Row++) {
                string line = "";
                for (int Col = 0; Col < MatrixSize; Col++) {
                    line += " " + Map[Row, Col].HeatDistance.ToString() + " ";
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}
