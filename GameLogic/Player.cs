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
            Player1 = 'O',
            Player2 = 'X'
        }

        char m_Sign;
        int m_Score;
        bool m_IsComputer;
        string m_Name;

        public Player(char i_Sign, bool i_IsComputer, string i_Name)
        {
            m_Sign = i_Sign;
            m_Score = 0;
            m_IsComputer = i_IsComputer;
            m_Name = i_Name;
        }

        public char Sign
        {
            get
            {
                return m_Sign;
            }
            set
            {
                m_Sign = value;
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

        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }
    }
}