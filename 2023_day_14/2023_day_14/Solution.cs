using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_day_14 {
    internal class Solution {
        public long Result_PartOne;
        public long Result_PartTwo;
        private List<long> CycleResults = new List<long>();
        public int MatrixSize;
        public char[,] Input;
        public char[,] InputOriginal;
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
                    Input = new char[MatrixSize, MatrixSize];
                    InputOriginal = new char[MatrixSize, MatrixSize];
                }
                int Col = 0;
                foreach (char c in lineOfText) {
                    Input[Row, Col] = c;
                    InputOriginal[Row, Col] = c;
                    Col++;
                }
                Row++;
            }
        }
        public void PartOne() {
            Tilt(0);
            Result_PartOne = CalcLoad();
        }
        public void PartTwo() {

            //finish first
            Tilt(1);
            Tilt(2);
            Tilt(3);
            CycleResults.Add(CalcLoad());
            for (int i = 0; i < 1000000000; i++) {
                Tilt(0);
                Tilt(1);
                Tilt(2);
                Tilt(3);
                long currentLoad = CalcLoad();
                if (CycleResults.Any(x=> x==currentLoad)) {
                    ;
                } 
                CycleResults.Add(currentLoad);
                     
                
            }
            Result_PartTwo = CalcLoad();
        }
        private bool EqualToOriginal() {
            bool Equal = true;
            for (int row = 0; row < MatrixSize; row++) {
                for (int col = 0; col < MatrixSize; col++) {
                    if (Input[row, col] != InputOriginal[row, col]) {//its air, check above
                        Equal = false;
                        return Equal;
                    }
                }
            }
            return Equal;
        }
        private long CalcLoad() {
            ;
            //calculate wheight
            long retval = 0;
            for (int row = 0; row < MatrixSize; row++) {
                for (int col = 0; col < MatrixSize; col++) {
                    if (Input[row, col] == 'O') {//its air, check above
                        retval += (MatrixSize - row);
                    }
                }
            }
            return retval;
        }
        public void Tilt(int direction) {//0-north

            if (direction == 0) { // North

                int changeCount = 0;
                do {
                    changeCount = 0;
                    for (int row = 0; row < MatrixSize - 1; row++) {
                        for (int col = 0; col < MatrixSize; col++) {
                            if (Input[row, col] == '.') {//its air, check above
                                if (Input[row + 1, col] == 'O') {//its O rock, move down one step
                                    Input[row, col] = 'O';
                                    Input[row + 1, col] = '.';
                                    changeCount++;
                                }
                            }
                        }
                    }
                } while (changeCount > 0);

            } else if (direction == 1) {//West

                int changeCount = 0;
                do {
                    changeCount = 0;
                    for (int col = 0; col < MatrixSize - 1; col++) {
                        for (int row = 0; row < MatrixSize ; row++) {
                            if (Input[row, col] == '.') {//its air, check above
                                if (Input[row , col + 1] == 'O') {//its O rock, move down one step
                                    Input[row, col] = 'O';
                                    Input[row , col + 1] = '.';
                                    changeCount++;
                                }
                            }
                        }
                    }
                } while (changeCount > 0);


            } else if (direction == 2) {//South
                int changeCount = 0;
                do {
                    changeCount = 0;
                    for (int row = MatrixSize - 1; row > 0; row--) {
                        for (int col = 0; col < MatrixSize; col++) {
                            if (Input[row, col] == '.') {//its air, check above
                                if (Input[row - 1, col] == 'O') {//its O rock, move down one step
                                    Input[row, col] = 'O';
                                    Input[row - 1, col] = '.';
                                    changeCount++;
                                }
                            }
                        }
                    }
                } while (changeCount > 0);


            } else if (direction == 3) {//East

                int changeCount = 0;
                do {
                    changeCount = 0;
                    for (int col = MatrixSize-1; col > 0; col--) {
                        for (int row = 0; row < MatrixSize; row++) {
                            if (Input[row, col] == '.') {//its air, check above
                                if (Input[row, col - 1] == 'O') {//its O rock, move down one step
                                    Input[row, col] = 'O';
                                    Input[row, col - 1] = '.';
                                    changeCount++;
                                }
                            }
                        }
                    }
                } while (changeCount > 0);

            }


        }


    }
}
