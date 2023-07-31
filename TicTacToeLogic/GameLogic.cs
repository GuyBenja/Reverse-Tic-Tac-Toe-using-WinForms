using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLogic
{
    public class GameLogic
    {
        private Player          m_Player1 = null;
        private Player          m_Player2 = null;
        private GameBoard       m_Board = null;
        private eTurn           m_CurrentTurn = eTurn.Player1;
        private bool            m_GameOver = false;
        private bool            m_IsBotPlay = false;
        private Player          m_Winner = null;

        public GameLogic(string i_Player1Name, string i_Player2Name, int i_BoardSize, bool i_IsBotPlay)
        {
            m_Player1 = new Player(i_Player1Name);
            m_Player2 = new Player((i_IsBotPlay) ? "Computer" : i_Player2Name);
            m_Board = new GameBoard(i_BoardSize);
            m_CurrentTurn = eTurn.Player1;
            m_GameOver = false;
            m_IsBotPlay = i_IsBotPlay;
            m_Winner = null;
        }

        public Player Player1
        {
            get { return m_Player1; }
            set { m_Player1 = value; }
        }

        public Player Player2
        {
            get { return m_Player2; }
            set { m_Player2 = value; }
        }

        public GameBoard Board
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public eTurn CurrentTurn
        {
            get { return m_CurrentTurn; }
            set { m_CurrentTurn = value; }
        }

        public bool GameOver
        {
            get { return m_GameOver; }
            set { m_GameOver = value; }
        }

        public bool BotPlay
        {
            get { return m_IsBotPlay; }
            set { m_IsBotPlay = value; }
        }

        public Player Winner
        {
            get { return m_Winner; }
            private set { m_Winner = value; }
        }

        public bool TrySetMove(int i_Row, int i_Col)
        {
            eCell cellSymbol = (m_CurrentTurn == eTurn.Player1) ? eCell.X : eCell.O;
            bool isValidMove = false;

            if (m_Board.TrySetCell(i_Row, i_Col, cellSymbol) == true)
            {
                bool isFullBoard = m_Board.IsBoardFull();
                int losingSequnce = (CurrentTurn == eTurn.Player1) ? m_Board.BoardSize : (-m_Board.BoardSize);
                bool isWinnerFound = m_Board.IsGameOver(i_Row, i_Col, cellSymbol, losingSequnce);
                m_GameOver = (isFullBoard || isWinnerFound);

                if (m_GameOver == true)
                {
                    if (isWinnerFound == true)
                    {
                        SetWinner();
                    }
                }

                else
                {
                    SetTurn();
                }

                isValidMove = true;
            }

            return isValidMove;
        }

        public void SetWinner()
        {
            if (m_CurrentTurn == eTurn.Player1)
            {
                Winner = m_Player2;
                Player2.Score++;
            }
            else
            {
                Winner = m_Player1;
                Player1.Score++;
            }
        }

        public void SetTurn()
        {
            m_CurrentTurn = (m_CurrentTurn == eTurn.Player1) ? eTurn.Player2 : eTurn.Player1;
        }

        public void MakeBotMove(out int o_Row, out int o_Col)
        {
            o_Row = 0;
            o_Col = 0;
            if (m_CurrentTurn == eTurn.Player2 && m_IsBotPlay)
            {
                int bestScore = int.MaxValue;
                int bestRow = -1;
                int bestCol = -1;
                eCell botSymbol = eCell.O;
                eCell opponentSymbol = eCell.X;
                int alpha = int.MinValue;
                int beta = int.MaxValue;

                for (int i = 0; i < m_Board.BoardSize; i++)
                {
                    for (int j = 0; j < m_Board.BoardSize; j++)
                    {
                        if (m_Board.GetCell(i, j).IsCellEmpty() == true)
                        {
                            m_Board.Board[i, j].Cell = botSymbol;
                            int score = botAlgorithmToFindingCell(m_Board, 0, opponentSymbol, alpha, beta);
                            m_Board.Board[i, j].Cell = eCell.EmptyCell;

                            if (score < bestScore || m_Board.EmptyCells == 1)
                            {
                                bestScore = score;
                                bestRow = i;
                                bestCol = j;
                            }
                        }
                    }
                }

                m_Board.TrySetCell(bestRow, bestCol, botSymbol);
                o_Row = bestRow;
                o_Col = bestCol;
                bool isFullBoard = m_Board.IsBoardFull();
                bool isWinnerFound = m_Board.IsGameOver(bestRow, bestCol, botSymbol, -(m_Board.BoardSize));
                m_GameOver = (isFullBoard || isWinnerFound);

                if (m_GameOver == true)
                {
                    if (isWinnerFound == true)
                    {
                        m_Winner = m_Player1;
                        Player1.Score++;
                    }
                }
                else
                {
                    SetTurn();
                }
            }
        }

        private int botAlgorithmToFindingCell(GameBoard i_Board, int i_Depth, eCell i_MaximizingPlayer, int i_Alpha, int i_Beta)
        {
            int boardSize = i_Board.BoardSize;
            int bestValue = (i_MaximizingPlayer == eCell.O) ? int.MinValue : int.MaxValue;

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (i_Board.GetCell(i, j).Cell == eCell.EmptyCell)
                    {
                        if (i_Board.IsGameOver(i, j, i_MaximizingPlayer == eCell.X ? eCell.O : eCell.X, boardSize))
                        {
                            return (i_MaximizingPlayer == eCell.O) ? -10 : 10;
                        }
                        else if (i_Depth == 0 || i_Board.IsBoardFull())
                        {
                            return 0;
                        }

                        i_Board.Board[i, j].Cell = i_MaximizingPlayer;
                        int value = botAlgorithmToFindingCell(i_Board, i_Depth - 1, i_MaximizingPlayer == eCell.X ? eCell.O : eCell.X, i_Alpha, i_Beta);
                        i_Board.Board[i, j].Cell = eCell.EmptyCell;

                        if (i_MaximizingPlayer == eCell.O)
                        {
                            bestValue = Math.Max(bestValue, value);
                            i_Alpha = Math.Max(i_Alpha, bestValue);
                        }
                        else
                        {
                            bestValue = Math.Min(bestValue, value);
                            i_Beta = Math.Min(i_Beta, bestValue);
                        }

                        if (i_Beta <= i_Alpha)
                        {
                            break;
                        }

                    }
                }
            }

            return bestValue;
        }

        public void ClearData()
        {
            m_Board.ClearBoard();
            m_Winner = null;
            m_GameOver = false;
            m_CurrentTurn = eTurn.Player1;
        }

        public void IncreaseScore()
        {
            if (m_CurrentTurn == eTurn.Player1)
            {
                m_Player2.Score++;
            }

            else
            {
                m_Player1.Score++;
            }
        }
    }
}
