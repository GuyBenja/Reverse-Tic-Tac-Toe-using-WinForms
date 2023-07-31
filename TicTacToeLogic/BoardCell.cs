using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLogic
{
    public struct BoardCell
    {
        private eCell m_Cell;

        public BoardCell(eCell i_Cell)
        {
            m_Cell = i_Cell;
        }

        public eCell Cell
        {
            get { return m_Cell; }
            set { m_Cell = value; }
        }

        public bool IsCellEmpty()
        {
            return (m_Cell == eCell.EmptyCell);
        }
    }
}
