namespace _2023_day_16 {
    public enum Direction {
        Down = 0,
        Left = 2,
        Up = 1,
        Right = 3,
    }
    public class Tile {
        public char Type;
        public bool Energized;
        public bool DownBeamPassed;
        public bool LeftBeamPassed;
        public bool UpBeamPassed;
        public bool RightBeamPassed;

    }
    public class Beam {
        public int Col;
        public int Row;
        public Direction Dir;
        public bool Finished;
    }
    internal class Solution {
        public uint Result_PartOne;
        public uint Result_PartTwo;

        public int MatrixSize;
        public Tile[,] Input;
        private Queue<Beam> Beams = new Queue<Beam>();
        //public List<Card> CardList = new List<Card>();
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
                    Input = new Tile[MatrixSize, MatrixSize];
                }
                int Col = 0;
                foreach (char c in lineOfText) {
                    Input[Row, Col] = new Tile();
                    Input[Row, Col].Type = c;
                    Col++;
                }
                Row++;
            }

        }
        public void PartOne() {
            //create first beam
            Beam newBeam = new Beam();
            newBeam.Dir = Direction.Right;
            //newBeam.Dir = Direction.Down;
            newBeam.Col = 0;
            newBeam.Row = 0;
            newBeam.Finished = false;
            Beams.Enqueue(newBeam);

            Result_PartOne = ProcessCurrent();
            ResetMatrix();
        }

        public void PartTwo() {
            List<uint> results = new List<uint>();
            for (int Row = 0; Row < MatrixSize; Row++) {
                for (int Col = 0; Col < MatrixSize; Col++) {
                    if (Row == 0) {
                        //create Right beam
                        Beam newBeam = new Beam();
                        newBeam.Dir = Direction.Down;
                        newBeam.Col = Col;
                        newBeam.Row = Row;
                        newBeam.Finished = false;
                        Beams.Enqueue(newBeam);
                        uint tmp = ProcessCurrent();
                        results.Add(tmp);
                        //Console.WriteLine("Row: " + Row.ToString() + " Col: " + Col.ToString() + " Result: " + tmp.ToString());
                        //DrawMatrix();
                        ResetMatrix();
                    }
                    if (Row == (MatrixSize - 1)) {
                        //create Right beam
                        Beam newBeam = new Beam();
                        newBeam.Dir = Direction.Up;
                        newBeam.Col = Col;
                        newBeam.Row = Row;
                        newBeam.Finished = false;
                        Beams.Enqueue(newBeam);
                        uint tmp = ProcessCurrent();
                        results.Add(tmp);
                        //Console.WriteLine("Row: " + Row.ToString() + " Col: " + Col.ToString() + " Result: " + tmp.ToString());
                        //DrawMatrix();
                        ResetMatrix();
                    }
                    if (Col == 0) {
                        //create Right beam
                        Beam newBeam = new Beam();
                        newBeam.Dir = Direction.Right;
                        newBeam.Col = Col;
                        newBeam.Row = Row;
                        newBeam.Finished = false;
                        Beams.Enqueue(newBeam);
                        uint tmp = ProcessCurrent();
                        results.Add(tmp);
                        //Console.WriteLine("Row: " + Row.ToString() + " Col: " + Col.ToString() + " Result: " + tmp.ToString());
                        //DrawMatrix();
                        ResetMatrix();
                    }
                    if (Col == (MatrixSize - 1)) {
                        //create Right beam
                        Beam newBeam = new Beam();
                        newBeam.Dir = Direction.Left;
                        newBeam.Col = Col;
                        newBeam.Row = Row;
                        newBeam.Finished = false;
                        Beams.Enqueue(newBeam);
                        uint tmp = ProcessCurrent();
                        results.Add(tmp);
                        //Console.WriteLine("Row: " + Row.ToString() + " Col: " + Col.ToString() + " Result: " + tmp.ToString());
                        //DrawMatrix();
                        ResetMatrix();
                    }
                }
            }
            Result_PartTwo = results.Max();
        }

        private void ResetMatrix() {
            for (int i = 0; i < MatrixSize; i++) {
                for (int j = 0; j < MatrixSize; j++) {
                    Input[i, j].Energized = false;
                    Input[i, j].UpBeamPassed = false;
                    Input[i, j].DownBeamPassed = false;
                    Input[i, j].RightBeamPassed = false;
                    Input[i, j].LeftBeamPassed = false;
                }
            }
        }
        private void DrawMatrix() {
            Console.WriteLine("");
            Console.WriteLine("Beam route:");

            for (int Row = 0; Row < MatrixSize; Row++) {
                string line = "";
                for (int Col = 0; Col < MatrixSize; Col++) {
                    if (Input[Row, Col].Type == '.') {
                        if (Input[Row, Col].Energized) {
                            line += "#";
                        } else {
                            line += ".";
                        }

                    } else {
                        line += Input[Row, Col].Type;
                    }
                }
                Console.WriteLine(line);
            }       
        }
        private uint ProcessCurrent() {
            while (Beams.Count() > 0) {
                Beam currentBeam = Beams.Dequeue();
                while (!currentBeam.Finished) {
                    //energize current
                    Input[currentBeam.Row, currentBeam.Col].Energized = true;
                    //DrawMatrix();
                    //cache beam direction
                    switch (currentBeam.Dir) {
                        case Direction.Down: {
                                if(Input[currentBeam.Row, currentBeam.Col].DownBeamPassed == true) {
                                    currentBeam.Finished = true;
                                } else {
                                    Input[currentBeam.Row, currentBeam.Col].DownBeamPassed = true;
                                }
                                break;
                            }
                        case Direction.Left: {
                                if (Input[currentBeam.Row, currentBeam.Col].LeftBeamPassed == true) {
                                    currentBeam.Finished = true;
                                } else {
                                    Input[currentBeam.Row, currentBeam.Col].LeftBeamPassed = true;
                                }
                                break;
                            }
                        case Direction.Up: {
                                if (Input[currentBeam.Row, currentBeam.Col].UpBeamPassed == true) {
                                    currentBeam.Finished = true;
                                } else {
                                    Input[currentBeam.Row, currentBeam.Col].UpBeamPassed = true;
                                }
                                break;
                            }
                        case Direction.Right: {
                                if (Input[currentBeam.Row, currentBeam.Col].RightBeamPassed == true) {
                                    currentBeam.Finished = true;
                                } else {
                                    Input[currentBeam.Row, currentBeam.Col].RightBeamPassed = true;
                                }
                                break;
                            }

                    }
                    //check cached
                    if (currentBeam.Finished) {
                        break;
                    }
                    //action
                    switch (Input[currentBeam.Row, currentBeam.Col].Type) {
                        case '.': {
                                ProcessEmpty(currentBeam);
                                break;
                            }
                        case '|': {
                                ProcessVertical(currentBeam);
                                break;
                            }
                        case '-': {
                                ProcessHorizontal(currentBeam);
                                break;
                            }
                        case '/': {
                                ProcessForward(currentBeam);
                                break;
                            }
                        case '\\': {
                                ProcessBackward(currentBeam);
                                break;
                            }
                        default: {
                                //Error
                                break;
                            }
                    }


                }

            }
            uint result = 0;
            for(int i  = 0; i < MatrixSize; i++) {
                for (int j = 0; j < MatrixSize; j++) {
                    if (Input[i, j].Energized) {
                        result += 1;
                    }
                }
            }
            return result;
        }
        private void ProcessEmpty(Beam currentBeam) { // "."
            switch (currentBeam.Dir) {
                case Direction.Down: {
                        currentBeam.Row += 1;
                        if (currentBeam.Row >= MatrixSize) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
                case Direction.Left: {
                        currentBeam.Col -= 1;
                        if (currentBeam.Col < 0) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
                case Direction.Up: {
                        currentBeam.Row -= 1;
                        if (currentBeam.Row < 0) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
                case Direction.Right: {
                        currentBeam.Col += 1;
                        if (currentBeam.Col >= MatrixSize) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
            }
        }

        private void ProcessVertical(Beam currentBeam) { // "|"
            switch (currentBeam.Dir) {
                case Direction.Down: {
                        //simple step
                        currentBeam.Row += 1;
                        if (currentBeam.Row >= MatrixSize) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
                case Direction.Left: {
                        //create new beam up
                        Beam newBeam = new Beam();
                        newBeam.Dir = Direction.Up;
                        newBeam.Col = currentBeam.Col;
                        newBeam.Row = currentBeam.Row - 1;
                        if (newBeam.Row < 0) {
                            //do nothing
                        } else {
                            newBeam.Finished = false;
                            Beams.Enqueue(newBeam);
                        }
                        //reflect down
                        currentBeam.Dir = Direction.Down;
                        currentBeam.Row += 1;
                        if (currentBeam.Row >= MatrixSize) {
                            currentBeam.Finished = true;
                        }
                        break;

                    }
                case Direction.Up: {
                        //simple step
                        currentBeam.Row -= 1;
                        if (currentBeam.Row < 0) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
                case Direction.Right: {
                        //create new beam up
                        Beam newBeam = new Beam();
                        newBeam.Dir = Direction.Up;
                        newBeam.Col = currentBeam.Col;
                        newBeam.Row = currentBeam.Row - 1;
                        if (newBeam.Row < 0) {
                            //do nothing
                        } else {
                            newBeam.Finished = false;
                            Beams.Enqueue(newBeam);
                        }
                        //reflect down
                        currentBeam.Dir = Direction.Down;
                        currentBeam.Row += 1;
                        if (currentBeam.Row >= MatrixSize) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
            }

        }
        private void ProcessHorizontal(Beam currentBeam) { // "-"
            switch (currentBeam.Dir) {
                case Direction.Down: {
                        //create new beam Left
                        Beam newBeam = new Beam();
                        newBeam.Dir = Direction.Left;
                        newBeam.Col = currentBeam.Col - 1;
                        newBeam.Row = currentBeam.Row;
                        if (newBeam.Col < 0) {
                            //do nothing
                        } else {
                            newBeam.Finished = false;
                            Beams.Enqueue(newBeam);
                        }
                        //reflect Right
                        currentBeam.Dir = Direction.Right;
                        currentBeam.Col += 1;
                        if (currentBeam.Col >= MatrixSize) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
                case Direction.Left: {
                        //simple step
                        currentBeam.Col -= 1;
                        if (currentBeam.Col < 0) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
                case Direction.Up: {
                        //create new beam Left
                        Beam newBeam = new Beam();
                        newBeam.Dir = Direction.Left;
                        newBeam.Col = currentBeam.Col - 1;
                        newBeam.Row = currentBeam.Row;
                        if (newBeam.Col < 0) {
                            //do nothing
                        } else {
                            newBeam.Finished = false;
                            Beams.Enqueue(newBeam);
                        }
                        //reflect Right
                        currentBeam.Dir = Direction.Right;
                        currentBeam.Col += 1;
                        if (currentBeam.Col >= MatrixSize) {
                            currentBeam.Finished = true;
                        }

                        break;
                    }
                case Direction.Right: {
                        //simple step
                        currentBeam.Col += 1;
                        if (currentBeam.Col >= MatrixSize) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
            }

        }
        private void ProcessBackward(Beam currentBeam) { // "\"
            switch (currentBeam.Dir) {
                case Direction.Down: {
                        currentBeam.Dir = Direction.Right;
                        //step right
                        currentBeam.Col += 1;
                        if (currentBeam.Col >= MatrixSize) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
                case Direction.Left: {
                        currentBeam.Dir = Direction.Up;
                        //step up
                        currentBeam.Row -= 1;
                        if (currentBeam.Row < 0) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
                case Direction.Up: {
                        currentBeam.Dir = Direction.Left;
                        currentBeam.Col -= 1;
                        if (currentBeam.Col < 0) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
                case Direction.Right: {
                        currentBeam.Dir = Direction.Down;
                        currentBeam.Row += 1;
                        if (currentBeam.Row >= MatrixSize) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
            }

        }
        private void ProcessForward(Beam currentBeam) { // "/"
            switch (currentBeam.Dir) {
                case Direction.Down: {
                        currentBeam.Dir = Direction.Left;
                        currentBeam.Col -= 1;
                        if (currentBeam.Col < 0) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
                case Direction.Left: {
                        currentBeam.Dir = Direction.Down;
                        currentBeam.Row += 1;
                        if (currentBeam.Row >= MatrixSize) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
                case Direction.Up: {
                        currentBeam.Dir = Direction.Right;
                        //step right
                        currentBeam.Col += 1;
                        if (currentBeam.Col >= MatrixSize) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
                case Direction.Right: {
                        currentBeam.Dir = Direction.Up;
                        //step up
                        currentBeam.Row -= 1;
                        if (currentBeam.Row < 0) {
                            currentBeam.Finished = true;
                        }
                        break;
                    }
            }

        }


    }

}
