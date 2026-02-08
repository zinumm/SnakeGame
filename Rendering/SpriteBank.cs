using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SnakeGame.Rendering;

public sealed class SpriteBank
{
    public Texture2D SnakeHead { get; }
    public Texture2D SnakeBody { get; }
    public Texture2D SnakeTail { get; }
    public Texture2D Food   { get; }
    public Texture2D TileLight { get; }
    public Texture2D TileDark { get; }

    public SpriteBank(ContentManager content)
    {
        SnakeHead = content.Load<Texture2D>("snakeHead");
        SnakeBody = content.Load<Texture2D>("snakeBody");
        SnakeTail = content.Load<Texture2D>("snakeTail");
        Food = content.Load<Texture2D>("food");
        TileLight = content.Load<Texture2D>("tileLight");
        TileDark = content.Load<Texture2D>("tileDark");
    }
}