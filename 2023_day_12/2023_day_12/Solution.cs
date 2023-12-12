namespace _2023_day_12 {
    public enum Type {
        Operational = 0,
        Damaged = 1,
        Unkown = 2,
    }
    public class Set {
        public List<Type> FirstSet = new List<Type>();
        public List<Type> FirstSetModified = new List<Type>();
        public List<int> SecondSet = new List<int>();
    }
    internal class Solution {
        //Brute force datasets
        public List<Set> RowsPartOne = new List<Set>();
        //public List<Set> RowsPartTwo = new List<Set>();

        public long Result_PartOne;
        public long Result_PartOne_Method_2;
        public long Result_PartTwo;
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
            while ((lineOfText = reader.ReadLine()) != null) {
                lineArray = lineOfText.Split(' ');
                Set newTmp = new Set();

                foreach (char chr in lineArray[0]) {
                    if (chr == '#') {
                        newTmp.FirstSet.Add(Type.Damaged);
                    } else if (chr == '.') {
                        newTmp.FirstSet.Add(Type.Operational);
                    } else if (chr == '?') {
                        newTmp.FirstSet.Add(Type.Unkown);
                    }
                }
                newTmp.SecondSet = lineArray[1].Split(',').Select(int.Parse).ToList();
                RowsPartOne.Add(newTmp);
            }
        }
        public void PartOne() {
            Result_PartOne = BruteForce(ref RowsPartOne);
        }
        public long BruteForce(ref List<Set> Rows) {
            long Retval = 0;
            for (int rowcnt = 0; rowcnt < Rows.Count; rowcnt++) {
                //for each row
                int digits = Rows[rowcnt].FirstSet.Where(x => x == Type.Unkown).Count();
                int numberofCylces = (int)Math.Pow(2, digits);
                for (int testcycles = 0; testcycles < numberofCylces; testcycles++) {
                    int currentDigit = 0;
                    Rows[rowcnt].FirstSetModified = new List<Type>();
                    for (int z = 0; z < Rows[rowcnt].FirstSet.Count(); z++) {
                        if (Rows[rowcnt].FirstSet[z] == Type.Unkown) {
                            if (((uint)(testcycles >> currentDigit) & (uint)0x1) == 1) {
                                Rows[rowcnt].FirstSetModified.Add(Type.Damaged);
                            } else {
                                Rows[rowcnt].FirstSetModified.Add(Type.Operational);
                            }
                            currentDigit++;
                        } else {
                            Rows[rowcnt].FirstSetModified.Add(Rows[rowcnt].FirstSet[z]);
                        }

                    }
                    if (TestOutput(Rows[rowcnt])) {
                        Retval++;
                    } else {
                        ;
                    }

                }
                ;
            }
            return Retval;
        }
        private bool TestOutput(Set _input) {
            List<int> retVal = new List<int>();
            int currentCnt = 0;
            foreach (Type typ in _input.FirstSetModified) {
                if (typ == Type.Damaged) {
                    currentCnt++;
                } else if (typ == Type.Operational) {
                    if (currentCnt != 0) {
                        retVal.Add(currentCnt);
                        currentCnt = 0;
                    }
                } else {
                    //ERROR
                    ;
                }
            }
            if (currentCnt != 0) {
                retVal.Add(currentCnt);
                currentCnt = 0;
            }

            bool AllEqual = true;
            if (retVal.Count == _input.SecondSet.Count) {
                for (int testIndex = 0; testIndex < retVal.Count(); testIndex++) {
                    if (retVal[testIndex] != _input.SecondSet[testIndex]) {
                        AllEqual = false;
                        break;
                    }
                }
            } else {
                AllEqual = false;
            }
            if (AllEqual) {
                return true;
            } else {
                return false;
            }
        }
        long debug;
        public void PartTwo() {

            Dictionary<string, long> cache = new Dictionary<string, long>();
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
            
            while ((lineOfText = reader.ReadLine()) != null) {
                lineArray = lineOfText.Split(' ');
                var springs = lineArray[0];
                var groups = lineArray[1].Split(',').Select(int.Parse).ToList();

                Result_PartOne_Method_2 += CalculateArrangement(springs, groups, springs);

                springs = string.Join('?', Enumerable.Repeat(springs, 5));
                groups = Enumerable.Repeat(groups, 5).SelectMany(g => g).ToList();

                Result_PartTwo += CalculateArrangement(springs, groups, springs);
                ;
            }
            ;

            //cached solution datasets

            
            long CalculateArrangement(string springs, List<int> Groups, string original) {
                var key = $"{springs},{string.Join(',', Groups)}";  // Cache key: spring pattern + group lengths
                if (cache.TryGetValue(key, out var value)) {
                    return value;
                }

                value = GetCount(springs, Groups, original);
                cache[key] = value;
                //Console.WriteLine("value " + value.ToString());
                return value;
            }
            long GetCount(string springs, List<int> groups, string original) {
                while (true) {
                    if (groups.Count == 0) {
                        if (springs.Contains('#')) {
                            return 0;
                        } else {
                            debug++;
                            return 1;
                        }
                        //return springs.Contains('#') ? 0 : 1; // No more groups to match: if there are no springs left, we have a match
                    }

                    if (string.IsNullOrEmpty(springs)) {
                        return 0; // No more springs to match, although we still have groups to match
                    }

                    if (springs.StartsWith('.')) {
                        //springs = springs.Trim('.'); // Remove all dots from the beginning
                        springs = springs[1..]; // Remove all dots from the beginning
                        continue;
                    }

                    if (springs.StartsWith('?')) {
                        long result = CalculateArrangement("." + springs[1..], groups, original) + CalculateArrangement("#" + springs[1..], groups, original);
                        return result; // Try both options recursively
                    }
                    if (springs.StartsWith('#')) // Start of a group
                    {
                        if (groups.Count == 0) {
                            return 0; // No more groups to match, although we still have a spring in the input
                        }

                        if (springs.Length < groups[0]) {
                            return 0; // Not enough characters to match the group
                        }

                        if (springs[..groups[0]].Contains('.')) {
                            return 0; // Group cannot contain dots for the given length
                        }

                        if (groups.Count > 1) {
                            if (springs.Length < groups[0] + 1 || springs[groups[0]] == '#') {
                                return 0; // Group cannot be followed by a spring, and there must be enough characters left
                            }

                            springs = springs[(groups[0] + 1)..]; // Skip the character after the group - it's either a dot or a question mark
                                                                  //groups = groups[1..];
                            //groups.RemoveAt(0); //does not work because its a reference
                            groups = groups.Skip(1).ToList();

                            continue;
                        }

                        springs = springs[groups[0]..]; // Last group, no need to check the character after the group
                        //groups = groups[1..];
                        //groups.RemoveAt(0);
                        groups = groups.Skip(1).ToList();


                        continue;
                    }

                    throw new Exception("Invalid input");
                }
            }
        }
    }
}
