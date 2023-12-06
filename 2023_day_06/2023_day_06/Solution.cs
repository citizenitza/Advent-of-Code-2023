using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_day_06 {
    internal class Solution {
        public ulong Result_PartOne;
        public List<ulong> Times = new List<ulong>();
        public List<ulong> Distances = new List<ulong>();
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
            lineOfText = reader.ReadLine();
            lineArray = lineOfText.Split(":");
            foreach(string item in lineArray[1].Trim().Split()) {
                if (string.IsNullOrEmpty(item)) {
                    continue;
                }
                Times.Add(Convert.ToUInt64(item));  
            }
            //second line
            lineOfText = reader.ReadLine();
            lineArray = lineOfText.Split(":");
            foreach (string item in lineArray[1].Trim().Split()) {
                if (string.IsNullOrEmpty(item)) {
                    continue;
                }
                Distances.Add(Convert.ToUInt64(item));
            }

        }

        public void PartOne() {
            //Part One and PartTwo
            Result_PartOne = 1;
            for (int Race = 0; Race < Times.Count; Race++) {
                ulong Wins = 0;
                for(uint accTime =0;accTime <= Times[Race]; accTime++) {
                    ulong Timespentmoving = Times[Race] - accTime;
                    ulong Distance = Timespentmoving * accTime;
                    if(Distance > Distances[Race]) {
                        Wins++;
                    }
                }
                Result_PartOne *= Wins;
            }
        }

    }
}
