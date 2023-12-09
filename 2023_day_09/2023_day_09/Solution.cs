namespace _2023_day_09 {
    public class Sequence {
        public List<List<int>> Level = new List<List<int>>(); //0-input, 1-first differences

    }
    internal class Solution {
        public int Result_PartOne;
        public int Result_PartTwo;
        public List<Sequence> SequenceList = new List<Sequence>();

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
            //fist line
            bool firstline = true;
            while ((lineOfText = reader.ReadLine()) != null) {
                Sequence newtmp = new Sequence();
                newtmp.Level.Add(lineOfText.Split(' ').Select(int.Parse).ToList());
                SequenceList.Add(newtmp);
            }
        }
        public void PartOne() {
            //Create levels
            foreach (Sequence seq in SequenceList) {
                int level = 0;
                bool ZeroSumReached = false;
                while (!ZeroSumReached) {
                    //add new level
                    seq.Level.Add(new List<int>());
                    for (int i = 1; i < seq.Level[level].Count(); i++) {//start from second
                        seq.Level[level + 1].Add(seq.Level[level][i] - seq.Level[level][i - 1]); //add differential to the next level;   
                    }
                    level++;
                    if (!seq.Level[level].Any(x => x != 0)) {
                        seq.Level[level].Add(0);//add extra zero, maybe not needed at all
                        ZeroSumReached = true;
                        break;
                    }
                }
                //Extrapolate
                //bot->top
                for (int i = seq.Level.Count() - 2; i >= 0; i--) {
                    int newItem = seq.Level[i].Last() + seq.Level[i + 1].Last();
                    seq.Level[i].Add(newItem);
                }
                //create result
                Result_PartOne += seq.Level[0].Last();
            }


        }
        public void PartTwo() {
            foreach (Sequence seq in SequenceList) {
                //Extrapolate
                //bot->top
                for (int i = seq.Level.Count() - 2; i >= 0; i--) {
                    int newItem = seq.Level[i].First() - seq.Level[i + 1].First();
                    seq.Level[i].Insert(0, newItem);
                }

                Result_PartTwo += seq.Level[0].First();

            }
        }
    }

}
