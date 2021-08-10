using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C21_Ex02
{
    class Board
    {
        int m_rows;
        int m_cols;
        static int[] m_columnFullness;
        static int m_columnCapacityLimit = 0;

        public Board(int i_ColNum, int i_RowNum)
        {
            m_rows = i_RowNum;
            m_cols = i_ColNum;
            InitFullness(i_ColNum, i_RowNum);
        }
        public void Init(int i_ColNum, int i_RowNum)
        {
            m_rows = i_RowNum;
            m_cols = i_ColNum;
        }

        public static void InitFullness(int i_ColNum, int i_RowNum)
        {
            m_columnFullness = new int[i_ColNum + 1];
            for (int i = 1; i <= i_ColNum; i++)
            {
                m_columnFullness[i] = 0;
            }

            m_columnCapacityLimit = i_RowNum;
        }

        public static bool AddToColumn(int i_ColIndex) // return true if success
        {
            bool addedSuccessfully = false;
            if (m_columnFullness[i_ColIndex] < m_columnCapacityLimit)
            {
                m_columnFullness[i_ColIndex]++;
                addedSuccessfully = true;
            }

            return addedSuccessfully;
        }

        public static bool IsCellFull(int i_ColNum, int i_RowNum)
        {
            return (i_RowNum > m_columnCapacityLimit - m_columnFullness[i_ColNum]);
        }

        public void PrintBoard()
        {
            for (int i = 1; i <= m_cols; i++)
            {
                Console.Write("  {0}  ", i);
            }

            for (int i = 1; i <= m_rows; i++)
            {
                Console.WriteLine();

                for (int j = 1; j <= m_cols; j++)
                {
                    Console.Write("| {0}  ", IsCellFull(j, i) ? "X" : " ");
                }
                Console.Write("|");
                Console.WriteLine();

                for (int j = 1; j <= m_cols; j++)
                {
                    Console.Write("=====");
                }
            }


        }
    }
}
