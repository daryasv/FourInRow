using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;

namespace C21_Ex02
{
    class UserInterface
    {

        public static void runGame()
        {
            string columnsNum;
            string rowsNum;
            bool isAgainstComputer;
            bool isGameOver = false;

            Console.WriteLine("Welcome!");
            Console.WriteLine("How Many Columns?");
            columnsNum = GetNumberFromUser();
            Console.WriteLine("How Many Rows?");
            rowsNum = GetNumberFromUser();
            Console.WriteLine("Do you want to play against computer?\n press 1 for YES\n press 0 for NO");
            isAgainstComputer = GetYesOrNoFromUser();

            while (!isGameOver)
            {
                ConsoleKeyInfo input;
                //start game
                //firt player turn
                input = Console.ReadKey(true);
                while (!isValidColumsNumber(input))
                {
                    if (input.KeyChar == 'Q')
                    {
                        //quit game
                    }
                    input = Console.ReadKey(true);
                }
                //make move :
                Game.AddToColumn(int.Parse(input.KeyChar.ToString()));

                //secound player 
                input = Console.ReadKey(true);
                while (!isValidColumsNumber(input))
                {
                    if (input.KeyChar == 'Q')
                    {
                        //quit game
                    }
                    input = Console.ReadKey(true);
                }
            }
        }

        //TODO : implement me
        private static bool isValidColumsNumber(ConsoleKeyInfo input)
        {
            throw new NotImplementedException();
        }

        public static string GetNumberFromUser()
        {
            string input = Console.ReadLine();

            while (!isValidNumber(input))
            {
                Console.WriteLine("Invalid Input - Try Again");
                input = Console.ReadLine();
            }

            return input;
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

        private static bool isValidNumber(string i_Input)
        {
            bool isValid = false;

            if (isOnlyNumbers(i_Input))
            {
                int inputAsNumber = int.Parse(i_Input);
                isValid = inputAsNumber >= 4 && inputAsNumber <= 8;
            }

            return isValid;
        }

        private static bool isOnlyNumbers(string input)
        {
            bool isOnlyNumbers = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsDigit(input[i]))
                {
                    isOnlyNumbers = false;
                    break;
                }
            }

            return isOnlyNumbers;
        }

    }

   
}
