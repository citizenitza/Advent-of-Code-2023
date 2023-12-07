namespace _2023_day_07 {
    public enum Type {
        Error = -1,
        FiveOfAKind = 0,
        FourOfAKind = 1,
        FullHouse = 2,
        ThreeOfAKind = 3,
        TwoPair = 4,
        OnePair = 5,
        HighCard = 6,
    }
    public class Hand {
        public string Cards;
        public string CardsOriginal;
        public uint Bid;
        public Type HandType;
        public Type PartTwoType;
    }
    internal class Solution {
        public uint Result_PartOne;
        public uint Result_PartTwo;
        private List<Hand> HandList = new List<Hand>();
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
            while ((lineOfText = reader.ReadLine()) != null) {
                lineArray = lineOfText.Split();
                Hand newHand = new Hand();
                newHand.CardsOriginal = lineArray[0];
                newHand.Cards = lineArray[0].Replace('A', 'Z').Replace('K', 'Y').Replace('Q', 'X').Replace('J', 'W');
                newHand.Bid = Convert.ToUInt32(lineArray[1]);
                newHand.HandType = GetHandType(newHand.Cards);
                newHand.PartTwoType = PartTwoType(newHand.HandType,newHand.CardsOriginal);
                if (newHand.HandType == Type.Error) {
                    ;
                }
                HandList.Add(newHand);
            }
        }

        public void PartOne() {
            SortHands(false);
            for (int i = 0; i < HandList.Count; i++) {
                Result_PartOne += HandList[i].Bid * ((uint)i + 1);
            }
        }


        private void SortHands(bool PartTwo) {

            for (int i = 0; i < HandList.Count; i++) {
                int smallestIndex = i;
                for (int j = i + 1; j < HandList.Count; j++) {
                    if (!Compare(HandList[j], HandList[smallestIndex], PartTwo)) {
                        smallestIndex = j;
                    }
                }
                Swap(HandList, i, smallestIndex);
            }
        }
        public static void Swap<T>(IList<T> list, int indexA, int indexB) {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
        private bool Compare(Hand A, Hand B,bool PartTwo) { //true if A > B
            bool retval = false;
            if (PartTwo) {
                if (A.PartTwoType < B.PartTwoType) {
                    retval = true;
                } else if (A.PartTwoType == B.PartTwoType) {
                    //check digits
                    for (int i = 0; i < 5; i++) {
                        if (A.Cards[i] > B.Cards[i]) {
                            return true;
                        } else if (A.Cards[i] < B.Cards[i]) {
                            return false;
                        }
                    }
                } else {
                    retval = false;
                }
            } else {
                if (A.HandType < B.HandType) {
                    retval = true;
                } else if (A.HandType == B.HandType) {
                    //check digits
                    for (int i = 0; i < 5; i++) {
                        if (A.Cards[i] > B.Cards[i]) {
                            return true;
                        } else if (A.Cards[i] < B.Cards[i]) {
                            return false;
                        }
                    }
                } else {
                    retval = false;
                }
            }
            return retval;
        }
        private Type PartTwoType(Type _typ, string _hand) {
            int JokerCount = _hand.Count(x => x == 'J');
            if (JokerCount > 0) {
                 if (_typ == Type.FiveOfAKind) {
                    return Type.FiveOfAKind;
                } else if (_typ == Type.FourOfAKind) {
                    return Type.FiveOfAKind;
                } else if (_typ == Type.FullHouse) {
                    if (JokerCount == 2) {
                        return Type.FiveOfAKind;
                    } else if (JokerCount == 3) {
                        return Type.FiveOfAKind;
                    }
                } else if (_typ == Type.ThreeOfAKind) {
                    if (JokerCount == 1) {
                        return Type.FourOfAKind;
                    } else if (JokerCount == 3) {
                        return Type.FourOfAKind;
                    }
                } else if (_typ == Type.TwoPair) {
                    if (JokerCount == 1) {
                        return Type.FullHouse;
                    } else if (JokerCount == 2) {
                        return Type.FourOfAKind;
                    }
                } else if (_typ == Type.OnePair) {
                    if (JokerCount == 1) {
                        return Type.ThreeOfAKind;
                    } else if (JokerCount == 2) {
                        return Type.ThreeOfAKind;
                    }
                } else if (_typ == Type.HighCard) {
                    if (JokerCount == 1) {
                        return Type.OnePair;
                    }
                } else {
                    return Type.Error;
                }
                return Type.Error;
            } else {
                return _typ;
            }
        }
        private Type GetHandType(string _hand) {
            int count = _hand.Distinct().Count();
            string distinct = new string(_hand.Distinct().ToArray());
            if (count == 1) {
                //Five of a kind
                return Type.FiveOfAKind;
            } else if (count == 2) {
                //four  of a kind or full house
                if (_hand.Count(x => x == distinct[0]) == 4 || _hand.Count(x => x == distinct[1]) == 4) {
                    //four of a kind
                    return Type.FourOfAKind;
                } else {
                    //full house
                    return Type.FullHouse;
                }
            } else if (count == 3) {
                //two pair or three of a kind
                if (_hand.Count(x => x == distinct[0]) == 3 || _hand.Count(x => x == distinct[1]) == 3 || _hand.Count(x => x == distinct[2]) == 3) {
                    //four of a kind
                    return Type.ThreeOfAKind;
                } else {
                    //full house
                    return Type.TwoPair;
                }
            } else if (count == 4) {
                //one pair
                return Type.OnePair;
            } else if (count == 5) {
                //High card
                return Type.HighCard;
            } else {
                //shouldnt be here
                return Type.Error;
                ;
            }
        }
        public void PartTwo() {
            foreach(Hand hnd in HandList) {
                hnd.Cards = hnd.Cards.Replace('W', '0');//replace jokers to lowest value
            }
            SortHands(true);
            for (int i = 0; i < HandList.Count; i++) {
                Result_PartTwo += HandList[i].Bid * ((uint)i + 1);
            }
        }

    }
}
