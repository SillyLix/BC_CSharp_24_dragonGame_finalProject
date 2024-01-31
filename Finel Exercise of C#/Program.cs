using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;



namespace FinalExerciseOfCSharp
{
    internal class Program
    {
        // variables that i need
        static char hasFriend;
        static string player1Name, player2Name;
        static int dragonDistance, Player2Guess, gameTurn = 0, dragonMaxHP = 10, dragonHP = dragonMaxHP, cityMaxHP = 15, cityHP = cityMaxHP, point;

        // Main game
        static void Main(string[] args)
        {
            PlayGame();
        }

        // it resets game / runs the game 
        static void PlayGame()
        {
            Console.Clear();
            hasFriend = 'o';
            gameTurn = 0;
            dragonMaxHP = 5;
            dragonHP = dragonMaxHP;
            cityMaxHP = 20;
            cityHP = cityMaxHP;
            GameSetup();
            AskForDragonDistance();
            GamePlay();
        }

        // Starts the game with introduction
        static void GameSetup()
        {
            //start up meta
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.Title = "Defend the city";


            //-------game------
            Console.WriteLine("City Guard: Oh Nooooo 😨..... run awayyy DRAGON IS COminG Its goNna KIlL uS aLLLLL.... 😭😭");
            Thread.Sleep(700);
            Console.ForegroundColor = ConsoleColor.Red; Console.BackgroundColor = ConsoleColor.Yellow;
            player2Name = Input("???: Don't worry I am the Great hero (Enter your name, Player 2): ");
            Thread.Sleep(700);


            // asks if player needs ai
            Console.ForegroundColor = ConsoleColor.Green; Console.BackgroundColor = ConsoleColor.Black;
            do
            {
                hasFriend =  InputChar("AI over here. Do you have a friend? Y/N: ").KeyChar;
                Console.Write("\n");
                if (hasFriend == 'n')
                {
                    player1Name = "AI 🤖";
                    Console.ForegroundColor = ConsoleColor.Green; Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine($"{player1Name} : Haha.. just as i thought you wouldn't have a friend 😆😈");
                    Thread.Sleep(2000);
                    break;
                }
                else if (hasFriend == 'y')
                {
                    Console.ForegroundColor = ConsoleColor.Green; Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine($"Damnnn... didn't thought you would have one. you sure!!... Well whatever.. ");
                    Console.ForegroundColor = ConsoleColor.Red; Console.BackgroundColor = ConsoleColor.Yellow;
                    player1Name = Input($"{player2Name} shouts: AND I WILL 🗡️ HELP THE CITY BY DEFENDING AGAINST THE EVIL DRAGON (Enter Player 1's Name): ");


                    break;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nPlease kindly put a \"y\" = yes or \"n\" = no \n");
            } while (true);

            Console.ForegroundColor = ConsoleColor.Red; Console.BackgroundColor = ConsoleColor.Yellow;

        }

        static void AiNumGen()
        {

            Console.ForegroundColor = ConsoleColor.Green; Console.BackgroundColor = ConsoleColor.Black;
            Random randomDis = new Random();
            dragonDistance = randomDis.Next(100);

            Console.ForegroundColor = ConsoleColor.Red; Console.BackgroundColor = ConsoleColor.Yellow;

        }
        // Function to ask for dragon's distance.
        static void AskForDragonDistance()
        {
            if (hasFriend == 'n')
            {
                Console.WriteLine("\n");

                AiNumGen();
            }
            else
            {
                while (true)
                {
                    dragonDistance = GetValidDragonDistance($"Player 1 {player1Name}: how far is the dragon from the city? (Do not show this to Player 2 {player2Name})");

                    break;
                }
            }

            Thread.Sleep(1000);
            Console.Clear();

        }

        // Function to tell Situation and get player 2 input
        static void GamePlay()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Clear();
            string dashWall = "-----------------------------------------------------------";
            Console.WriteLine($"\nit's Player 2's ({player2Name}) turn.");
            Thread.Sleep(700);
            
            while (true)
            {
                gameTurn++;

                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;

                Console.WriteLine(dashWall);
                //tell Situation
                Console.WriteLine($"Situation: Turn: {gameTurn} City: {cityHP}/{cityMaxHP} Dragon {dragonHP}/{dragonMaxHP}    {dragonDistance}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"If you hit the dragon on this turn you will get {TurnPoint(gameTurn)} point");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(dashWall + "\n\n");
                Thread.Sleep(700);

                //check if game over
                if (cityHP == 0)
                {
                    DefeatDragonStory();
                }
                else if (dragonHP == 0)
                {
                    DragonWinStory();
                }

                //get player 2 input
                Player2Guess = Convert.ToInt16(Input("How far do you want to shoot your magic? "));
                Thread.Sleep(700);

                CheckIfHit(Player2Guess, dragonDistance);
            }
        }

        // Check if player 2 hit the dragon or not
        static void CheckIfHit(int distant, int hitDistant)
        {
            if (distant <= 100 && distant >= 0)
            {
                if (distant == hitDistant)
                {
                    Console.WriteLine($"It's a hit! Current point: {point += TurnPoint(gameTurn)}");
                    AskForDragonDistance();
                    dragonHP--;
                    Thread.Sleep(1000);
                }

                else if (hitDistant > distant + 2)
                {
                    Console.WriteLine("try shooting it further");
                    cityHP--;
                    Thread.Sleep(500);
                }
                else if (hitDistant < distant - 2)
                {
                    Console.WriteLine("Your shot went too far");
                    cityHP--;
                    Thread.Sleep(500);
                }
                else
                {
                    Console.WriteLine("That was close but didn't touch the dragon, try again");
                    cityHP--;
                    Thread.Sleep(500);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Please enter a valid number between 0 and 100.");
                Console.ForegroundColor = ConsoleColor.Black;
                Thread.Sleep(500);
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

        // Function gives point depending on turn.
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
                Console.ForegroundColor = ConsoleColor.Red;

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
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"\n\nCongratulations, {player2Name}! You have defeated the mighty dragon {player1Name} and saved the city!\n" +
                $"The people cheer for you, and you become a legendary hero. The city is safe, and you have collected " +
                $"{point} dragon blood as a symbol of your victory." +
                $"\nAfter may years: the dragon blood you have react strongly and they burst.\nYou get to know that an another dragon has come to distroy the city...");

            Console.ForegroundColor= ConsoleColor.Red;
            string playAgain = Input("The people of the city come to beg you for help will you try to kill the dragon again? (Y/N): ");
            PlayAgain(playAgain);
        }

        //If player says yes play again
        static void PlayAgain(string userInput)
        {
            if (userInput.ToLower() == "y" || userInput.ToLower() == "yes")
            {
                PlayGame();
            }
            else if(userInput.ToLower() == "no" || userInput.ToLower() == "n")
            {
                Console.WriteLine("Well.. Everyone should make there own chooses.");
                Thread.Sleep(1000);
                System.Environment.Exit(1);
            }
            else
            {
                Input("input invalid Y/N: ");
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
        static ConsoleKeyInfo InputChar(string prompt)
        {
            Console.Write(prompt);
            Console.ForegroundColor = ConsoleColor.Blue;
            ConsoleKeyInfo input = Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.Black;
            return input;
        }

    }
}
