using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SnakeGame.Core;

namespace SnakeGame.Rendering;

public sealed class HudRenderer
{
    private readonly SpriteFont _font;
    private readonly Texture2D _pixel;
    private readonly int _width;

    public HudRenderer(ContentManager content, GraphicsDevice gd, int width)
    {
        _font = content.Load<SpriteFont>("hud");

        _pixel = new Texture2D(gd, 1, 1);
        _pixel.SetData(new[] {Color.White});

        _width = width;
    }

    public void Draw(SpriteBatch sb, GameState state, int score, int highScore)
    {
        var hudColor = state == GameState.GameOver ? Color.DarkRed : Color.DarkSlateGray;
        sb.Draw(_pixel, new Rectangle(0, 0, _width, GameConfig.HudHeight), hudColor);

        string left = $"Score: {score} - Best: {highScore}";
        sb.DrawString(_font, left, new Vector2(8,6), Color.White);

        string right = state switch
        {
            GameState.Title => "Enter = Start",
            GameState.Playing => "WASD / Arrows",
            GameState.GameOver => "GAME OVER - R = Restart",
            _ => ""
        };

        //Alinhar a direita
        var size = _font.MeasureString(right);
        sb.DrawString(_font, right, new Vector2(_width - size.X -8, 6), Color.White);
    }
}