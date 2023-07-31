using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLogic
{
    public class GameBoard
    {
        private BoardCell[,]    m_Board;
        private int             m_BoardSize;
        private int             m_NumberOfEmptyCells;

        private const int       k_MinBoardSize = 4;
        private const int       k_MaxBoardSize = 10;

        private int[]           m_CountCellsTakenInEachRow;
        private int[]           m_CountCellsTakenInEachCol;
        private int             m_CountCellsTakenInDiagonal;
        private int             m_CountCellsTakenInReverseDiagonal;

        public GameBoard(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_NumberOfEmptyCells = (i_BoardSize * i_BoardSize);
            m_Board = new BoardCell[i_BoardSize, i_BoardSize];

            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    m_Board[i, j] = new BoardCell(eCell.EmptyCell);
                }
            }

            m_CountCellsTakenInEachRow = new int[m_BoardSize];
            m_CountCellsTakenInEachCol = new int[m_BoardSize];
            m_CountCellsTakenInDiagonal = 0;
            m_CountCellsTakenInReverseDiagonal = 0;
        }

        public BoardCell[,] Board
        {
            get { return m_Board; }
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
            private set { m_BoardSize = value; }
        }

        public int EmptyCells
        {
            get { return m_NumberOfEmptyCells; }
        }

        public bool TryToSetBoardSize(int i_BoardSize)
        {
            bool validSize = false;

            if (i_BoardSize >= k_MinBoardSize && i_BoardSize <= k_MaxBoardSize)
            {
                m_BoardSize = i_BoardSize;
                validSize = true;
            }

            return validSize;
        }

        public bool TrySetCell(int i_Row, int i_Col, eCell i_Symbol)
        {
            bool isValidMove = false;

            if ((i_Row >= 0 && i_Row < m_BoardSize) && (i_Col >= 0 && i_Col < m_BoardSize))
            {
                if (m_Board[i_Row, i_Col].IsCellEmpty())
                {
                    m_Board[i_Row, i_Col].Cell = i_Symbol;
                    m_NumberOfEmptyCells--;
                    updateCounters(i_Row, i_Col, i_Symbol);
                    isValidMove = true;
                }
            }

            return isValidMove;
        }

        public BoardCell GetCell(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col];
        }

        public void ClearCell(int i_Row, int i_Col)
        {
            m_Board[i_Row, i_Col].Cell = eCell.EmptyCell;
        }

        public void AdjustNumberOfEmptyCells()
        {
            m_NumberOfEmptyCells = m_BoardSize * m_BoardSize;
        }

        public void ClearBoard()
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                m_CountCellsTakenInEachRow[i] = 0;
                m_CountCellsTakenInEachCol[i] = 0;

                for (int j = 0; j < m_BoardSize; j++)
                {
                    ClearCell(i, j);
                }
            }

            AdjustNumberOfEmptyCells();
            m_CountCellsTakenInDiagonal = 0;
            m_CountCellsTakenInReverseDiagonal = 0;
        }

        private void updateCounters(int i_Row, int i_Col, eCell i_Symbol)
        {
            if (i_Symbol == eCell.X)
            {
                m_CountCellsTakenInEachRow[i_Row]++;
                m_CountCellsTakenInEachCol[i_Col]++;

                if (i_Row == i_Col)
                {
                    m_CountCellsTakenInDiagonal++;
                }

                if (i_Row == m_BoardSize - 1 - i_Col)
                {
                    m_CountCellsTakenInReverseDiagonal++;
                }
            }

            else if (i_Symbol == eCell.O)
            {
                m_CountCellsTakenInEachRow[i_Row]--;
                m_CountCellsTakenInEachCol[i_Col]--;

                if (i_Row == i_Col)
                {
                    m_CountCellsTakenInDiagonal--;
                }

                if (i_Row == m_BoardSize - 1 - i_Col)
                {
                    m_CountCellsTakenInReverseDiagonal--;
                }
            }
        }

        public bool IsBoardFull()
        {
            return (m_NumberOfEmptyCells == 0);
        }

        public bool IsGameOver(int i_Row, int i_Col, eCell i_Symbol, int i_LosingSequence)
        {
            bool gameOver = false;

            if (m_CountCellsTakenInEachRow[i_Row] == i_LosingSequence ||
                m_CountCellsTakenInEachCol[i_Col] == i_LosingSequence ||
                m_CountCellsTakenInDiagonal == i_LosingSequence ||
                m_CountCellsTakenInReverseDiagonal == i_LosingSequence)
            {
                gameOver = true;
            }

            return gameOver;
        }
    }
}
