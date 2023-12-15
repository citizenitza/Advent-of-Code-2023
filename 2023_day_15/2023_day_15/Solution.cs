using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_day_15 {
    public class Lens{
        public string Label;
        public int FocalPoint;
    }
    public class Box {
        public List<Lens> Lenses = new List<Lens>();
    }
    internal class Solution {
        public long Result_PartOne;
        public long Result_PartTwo;
        List<string> Inputs = new List<string>();
        Box[] Boxes = new Box[256];

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
            //fist line
            bool firstline = true;
            while ((lineOfText = reader.ReadLine()) != null) {
                Inputs = lineOfText.Split(',').ToList();
                
            }
        }
        public void PartOne() {
            foreach (string input in Inputs) {

                Result_PartOne +=GetHash(input);
            }
        }
        public void PartTwo() {
            Init();
            string[] lineArray;
            foreach (string input in Inputs) {
                if (input.Any(x => x == '-')) {
                    lineArray = input.Split('-');
                    GetHash(lineArray[0]);
                    ;
                    Boxes[GetHash(lineArray[0])].Lenses.Remove(Boxes[GetHash(lineArray[0])].Lenses.Find(x => x.Label == lineArray[0]));
                    ;
                } else if (input.Any(x => x == '=')) {
                    lineArray = input.Split('=');
                    //add new

                    if (Boxes[GetHash(lineArray[0])].Lenses.Any(x => x.Label == lineArray[0])){
                        Boxes[GetHash(lineArray[0])].Lenses.Find(x => x.Label == lineArray[0]).FocalPoint = Convert.ToInt32(lineArray[1]);
                    } else {
                        Lens newLense = new Lens();
                        newLense.Label = lineArray[0];
                        newLense.FocalPoint = Convert.ToInt32(lineArray[1]);
                        Boxes[GetHash(lineArray[0])].Lenses.Add(newLense);
                    }


                } else {
                    throw new Exception("Invalid input");
                }
            }
            for (int i = 0; i < 256; i++) {
                int fp_p1 = 1 + i;

                int slotnumber = 1;
                foreach(Lens ln in Boxes[i].Lenses) {
                    int fp_p2 = 0;
                    int fp_p3 = 0;
                    fp_p2 = slotnumber;
                    fp_p3 = ln.FocalPoint;
                    long res = (fp_p1 * fp_p2 * fp_p3);
                    Result_PartTwo += res;
                    slotnumber++;
                }
            }

        }
        private long GetHash(string _input) {
            long retVal = 0;
            byte[] ascii = Encoding.ASCII.GetBytes(_input);
            foreach(byte ch in ascii) {
                retVal += ch;
                retVal *= 17;
                retVal = (retVal % 256);
            }
            return retVal;
        }
        private void Init() {
            for(int i = 0; i < 256; i++) {
                Boxes[i] = new Box();
                Boxes[i].Lenses = new List<Lens>();
            }
        }

    }
}
