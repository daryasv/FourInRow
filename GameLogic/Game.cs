using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameLogic
{
    public class Game
    {
        public enum WinnerType
        {
            Player1,
            Player2,
            Draw,
            None
        }

        Board m_Board;
        Player m_Player1;
        Player m_Player2;
        Player m_CurrentPlayer;
        WinnerType m_WinnerType;

        public Game(int i_ColumnNum, int i_RowNum, bool i_IsAgainstComputer)
        {
            m_Board = new Board(i_ColumnNum, i_RowNum);
            m_Player1 = new Player(Player.PlayerType.Player1, false);
            m_Player2 = new Player(Player.PlayerType.Player2, i_IsAgainstComputer);
            m_CurrentPlayer = m_Player1;
            m_WinnerType = WinnerType.None;
        }


        public Board GetBoard()
        {
            return this.m_Board;
        }

        public Board MakeMove(int i_ColNum, out Board.MoveResponse io_MoveResponse)
        {
            io_MoveResponse = m_Board.AddToColumn(i_ColNum, m_CurrentPlayer.Type);

            if (io_MoveResponse == Board.MoveResponse.Success)
            {
                if (!CheckIfEndGame() && m_Player2.IsComputer)
                {
                    m_CurrentPlayer = m_Player2;
                    //get random move from moves that left to play
                    m_Board.AddToRandomColumn(m_CurrentPlayer.Type);
                    CheckIfEndGame();
                }

                switchPlayer();
            }

            return m_Board;
        }

        private bool CheckIfEndGame()
        {
            bool ended = false;
            if (m_Board.IsWon())
            {
                m_CurrentPlayer.Score++;
                m_WinnerType = m_CurrentPlayer.Type == Player.PlayerType.Player1 ? WinnerType.Player1 : WinnerType.Player2;
                ended = true;
            }
            else if (IsBoardFull())
            {
                m_WinnerType = WinnerType.Draw;
                ended = true;
            }

            return ended;
        }

        private void switchPlayer()
        {
            m_CurrentPlayer = m_CurrentPlayer.Type == Player.PlayerType.Player1 ? m_Player2 : m_Player1;
        }

        public WinnerType getWinner()
        {
            return m_WinnerType;
        }

        public bool IsBoardFull()
        {
            return m_Board.IsBoardFull();
        }

        public Player.PlayerType getCurrentPlayerType()
        {
            return m_CurrentPlayer.Type;
        }
    }
}
