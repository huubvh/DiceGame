using System;
using System.Collections.Generic;
using System.Text;

namespace DiceGame
{
    class Program
    {
  

        static void Main(string[] args)
        {
            Play();
        }

        static void Play()
        {
            Console.WriteLine("Welcome to the Casino, what do you want to play?");
            Console.WriteLine("For Dice press 'D'");
            Console.WriteLine("For Cards press 'C'");

            string input = Console.ReadLine();
                if (input == "d"|| input == "D")
            {
                PlayDiceGame(3,6);
            }
                if(input == "c"|| input == "C")
            {
                Console.WriteLine("This still needs to be implemented");
                Play();
            }
            else
            {
                Environment.Exit(1);
            }

        }
        static void PlayDiceGame(int diceAmount, int diceType)
        {
            Console.WriteLine("Welcome to the Dice Game. \nYou can roll three dice. Make sure you don't roll a pair, or you'll lose!");

            // initialize
            DiceGame currentGame = new DiceGame();
            currentGame.playerDice = new List<int>();
            currentGame.diceAmount = diceAmount;
            currentGame.diceType = diceType;

            //start
            currentGame.pushDie = Dice.DiceRoll(currentGame.diceType);
            Console.WriteLine("\nThe push is " + currentGame.pushDie);

            currentGame.playerDice = PlayerRolls(currentGame);
            


        }
        static List<int> PlayerRolls(DiceGame currentGame)
        {
            List<int> matchDice = new List<int>();
            List<int> checkDice = new List<int>();

            int loop = 0;
            while (loop < currentGame.diceAmount)
            {
                Console.WriteLine("\nPress a key to roll a die");
                Console.ReadKey();
                int roll = Dice.DiceRoll(currentGame.diceType);
                currentGame.playerDice.Add(roll);
                checkDice.Add(roll);
                Console.WriteLine("\t\tYou've rolled a " + roll);
                foreach (int i in checkDice)
                {
                    if (i == roll)
                    {
                        Console.WriteLine("With this roll, you have pair of " + roll + "'s. You lose!");
                    }
                }
                    
            
                matchDice.Add(roll);
                loop++;
            }

            return matchDice;

        }


    }
}
