namespace Sudoku;

public static class SudokuValidator
{
    private const int BoardDimension = 9;
    private const int SubgridDimension = 3;
    
    public static bool ValidSolution(int[][] board) 
        => !ContainsZeroes(board) 
           && ValidateRows(board) 
           && ValidateColumns(board) 
           && ValidateSubGrids(board);

    private static bool ValidateRows(int[][] board)
    {
        for (var row = 0; row < BoardDimension; row++)
        {
            if (!IsValidSet(board[row]))
            {
                return false;
            }
        }

        return true;
    }

    private static bool ValidateColumns(IReadOnlyList<int[]> board)
    {
        for (var col = 0; col < BoardDimension; col++)
        {
            var column = new int[BoardDimension];
            for (var row = 0; row < BoardDimension; row++)
            {
                column[row] = board[row][col];
            }

            if (!IsValidSet(column))
            {
                return false;
            }
        }

        return true;
    }

    private static bool ValidateSubGrids(IReadOnlyList<int[]> board)
    {
        for (var startRow = 0; startRow < BoardDimension; startRow += SubgridDimension)
        {
            for (var startCol = 0; startCol < BoardDimension; startCol += SubgridDimension)
            {
                var subgrid = new int[BoardDimension];
                var index = 0;
                for (var row = startRow; row < startRow + SubgridDimension; row++)
                {
                    for (var col = startCol; col < startCol + SubgridDimension; col++)
                    {
                        subgrid[index] = board[row][col];
                        index++;
                    }
                }

                if (!IsValidSet(subgrid))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool ContainsZeroes(int[][] board)
    {
        if (board is null) throw new ArgumentNullException(nameof(board));
        return board.Any(row => Array.IndexOf(row, 0) != -1);
    }

    private static bool IsValidSet(IReadOnlyCollection<int> numbers)
        => numbers.Distinct().ToArray().Length == numbers.Count;
}