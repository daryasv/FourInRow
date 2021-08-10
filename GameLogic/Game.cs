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
        Player m_CurrentPlayer;
        char m_WinnerSign;

        public Game(int i_ColumnNum, int i_RowNum, bool i_IsAgainstComputer)
        {
            m_Board = new Board(i_ColumnNum, i_RowNum);
            m_Player1 = new Player(Player.PlayerType.Player1, false);
            m_Player2 = new Player(Player.PlayerType.Player2, i_IsAgainstComputer);
            m_CurrentPlayer = m_Player1;
            m_WinnerSign = ' ';
        }


        public Board GetBoard()
        {
            return this.m_Board;
        }

        public Board MakeMove(int i_ColNum)
        {
            bool addedSuccessfully = m_Board.AddToColumn(i_ColNum, m_CurrentPlayer.Sign);

            if (addedSuccessfully)
            {
                if(!CheckIfEndGame() && m_Player2.IsComputer)
                {
                    m_CurrentPlayer = m_Player2;
                    //get random move from moves that left to play
                    m_Board.AddToRandomColumn(m_CurrentPlayer.Sign);
                    CheckIfEndGame();
                }

                switchPlayer();
            }

            return m_Board;
        }

        private bool CheckIfEndGame()
        {
            bool ended = false;
            if (m_Board.GetWinnerSign() != ' ')
            {
                m_CurrentPlayer.Score++;
                m_WinnerSign = m_Board.GetWinnerSign();
                ended = true;
            }
            else if (IsBoardFull())
            {
                //todo: set draw
                ended = true;
            }

            return ended;
        }

        private void switchPlayer()
        {
            m_CurrentPlayer = m_CurrentPlayer.Type == Player.PlayerType.Player1 ? m_Player2 : m_Player1;
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
