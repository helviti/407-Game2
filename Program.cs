using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Game_2 {
    class Program {

        public static int lineMin = 0;
        public static int lineMax = 100;

        public static int turnCounter = 0;

        static int[] pos = {-1, -1, -1 };

        static int?[] satVal = { null, null, null };

        static string[] players = { "Ali", "Can", "Dilan" };

        public static SortedList<int, string> listOfElements = new SortedList<int, string> ();

        public static void Main (string[] args) {
            //Main game loop
            while (true) {
                Console.Clear ();
                gameInit ();
                Console.WriteLine ("Press any key to pass the turn to the next player. Press (ESC) to exit and start over. \n");
                gameRun ();
            }

        }

        public static void gameInit () {

            Console.WriteLine ("Welcome to the optimal store value game!\n");
            //Initialization with user input
            while (pos[0] == -1) {
                Console.WriteLine ("Enter the starting position of {0}:", players[0]);
                pos[0] = takeInput ();
                if (pos[0] != -1 && keyDoesNotExist (pos[0])) {
                    listOfElements.Add (pos[0], players[0]);
                    break;
                } else {
                    Console.WriteLine ("Your input was incorrect. Choose between 0 and 100.");
                    pos[0] = -1;
                }
            }
            while (pos[1] == -1) {
                Console.WriteLine ("\nEnter the starting position of {0}:", players[1]);
                pos[1] = takeInput ();
                if (pos[1] != -1 && keyDoesNotExist (pos[1])) {
                    listOfElements.Add (pos[1], players[1]);
                    break;
                } else {
                    Console.WriteLine ("Your input was incorrect. Choose between 0 and 100.");
                    pos[1] = -1;
                }
            }
            while (pos[2] == -1) {
                Console.WriteLine ("\nEnter the starting position of {0}:", players[2]);
                pos[2] = takeInput ();
                if (pos[2] != -1 && keyDoesNotExist (pos[2])) {
                    listOfElements.Add (pos[2], players[2]);
                    break;
                } else {
                    Console.WriteLine ("Your input was incorrect. Choose between 0 and 100.");
                    pos[2] = -1;
                }
            }

            Console.Clear ();

            while (satVal[0] == null) {
                Console.WriteLine ("\nEnter the saturation value of {0} if you want to limit the values, enter (0) otherwise.", players[0]);
                var tempVal = takeInput ();
                if (tempVal != -1) {
                    satVal[0] = tempVal;
                    break;
                } else {
                    Console.WriteLine ("Your input was incorrect. Choose between 0 and 100.");
                    satVal[0] = null;
                }
            }
            while (satVal[1] == null) {
                Console.WriteLine ("\nEnter the saturation value of {0} if you want to limit the values, enter (0) otherwise.", players[1]);
                var tempVal = takeInput ();
                if (tempVal != -1) {
                    satVal[1] = tempVal;
                    break;
                } else {
                    Console.WriteLine ("Your input was incorrect. Choose between 0 and 100.");
                    satVal[1] = null;
                }
            }
            while (satVal[2] == null) {
                Console.WriteLine ("\nEnter the saturation value of {0} if you want to limit the values, enter (0) otherwise.", players[2]);
                var tempVal = takeInput ();
                if (tempVal != -1) {
                    satVal[2] = tempVal;
                    break;
                } else {
                    Console.WriteLine ("Your input was incorrect. Choose between 0 and 100.");
                    satVal[2] = null;
                }
            }
            Console.WriteLine ("\nAll configuration is done. Press any key to start.");
            Console.ReadKey ();
            Console.Clear ();

            Console.WriteLine ("\n\n");

        }

        public static void gameRun () {
            while (true) {
                turnCounter += 1;

                Console.WriteLine (">  {0}: " + findKeyFromValue (players[0]) + " - {1}: " + findKeyFromValue (players[1]) + " - {2}: " + findKeyFromValue (players[2]), players[0], players[1], players[2]);

                //A's turn
                if (turnCounter % 3 == 1) {
                    var tempVar = removeFromListAndReturn (players[0]);
                    int tempValue = tempVar.removedKey;

                    listOfElements.Add (findMaxVal (tempValue), players[0]);

                    ;

                }

                //B's turn
                if (turnCounter % 3 == 2) {
                    var tempVar = removeFromListAndReturn (players[1]);
                    int tempValue = tempVar.removedKey;

                    listOfElements.Add (findMaxVal (tempValue), players[1]);
                }
                
                //C's turn
                if (turnCounter % 3 == 0) {
                    var tempVar = removeFromListAndReturn (players[2]);
                    int tempValue = tempVar.removedKey;

                    listOfElements.Add (findMaxVal (tempValue), players[2]);
                }

                // Thread.Sleep (800);
                if (Console.ReadKey ().Key == ConsoleKey.Escape) {
                    Console.WriteLine ("esc pressed");
                    break;
                }
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

            //Find whose turn it currently is
            int turnIndex = (turnCounter-1)%3;






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


            if (initVal>=satVal[turnIndex] && satVal[turnIndex]!=0){
                return dummyInit;
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