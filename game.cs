using System;

namespace ConsoleGameSuite
{
    class Program
    {
        static int number = 10;
        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {

                Console.WriteLine("=== SICK CONSOLE GAME SUITE ===");
                Console.WriteLine("1. Naughts and Crosses (PvP)");
                Console.WriteLine("2. Rock, Paper, Scissors (PvC)");
                Console.WriteLine("3. Exit");
                Console.Write("\nSelect an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PlayNaughtsAndCrosses();
                        break;
                    case "2":
                        PlayRPS();
                        break;
                    case "3":
                        running = false;
                        Console.WriteLine("Thanks for playing! Goodbye.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }


        static void PlayNaughtsAndCrosses()
        {
            // 2D Array for the board
            char[,] board = {
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' }
            };

            char currentPlayer = 'X';
            int turns = 0;
            bool gameWon = false;

            while (turns < 9 && !gameWon)
            {
                Console.Clear();
                DrawBoard(board);
                Console.WriteLine($"Player {currentPlayer}'s turn.");

                int row, col;
                while (true)
                {
                    Console.Write("Enter row (0-2) and column (0-2) separated by a space: ");
                    string input = Console.ReadLine();
                    string[] parts = input.Split(' ');

                    if (parts.Length == 2 && 
                        int.TryParse(parts[0], out row) && 
                        int.TryParse(parts[1], out col) &&
                        row >= 0 && row <= 2 && col >= 0 && col <= 2)
                    {
                        if (board[row, col] == ' ')
                        {
                            board[row, col] = currentPlayer;
                            break;
                        }
                        else Console.WriteLine("Square already taken!");
                    }
                    else Console.WriteLine("Invalid input. Use format: row col (e.g., 1 1)");
                }

                gameWon = CheckWin(board, currentPlayer);
                if (gameWon)
                {
                    Console.Clear();
                    DrawBoard(board);
                    Console.WriteLine($"Player {currentPlayer} wins!");
                }
                else
                {
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                    turns++;
                }
            }

            if (!gameWon) Console.WriteLine("It's a draw!");
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }

        static void DrawBoard(char[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($" {board[i, 0]} | {board[i, 1]} | {board[i, 2]} ");
                if (i < 2) Console.WriteLine("---+---+---");
            }
        }

        static bool CheckWin(char[,] b, char p)
        {
            for (int i = 0; i < 3; i++)
            {
                // Rows & Columns
                if ((b[i, 0] == p && b[i, 1] == p && b[i, 2] == p) ||
                    (b[0, i] == p && b[1, i] == p && b[2, i] == p)) return true;
            }
            // Diagonals
            return (b[0, 0] == p && b[1, 1] == p && b[2, 2] == p) ||
                   (b[0, 2] == p && b[1, 1] == p && b[2, 0] == p);
        }

        static void PlayRPS()
        {
            string[] options = { "rock", "paper", "scissors" };
            Random rand = new Random();
            bool keepPlaying = true;

            while (keepPlaying)
            {
                Console.Clear();
                Console.WriteLine("--- Rock, Paper, Scissors ---");
                Console.Write("Enter your choice (rock, paper, scissors) or 'back': ");
                string userChoice = Console.ReadLine().ToLower();

                if (userChoice == "back") break;
                if (Array.IndexOf(options, userChoice) == -1)
                {
                    Console.WriteLine("Invalid choice. Press enter to try again.");
                    Console.ReadLine();
                    continue;
                }

                string cpuChoice = options[rand.Next(0, 3)];
                Console.WriteLine($"Computer chose: {cpuChoice}");

                if (userChoice == cpuChoice) Console.WriteLine("It's a tie!");
                else if ((userChoice == "rock" && cpuChoice == "scissors") ||
                         (userChoice == "paper" && cpuChoice == "rock") ||
                         (userChoice == "scissors" && cpuChoice == "paper"))
                {
                    Console.WriteLine("You win!");
                }
                else Console.WriteLine("Computer wins!");

                Console.Write("\nPlay again? (y/n): ");
                if (Console.ReadLine().ToLower() != "y") keepPlaying = false;
            }
        }
    }
}
