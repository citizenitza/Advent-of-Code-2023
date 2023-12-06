using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _2023_day_05 {
    public class Range {
        public long DestinationStart;
        public long SourceStart;
        public long Lenght;
    }
    public class Map {
        public List<Range> Ranges = new List<Range>();
    }
    internal class Solution {
        public long Result_PartOne;
        public long Result_PartTwo;
        //public List<Maps> Maps = new List<Maps>();
        public Map[] Maps = new Map[7];
        public List<long> Seeds = new List<long>();
        public List<long> SeedsForPartTwo = new List<long>();
        //public List<long> Locations= new List<long>();
        //public List<long> LocationsForPartTwo= new List<long>();
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
            bool firstLine = true;
            int MapIndex = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
                if (firstLine) {
                    //read seeds
                    firstLine = false;
                    Seeds = lineOfText.Split(":")[1].Trim().Split().Select(long.Parse).ToList();
                    //parttwo
                    lineArray = lineOfText.Split(":")[1].Trim().Split();
                    for(int i = 0; i < lineArray.Count() - 1; i += 2) {
                        long Seedstart = long.Parse(lineArray[i]);
                        for(long j = 0;j< long.Parse(lineArray[i + 1]); j++) {
                            SeedsForPartTwo.Add(Seedstart + j);
                        }
                    }
                    reader.ReadLine();//read next empty line as well

                } else {
                    if (lineOfText == String.Empty) {
                        //next map
                        MapIndex++;
                        continue;
                    } else {
                        if (System.Char.IsDigit(lineOfText[0])) {
                            //new range
                            lineArray = lineOfText.Split();
                            Range newrange = new Range();
                            newrange.DestinationStart = Convert.ToInt64(lineArray[0]);
                            newrange.SourceStart = Convert.ToInt64(lineArray[1]);
                            newrange.Lenght = Convert.ToInt64(lineArray[2]);
                            Maps[MapIndex].Ranges.Add(newrange);
                        } else {
                            //initiate map
                            Maps[MapIndex] = new Map();
                        }
                    }
                }

            }
        }

        public void PartOne() {
            foreach (long seed in Seeds) {
                long curentValue = seed;
                for (int i = 0; i < 7; i++) {//convert for all map
                    curentValue = GardenConvert(curentValue, i);
                }
                if(curentValue< Result_PartOne || Result_PartOne == 0) {
                    Result_PartOne = curentValue;
                }
                //Locations.Add(curentValue);
            }
            //Result_PartOne = Locations.Min();
        }
        public void PartTwo() {
            long debugindex = 0;
            foreach (long seed in SeedsForPartTwo) {
                long curentValue = seed;
                for (int i = 0; i < 7; i++) {//convert for all map
                    curentValue = GardenConvert(curentValue, i);
                }
                if (curentValue < Result_PartTwo || Result_PartTwo == 0) {
                    Result_PartTwo = curentValue;
                }
                debugindex++;
            }
            //Result_PartTwo = LocationsForPartTwo.Min();
        }
        private long GardenConvert(long _sourceValue, int Mapindex) {
            long retval = _sourceValue;
            foreach(Range rule in Maps[Mapindex].Ranges) {
                //check if within range
                if(rule.SourceStart <= _sourceValue && _sourceValue < (rule.SourceStart + rule.Lenght)) {
                    //range ok -> calculate shift
                    long shift = rule.DestinationStart - rule.SourceStart;
                    retval = _sourceValue + shift;
                    break;
                }
            }

            return retval;
        }
    }
}
