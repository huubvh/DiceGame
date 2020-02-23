using System;
using System.Collections.Generic;

namespace DiceGame
{

    public class Casino
    {

        public static int playerBalance = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Casino, you have been awarded 100 in your balance. \nWhat do you want to play?");
            playerBalance = 100;
            Play();
        }

        public static void Play()
        {
           
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
        public static void PlayDiceGame(int diceAmount, int diceType)
        {
            Console.WriteLine("Welcome to the Dice Game. \nYou can roll three dice. Make sure you don't roll a pair, or you might lose!");


            // initialize
            DiceGame currentGame = new DiceGame();
            currentGame.playerTurn = new PlayerTurn();
            currentGame.playerTurn.PlayerDice = new List<int>();
            currentGame.diceAmount = diceAmount;
            currentGame.diceType = diceType;
            currentGame.bet = enterBet();
            
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
                    currentGame.win = currentGame.bet * 2;
                    Console.WriteLine("You won! Single payout, you win "+ currentGame.win +"!");
                    playerBalance += currentGame.win - currentGame.bet;
                    break;
                case Enums.resultType.winShove:
                    currentGame.win = currentGame.bet * 10;
                    playerBalance += currentGame.win - currentGame.bet;
                    Console.WriteLine("You won the Shove! Nine times payout, you win "+ currentGame.win +"!");
                    break;
                case Enums.resultType.lose:
                    Console.WriteLine("You have lost, better luck next time");
                    playerBalance -= currentGame.bet;
                    break;
                default:
                    Console.WriteLine("An error has occurred, We cannot determine the outcome of the game.");
                    break;


            }

            Console.WriteLine("\nYour current balance = " + playerBalance);
            Console.WriteLine("\nPress 'p' to play again, press any other key to end the game");
            string input = Console.ReadLine();
            if (input == "p")
            {
                Play();
            }



        }
        public static PlayerTurn PlayerRolls(DiceGame inputGame)
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

        public static Enums.resultType Shove(DiceGame inputGame)
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

        public static int enterBet()
        {
            int betAmount = 0;
            Console.WriteLine("Your current balance = "+playerBalance);
            Console.WriteLine("How much do you want to bet?");
            string betAmountString = Console.ReadLine();
            
            try
            {
                betAmount = int.Parse(betAmountString);
            }
            catch (FormatException)
            {
                Console.WriteLine($"you have not entered a valid amount");
                enterBet();
            }
            
            return (betAmount);

        }
    }
}
