using System;
using System.Security.Principal;
using System.Threading;

namespace FinalExerciseOfCSharp
{
    internal class Program
    {
        static string player1Name, player2Name;
        static int dragonDistance, Player2Guess, gameTurn = 0, dragonMaxHP = 15, dragonHP = 15, cityMaxHP = 15, cityHP = 15, point;

        static void Main(string[] args)
        {
            PlayGame();
        }

        static void PlayGame()
        {
            gameTurn = 0;
            dragonMaxHP = 15;
            dragonHP = 15;
            cityMaxHP = 15;
            cityHP = 15;
            GameSetup();
            AskForDragonDistance();
            GamePlay();
        }

        // Starts the game with introduction
        static void GameSetup()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.Title = "Defend the city";

            Console.WriteLine("City Guard: Oh Nooooo 😨..... run awayyy DRAGON IS COminG Its goNna KIlL aS aLLLLL.... 😭😭");

            Console.ForegroundColor = ConsoleColor.Red;
            player2Name = Input("???: Don't worry I am the Grate hero (Enter your name, Player 2): ");
            Console.ForegroundColor = ConsoleColor.Red;
            player1Name = Input($"{player2Name} shouts: AND I WILL 🗡️ HELP THE CITY BY DEFENDING AGAINST THE EVIL DRAGON (Enter Player 1's Name): ");

            Console.ForegroundColor = ConsoleColor.Black;
        }

        // Function to ask for dragon's distance.
        static void AskForDragonDistance()
        {
            while (true)
            {
                dragonDistance = GetValidDragonDistance($"Player 1 {player1Name}: how far is the dragon from the city? (Do not show this to Player 2 {player2Name})");
                Thread.Sleep(1000);
                Console.Clear();

                break;
            }
        }

        // Function to tell Situation and get player 2 input
        static void GamePlay()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"Good. Now it's Player 2's ({player2Name}) turn.\n");

            while (true)
            {
                gameTurn++;

                //tell Situation
                Console.WriteLine($"Situation: Turn: {gameTurn} City: {cityHP}/{cityMaxHP} Dragon {dragonHP}/{dragonMaxHP}");
                Console.ForegroundColor = ConsoleColor.Green;
                //get player 2 input
                Console.WriteLine($"If you hit the dragon on this turn you will get {TurnPoint(gameTurn)} point");
                Console.ForegroundColor = ConsoleColor.Black;

                Player2Guess = Convert.ToInt16(Input("How far do you want to shoot your magic? "));
                CheckIfHit(Player2Guess, dragonDistance);

                //check if game over
                if (cityHP == 0)
                {
                    DefeatDragonStory();
                }
                else if (dragonHP == 0)
                {
                    DragonWinStory();
                }
            }
        }

        // Check if player 2 hit the dragon or not
        static void CheckIfHit(int distant, int hitDistant)
        {
            if (distant == hitDistant)
            {
                Console.WriteLine($"It's a hit! Current point: {point += TurnPoint(gameTurn)}");
                AskForDragonDistance();
                dragonHP--;
            }
            else if (distant <= distant - 20)
            {
                Console.WriteLine("Your shot wasn't even near it, try shooting further");
                cityHP--;
            }
            else if (distant >= distant + 20)
            {
                Console.WriteLine("Your shot went too far");
                cityHP--;
            }
            else
            {
                Console.WriteLine("That was close but didn't touch the dragon, try again");
                cityHP--;
            }
        }

        // Function to get a valid dragon distance input
        static int GetValidDragonDistance(string prompt)
        {
            while (true)
            {
                int distance = Convert.ToInt16(Input($"{prompt}: "));
                if (distance >= 0 && distance <= 100)
                {
                    return distance;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Please enter a valid number between 0 and 100.");
                    Console.ForegroundColor = ConsoleColor.Black;
                }
            }
        }

        static int TurnPoint(int gameTurn)
        {
            if (gameTurn % 3 == 0 && gameTurn % 5 == 0)
            {
                return 10;
            }
            else if (gameTurn % 3 == 0)
            {
                return 3;
            }
            else
            {
                return 1;
            }
        }

        // Function to display the story when the dragon is defeated
        static void DefeatDragonStory()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\nPlayer 1 {player1Name}, the Great Dragon wins.\n" +
                $" {player1Name} destroys the city with his/her flaming hot fire, and {player1Name} goes to another city to destroy it as well. \n" +
                $"While the dragon was going to another city, the great hero {player2Name} meets a god.\n She says, " +
                $"\"Do you wish to defeat the dragon and stop him/her from destroying the world?\"\n" +
                $"She continues, \"You, who has collected {point} dragon blood, have the right to get reincarnated.\"" +
                $"\nWell, do you wish to fight the Dragon again even though The Great dragon {player1Name} has fully recovered?");

            string playAgain = Input("Your choice (Y/N): ");
            PlayAgain(playAgain);


        }

        // Function to display the story when the dragon is defeated
        static void DragonWinStory()
        {
            Console.WriteLine($"\n\nCongratulations, {player2Name}! You have defeated the mighty dragon {player1Name} and saved the city!\n" +
                $"The people cheer for you, and you become a legendary hero. The city is safe, and you have collected " +
                $"{point} dragon blood as a symbol of your victory." +
                $"\nAfter may years: the dragon blood you have react strongly and they burst.\nYou get to know that an another dragon has come to distroy the city...");

            string playAgain = Input("The people of the city come to beg you for help will you try to kill the dragon again? (Y/N): ");
            PlayAgain(playAgain);
        }

        static void PlayAgain(string userInput)
        {
            if (userInput.ToLower() == "y" || userInput.ToLower() == "yes")
            {
                PlayGame();
            }
            else
            {
                Console.WriteLine("Well.. Everyone should make there own chooses.");
                Thread.Sleep(1000);
                System.Environment.Exit(1);
            }
        }

        // Function to get player input
        static string Input(string prompt)
        {
            Console.Write(prompt);
            Console.ForegroundColor = ConsoleColor.Blue;
            string input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Black;
            return input;
        }
    }
}
