using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C21_Ex02
{
    internal class Player
    {
        char m_Sign;
        int m_Score;
        bool m_IsComputer;

        public Player(char i_Sign, bool i_IsComputer)
        {
            m_Sign = i_Sign;
            m_Score = 0;
            m_IsComputer = i_IsComputer;
        }

    }
}
