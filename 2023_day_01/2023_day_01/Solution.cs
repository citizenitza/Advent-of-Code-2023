using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2023_day_01 {
    public class NumberString {
        public string Name;
        public string Value;
        public NumberString(string name, string value) {
            Name = name;
            Value = value;
        }
    }
    internal class Solution {

        public int Result_PartOne = 0;
        public int Result_PartTwo = 0;
        private List<NumberString> Items = new List<NumberString>();
        public void PartOne() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            int lineIndex = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);

            Result_PartOne = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
                string newInteger = "";
                //get first char
                for (int i  = 0; i < lineOfText.Length; i++) {
                    if (System.Char.IsDigit(lineOfText[i])) {
                        newInteger += lineOfText[i];
                        break;
                    }
                }
                //get last char
                //get first char
                for (int i = (lineOfText.Length-1); i >= 0; i--) {
                    if (System.Char.IsDigit(lineOfText[i])) {
                        newInteger += lineOfText[i];
                        break;
                    }
                }
                Result_PartOne += Convert.ToInt32(newInteger); ;
            }

        }

        public void PartTwo() {
            Init();
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            int lineIndex = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);

            Result_PartTwo = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
                //replace 
                string newInteger = "";
                //get first char
                for (int i = 0; i < lineOfText.Length; i++) {
                    if (System.Char.IsDigit(lineOfText[i])) {
                        newInteger += lineOfText[i];
                        break;
                    } else {
                        string resTmp = CheckIfStringDigit(lineOfText, i);
                        if (resTmp != "ERROR") {
                            newInteger += resTmp;
                            break;
                        }
                    }
                }
                //get last char
                //get first char
                for (int i = (lineOfText.Length - 1); i >= 0; i--) {
                    if (System.Char.IsDigit(lineOfText[i])) {
                        newInteger += lineOfText[i];
                        break;
                    } else {
                        string resTmp = CheckIfStringDigit(lineOfText, i);
                        if (resTmp != "ERROR") {
                            newInteger += resTmp;
                            break;
                        }
                    }
                }
                Result_PartTwo += Convert.ToInt32(newInteger); ;
            }

        }
        private string CheckIfStringDigit(string _original, int _position) {
            StringBuilder builder = new StringBuilder(_original);
            foreach(NumberString toReplace in Items) {
                try {
                    if (_original.Substring(_position, toReplace.Name.Length) == toReplace.Name) {
                        builder.Replace(toReplace.Name, toReplace.Value);
                        return toReplace.Value;
                    }
                }catch(Exception ex) when (ex is System.ArgumentOutOfRangeException) { 
                    continue;
                }
            }

            return "ERROR";
        }
        private void Init() {
            Items.Add(new NumberString("one", "1"));
            Items.Add(new NumberString("two", "2"));
            Items.Add(new NumberString("three", "3"));
            Items.Add(new NumberString("four", "4"));
            Items.Add(new NumberString("five", "5"));
            Items.Add(new NumberString("six", "6"));
            Items.Add(new NumberString("seven", "7"));
            Items.Add(new NumberString("eight", "8"));
            Items.Add(new NumberString("nine", "9"));
        }
    }
}
