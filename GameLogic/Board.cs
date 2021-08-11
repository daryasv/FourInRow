using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Board
    {
        public enum MoveResponse
        {
            Success,
            OutOfRange,
            ColumnIsFull
        }

        int m_rows;
        int m_cols;
        int[,] m_BoardMatrix;
        int[] m_AvailableIndexInColumn;
        bool m_Won;

        public Board(int i_ColNum, int i_RowNum)
        {
            m_rows = i_RowNum;
            m_cols = i_ColNum;
            m_Won = false;
            InitBoard();
        }

        public void InitBoard()
        {
            m_BoardMatrix = new int[m_cols, m_rows];
            m_Won = false;

            for (int i = 0; i < m_cols; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {
                    m_BoardMatrix[i, j] = 0;
                }
            }

            m_AvailableIndexInColumn = new int[m_cols];
            for (int i = 0; i < m_cols; i++)
            {
                m_AvailableIndexInColumn[i] = m_rows - 1;
            }
        }

        public MoveResponse AddToColumn(int i_ColIndex, Player.PlayerType i_PlayerType) // return true if success
        {
            MoveResponse moveResponse = MoveResponse.Success;
            if (m_AvailableIndexInColumn.Length < i_ColIndex)
            {
                moveResponse = MoveResponse.OutOfRange;
            }
            else if (m_AvailableIndexInColumn[i_ColIndex - 1] < 0)
            {
                moveResponse = MoveResponse.ColumnIsFull;
            }
            else
            {
                int nextAvailableIndexInCol = m_AvailableIndexInColumn[i_ColIndex - 1];

                m_BoardMatrix[i_ColIndex - 1, nextAvailableIndexInCol] = (int)i_PlayerType;
                if (checkForWin(i_ColIndex - 1, nextAvailableIndexInCol, (int)i_PlayerType))
                {
                    m_Won = true;
                }

                m_AvailableIndexInColumn[i_ColIndex - 1]--;
            }

            return moveResponse;
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

        private bool checkForWin(int i_ColIndex, int i_RowIndex, int i_PlayerI)
        {
            return isHorizontalSequence(i_ColIndex, i_PlayerI) || isVerticalSequence(i_RowIndex, i_PlayerI) ||
                isDecreasingDiagonalSequence(i_ColIndex, i_RowIndex, i_PlayerI) || isIncreasingDiagonalSequence(i_ColIndex, i_RowIndex, i_PlayerI);
        }

        public Player.PlayerType GetCellPlayerType(int i_ColIndex, int i_RowIndex)
        {
            Player.PlayerType player = (Player.PlayerType)m_BoardMatrix[i_ColIndex, i_RowIndex];
            return player;
        }

        private bool isHorizontalSequence(int i_ColIndex, int i_PlayerI)
        {
            int sequenceSize = 0;
            bool isThereSequence = false;

            for (int i = 0; i < m_rows; i++)
            {
                if (m_BoardMatrix[i_ColIndex, i] == i_PlayerI)
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

        private bool isVerticalSequence(int i_RowIndex, int i_PlayerI)
        {
            int sequenceSize = 0;
            bool isThereSequence = false;

            for (int i = 0; i < m_cols; i++)
            {
                if (m_BoardMatrix[i, i_RowIndex] == i_PlayerI)
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

        private bool isDecreasingDiagonalSequence(int i_ColIndex, int i_RowIndex, int i_PlayerI)
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
                if (m_BoardMatrix[i, startOfDiagnolCheckRow] == i_PlayerI)
                {
                    sequenceSize++;
                }
                else
                {
                    sequenceSize = 0;
                }

                if (sequenceSize == 4)
                {
                    isSequence = true;
                    break;
                }

                startOfDiagnolCheckRow++;
            }

            return isSequence;
        }

        private bool isIncreasingDiagonalSequence(int i_ColIndex, int i_RowIndex, int i_PlayerI)
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

            for (int i = startOfDiagnolCheckColumn; i < m_cols && startOfDiagnolCheckRow < m_rows && startOfDiagnolCheckRow >= 0; i++)
            {
                if (m_BoardMatrix[i, startOfDiagnolCheckRow] == i_PlayerI)
                {
                    sequenceSize++;
                }
                else
                {
                    sequenceSize = 0;
                }

                if (sequenceSize == 4)
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
            return m_BoardMatrix[i_ColNum - 1, i_RowNum - 1] == 0;
        }


        public int GetRowSize()
        {
            return m_rows;
        }

        public int GetColumnSize()
        {
            return m_cols;
        }

        public void AddToRandomColumn(Player.PlayerType i_PlayerType)
        {
            //Save all available columns
            List<int> available = new List<int>();

            for (int i = 0; i < m_AvailableIndexInColumn.Length; i++)
            {
                if (m_AvailableIndexInColumn[i] > -1)
                {
                    available.Add(i + 1);
                }
            }

            if (available.Count > 0)
            {
                //Get random index of available columns
                Random random = new Random();
                int randomIndex = random.Next(0, available.Count);
                //Send random column index
                AddToColumn(available.ElementAt(randomIndex), i_PlayerType);
            }
        }

        public bool IsWon()
        {
            return m_Won;
        }
    }
}
