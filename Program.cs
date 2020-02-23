using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Game_2 {
    class Program {

        public static int lineMin = 0;
        public static int lineMax = 100;

        public static SortedList<int, string> listOfElements = new SortedList<int, string> ();

        public static void Main (string[] args) {
            int posA = -1;
            int posB = -1;
            int posC = -1;
            int turnCounter = 0;

            //Initialization with user input
            while (posA == -1) {
                Console.WriteLine ("Enter the starting position of A:");
                posA = takeInput ();
                if (posA != -1 && keyDoesNotExist (posA)) {
                    listOfElements.Add (posA, "A");
                    break;
                } else {
                    Console.WriteLine ("Your input was incorrect. Choose between 0 and 100.");
                    posA = -1;
                }
            }
            while (posB == -1) {
                Console.WriteLine ("\nEnter the starting position of B:");
                posB = takeInput ();
                if (posB != -1 && keyDoesNotExist (posB)) {
                    listOfElements.Add (posB, "B");
                    break;
                } else {
                    Console.WriteLine ("Your input was incorrect. Choose between 0 and 100.");
                    posB = -1;
                }
            }
            while (posC == -1) {
                Console.WriteLine ("\nEnter the starting position of C:");
                posC = takeInput ();
                if (posC != -1 && keyDoesNotExist (posC)) {
                    listOfElements.Add (posC, "C");
                    break;
                } else {
                    Console.WriteLine ("Your input was incorrect. Choose between 0 and 100.");
                    posC = -1;
                }
            }

            Console.WriteLine ("\nAll configuration is done. Press any key to start.");
            Console.ReadKey ();
            Console.Clear ();

            Console.WriteLine ("\n\n");

            //Main game logic
            while (true) {
                turnCounter += 1;

                //A's turn
                if (turnCounter % 3 == 1) {
                    var tempVar = removeFromListAndReturn ("A");
                    int tempValue = tempVar.removedKey;

                    listOfElements.Add (findMaxVal (tempValue), "A");

                    ;

                }

                //B's turn
                if (turnCounter % 3 == 2) {
                    var tempVar = removeFromListAndReturn ("B");
                    int tempValue = tempVar.removedKey;

                    listOfElements.Add (findMaxVal (tempValue), "B");
                }

                //C's turn
                if (turnCounter % 3 == 0) {
                    var tempVar = removeFromListAndReturn ("C");
                    int tempValue = tempVar.removedKey;

                    listOfElements.Add (findMaxVal (tempValue), "C");
                }

                Console.WriteLine (">  A: " + findKeyFromValue ("A") + " - B: " + findKeyFromValue ("B") + " - C: " + findKeyFromValue ("C"));

                Thread.Sleep (800);

            }

        }

        public static int takeInput () {
            int fin = Convert.ToInt32 (Console.ReadLine ());
            if (fin >= lineMin && fin <= lineMax) {
                return fin;
            } else {
                return -1;
            }
        }

        public static bool keyDoesNotExist (int key) {

            if (listOfElements.ContainsKey (key)) {
                return false;
            } else {
                return true;
            }

        }

        public static int findMaxVal (int dummyInit) {

            float listMin = listOfElements.ElementAt (0).Key;
            float listMax = listOfElements.ElementAt (1).Key;
            float posInit = (float) dummyInit;

            float checkedMaxValue = -1;
            int checkedMaxValuePosition = -1;
            float tempVal = -1;

            SortedList<int, int> valueList = new SortedList<int, int> ();

            for (int i = lineMin; i < lineMax; i++) {

                if (i == listMin || i == listMax) {
                    continue;
                }
                if (i < listMin) {
                    tempVal = (i - lineMin) + ((listMin - i) / 2);
                    if (tempVal > checkedMaxValue) {
                        checkedMaxValue = tempVal;
                        checkedMaxValuePosition = i;
                    }

                    continue;
                }

                if (i > listMin && i < listMax) {
                    tempVal = ((i - listMin) / 2) + ((listMax - i) / 2);
                    if (tempVal > checkedMaxValue) {
                        checkedMaxValue = tempVal;
                        checkedMaxValuePosition = i;
                    }
                    continue;
                }

                if (i > listMax) {
                    tempVal = ((i - listMax) / 2) + (lineMax - i);
                    if (tempVal > checkedMaxValue) {
                        checkedMaxValue = tempVal;
                        checkedMaxValuePosition = i;
                    }
                    continue;
                }

            }

            //Calculating the initial value by hand just once here
            float initVal = -1;

            if (posInit < listMin) {
                initVal = (posInit - lineMin) + (listMin - posInit) / 2;
            }

            if (listMin < posInit && posInit < listMax) {
                initVal = (posInit - listMin) / 2 + (listMax - posInit) / 2;
            }

            if (listMax < posInit) {
                initVal = (lineMax - posInit) + (posInit - listMax) / 2;
            }

            if (checkedMaxValue > initVal) {
                return checkedMaxValuePosition;
            } else return dummyInit;

        }

        public static (int removedKey, string removedVal) removeFromListAndReturn (string name) {
            int index = listOfElements.IndexOfValue (name);
            int tempKey = listOfElements.ElementAt (index).Key;
            string tempVal = listOfElements.ElementAt (index).Value;

            listOfElements.RemoveAt (index);
            return (tempKey, tempVal);

        }

        public static int findKeyFromValue (string name) {
            int index = listOfElements.IndexOfValue (name);
            int key = listOfElements.ElementAt (index).Key;
            return key;
        }

    }
}