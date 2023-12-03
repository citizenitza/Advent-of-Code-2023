using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace _2023_day_03 {
    internal class Solution {
        private class Number {
            public string Value;
            public bool HasNeighbour;
            public List<Star> StarNeighbours = new List<Star>();
        }
        private class Star {
            public int row;
            public int col;
        }
        public uint Result_PartOne;
        public uint Result_PartTwo;

        //static int size = 12;
        static int size = 140;
        char[,] inputMatrix = new char[size, size];
        private List<Number> NumberList;
        public Solution() {
            NumberList = new List<Number>();
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            FileStream filestream = new FileStream(ConfigPath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);

            int lineIndex = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
                int charIndex = 0;
                foreach (char chr in lineOfText) {
                    inputMatrix[lineIndex, charIndex] = chr;
                    charIndex++;
                }
                lineIndex++;
            }
        }

        public void PartOne() {
            bool NumberInProgress = false;
            Number newTmp = new Number();
            for (int row = 0; row < size; row++) {
                for (int col = 0; col < size; col++) {

                    if (System.Char.IsDigit(inputMatrix[row, col])) {
                        if (NumberInProgress) {
                            newTmp.Value += inputMatrix[row, col];
                            CheckStarNeighbour(row, col, ref newTmp);
                            if (!newTmp.HasNeighbour) {
                                newTmp.HasNeighbour = CheckNeighbour(row, col);
                            }
                        } else {
                            NumberInProgress = true;
                            newTmp = new Number();
                            newTmp.Value += inputMatrix[row, col];
                            newTmp.HasNeighbour = CheckNeighbour(row, col);
                            CheckStarNeighbour(row, col, ref newTmp);
                        }
                    } else {
                        if (NumberInProgress) {
                            NumberInProgress = false;
                            NumberList.Add(newTmp);
                        } else {
                            continue;
                        }
                    }
                }

            }
            //if any number remain in buffer
            if (NumberInProgress) {
                NumberInProgress = false;
                NumberList.Add(newTmp);
            }
            //sum
            Result_PartOne = 0;
            foreach (Number member in NumberList) {
                if (member.HasNeighbour) {
                    Result_PartOne += Convert.ToUInt32(((Number)member).Value);
                }
            }

        }
        public void PartTwo() {
            for (int row = 0; row < size; row++) {
                for (int col = 0; col < size; col++) {
                    if (inputMatrix[row, col] == '*') {
                        //check if neighbour for any number
                        int count = 0;
                        uint ratio = 1;
                        foreach (Number nmb in NumberList) {
                            foreach (Star star in nmb.StarNeighbours) {
                                if (star.row == row && star.col == col) {
                                    count++;
                                    ratio *= Convert.ToUInt32(nmb.Value);

                                }
                            }
                        }
                        if (count == 2) {
                            //its a gear
                            Result_PartTwo += ratio;
                        }
                    }
                }

            }

        }
        private void CheckStarNeighbour(int _row, int _col, ref Number _currentNumber) {
            //check top/
            for (int i = _row - 1; i <= _row + 1; i++) {
                for (int j = _col - 1; j <= _col + 1; j++) {
                    if (i == _row && j == _col) {
                        continue;
                    }
                    CheckStarNeighbourSub(i, j, ref _currentNumber);
                       
                }
            }
        }
        private void CheckStarNeighbourSub(int _row, int _col, ref Number _currentNumber) {
            try {
                if (inputMatrix[_row, _col] == '*') {
                    if (!_currentNumber.StarNeighbours.Any(x => (x.row == _row && x.col == _col))) {
                        //doesn exist already
                        Star newStar = new Star();
                        newStar.row = _row;
                        newStar.col = _col;
                        _currentNumber.StarNeighbours.Add(newStar);
                    }
                }
            } catch (Exception ex) when (ex is System.IndexOutOfRangeException) { };
        }
        private bool CheckNeighbour(int _row,int _col) {
            bool result = false;
            for(int i = _row - 1; i <= _row + 1; i++) {
                for (int j = _col - 1; j <= _col + 1; j++) {
                    if(i ==_row &&j == _col) {
                        continue;
                    }
                    if (CheckNeighbourSub(i, j)) {
                        return true;
                    }
                }
            }
            return result;
        }
        private bool CheckNeighbourSub(int _row, int _col) {
            try {
                if (!(System.Char.IsDigit(inputMatrix[_row , _col]) || inputMatrix[_row , _col] == '.')) {
                    //if neither -> symbol
                    return true;
                }
            } catch (Exception ex) when (ex is System.IndexOutOfRangeException) { };
            return false;
        }
    }


}
