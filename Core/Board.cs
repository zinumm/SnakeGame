namespace SnakeGame.Core;

public sealed class Board
{
    public int Cols { get; }
    public int Rows { get; }
    public int CellSize { get; }

    public int PixelWidth => Cols * CellSize;
    public int PixelHeight => Rows * CellSize;

    public Board(int cols, int rows, int cellsize)
    {
        Cols = cols;
        Rows = rows;
        CellSize = cellsize;
    }
}