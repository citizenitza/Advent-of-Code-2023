using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_day_10 {
    public enum Type {
        Ground = 0, // .
        NorthSouth =1, // |
        EastWest = 2, // -
        NorthEast = 3, // L
        NorthWest = 4, // J
        SouthWest = 5, // 7
        SouthEast = 6, // F
    }
    public class Tile {
        public bool StartPos=false;
        public bool MainPath = false;
        public bool InnerLoopTile = false;
        public Type Type;
        public string Char;
        public int Row;
        public int Col;
        public string VisualizeChar() {
            if (Type == Type.NorthSouth) { // |
                return "║";
            } else if (Type == Type.EastWest) { // -
                return "═";
            } else if (Type == Type.NorthEast) { // L
                return "╚";
            } else if (Type == Type.NorthWest) {// J
                return "╝";
            } else if (Type == Type.SouthWest) { // 7
                return "╗";
            } else if (Type == Type.SouthEast) { // F
                return "╔";
            } else { // F
                return ".";
            }
        }
        public int[] ExitNextCoordinate(int EnterRow, int EnterCol) {
            int[] retVal = new int[2];
            if(Type == Type.NorthSouth) { // |
                retVal[0] = Row + (Row - EnterRow);
                retVal[1] = EnterCol;
            } else if (Type == Type.EastWest) { // -
                retVal[0] = EnterRow;
                retVal[1] = Col + (Col - EnterCol); 
            }  else if (Type == Type.NorthEast) { // L
                retVal[0] = Row + (Col - EnterCol);
                retVal[1] = Col + (Row - EnterRow);
            } else if (Type == Type.NorthWest) {// J
                retVal[0] = Row - (Col - EnterCol); 
                retVal[1] = Col - (Row - EnterRow);
            } else if (Type == Type.SouthWest) { // 7
                retVal[0] = Row + (Col - EnterCol);
                retVal[1] = Col + (Row - EnterRow);
            } else if (Type == Type.SouthEast) { // F
                retVal[0] = Row - (Col - EnterCol);
                retVal[1] = Col - (Row - EnterRow);
            }


            return retVal;
        }
    }
    internal class Solution {
        public int Result_PartOne;
        public int Result_PartTwo;

        public Tile[,] PipeMap;
        public int MatrixSize;
        int StartCol;
        int StartRow;

        public Solution() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            int lineIndex = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            uint Impossiblesum = 0;
            string[] lineArray;
            //fist line
            bool firstline = true;
            int Row = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
                if (firstline) {
                    firstline = false;
                    MatrixSize = lineOfText.Count();
                    PipeMap = new Tile[MatrixSize, MatrixSize];
                }
                int Col = 0;
                foreach (char c in lineOfText) {
                    PipeMap[Row, Col] = new Tile();
                    PipeMap[Row, Col].Char = c.ToString();
                    PipeMap[Row, Col].Row = Row;
                    PipeMap[Row, Col].Col = Col;
                    if (c == '.') {
                        PipeMap[Row, Col].Type = Type.Ground;
                    } else if (c == '|') {
                        PipeMap[Row, Col].Type = Type.NorthSouth;
                    } else if (c == '-') {
                        PipeMap[Row, Col].Type = Type.EastWest;
                    } else if (c == 'L') {
                        PipeMap[Row, Col].Type = Type.NorthEast;
                    } else if (c == 'J') {
                        PipeMap[Row, Col].Type = Type.NorthWest;
                    } else if (c == '7') {
                        PipeMap[Row, Col].Type = Type.SouthWest;
                    } else if (c == 'F') {
                        PipeMap[Row, Col].Type = Type.SouthEast;
                    } else if (c == 'S') {//starting position
                        //PipeMap[Row, Col].Type = Type.NorthWest;//Hardcoded example 1
                        //PipeMap[Row, Col].Type = Type.SouthEast;//Hardcoded example 2
                        //PipeMap[Row, Col].Type = Type.SouthEast;//Hardcoded example 3
                        //PipeMap[Row, Col].Type = Type.SouthWest;//Hardcoded example 4
                        PipeMap[Row, Col].Type = Type.NorthWest;//Hardcoded example input
                        PipeMap[Row, Col].StartPos = true;
                        StartCol = Col;
                        StartRow = Row;
                    } else {
                        //Error
                        ;
                    }
                    Col++;
                }
                Row++;
            }
        }
        public void PartOne() {
            int CurrentRow = StartRow;
            int CurrentCol = StartCol;
            //shortcut - first step by hand; Input
            int NextRow = StartRow-1;
            int NextCol = StartCol;
            //shortcut Example 3
            //int NextRow = StartRow;
            //int NextCol = StartCol+1;
            //shortcut Example 4
            //int NextRow = StartRow + 1;
            //int NextCol = StartCol;
            bool StartReached = false;
            int steps = 0;
            while (!StartReached) {
                int[] newtmp = PipeMap[NextRow, NextCol].ExitNextCoordinate(CurrentRow, CurrentCol);
                PipeMap[CurrentRow, CurrentCol].MainPath = true;
                CurrentRow = NextRow;
                CurrentCol = NextCol;
                NextRow = newtmp[0];
                NextCol = newtmp[1];
                steps++;
                if (CurrentRow == StartRow && CurrentCol == StartCol) {
                    StartReached = true;
                    break;
                }

            }
            Result_PartOne = steps / 2;
        }
        public void PartTwo() {
            int InnerTileCnt = 0;
            for (int row = 0; row < MatrixSize; row++) {
                bool Inner = false;
                bool CornerDirectionUp = false;
                bool CornenrInProgress = false;
                for (int col = 0; col < MatrixSize; col++) {
                    if (PipeMap[row, col].Type == Type.Ground || !PipeMap[row, col].MainPath) {
                        if (Inner) {
                            InnerTileCnt++;
                            PipeMap[row, col].InnerLoopTile = true;
                        }
                    } else {
                        if (PipeMap[row, col].MainPath && PipeMap[row, col].Type != Type.EastWest) {
                            if (PipeMap[row, col].Type == Type.NorthSouth) {
                                Inner = !Inner;
                            } else if (PipeMap[row, col].Type == Type.SouthWest || PipeMap[row, col].Type == Type.SouthEast) { //down
                                Inner = !Inner;

                                //if (CornenrInProgress) {
                                //    if (CornerDirectionUp) {
                                //        Inner = !Inner;
                                //    }
                                //} else {
                                //    CornerDirectionUp = false;
                                //}
                                //CornenrInProgress = !CornenrInProgress;
                            } else if (PipeMap[row, col].Type == Type.NorthWest || PipeMap[row, col].Type == Type.NorthEast) { // up
                                //if (CornenrInProgress) {
                                //    if (!CornerDirectionUp) {
                                //        Inner = !Inner;
                                //    }
                                //} else {
                                //    CornerDirectionUp = true;
                                //}
                                //CornenrInProgress = !CornenrInProgress;
                            }
                        }
                    }
                }
            }
            Result_PartTwo = InnerTileCnt;
            Debug();
        }

        private void Debug() {
            for (int row = 0; row < MatrixSize; row++) {
                for (int col = 0; col < MatrixSize; col++) {
                    if(PipeMap[row, col].InnerLoopTile && PipeMap[row, col].MainPath) {
                        //Error
                        ;
                    }
                } 
            }
        }

    }
}
