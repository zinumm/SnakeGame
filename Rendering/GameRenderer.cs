using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
    private readonly SpriteBank _sprites;

    private readonly HudRenderer _hud;

    public GameRenderer(GraphicsDevice gd, Board board, SpriteBank sprites, ContentManager content)
    {
        _board = board;

        _sb = new SpriteBatch(gd);

        _pixel = new Texture2D(gd, 1, 1);
        _pixel.SetData(new[] { Color.White });

        _hudRect = new Rectangle(0, 0, _board.PixelWidth, GameConfig.HudHeight);
        _boardRect = new Rectangle(0, GameConfig.HudHeight, _board.PixelWidth, _board.PixelHeight);

        _sprites = sprites;

        _hud = new HudRenderer(content, gd, _board.PixelWidth);
    }

    public void Draw(Snake snake, Food food, GameState state, int score, int highScore)
    {
        _sb.Begin();

        //HUD
        _hud.Draw(_sb, state, score, highScore);

        //Board
        DrawTiles();

        //Separador
        _sb.Draw(_pixel, new Rectangle(0, GameConfig.HudHeight - 1, _board.PixelWidth, 1), Color.DimGray);

        DrawGrid();

        //cobra
        DrawSnake(snake);

        //comida
        DrawFood(food);

        //barrinha de score
        DrawScoreBars(score);
        _sb.End();
    }

    private void DrawGrid()
    {
        int top = GameConfig.HudHeight;

        for (int x = 0; x <= _board.Cols; x++)
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

    private void DrawTiles()
    {
        int top = GameConfig.HudHeight;

        for (int y = 0; y < _board.Rows; y++)
        {
            for (int x = 0; x < _board.Cols; x++)
            {
                var tex = ((x + y) & 1) == 0 ? _sprites.TileLight : _sprites.TileDark;

                var rect = new Rectangle(
                    x * _board.CellSize,
                    top + y * _board.CellSize,
                    _board.CellSize,
                    _board.CellSize
                );

                _sb.Draw(tex, rect, Color.White);
            }
        }
    }

    private void DrawFood(Food food)
    {
        DrawSpriteAtCell(_sprites.Food, food.Position);
    }

    private void DrawSnake(Snake snake)
    {
        var headRot = RotationFromDir(snake.Direction);
        DrawSpriteAtCell(_sprites.SnakeHead, snake.Body[0], headRot);

        for (int i = 1; i < snake.Body.Count - 1; i++)
        {
            var p = snake.Body[i];
            DrawSpriteAtCell(_sprites.SnakeBody, p);
        }

        var tailIndex = snake.Body.Count - 1;
        var tail = snake.Body[tailIndex];
        var beforeTail = snake.Body[tailIndex - 1];

        // direção “saindo do beforeTail para a tail”
        var tailDir = new Microsoft.Xna.Framework.Point(tail.X - beforeTail.X, tail.Y - beforeTail.Y);
        var tailRot = RotationFromDir(tailDir);

        DrawSpriteAtCell(_sprites.SnakeTail, tail, tailRot);

    }

    private void DrawSpriteAtCell(Texture2D tex, Microsoft.Xna.Framework.Point p, float rotation)
{
    int top = GameConfig.HudHeight;

    int x = p.X * _board.CellSize;
    int y = top + p.Y * _board.CellSize;

    var origin = new Vector2(tex.Width / 2f, tex.Height / 2f);

    // desenhar no centro da célula
    var pos = new Vector2(x + _board.CellSize / 2f, y + _board.CellSize / 2f);

    // escala para caber em 16x16 (ou no CellSize)
    var scale = new Vector2(
        _board.CellSize / (float)tex.Width,
        _board.CellSize / (float)tex.Height
    );

    _sb.Draw(
        tex,                 // Texture2D
        pos,                 // Vector2 (centro da célula)
        null,                // Rectangle? source
        Color.White,         // Color
        rotation,            // float
        origin,              // Vector2 (centro do sprite)
        scale,               // Vector2 (escala)
        SpriteEffects.None,  // SpriteEffects
        0f                   // layerDepth
    );

}

private void DrawSpriteAtCell(Texture2D tex, Microsoft.Xna.Framework.Point p)
    => DrawSpriteAtCell(tex, p, 0f);

    private static float RotationFromDir(Microsoft.Xna.Framework.Point d)
    {
        //sprites desenhados apontando para a direita como base (0 rad)
        if (d.X == 1) return 0f;
        if (d.X == -1) return MathF.PI;
        if (d.Y == 1) return MathF.PI / 2f;
        return -MathF.PI / 2f;
    }
}