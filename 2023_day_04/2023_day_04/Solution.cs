using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_day_04 {
    public class Card {
        public int GameNumber;
        public List<uint> WinningNumbers = new List<uint>();
        public List<uint> MyNumbers = new List<uint>();
        public int Cnt;
    }
    internal class Solution {
        public uint Result_PartOne;
        public uint Result_PartTwo;
        public List<Card> CardList = new List<Card>();

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
            while ((lineOfText = reader.ReadLine()) != null) {
                Card newCard = new Card();
                newCard.Cnt = 1;
                lineArray = lineOfText.Split(":");
                newCard.GameNumber = Convert.ToInt32(lineArray[0].Split(" ").Last());
                lineArray = lineArray[1].Split("|");
                //for each winning number
                foreach (string nmb in lineArray[0].Trim().Split(" ")) {
                    if (nmb == String.Empty) {
                        continue;
                    }
                    newCard.WinningNumbers.Add(Convert.ToUInt32(nmb.Trim()));
                }
                //for each my number
                foreach (string nmb in lineArray[1].Trim().Split(" ")) {
                    if (nmb == String.Empty) {
                        continue;
                    }
                    newCard.MyNumbers.Add(Convert.ToUInt32(nmb.Trim()));
                }
                CardList.Add(newCard);
            }
        }
        public void PartOne() {
            //Part One
            foreach (Card card in CardList) {
                uint CardPoint = 0;
                foreach (int myNumber in card.MyNumbers) {
                    if (card.WinningNumbers.Any(x => x == myNumber)) {
                        if (CardPoint == 0) {
                            CardPoint = 1;
                        } else {
                            CardPoint *= 2;
                        }
                    }
                }
                Result_PartOne += CardPoint;
            }
        }
        public void PartTwo() {
            foreach (Card card in CardList) {
                uint CardPoint = 0;
                int point = card.MyNumbers.Intersect(card.WinningNumbers).ToList().Count();
                for (int i = card.GameNumber; i < (card.GameNumber + point); i++) {
                    CardList[i].Cnt += (1 * card.Cnt);
                }
            }
            foreach (Card card in CardList) {
                Result_PartTwo += (uint)card.Cnt;
            }
        }
    }
}
