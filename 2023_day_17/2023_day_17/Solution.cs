using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_day_17 {
    public enum Direction {
        Down = 0,
        Left = 2,
        Up = 1,
        Right = 3,
        Nodirection = 4,
    }
    public struct Tile {
        public uint HeatLossValue;
        public uint HeatDistance;
        public int StraightCount;
        public Direction LastDirection;
        //public bool Start;
        //public bool End;
        public bool Visited;
        public string MainPath;

    }
    public struct StackData {
        public int row;
        public int col;
    }
    internal class Solution {
        public uint Result_PartOne;
        public uint Result_PartTwo;
        public uint CycleCnt = 0;
        public int MatrixSize;
        public Tile[,] Map;
        public Tile[,] PrevMap;
        string HeatDistMapString;
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
                    PrevMap = new Tile[MatrixSize, MatrixSize];

                }
                int Col = 0;
                foreach (char c in lineOfText) {
                    Map[Row, Col] = new Tile();
                    Map[Row, Col].HeatLossValue = Convert.ToUInt32(c.ToString());
                    Col++;
                }
                Row++;
            }
            //init first

        }

        List <StackData> Stack = new List <StackData>(); 
        public void PartOne() {
            Map[0, 0].Visited = true;
            Map[0, 0].StraightCount = 1;
            ProcessNeighbours(0, 0, Direction.Nodirection);
            Result_PartOne = Map[MatrixSize - 1, MatrixSize - 1].HeatDistance;
            DrawAocDebugMap();
            DrawHeadDistance();
        }
        public void PartTwo() {
        }

        private void ProcessNeighbours(int _row, int _col, Direction _lastDirection) {
            CycleCnt++;
            Tile current = Map[_row, _col];
            Map[_row, _col].LastDirection = _lastDirection;
            StackData stackitem = new StackData();
            stackitem.row = _row;
            stackitem.col = _col;
            Stack.Add(stackitem);
            //debug
            //Console.WriteLine("Row: " + _row.ToString() + " Col: " + _col.ToString());
            //Console.WriteLine("Current Straight count: " + Map[_row, _col].StraightCount.ToString());
            //DrawHeatDistanceMap();
            //Console.Clear();

            if (current.HeatDistance == 76 && _row == 9 && _col == 12) {
                CopyDistMap();
            }


            //right
            do {
                if (_col != (MatrixSize - 1) && _lastDirection != Direction.Left) {
                    int newcol = _col + 1;
                    bool TurnTaken = false;
                    if (_col == 0 && _row == 0) {
                        ;
                    }
                    if (_lastDirection == Direction.Right) {
                        if (current.StraightCount > 2) {
                            //cant go more 
                            break;
                        } else {
                            TurnTaken = false;
                            //Map[_row, newcol].StraightCount = current.StraightCount + 1;
                        }
                    } else {
                        //turn taken
                        TurnTaken = true;
                        //Map[_row, newcol].StraightCount = 1;
                    }

                    //accessible
                    uint newHeatDistance = current.HeatDistance + Map[_row, newcol].HeatLossValue;
                    if (Map[_row, newcol].Visited) {
                        if (newHeatDistance <= Map[_row, newcol].HeatDistance) {
                            Map[_row, newcol].HeatDistance = newHeatDistance;
                            if (TurnTaken) {
                                Map[_row, newcol].StraightCount = 1;
                            } else {
                                Map[_row, newcol].StraightCount = current.StraightCount + 1;
                            }
                            bool NextStep = false;
                            if (Map[MatrixSize - 1, MatrixSize - 1].Visited) {
                                if (Map[_row, newcol].HeatDistance < Map[MatrixSize - 1, MatrixSize - 1].HeatDistance) {
                                    if (_row < MatrixSize - 1 || newcol < MatrixSize - 1) {
                                        NextStep = true;
                                    }
                                }
                            } else {
                                NextStep = true;
                            }

                            if (NextStep) {
                                ProcessNeighbours(_row, newcol, Direction.Right);
                            }

                            if (_row == MatrixSize - 1 && newcol == MatrixSize - 1) {
                                DrawHeatDistanceMap();
                            }
                            //if (_row < MatrixSize - 1 || newcol < MatrixSize - 1) {
                            //    ProcessNeighbours(_row, newcol, Direction.Right);
                            //}
                        }

                    } else {
                        Map[_row, newcol].Visited = true;
                        Map[_row, newcol].HeatDistance = newHeatDistance;
                        //recurse
                        if (TurnTaken) {
                            Map[_row, newcol].StraightCount = 1;
                        } else {
                            Map[_row, newcol].StraightCount = current.StraightCount + 1;
                        }

                        bool NextStep = false;
                        if (Map[MatrixSize - 1, MatrixSize - 1].Visited) {
                            if (Map[_row, newcol].HeatDistance < Map[MatrixSize - 1, MatrixSize - 1].HeatDistance) {
                                if (_row < MatrixSize - 1 || newcol < MatrixSize - 1) {
                                    NextStep = true;
                                }
                            }
                        } else {
                            NextStep = true;
                        }

                        if (NextStep) {
                            ProcessNeighbours(_row, newcol, Direction.Right);
                        }
                        if (_row == MatrixSize - 1 && newcol == MatrixSize - 1) {
                            DrawHeatDistanceMap();
                        }
                        //if (_row < MatrixSize - 1 || newcol < MatrixSize - 1) {
                        //    ProcessNeighbours(_row, newcol, Direction.Right);
                        //}
                    }


                }
            } while (false);
            //down
            do {
                if (_row != (MatrixSize - 1) && _lastDirection != Direction.Up) {
                    int newrow = _row + 1;
                    bool TurnTaken = false;
                    if (_lastDirection == Direction.Down) {
                        if (current.StraightCount > 2) {
                            //cant go more 
                            break;
                        } else {
                            TurnTaken = false;
                            //Map[newrow, _col].StraightCount = current.StraightCount + 1;
                        }
                    } else {
                        //turn taken
                        TurnTaken = true;
                        //Map[newrow, _col].StraightCount = 1;
                    }

                    //accessible
                    uint newHeatDistance = current.HeatDistance + Map[newrow, _col].HeatLossValue;
                    if (Map[newrow, _col].Visited) {
                        if (newHeatDistance <= Map[newrow, _col].HeatDistance) {
                            Map[newrow, _col].HeatDistance = newHeatDistance;
                            if (TurnTaken) {
                                Map[newrow, _col].StraightCount = 1;
                            } else {
                                Map[newrow, _col].StraightCount = current.StraightCount + 1;
                            }
                            bool NextStep = false;
                            if (Map[MatrixSize - 1, MatrixSize - 1].Visited) {
                                if (Map[newrow, _col].HeatDistance < Map[MatrixSize - 1, MatrixSize - 1].HeatDistance) {
                                    if (newrow < MatrixSize - 1 || _col < MatrixSize - 1) {
                                        NextStep = true;
                                    }
                                }
                            } else {
                                NextStep = true;
                            }

                            if (NextStep) {
                                ProcessNeighbours(newrow, _col, Direction.Down);
                            }
                            if (newrow == MatrixSize - 1 && _col == MatrixSize - 1) {
                                DrawHeatDistanceMap();
                            }
                            //if (newrow < MatrixSize - 1 || _col < MatrixSize - 1) {
                            //    ProcessNeighbours(newrow, _col, Direction.Down);
                            //}
                        }

                    } else {
                        Map[newrow, _col].Visited = true;
                        Map[newrow, _col].HeatDistance = newHeatDistance;
                        //recurse
                        if (TurnTaken) {
                            Map[newrow, _col].StraightCount = 1;
                        } else {
                            Map[newrow, _col].StraightCount = current.StraightCount + 1;
                        }
                        bool NextStep = false;
                        if (Map[MatrixSize - 1, MatrixSize - 1].Visited) {
                            if (Map[newrow, _col].HeatDistance < Map[MatrixSize - 1, MatrixSize - 1].HeatDistance) {
                                if (newrow < MatrixSize - 1 || _col < MatrixSize - 1) {
                                    NextStep = true;
                                }
                            }
                        } else {
                            NextStep = true;
                        }

                        if (NextStep) {
                            ProcessNeighbours(newrow, _col, Direction.Down);
                        }
                        if (newrow == MatrixSize - 1 && _col == MatrixSize - 1) {
                            DrawHeatDistanceMap();
                        }
                        //if (newrow < MatrixSize - 1 || _col < MatrixSize - 1) {
                        //    ProcessNeighbours(newrow, _col, Direction.Down);
                        //}
                    }


                }
            } while (false);
            //top neighbour
            do {
                if (_row != 0 && _lastDirection != Direction.Down) {
                    int newrow = _row - 1;
                    bool TurnTaken = false;
                    if (_lastDirection == Direction.Up) {
                        if (current.StraightCount > 2) {
                            //cant go more 
                            break;
                        } else {
                            TurnTaken = false;
                            //Map[newrow, _col].StraightCount = current.StraightCount + 1;
                        }
                    } else {
                        //turn taken
                        TurnTaken = true;
                        //Map[newrow, _col].StraightCount = 1;
                    }

                    //accessible
                    uint newHeatDistance = current.HeatDistance + Map[newrow, _col].HeatLossValue;
                    if (Map[newrow, _col].Visited) {
                        if (newHeatDistance <= Map[newrow, _col].HeatDistance) {
                            Map[newrow, _col].HeatDistance = newHeatDistance;
                            if (TurnTaken) {
                                Map[newrow, _col].StraightCount = 1;
                            } else {
                                Map[newrow, _col].StraightCount = current.StraightCount + 1;
                            }

                            bool NextStep = false;
                            if (Map[MatrixSize - 1, MatrixSize - 1].Visited) {
                                if (Map[newrow, _col].HeatDistance < Map[MatrixSize - 1, MatrixSize - 1].HeatDistance) {
                                    if (newrow < MatrixSize - 1 || _col < MatrixSize - 1) {
                                        NextStep = true;
                                    }
                                }
                            } else {
                                NextStep = true;
                            }

                            if (NextStep) {
                                ProcessNeighbours(newrow, _col, Direction.Up);

                            }
                            //if (newrow < MatrixSize-1 || _col < MatrixSize - 1) {
                            //    ProcessNeighbours(newrow, _col, Direction.Up);
                            //}
                        }

                    } else {
                        Map[newrow, _col].Visited = true;
                        Map[newrow, _col].HeatDistance = newHeatDistance;
                        //recurse
                        if (TurnTaken) {
                            Map[newrow, _col].StraightCount = 1;
                        } else {
                            Map[newrow, _col].StraightCount = current.StraightCount + 1;
                        }
                        bool NextStep = false;
                        if (Map[MatrixSize - 1, MatrixSize - 1].Visited) {
                            if (Map[newrow, _col].HeatDistance < Map[MatrixSize - 1, MatrixSize - 1].HeatDistance) {
                                if (newrow < MatrixSize - 1 || _col < MatrixSize - 1) {
                                    NextStep = true;
                                }
                            }
                        } else {
                            NextStep = true;
                        }

                        if (NextStep) {
                            ProcessNeighbours(newrow, _col, Direction.Up);
                        }
                        //if (newrow < MatrixSize - 1 || _col < MatrixSize - 1) {
                        //    ProcessNeighbours(newrow, _col, Direction.Up);
                        //}
                    }

                }
            } while (false);

            //left
            do {
                if (_col != 0 && _lastDirection != Direction.Right) {
                    int newcol = _col - 1;
                    bool TurnTaken = false;
                    if (_lastDirection == Direction.Left) {
                        if (current.StraightCount > 2) {
                            //cant go more 
                            break;
                        } else {
                            TurnTaken = false;
                            //Map[_row, newcol].StraightCount = current.StraightCount + 1;
                        }
                    } else {
                        //turn taken
                        TurnTaken = true;
                        //Map[_row, newcol].StraightCount = 1;
                    }

                    //accessible
                    uint newHeatDistance = current.HeatDistance + Map[_row, newcol].HeatLossValue;
                    if (Map[_row, newcol].Visited) {
                        if (newHeatDistance <= Map[_row, newcol].HeatDistance) {
                            Map[_row, newcol].HeatDistance = newHeatDistance;
                            if (TurnTaken) {
                                Map[_row, newcol].StraightCount = 1;
                            } else {
                                Map[_row, newcol].StraightCount = current.StraightCount + 1;
                            }

                            bool NextStep = false;
                            if (Map[MatrixSize - 1, MatrixSize - 1].Visited) {
                                if (Map[_row, newcol].HeatDistance < Map[MatrixSize - 1, MatrixSize - 1].HeatDistance) {
                                    if (_row < MatrixSize - 1 || newcol < MatrixSize - 1) {
                                        NextStep = true;
                                    }
                                }
                            } else {
                                NextStep = true;
                            }

                            if (NextStep) {
                                ProcessNeighbours(_row, newcol, Direction.Left);
                            }
                            //if (_row < MatrixSize - 1 || newcol < MatrixSize - 1) {
                            //    ProcessNeighbours(_row, newcol, Direction.Left);
                            //}
                        }

                    } else {
                        Map[_row, newcol].Visited = true;
                        Map[_row, newcol].HeatDistance = newHeatDistance;
                        //recurse
                        if (TurnTaken) {
                            Map[_row, newcol].StraightCount = 1;
                        } else {
                            Map[_row, newcol].StraightCount = current.StraightCount + 1;
                        }

                        bool NextStep = false;
                        if (Map[MatrixSize - 1, MatrixSize - 1].Visited) {
                            if (Map[_row, newcol].HeatDistance < Map[MatrixSize - 1, MatrixSize - 1].HeatDistance) {
                                if (_row < MatrixSize - 1 || newcol < MatrixSize - 1) {
                                    NextStep = true;
                                }
                            }
                        } else {
                            NextStep = true;
                        }

                        if (NextStep) {
                            ProcessNeighbours(_row, newcol, Direction.Left);
                        }

                        //if (_row < MatrixSize - 1 || newcol < MatrixSize - 1) {
                        //    ProcessNeighbours(_row, newcol, Direction.Left);
                        //}
                    }


                }
            } while (false);
            Stack.RemoveAt(Stack.Count - 1);    
        }
        private void CopyDistMap() {
            //for (int Row = 0; Row < MatrixSize; Row++) {
            //    string line = "";
            //    for (int Col = 0; Col < MatrixSize; Col++) {
            //        line += Map[Row, Col].HeatDistance.ToString() + " ";
            //        HeatDistanceMap[Row, Col] = Map[Row, Col].HeatDistance;
            //    }
            //    HeatDistMapString += (line + "\r\n");
            //}
        }
        private void DrawHeatDistanceMap() {
            //for (int Row = 0; Row < MatrixSize; Row++) {
            //    string line = "";
            //    for (int Col = 0; Col < MatrixSize; Col++) {
            //        line += " " + Map[Row, Col].HeatLossValue.ToString() + " ";
            //    }
            //    Console.WriteLine(line);
            //}
            Console.WriteLine("");
            Console.WriteLine("");

            var originalColor = Console.ForegroundColor;
            for (int Row = 0; Row < MatrixSize; Row++) {
                string line = "";
                for (int Col = 0; Col < MatrixSize; Col++) {

                    if (Stack.Any(x => x.row == Row && x.col == Col)) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" " + Map[Row, Col].HeatDistance.ToString().PadRight(4));
                        Console.ForegroundColor = originalColor;
                    } else {
                        Console.Write(" " + Map[Row, Col].HeatDistance.ToString().PadRight(4));
                    }
                    //if (Map[Row, Col].HeatDistance == PrevMap[Row, Col].HeatDistance) {
                    //    Console.Write(" " + Map[Row, Col].HeatDistance.ToString().PadRight(4));
                    //} else {
                    //    Console.ForegroundColor = ConsoleColor.Red;
                    //    Console.Write(" " + Map[Row, Col].HeatDistance.ToString().PadRight(4));
                    //    Console.ForegroundColor = originalColor;
                    //}
                }
                Console.Write("\r\n");
                //Console.WriteLine(line);
            }
            ;
            Array.Copy(Map, PrevMap, Map.Length);
            //PrevMap = Map;
            ;
        }
        private void DrawHeadDistance() {
            Console.WriteLine("");
            Console.WriteLine("");

            for (int Row = 0; Row < MatrixSize; Row++) {
                string line = "";
                for (int Col = 0; Col < MatrixSize; Col++) {
                    line += Map[Row, Col].HeatDistance.ToString() + " ";
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            var originalColor = Console.ForegroundColor;
            for (int Row = 0; Row < MatrixSize; Row++) {
                //string line = "";
                for (int Col = 0; Col < MatrixSize; Col++) {
                    if (Map[Row, Col].MainPath == null) {
                        Console.Write(Map[Row, Col].HeatLossValue.ToString() + " ");
                    } else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Map[Row,Col].MainPath + " ");
                        Console.ForegroundColor = originalColor;
                    }
                    
                }
                Console.Write("\r\n");
            }
            Console.WriteLine("");
            Console.WriteLine("");
            //for (int Row = 0; Row < MatrixSize; Row++) {
            //    string line = "";
            //    for (int Col = 0; Col < MatrixSize; Col++) {
            //        line += Map[Row, Col].LastDirection.ToString() + "   ";
            //    }
            //    Console.WriteLine(line);
            //}

        }


        private void DrawAocDebugMap() {
            //Console.WriteLine("");
            //Console.WriteLine("");
            int CurrentRow = MatrixSize - 1;
            int CurrentCol = MatrixSize - 1;
            while (true) {
                if(Map[CurrentRow, CurrentCol].LastDirection == Direction.Left) {
                    Map[CurrentRow, CurrentCol].MainPath = "<";
                    CurrentCol++;
                    continue;
                } else if (Map[CurrentRow, CurrentCol].LastDirection == Direction.Right) {
                    Map[CurrentRow, CurrentCol].MainPath = ">";
                    CurrentCol--;
                    continue;
                } else if (Map[CurrentRow, CurrentCol].LastDirection == Direction.Up) {
                    Map[CurrentRow, CurrentCol].MainPath = "^";
                    CurrentRow++;
                    continue;
                } else if (Map[CurrentRow, CurrentCol].LastDirection == Direction.Down) {
                    Map[CurrentRow, CurrentCol].MainPath = "V";
                    CurrentRow--;
                    continue;
                } else if (Map[CurrentRow, CurrentCol].LastDirection == Direction.Nodirection) {
                    break;
                }


            }
            //Console.WriteLine("Done");

        }

    }

}
