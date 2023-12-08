using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_day_08 {
    public class Node {
        public string Name;
        public string Left;
        public string Right;
    }
    internal class Solution {
        public uint Result_PartOne;
        public ulong Result_PartTwo;
        private List<string> Instructions = new List<string>();
        private List<string> PartTwoStartPositions = new List<string>();
        private List<Node> Nodes = new List<Node>();
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
                if(lineOfText == string.Empty) {
                    continue;
                }
                if (firstline) {
                    firstline = false;
                    foreach(char ch in lineOfText) {
                        Instructions.Add(ch.ToString());
                    }
                } else {
                    lineArray = lineOfText.Split('=');
                    Node newTmp = new Node();
                    newTmp.Name = lineArray[0].Trim();
                    lineArray = lineArray[1].Replace("(", "").Replace(")", "").Split(',');
                    newTmp.Left = lineArray[0].Trim();
                    newTmp.Right = lineArray[1].Trim();
                    Nodes.Add(newTmp);
                    if (newTmp.Name[2] == 'A') {
                        PartTwoStartPositions.Add(newTmp.Name);
                    }
                }
            }
        }

        public void PartOne() {
            string currentPos = "AAA";
            int currentNodeIndex = getNodeIndex(currentPos);
            int instructionIndex = 0;
            uint steps = 0;
            while(currentPos != "ZZZ") {
                //make step
                steps++;
                string nextPos = "";
                if (Instructions[instructionIndex] == "L") {
                    nextPos = Nodes[currentNodeIndex].Left;

                }else if (Instructions[instructionIndex] == "R") {
                    nextPos = Nodes[currentNodeIndex].Right;
                }
                currentNodeIndex = getNodeIndex(nextPos);
                currentPos = Nodes[currentNodeIndex].Name;
                instructionIndex++;
                if (instructionIndex >= Instructions.Count) {
                    instructionIndex = 0;
                }
            }
            Result_PartOne = steps;
        }
        private int getNodeIndex(string _Name) {
            int index = 0;
            foreach (Node node in Nodes) {
                if (node.Name == _Name) {
                    return index; 
                }
                index++;
            }
            return -1;//Error
        }
        public void PartTwo() {

            ulong[] Results = new ulong[PartTwoStartPositions.Count];
            for (int i = 0; i < PartTwoStartPositions.Count; i++) { // for each starting position
                string currentPos = PartTwoStartPositions[i];
                int currentNodeIndex = getNodeIndex(currentPos);
                int instructionIndex = 0;
                uint steps = 0;
                while (currentPos[2] != 'Z') {
                    //make step
                    steps++;
                    string nextPos = "";
                    if (Instructions[instructionIndex] == "L") {
                        nextPos = Nodes[currentNodeIndex].Left;

                    } else if (Instructions[instructionIndex] == "R") {
                        nextPos = Nodes[currentNodeIndex].Right;
                    }
                    currentNodeIndex = getNodeIndex(nextPos);
                    currentPos = Nodes[currentNodeIndex].Name;
                    instructionIndex++;
                    if (instructionIndex >= Instructions.Count) {
                        instructionIndex = 0;
                    }
                }
                Results[i] = steps;
            }

            Result_PartTwo = 1;
            for (int i = 0; i < Results.Count(); i++) {
                Result_PartTwo = lcm(Result_PartTwo, Results[i]);
            }
        }

        static ulong gcf(ulong a, ulong b) {
            while (b != 0) {
                ulong temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static ulong lcm(ulong a, ulong b) {
            return (a / gcf(a, b)) * b;
        }
    }
}
