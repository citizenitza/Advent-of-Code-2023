using System.Data;
using System.Text;

namespace _2023_day_13 {
    internal class Solution {
        public long Result_PartOne;
        public long Result_PartTwo;
        List<string[]> input = new List<string[]>();
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
            List<string> tmp = new List<string>();
            string[] rows;
            while ((lineOfText = reader.ReadLine()) != null) {
                if (lineOfText == string.Empty) {
                    rows = new string[tmp.Count()];
                    rows = tmp.ToArray();
                    input.Add(rows);
                    tmp = new List<string>();
                } else {
                    tmp.Add(lineOfText);
                }
            }
            //add last
            rows = new string[tmp.Count()];
            rows = tmp.ToArray();
            input.Add(rows);
        }
        public void PartOne() {

            //vertical
            foreach (string[] table in input) {
                List<int> symmetryaxis = new List<int>();
                for (int row = 0; row < table.Count(); row++) {
                    symmetryaxis.AddRange(ReflectionIndexes(table[row]));
                }
                int resultVer = symmetryaxis.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
                if (symmetryaxis.Where(x => x == resultVer).Count() == table.Count()) {
                    //vertical symmetry found
                    Result_PartOne += resultVer;
                    Result_PartTwo += CalcPartTwo(table, true, resultVer);
                    ;
                } else {
                    //search for horizontal
                    List<int> symmetryaxisHorizontal = new List<int>();
                    for (int col = 0; col < table[0].Length; col++) {
                        string line = string.Join("", table.Select(s => string.IsNullOrEmpty(s) ? null : s.Substring(col, 1)));
                        ;
                        symmetryaxisHorizontal.AddRange(ReflectionIndexes(line));
                    }
                    int resultHzn = symmetryaxisHorizontal.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
                    if (symmetryaxisHorizontal.Where(x => x == resultHzn).Count() == table[0].Length) {
                        Result_PartOne += (100 * resultHzn);
                        Result_PartTwo += CalcPartTwo(table, false, resultHzn);
                    } else {
                        throw new Exception("Invalid");
                    }
                    ;
                }
            }

        }

        private List<int> ReflectionIndexes(string line) {
            List<int> Retval = new List<int>();
            for (int i = 1; i < line.Length; i++) {
                int checkrange = 1;
                bool done = false;
                bool symmetrical = true;
                while (!done) {
                    if (line[i - checkrange] == line[i + (checkrange - 1)]) {
                        //symmetrical
                    } else {
                        //not symmetrical
                        symmetrical = false;
                        break;
                    }
                    checkrange++;
                    if ((i - checkrange) < 0 || (i + (checkrange - 1)) == line.Length) {
                        //reached the end
                        done = true;
                        break;
                    }

                }
                if (symmetrical) {
                    Retval.Add(i);
                }

            }
            return Retval;
        }
        public int CalcPartTwo(string[] table, bool OriginalVertical, int originalIndex) {
            bool resultfound = false;
            for (int chrow = 0; chrow < table.Count(); chrow++) {
                for (int chcol = 0; chcol < table[0].Length; chcol++) {
                    //change one character
                    StringBuilder sb = new StringBuilder(table[chrow]);
                    sb[chcol] = Change(sb[chcol]);
                    table[chrow] = sb.ToString();
                    //if (OriginalVertical) {
                    //find horizontal
                    List<int> symmetryaxisHorizontal = new List<int>();
                    for (int col = 0; col < table[0].Length; col++) {
                        string line = string.Join("", table.Select(s => string.IsNullOrEmpty(s) ? null : s.Substring(col, 1)));
                        ;
                        symmetryaxisHorizontal.AddRange(ReflectionIndexes(line));
                    }
                    int resultHzn = symmetryaxisHorizontal.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
                    
                    if (symmetryaxisHorizontal.Where(x => x == resultHzn).Count() == table[0].Length) {
                        if (OriginalVertical) {
                            return 100 * resultHzn;
                        } else {
                            if (originalIndex != resultHzn) {
                                return 100 * resultHzn;
                            } else {
                                resultHzn = symmetryaxisHorizontal.GroupBy(x => x).OrderByDescending(x => x.Count()).ToList()[1].Key;
                                if (symmetryaxisHorizontal.Where(x => x == resultHzn).Count() == table[0].Length) {
                                    return 100 * resultHzn;
                                }
                            }
                        }
                    }
                    //} else {
                    //find vertical
                    List<int> symmetryaxis = new List<int>();
                    for (int row = 0; row < table.Count(); row++) {
                        symmetryaxis.AddRange(ReflectionIndexes(table[row]));
                    }
                    int resultVer = symmetryaxis.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
                    if (symmetryaxis.Where(x => x == resultVer).Count() == table.Count()) {
                        //vertical symmetry found
                        if (!OriginalVertical) {
                            return resultVer;
                        } else {
                            if (originalIndex != resultVer) {
                                return resultVer;
                            } else {
                                resultVer = symmetryaxis.GroupBy(x => x).OrderByDescending(x => x.Count()).ToList()[1].Key;
                                if (symmetryaxis.Where(x => x == resultVer).Count() == table.Count()) {
                                    return resultVer;
                                }
                            }
                        }
                    }
                    //}


                    //change back
                    sb = new StringBuilder(table[chrow]);
                    sb[chcol] = Change(sb[chcol]);
                    table[chrow] = sb.ToString();
                }
            }
            return -1;
        }
        private char Change(char ch) {
            if (ch == '.') {
                return '#';
            } else {
                return '.';
            }
        }
    }
}
