using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeLogic;

namespace TicTacToeForm
{
    public class MyButton:Button
    {
        private readonly int r_RowIndex;
        private readonly int r_ColIndex;
        
        public MyButton(int i_Row, int i_Col): base()
        {
            r_RowIndex = i_Row;
            r_ColIndex = i_Col;
        }

        public int Row
        {
            get { return r_RowIndex; }
        }

        public int Col
        {
            get { return r_ColIndex; }
        }
    }
}
