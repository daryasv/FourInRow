using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameLogic
{
    public class Game
    {
        Board m_Board;
        Player m_Player1;
        Player m_Player2;
        Player.PlayerType m_CurrentPlayerType;
        char m_WinnerSign;

        public Game(int i_ColumnNum, int i_RowNum, bool i_IsAgainstComputer)
        {
            m_Board = new Board(i_ColumnNum, i_RowNum);
            m_Player1 = new Player(Player.PlayerType.Player1, false);
            m_Player2 = new Player(Player.PlayerType.Player2, i_IsAgainstComputer);
            m_CurrentPlayerType = Player.PlayerType.Player1;
            m_WinnerSign = ' ';
        }


        public Board GetBoard()
        {
            return this.m_Board;
        }

        public Board MakeMove(int i_ColNum)
        {
            Player currentPlayer = m_CurrentPlayerType == Player.PlayerType.Player1 ? m_Player1 : m_Player2;
            bool addedSuccessfully = m_Board.AddToColumn(i_ColNum, currentPlayer.Sign);

            if (addedSuccessfully)
            {
                if (m_Board.GetWinnerSign() != ' ')
                {
                    currentPlayer.Score++;
                    m_WinnerSign = m_Board.GetWinnerSign();
                }
                if (IsBoardFull())
                {
                    m_WinnerSign = m_Player1.Score > m_Player2.Score ? m_Player1.Sign : m_Player2.Sign;
                }

                switchPlayer();
            }
            //if against computer
            // run player2 move

            return m_Board;
        }


        private void switchPlayer()
        {
            m_CurrentPlayerType = m_CurrentPlayerType == Player.PlayerType.Player1 ? Player.PlayerType.Player2 : Player.PlayerType.Player1;
        }

        public string getWinner()
        {
            string winner = "";

            if (m_WinnerSign == m_Player1.Sign)
            {
                winner = m_Player1.Name;
            }

            if (m_WinnerSign == m_Player2.Sign)
            {
                winner = m_Player2.Name;
            }

            return winner;
        }

        public bool IsBoardFull()
        {
            return m_Board.IsBoardFull();
        }
    }
}
