using System;
using GameLogic;
using Ex02.ConsoleUtils;

namespace ConsoleUserInterface
{
    class UserInterface
    {
        public void RunGame()
        {
            int columnsNum;
            int rowsNum;
            bool isAgainstComputer;
            bool isGameOver = false;
            bool isQuit = false;

            Console.WriteLine("Welcome!");
            Console.WriteLine("How Many Columns?");
            columnsNum = GetBoardRowOrColNumber();
            Console.WriteLine("How Many Rows?");
            rowsNum = GetBoardRowOrColNumber();
            Console.WriteLine("Do you want to play against computer?\n press 1 for YES\n press 0 for NO");
            isAgainstComputer = GetYesOrNoFromUser();

            Game game = new Game(columnsNum, rowsNum, isAgainstComputer);
            PrintBoard(game.GetBoard());


            while (!isGameOver)
            {
                //start game
                //first player turn
                ConsoleKeyInfo userChoice = Console.ReadKey(true);
                if (userChoice.KeyChar == 'Q' || userChoice.KeyChar == 'q')
                {
                    isGameOver = true;
                    isQuit = true;
                    break;
                }
                else if (isValidColumsNumber(userChoice))
                {
                    if(Int32.TryParse(userChoice.KeyChar.ToString(), out int colIndex))
                    {
                        Board board = game.MakeMove(colIndex);
                        Ex02.ConsoleUtils.Screen.Clear();
                        PrintBoard(board);

                        if(game.getWinner() != "")
                        {
                            Console.WriteLine(game.getWinner());
                            isGameOver = true;
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Key");
                }
            }
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
                    Console.Write("| {0}  ", i_Board.GetCellChar(j, i));
                }

                Console.Write("|");
                Console.WriteLine();
                for (int j = 0; j < i_Board.GetColumnSize(); j++)
                {
                    Console.Write("=====");
                }
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
