using System.Net.WebSockets;

namespace WebCheckers.Pages
{
    public partial class Home
    {
        private Cell? selectedCell;
        private readonly Logic logic = new();
        string act = "";
        public PieceColor Turn => logic.Turn;

        private void HandleClick(Cell cell)
        {
            var Col = cell.Col;
            var Row = cell.Row;

            // Если в клетке есть шашка — выбираем её
            if (cell.Checker != null && cell.Checker.Colour == logic.Turn) 
            {
                selectedCell = cell;
                act = $"{cell.Row}{cell.Col}";
            }
            // Если в клетке нет шашки и есть выделенная шашка — делаем ход
            else if (selectedCell != null && cell.Checker == null)
            {
                string fullAct = $"{act} {cell.Row}{cell.Col}";
                bool success = logic.Action(board, fullAct);
                selectedCell = null;
                act = "";
            }
            // Если повторно клик, то снять выделение
            else if (selectedCell != null)
            {
                selectedCell = null;
                act = "";
            }
            winner = logic.CheckWinner(board);
        }

        private bool HasAnyMoves(Cell cell)
        {
            if(cell.Checker != null)
            {
                for (int rr = 8; rr >= 1; rr--)
                {
                    for (int cc = 1; cc <= 8; cc++)
                    {
                        var (canMove, victims) = logic.CheckActMove(board, $"{cell.Row}{cell.Col} {rr}{cc}".Split(" "));
                    
                        if (canMove) return true; 
                    }
                }
            }
            return false;
        } 
    }
}