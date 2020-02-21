using System;
using System.Collections.Generic;

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
            Console.WriteLine("Welcome to the Dice Game. \nYou can roll three dice. Make sure you don't roll a pair, or you might lose!");

            // initialize
            DiceGame currentGame = new DiceGame();
            currentGame.playerTurn = new PlayerTurn();
            currentGame.playerTurn.PlayerDice = new List<int>();
            currentGame.diceAmount = diceAmount;
            currentGame.diceType = diceType;

            //start
            currentGame.pushDie = Dice.DiceRoll(currentGame.diceType);
            Console.WriteLine("\nThe push is " + currentGame.pushDie);

            //players turn
            currentGame.playerTurn = PlayerRolls(currentGame);
            
            if (currentGame.playerTurn.Result)
            {
                //win
                currentGame.result = Enums.resultType.winDirect;
            } 
            else
            {
                // go to shove
                currentGame.result = Shove(currentGame);
            }

            switch (currentGame.result)
            {
                case Enums.resultType.winDirect:
                    Console.WriteLine("You won! single payout!");
                    break;
                case Enums.resultType.winShove:
                    Console.WriteLine("You won the Shove! double payout!");
                    break;
                case Enums.resultType.lose:
                    Console.WriteLine("You have lost!");
                    break;
                default:
                    Console.WriteLine("An error has occurred, We cannot determine the outcome of the game.");
                    break;


            }
            
            Console.WriteLine("\nPress 'p' to play again, press any other key to end the game");
            string input = Console.ReadLine();
            if (input == "p")
            {
                Play();
            }



        }
        static PlayerTurn PlayerRolls(DiceGame inputGame)
        {
            List<int> currentGameDice = new List<int>();
            currentGameDice.Add(inputGame.pushDie);

            PlayerTurn currentTurn = inputGame.playerTurn;

            int loop = 0;
            while (loop < inputGame.diceAmount)
            {
                Console.WriteLine("\nPress a key to roll a die");
                Console.ReadKey();
                int roll = Dice.DiceRoll(inputGame.diceType);
                Console.WriteLine("\t\tYou've rolled a " + roll);
                currentTurn.PlayerDice.Add(roll);
                
                foreach (int i in currentGameDice)
                {
                    if (i == roll)
                    {
                        currentTurn.pair = i;
                        Console.WriteLine("With this roll, you have pair of " + roll + "'s. Your turn has ended");
                        inputGame.playerTurn.Result = false;
                        currentTurn.Result = false;
                        return (currentTurn);
                    }
                    
                }
                currentGameDice.Add(roll);
                    
            
                loop++;
            }
            currentTurn.Result = true;
            return currentTurn;

        }

        static Enums.resultType Shove(DiceGame inputGame)
        {
            DiceGame shove = new DiceGame();
            shove.shoveDie = Dice.DiceRoll(inputGame.diceType);
            Console.WriteLine("\nÝou've got one chance to save yourself!");
            Console.WriteLine("\nPress a key to roll the die. You have to roll a " + inputGame.playerTurn.pair + " to win.");
            Console.ReadKey();
            Console.WriteLine("\tYou've rolled a " + shove.shoveDie + "!");
            
            if (shove.shoveDie == inputGame.playerTurn.pair)
            {
                shove.result = Enums.resultType.winShove;

            }
            else
            {
                shove.result = Enums.resultType.lose;
            }

            return shove.result;
        }
    }
}
