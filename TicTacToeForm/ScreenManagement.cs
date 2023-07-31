using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeLogic;

namespace TicTacToeForm
{
    public static class ScreenManagement
    {
        private static GameLogic        s_GameLogic = null;
        private static FormGameBorad    s_GameBoradForm = null;
        private static FormGameSetting  s_GameSettingForm = null;

        public static bool ShouldPlayMore { get; set; }

        public static string Player1Name
        {
            get { return s_GameLogic.Player1.Name; }
        }

        public static string Player2Name
        {
            get { return s_GameLogic.Player2.Name; }
        }

        public static int Player1Score
        {
            get { return s_GameLogic.Player1.Score; }
        }

        public static int Player2Score
        {
            get { return s_GameLogic.Player2.Score; }
        }

        public static bool IsBotEnabled
        {
            get { return s_GameLogic.BotPlay; }
        }

        static ScreenManagement()
        {
            ShouldPlayMore = false;
        }

        public static void StartGame()
        {
            s_GameSettingForm = new FormGameSetting();
            s_GameSettingForm.ShowDialog();

            if(!s_GameSettingForm.ClosedByX)
            {
                InitializeGameData();
                GameInProgress();
            }
        
        }

        public static void InitializeGameData()
        {
            int boardSize = s_GameSettingForm.BoardSize;
            string firstPlayerName = (s_GameSettingForm.NamePlayer1 != "") ? s_GameSettingForm.NamePlayer1 : "Player1";
            string secondPlayerName = (s_GameSettingForm.NamePlayer2 != "") ? s_GameSettingForm.NamePlayer2 : "Player2";
            bool isBotPlay = s_GameSettingForm.BotPlay;

            s_GameLogic = new GameLogic(firstPlayerName, secondPlayerName, boardSize, isBotPlay);
        }

        public static void GameInProgress()
        {
            ShouldPlayMore = false;
            s_GameBoradForm = new FormGameBorad(s_GameLogic.Board.BoardSize);

            s_GameBoradForm.ShowDialog();
        }

        public static void MakeMove(ref MyButton io_Button, out bool o_ShouldBeClosed)
        {
            s_GameLogic.TrySetMove(io_Button.Row, io_Button.Col);
            applyChangesOnButton(io_Button.Row, io_Button.Col);
            o_ShouldBeClosed = calculateGameResult();
        }

        public static void MakeBotMove(out bool o_ShouldBeClosed)
        {
            int row;
            int col;

            s_GameLogic.MakeBotMove(out row, out col);
            applyChangesOnButton(row, col);
            o_ShouldBeClosed = calculateGameResult();
        }

        private static void applyChangesOnButton(int i_Row, int i_Col)
        {
            s_GameBoradForm.Buttons[i_Row, i_Col].Text = s_GameLogic.Board.Board[i_Row, i_Col].Cell.ToString();
            s_GameBoradForm.Buttons[i_Row, i_Col].Enabled = false;
        }

        private static bool calculateGameResult()
        {
            bool returnValue = false;
            DialogResult result = 0;
            string caption;

            if (s_GameLogic.GameOver)
            {
                if (s_GameLogic.Winner != null)
                {
                    caption = string.Format(@"The winner is {0}!
Would you like to play another round?", s_GameLogic.Winner.Name);

                    result = MessageBox.Show(caption, "A Win!", MessageBoxButtons.YesNo);
                }
                else if (s_GameLogic.Board.IsBoardFull())
                {
                    caption = string.Format(@"Tie!
Would you like to play another round?");

                    result = MessageBox.Show(caption, "A Tie!", MessageBoxButtons.YesNo);
                }

                if (result == DialogResult.Yes)
                {
                    ResetGameBoard();
                    ShouldPlayMore = true;
                    returnValue = true;
                }
                else if (result == DialogResult.No)
                {
                    ShouldPlayMore = false;
                    Application.Exit();
                }
            }

            return returnValue;
        }

        public static void ResetGameBoard()
        {
            s_GameLogic.ClearData();
        }
    }
}
