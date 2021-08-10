using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Player
    {
        public enum PlayerType
        {
            Player1 = 1,
            Player2 = 2
        }

        private PlayerType m_PlayerType;
        private int m_Score;
        private char m_Sign;
        private bool m_IsComputer;
      

        public Player(Player.PlayerType i_PlayerType, bool i_IsComputer)
        {
            this.m_PlayerType = i_PlayerType;
            this.m_Score = 0;
            this.m_Sign = i_PlayerType == PlayerType.Player1 ? 'X' : 'O';
            this.m_IsComputer = i_IsComputer;
        }

        public char Sign
        {
            get
            {
                return m_Sign;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public PlayerType Type
        {
            get
            {
                return m_PlayerType;
            }
            set
            {
                m_PlayerType = value;
            }
        }

        public String Name
        {
            get
            {
                return m_PlayerType.ToString();
            }
        }

        public bool IsComputer
        {
            get
            {
                return m_IsComputer;
            }
        }
    }
}