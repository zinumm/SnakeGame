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

    public void Draw(Snake snake, Food food, GameState state, int score)
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
        
        //cobra
        DrawSnake(snake);

        //comida
        DrawCell(food.Position, Color.OrangeRed);

        //barrinha de score
        DrawScoreBars(score);
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
        foreach (var p in snake.Body)
            DrawCell(p, Color.LimeGreen);
    }

    private void DrawCell(Microsoft.Xna.Framework.Point p, Color color)
    {
        int top = GameConfig.HudHeight;
        var rect = new Rectangle(
            p.X * _board.CellSize,
            top + p.Y * _board.CellSize,
            _board.CellSize,
            _board.CellSize
        );
        _sb.Draw(_pixel, rect, color);
    }

    private void DrawScoreBars(int score)
    {
        int x = 8;
        int y = 8;
        int size = 8;
        int gap = 2;

        for (int i = 0; i < score; i++)
        {
            _sb.Draw(_pixel, new Rectangle(x, y, size, size), Color.Gold);
            x += size + gap;

            //quebra linha se passar a largura
            if (x + size > _board.PixelWidth - 8)
            {
                x = 8;
                y += size + gap;
            }
        }
    }
}