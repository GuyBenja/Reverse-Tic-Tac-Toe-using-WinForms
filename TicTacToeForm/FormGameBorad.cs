using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeLogic;

namespace TicTacToeForm
{
    public partial class FormGameBorad : Form
    {
        private MyButton[,] m_Buttons;
        private Label       m_Player1Label = new Label();
        private Label       m_Player2Label = new Label();

        private const int k_ButtonSize=40;
        private const int k_DistanceBetweenButtons = 5;
        private const int k_DistanceFromEdges = 10;

        public FormGameBorad(int i_BoardSize)
        {
            initializeForm(i_BoardSize);
        }

        public MyButton[,] Buttons
        {
            get { return m_Buttons; }
        }

        private void initializeForm(int i_BoardSize)
        {
            m_Buttons = new MyButton[i_BoardSize, i_BoardSize];
            int buttonTopPos = k_DistanceFromEdges;
            int buttonLeftPos = k_DistanceFromEdges;

            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    m_Buttons[i, j] = new MyButton(i, j);
                    m_Buttons[i, j].Width = k_ButtonSize;
                    m_Buttons[i, j].Height = k_ButtonSize;
                    m_Buttons[i, j].Top = buttonTopPos;
                    m_Buttons[i, j].Left = buttonLeftPos;
                    m_Buttons[i, j].Click += button_Click;
                    m_Buttons[i, j].TabStop = false;
                    Controls.Add(m_Buttons[i, j]);
                    buttonLeftPos += (k_ButtonSize + k_DistanceBetweenButtons);
                    AcceptButton = null;
                }

                buttonLeftPos = k_DistanceFromEdges;
                buttonTopPos += (k_ButtonSize + k_DistanceBetweenButtons);
            }

            int screenSize = buttonTopPos + (3 * k_DistanceFromEdges);
            Width = buttonTopPos + k_DistanceFromEdges + (2 * k_DistanceBetweenButtons);
            Height = screenSize + (4 * k_DistanceFromEdges);
            MinimizeBox = false;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            Text = "Reverse TicTacToe";

            string player1LabelStr = string.Format(@"{0} : {1}", ScreenManagement.Player1Name, ScreenManagement.Player1Score);
            m_Player1Label.Text = player1LabelStr;
            m_Player1Label.Location = new Point(ClientSize.Width / 9, buttonTopPos + k_DistanceFromEdges);
            m_Player1Label.AutoSize = true;
            Controls.Add(m_Player1Label);

            string player2LabelStr = string.Format(@"{0} : {1}", ScreenManagement.Player2Name, ScreenManagement.Player2Score);
            m_Player2Label.Text = player2LabelStr;
            m_Player2Label.Location = new Point((ClientSize.Width / 9) * 5, buttonTopPos + k_DistanceFromEdges);
            m_Player2Label.AutoSize = true;
            Controls.Add(m_Player2Label);
        }

        private void button_Click(object sender, EventArgs e)
        {
            MyButton button = sender as MyButton;
            bool shouldBeClosed;

            ScreenManagement.MakeMove(ref button, out shouldBeClosed);

            if (shouldBeClosed)
            {
                this.Close();
                return;
            }

            if (ScreenManagement.IsBotEnabled)
            {
                ScreenManagement.MakeBotMove(out shouldBeClosed);

                if (shouldBeClosed)
                {
                    this.Close();
                }
            }

        }
    }
}
