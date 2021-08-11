using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Board
    {
        int m_rows;
        int m_cols;
        char[,] m_BoardMatrix;
        int[] m_AvailableIndexInColumn;
        char m_WinnerSign;

        public Board(int i_ColNum, int i_RowNum)
        {
            m_rows = i_RowNum;
            m_cols = i_ColNum;
            m_WinnerSign = ' ';
            InitBoard();
        }

        public void InitBoard()
        {
            m_BoardMatrix = new char[m_cols, m_rows];
            m_WinnerSign = ' ';

            for (int i = 0; i < m_cols; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {
                    m_BoardMatrix[i, j] = ' ';
                }
            }

            m_AvailableIndexInColumn = new int[m_cols];
            for (int i = 0; i < m_cols; i++)
            {
                m_AvailableIndexInColumn[i] = m_rows - 1;
            }
        }

        public char GetWinnerSign()
        {
            return m_WinnerSign;
        }

        public bool AddToColumn(int i_ColIndex, char i_Sign) // return true if success
        {
            bool addedSuccessfully = false;
            
            if (m_AvailableIndexInColumn.Length >= i_ColIndex && m_AvailableIndexInColumn[i_ColIndex - 1] > -1)
            {
                int nextAvailableIndexInCol = m_AvailableIndexInColumn[i_ColIndex - 1];

                m_BoardMatrix[i_ColIndex - 1, nextAvailableIndexInCol] = i_Sign;
                if (checkForWin(i_ColIndex - 1, nextAvailableIndexInCol, i_Sign))
                {
                    m_WinnerSign = i_Sign;
                }

                m_AvailableIndexInColumn[i_ColIndex - 1]--;
                addedSuccessfully = true;
            }

            return addedSuccessfully;
        }

        public bool IsBoardFull()
        {
            bool isBoardFull = true;

            for (int i = 0; i < m_cols; i++)
            {
                if (m_AvailableIndexInColumn[i] != -1)
                {
                    isBoardFull = false;
                    break;
                }
            }

            return isBoardFull;
        }

        private bool checkForWin(int i_ColIndex, int i_RowIndex, char i_Sign)
        {
            return isHorizontalSequence(i_ColIndex, i_Sign) || isVerticalSequence(i_RowIndex, i_Sign) ||
                isDecreasingDiagonalSequence(i_ColIndex, i_RowIndex, i_Sign) || isIncreasingDiagonalSequence(i_ColIndex, i_RowIndex, i_Sign);
        }

        public char GetCellChar(int i_ColIndex, int i_RowIndex)
        {
            return m_BoardMatrix[i_ColIndex, i_RowIndex];
        }

        private bool isHorizontalSequence(int i_ColIndex, char i_Sign)
        {
            int sequenceSize = 0;
            bool isThereSequence = false;

            for (int i = 0; i < m_rows; i++)
            {
                if (m_BoardMatrix[i_ColIndex, i] == i_Sign)
                {
                    sequenceSize++;
                }
                else
                {
                    sequenceSize = 0;
                }

                if (sequenceSize == 4)
                {
                    isThereSequence = true;
                    break;
                }
            }

            return isThereSequence;
        }

        private bool isVerticalSequence(int i_RowIndex, char i_Sign)
        {
            int sequenceSize = 0;
            bool isThereSequence = false;

            for (int i = 0; i < m_cols; i++)
            {
                if (m_BoardMatrix[i, i_RowIndex] == i_Sign)
                {
                    sequenceSize++;
                }
                else
                {
                    sequenceSize = 0;
                }

                if (sequenceSize == 4)
                {
                    isThereSequence = true;
                    break;
                }
            }

            return isThereSequence;
        }

        private bool isDecreasingDiagonalSequence(int i_ColIndex, int i_RowIndex, char i_Sign)
        {
            int startOfDiagnolCheckColumn = i_ColIndex;
            int startOfDiagnolCheckRow = i_RowIndex;
            int sequenceSize = 0;
            bool isSequence = false;

            while (startOfDiagnolCheckColumn > 0 && startOfDiagnolCheckRow > 0)
            {
                startOfDiagnolCheckColumn--;
                startOfDiagnolCheckRow--;
            }

            for (int i = startOfDiagnolCheckColumn; i < m_cols && startOfDiagnolCheckRow < m_rows; i++)
            {
                if (m_BoardMatrix[i, startOfDiagnolCheckRow] == i_Sign)
                {
                    sequenceSize++;
                } else
                {
                    sequenceSize = 0;
                }

                if(sequenceSize == 4)
                {
                    isSequence = true;
                    break;
                }

                startOfDiagnolCheckRow++;
            }

            return isSequence;
        }
        
        private bool isIncreasingDiagonalSequence(int i_ColIndex, int i_RowIndex, char i_Sign)
        {
            int startOfDiagnolCheckColumn = i_ColIndex;
            int startOfDiagnolCheckRow = i_RowIndex;
            int sequenceSize = 0;
            bool isSequence = false;

            while (startOfDiagnolCheckColumn > 0 && startOfDiagnolCheckRow <= m_rows)
            {
                startOfDiagnolCheckColumn--;
                startOfDiagnolCheckRow++;
            }

            for (int i = startOfDiagnolCheckColumn; i < m_cols && startOfDiagnolCheckRow < m_rows; i++)
            {
                if (m_BoardMatrix[i, startOfDiagnolCheckRow] == i_Sign)
                {
                    sequenceSize++;
                } else
                {
                    sequenceSize = 0;
                }

                if(sequenceSize == 4)
                {
                    isSequence = true;
                    break;
                }

                startOfDiagnolCheckRow--;
            }

            return isSequence;
        }

        public bool IsCellFull(int i_ColNum, int i_RowNum)
        {
            return m_BoardMatrix[i_ColNum - 1, i_RowNum - 1] == ' ';
        }


        public int GetRowSize()
        {
            return m_rows;
        }

        public int GetColumnSize()
        {
            return m_cols;
        }

        public void AddToRandomColumn(char i_Sign)
        {
            //Save all available columns
            List<int> available = new List<int>();

            for(int i = 0; i < m_AvailableIndexInColumn.Length; i++)
            {
                if(m_AvailableIndexInColumn[i] > -1)
                {
                    available.Add(i+1);
                }
            }

            if(available.Count > 0)
            {
                //Get random index of available columns
                Random random = new Random();
                int randomIndex = random.Next(0, available.Count);
                //Send random column index
                AddToColumn(available.ElementAt(randomIndex), i_Sign);
            }
        }
    }
}
