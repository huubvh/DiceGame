using System;
using System.Collections.Generic;

namespace DiceGame
{
    class PushShoveDiceGame
    {
        public static int winDirect = 0;
        public static int winShove = 0;
        public static int lose = 0;
        public static int countGames = 0;

        static void MainOld()
        {
            Initialize();
        }
        static void Initialize()
        {
            // initialize
            Random roll = new Random();
            int pushDie = roll.Next(1, 7);
            int matchOne = roll.Next(1, 7);
            int matchTwo = roll.Next(1, 7);
            int matchThree = roll.Next(1, 7);
            int shoveDie = roll.Next(1, 7);

            /*
            // testdata
            pushDie = 1;
            matchOne = 3;
            matchTwo = 4;
            matchThree = 2;
            shoveDie = 5;
            */

            // list dice for debug

            List<int> allDice = new List<int>();
            allDice.Add(pushDie);
            allDice.Add(matchOne);
            allDice.Add(matchTwo);
            allDice.Add(matchThree);
            allDice.Add(shoveDie);

            /*
            foreach (int i in allDice)
            {
                Console.Write(i + "/");    
            }
            Console.WriteLine("\n");
            */
            int result = 0;            
            bool lost = RunTheGame(pushDie, matchOne, matchTwo, matchThree, shoveDie);
            if (!lost)
            {
                Console.WriteLine("You have won!");
                result = 1;
            }

            EndGame(result);

        }
        
        static bool RunTheGame(int pushDie, int matchOne, int matchTwo,int matchThree, int shoveDie)
        {
            // start game
            Console.WriteLine("Welcome to the game. you can roll three dice. Make sure you don't roll a pair!");

            Console.WriteLine("\nThe push is " + pushDie);

            List<int> matchDice = new List<int>();
            matchDice.Add(matchOne);
            matchDice.Add(matchTwo);
            matchDice.Add(matchThree);

            bool lost = false;
            List<int> checkDice = new List<int>();
            checkDice.Add(pushDie);
            foreach (int i in matchDice)
            {
                Console.WriteLine("\nPress a key to roll a die");
                Console.ReadKey();
                Console.WriteLine("\tYou've rolled a " + i);

                lost = CheckForLoss(checkDice, i, shoveDie);

                checkDice.Add(i);
            }
            
            return lost; 
        }

        static bool CheckForLoss (List<int> checkDice, int currentDie, int shoveDie)
        {
            foreach (int i in checkDice)
            {
                if (i == currentDie)
                {
                    Console.WriteLine("With this roll, you have pair of " + i + "'s. You lose!");

                    Shove(currentDie, shoveDie);
                }
            }
            return false;
        }

        static void Shove(int currentDie,int shoveDie)
        {
            int result;
            Console.WriteLine("\nÝou've got one chance to save yourself!");
            Console.WriteLine("\nPress a key to roll the die. You have to roll a "+currentDie+" to win.");
            Console.ReadKey();
            Console.WriteLine("\tYou've rolled a " + shoveDie + "!");
            if (currentDie == shoveDie)
            {
                Console.WriteLine("Winner winner, chicken dinner!!");
                //win
                result = 2;
                EndGame(result);
            }
            else
            {
                Console.WriteLine("You couldn't save yourself, you've lost :(");
                //lose
                result = 3;
                EndGame(result);
            }


        }
        
        static void EndGame(int result)
        {
            if (result == 1)
            {
                winDirect++;
            }
            if (result == 2)
            {
                winShove++;
            }
            if (result == 3)
            {
                lose++;
            }
            countGames++;
            Console.WriteLine("\nPress 'p' to play again, press any other key to end the game");
            string input = Console.ReadLine();
            if (input == "p")
            {
                Initialize();
            }
            Console.WriteLine("Games played:\t" + countGames);
            Console.WriteLine("Games won directly\t" + winDirect);
            Console.WriteLine("Games won by shove\t" + winShove);
            Console.WriteLine("Games lost \t" + lose);

            Console.WriteLine("Thanks for playing!\n\n\n");
            Environment.Exit(1);

        }
    }
}
