using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnakeGame.Core;

namespace SnakeGame.Rendering;

public sealed class GameRenderer
{
    private readonly SpriteBatch _sb;
    private readonly Texture2D _pixel;
    private readonly Board _board;

    private readonly Rectangle _hudRect;
    private readonly Rectangle _boardRect;

    public GameRenderer(GraphicsDevice gd, Board board)
    {
        _board = board;

        _sb = new SpriteBatch(gd);

        _pixel = new Texture2D(gd, 1, 1);
        _pixel.SetData(new [] {Color.White});

        _hudRect = new Rectangle(0, 0, _board.PixelWidth, GameConfig.HudHeight);
        _boardRect = new Rectangle(0, GameConfig.HudHeight, _board.PixelWidth, _board.PixelHeight);
    }

    public void Draw(Snake snake, GameState state)
    {
        _sb.Begin();

        //HUD
        var hudColor = state == GameState.GameOver ? Color.DarkRed : Color.DarkSlateGray;
        _sb.Draw(_pixel, _hudRect, hudColor);

        //Board
        _sb.Draw(_pixel, _boardRect, Color.Black);

        //Separador
        _sb.Draw(_pixel, new Rectangle(0, GameConfig.HudHeight - 1, _board.PixelWidth, 1), Color.DimGray);

        DrawGrid();
        DrawSnake(snake);

        _sb.End();
    }

    private void DrawGrid()
    {
        int top = GameConfig.HudHeight;

        for (int x = 0; x <= _board.Cols; x++ )
        {
            int px = x * _board.CellSize;
            _sb.Draw(_pixel, new Rectangle(px, top, 1, _board.PixelHeight), Color.DimGray);
        }

        for (int y = 0; y <= _board.Rows; y++)
        {
            int py = top + y * _board.CellSize;
            _sb.Draw(_pixel, new Rectangle(0, py, _board.PixelWidth, 1), Color.DimGray);
        }
    }

    private void DrawSnake(Snake snake)
    {
        int top = GameConfig.HudHeight;

        foreach (var p in snake.Body)
        {
            var rect =  new Rectangle(
                p.X * _board.CellSize,
                top + p.Y * _board.CellSize,
                _board.CellSize,
                _board.CellSize
            );

            _sb.Draw(_pixel, rect, Color.LimeGreen);
        }
    }
}