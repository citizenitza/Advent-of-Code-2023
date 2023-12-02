using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_day_02 {
    public class MaxValues {
        public string Color;
        public uint Limit;
        public MaxValues(string color, uint cnt) {
            Color = color;
            Limit = cnt;
        }
    }

    internal class Solution {
        private List<MaxValues> Limits = new List<MaxValues>();
        public uint Result_PartOne;
        public uint Result_PartTwo;
        public Solution() {
            Limits.Add(new MaxValues("red", 12));
            Limits.Add(new MaxValues("green", 13));
            Limits.Add(new MaxValues("blue", 14));
        }
        public void PartOne() {
            
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
            while ((lineOfText = reader.ReadLine()) != null) {
                lineArray = lineOfText.Split(":");
                uint CurrentGame = Convert.ToUInt32(lineArray[0].Split(" ")[1]);
                lineArray = lineArray[1].Split(";");
                bool impossibleBag = false;
                foreach (string set in lineArray) {
                    //for each sets
                    string[] Ballsets = set.Split(",");
                    foreach (string Ball in Ballsets) {
                        string Color = Ball.Trim().Split(" ")[1];
                        uint Cnt = Convert.ToUInt32(Ball.Trim().Split(" ")[0]);
                        //check if possible
                       if(Cnt > Limits.Where(x => x.Color == Color).First().Limit) {
                            //impossible
                            impossibleBag = true;
                            break;
                        }

                    }
                    if (impossibleBag) {
                        break;
                    }
                }
                if (!impossibleBag) {
                    Impossiblesum += CurrentGame;
                }
            }
            Result_PartOne = Impossiblesum;
        }


        public void PartTwo() {
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
            while ((lineOfText = reader.ReadLine()) != null) {
                lineArray = lineOfText.Split(":");
                uint CurrentGame = Convert.ToUInt32(lineArray[0].Split(" ")[1]);
                lineArray = lineArray[1].Split(";");
                bool impossibleBag = false;
                uint MaxRed = 0;
                uint MaxGreen = 0;
                uint MaxBlue = 0;
                foreach (string set in lineArray) {
                    //for each sets
                    string[] Ballsets = set.Split(",");
                    foreach (string Ball in Ballsets) {
                        string Color = Ball.Trim().Split(" ")[1];
                        uint Cnt = Convert.ToUInt32(Ball.Trim().Split(" ")[0]);
                        switch (Color) {
                            case "red": {
                                    if (Cnt > MaxRed) {
                                        MaxRed = Cnt;
                                    }
                                    break;
                                }
                            case "green": {
                                    if (Cnt > MaxGreen) {
                                        MaxGreen = Cnt;
                                    }
                                    break;
                                }
                            case "blue": {
                                    if (Cnt > MaxBlue) {
                                        MaxBlue = Cnt;
                                    }
                                    break;
                                }
                        }

                    }
                }
                uint Power = MaxRed * MaxGreen * MaxBlue;
                Result_PartTwo += Power;
            }

        }
    }
}
