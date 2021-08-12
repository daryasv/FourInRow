using System;
using GameLogic;
using Ex02.ConsoleUtils;

namespace ConsoleUserInterface
{
    public class UserInterface
    {
        Game m_Game;

        public void RunGame()
        {
            Board updatedBoard;
            int columnsNum;
            int rowsNum;
            bool isAgainstComputer;
            bool stillPlaying = true;

            Console.WriteLine("Welcome!");
            Console.WriteLine("How Many Columns?");
            columnsNum = getBoardRowOrColNumber();
            Console.WriteLine("How Many Rows?");
            rowsNum = getBoardRowOrColNumber();
            Console.WriteLine("Do you want to play against computer?\n press 1 for YES\n press 0 for NO");
            isAgainstComputer = getYesOrNoFromUser();
            m_Game = new Game(columnsNum, rowsNum, isAgainstComputer);
            Ex02.ConsoleUtils.Screen.Clear();
            printBoard(m_Game.Board);
            while (stillPlaying)
            {
                //start m_Game
                //first player turn
                ConsoleKeyInfo userChoice = Console.ReadKey(true);

                if (userChoice.KeyChar == 'Q' || userChoice.KeyChar == 'q')
                {
                    m_Game.PlayerQuit();
                    Player.PlayerType playerType = m_Game.GetCurrentPlayerType();
                    Game.WinnerType winnerType = playerType == Player.PlayerType.Player1 ? Game.WinnerType.Player2 : Game.WinnerType.Player1;
                    printWinner(winnerType);
                    bool newRound = checkIfRunNewRound();
                    if (newRound)
                    {
                        initNewGameBoard(columnsNum, rowsNum, isAgainstComputer);
                        continue;
                    }
                    else
                    {
                        stillPlaying = false;
                    }
                }
                else if (isValidColumsNumber(userChoice, columnsNum))
                {
                    updatedBoard = m_Game.MakeMove(int.Parse(userChoice.KeyChar.ToString()), out Board.MoveResponse moveResponse);
                    if (moveResponse == Board.MoveResponse.Success)
                    {
                        Ex02.ConsoleUtils.Screen.Clear();
                        printBoard(updatedBoard);
                        Game.WinnerType winnerType = m_Game.winnerType;
                        if (winnerType != Game.WinnerType.None)
                        {
                            printWinner(winnerType);
                            bool newRound = checkIfRunNewRound();
                            if (newRound)
                            {
                                initNewGameBoard(columnsNum, rowsNum, isAgainstComputer);
                                continue;
                            }
                            else
                            {
                                stillPlaying = false;
                            }
                        }
                    }
                    else if (moveResponse == Board.MoveResponse.OutOfRange)
                    {
                        Console.WriteLine("\n Selected column is out of range");
                    }
                    else if (moveResponse == Board.MoveResponse.ColumnIsFull)
                    {
                        Console.WriteLine("\n Selected column is already full");
                    }
                }
                else
                {
                    Console.WriteLine("\n Invalid column number");
                }
            }

            Console.WriteLine("Good bye...");
        }

        private bool checkIfRunNewRound()
        {
            Console.WriteLine("Would you like to play another round?\n press 1 for YES\n press 0 for NO");

            return getYesOrNoFromUser();
        }

        private void initNewGameBoard(int i_ColSize, int i_RowSize, bool i_AgainstComputer)
        {
            m_Game.Init();
            Ex02.ConsoleUtils.Screen.Clear();
            printBoard(m_Game.Board);
        }

        private void printWinner(Game.WinnerType winnerType)
        {
            if (winnerType == Game.WinnerType.Player1)
            {
                Console.WriteLine("\nGame ended.\n Player 1 won!");
            }
            else if (winnerType == Game.WinnerType.Player2)
            {
                Console.WriteLine("\nGame ended.\n Player 2 won!");
            }
            else if (winnerType == Game.WinnerType.Draw)
            {
                Console.WriteLine("\nGame ended with a draw.");
            }

            Console.WriteLine("\n Score board:\n Player1: {0} , Player2: {1}\n", m_Game.GetFirstPlayerScore(), m_Game.GetSecondPlayerScore());
        }

        private void printBoard(Board i_Board)
        {
            for (int i = 0; i < i_Board.Columns; i++)
            {
                Console.Write("  {0}  ", i + 1);
            }

            for (int i = 0; i < i_Board.Rows; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < i_Board.Columns; j++)
                {
                    Console.Write("| {0}  ", getPlayerSign(i_Board.GetCellPlayerType(j, i)));
                }

                Console.Write("|");
                Console.WriteLine();
                for (int j = 0; j < i_Board.Columns; j++)
                {
                    Console.Write("=====");
                }
            }
        }

        private char getPlayerSign(Player.PlayerType i_PlayerType)
        {
            if (i_PlayerType == Player.PlayerType.Player1)
            {
                return 'X';
            }
            else if (i_PlayerType == Player.PlayerType.Player2)
            {
                return 'O';
            }
            else
            {
                return ' ';
            }
        }

        private bool isValidColumsNumber(ConsoleKeyInfo i_Input, int i_ColNum)
        {
            bool valid = false;
            int num = 0;

            if (Int32.TryParse(i_Input.KeyChar.ToString(), out num) && num > 0 && num <= i_ColNum)
            {
                valid = true;
            }

            return valid;
        }

        private static bool getYesOrNoFromUser()
        {
            string input = Console.ReadLine();

            while (input != "1" && input != "0")
            {
                Console.WriteLine("Invalid Input - Try Again");
                input = Console.ReadLine();
            }

            return input == "1";
        }

        private static int getBoardRowOrColNumber()
        {
            int num = 0;
            string input = Console.ReadLine();

            while (!Int32.TryParse(input, out num) || num > 8 || num < 4)
            {
                Console.WriteLine("Invalid Input - expect to get a number between 4 and 8");
                input = Console.ReadLine();
            }

            return num;
        }
    }
}
