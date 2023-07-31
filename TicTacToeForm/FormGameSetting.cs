using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeForm
{
    public partial class FormGameSetting : Form
    {
        private bool m_closedByX = true;

        private const int k_MaxNumberForNumericUpDown = 10;
        private const int k_MinNumberForNumericUpDown = 4;

        public FormGameSetting()
        {
            InitializeComponent();
        }

        private void numericUpDownCols_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownRows.Value > 10 || numericUpDownRows.Value < 4)
            {
                numericUpDownCols.Value = k_MaxNumberForNumericUpDown;
            }

            numericUpDownRows.Value = numericUpDownCols.Value;
        }

        private void numericUpDownRows_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownRows.Value > 10 || numericUpDownRows.Value < 4)
            {
                numericUpDownRows.Value = k_MaxNumberForNumericUpDown;
            }

            numericUpDownCols.Value = numericUpDownRows.Value;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            m_closedByX = false;

            this.Close();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPlayer2.Checked)
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.Text = "";
            }
            else
            {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "[Computer]";
            }
        }

        public int BoardSize
        {
            get { return (int)numericUpDownRows.Value; }
        }

        public string NamePlayer1
        {
            get { return textBoxPlayer1.Text; }
        }

        public string NamePlayer2
        {
            get { return textBoxPlayer2.Text; }
        }

        public bool BotPlay
        {
            get { return !checkBoxPlayer2.Checked; }
        }

        public bool ClosedByX
        {
            get { return m_closedByX; }
        }
    }
}
