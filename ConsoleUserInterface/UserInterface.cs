using System;
using GameLogic;
using Ex02.ConsoleUtils;

namespace ConsoleUserInterface
{
    class UserInterface
    {
        int m_PlayerOneScore = 0;
        int m_PlayerTwoScore = 0;
        Game m_Game;

        public void RunGame()
        {
            m_PlayerOneScore = 0;
            m_PlayerTwoScore = 0;

            int columnsNum;
            int rowsNum;
            bool isAgainstComputer;

            Console.WriteLine("Welcome!");
            Console.WriteLine("How Many Columns?");
            columnsNum = GetBoardRowOrColNumber();
            Console.WriteLine("How Many Rows?");
            rowsNum = GetBoardRowOrColNumber();
            Console.WriteLine("Do you want to play against computer?\n press 1 for YES\n press 0 for NO");
            isAgainstComputer = GetYesOrNoFromUser();

            InitNewGameBoard(columnsNum, rowsNum, isAgainstComputer);

            while (true)
            {
                //start m_Game
                //first player turn
                ConsoleKeyInfo userChoice = Console.ReadKey(true);
                if (userChoice.KeyChar == 'Q' || userChoice.KeyChar == 'q')
                {
                    Player.PlayerType playerType = m_Game.getCurrentPlayerType();
                    Game.WinnerType winnerType = playerType == Player.PlayerType.Player1 ? Game.WinnerType.Player2 : Game.WinnerType.Player1;
                    SetWinner(winnerType);
                    bool newRound = CheckIfRunNewRound();
                    if (newRound)
                    {
                        InitNewGameBoard(columnsNum, rowsNum, isAgainstComputer);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (isValidColumsNumber(userChoice))
                {
                    if (Int32.TryParse(userChoice.KeyChar.ToString(), out int colIndex))
                    {
                        Board board = m_Game.MakeMove(colIndex, out Board.MoveResponse moveResponse);
                        if (moveResponse == Board.MoveResponse.Success)
                        {
                            Ex02.ConsoleUtils.Screen.Clear();
                            PrintBoard(board);
                            Game.WinnerType winnerType = m_Game.getWinner();
                            if (winnerType != Game.WinnerType.None)
                            {
                                SetWinner(winnerType);
                                bool newRound = CheckIfRunNewRound();
                                if (newRound)
                                {
                                    InitNewGameBoard(columnsNum, rowsNum, isAgainstComputer);
                                    continue;
                                }
                                else
                                {
                                    break;
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
                }
                else
                {
                    Console.WriteLine("\n Invalid column number");
                }
            }
            Console.WriteLine("Good bye...");
        }

        private bool CheckIfRunNewRound()
        {
            Console.WriteLine("Would you like to play another round?\n press 1 for YES\n press 0 for NO");
            return GetYesOrNoFromUser();
        }

        private void InitNewGameBoard(int i_ColSize, int i_RowSize, bool i_AgainstComputer)
        {
            m_Game = new Game(i_ColSize, i_RowSize, i_AgainstComputer);
            Ex02.ConsoleUtils.Screen.Clear();
            PrintBoard(m_Game.GetBoard());
        }

        private void SetWinner(Game.WinnerType winnerType)
        {
            if (winnerType == Game.WinnerType.Player1)
            {
                m_PlayerOneScore++;
                Console.WriteLine("\nGame ended.\n Player 1 won!");
            }
            else if (winnerType == Game.WinnerType.Player2)
            {
                m_PlayerTwoScore++;
                Console.WriteLine("\nGame ended.\n Player 2 won!");
            }
            else if (winnerType == Game.WinnerType.Draw)
            {
                Console.WriteLine("\nGame ended with a draw.");
            }
            Console.WriteLine("\n Score board:\n Player1: {0} , Player2: {1}\n", m_PlayerOneScore, m_PlayerTwoScore);
        }

        private void PrintBoard(Board i_Board)
        {

            for (int i = 0; i < i_Board.GetColumnSize(); i++)
            {
                Console.Write("  {0}  ", i + 1);
            }

            for (int i = 0; i < i_Board.GetRowSize(); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < i_Board.GetColumnSize(); j++)
                {
                    Console.Write("| {0}  ", GetPlayerSign(i_Board.GetCellPlayerType(j, i)));
                }

                Console.Write("|");
                Console.WriteLine();
                for (int j = 0; j < i_Board.GetColumnSize(); j++)
                {
                    Console.Write("=====");
                }
            }

        }

        private char GetPlayerSign(Player.PlayerType i_PlayerType)
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

        //TODO : implement me
        private bool isValidColumsNumber(ConsoleKeyInfo input)
        {
            return true;
        }



        public static bool GetYesOrNoFromUser()
        {
            string input = Console.ReadLine();

            while (input != "1" && input != "0")
            {
                Console.WriteLine("Invalid Input - Try Again");
                input = Console.ReadLine();
            }

            return input == "1";
        }


        public static int GetBoardRowOrColNumber()
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
