using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace C21_Ex02
{
    class Game
    {
        Board m_Board;
        Player m_Player1;
        Player m_Player2;
        

        public Game(int i_ColumnNum, int i_RowNum, bool i_IsAgainstComputer)
        {
            m_Board = new Board(i_ColumnNum, i_RowNum);
            m_Player1 = new Player('X', false);
            m_Player2 = new Player('O', i_IsAgainstComputer);
        }

      
    }
}
