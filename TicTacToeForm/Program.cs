using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeForm
{
    public class Program
    {
        public static void Main()
        {
            ScreenManagement.StartGame();
            while (ScreenManagement.ShouldPlayMore)
            {
                ScreenManagement.GameInProgress();
            }
        }
    }
}
