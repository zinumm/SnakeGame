using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SnakeGame.Core;
using SnakeGame.Rendering;
using SnakeGame.Input;
using Microsoft.Xna.Framework.Input;

namespace SnakeGame.App;

public sealed class SnakeApp
{
    private readonly GraphicsDeviceManager _graphics;
    private readonly GameWindow _window;

    private Board _board = null!;
    private Snake _snake = null!;
    private GameRenderer _renderer = null!;

    private float _tickAcc = 0f;
    private const float TickInterval = 0.15f;

    private readonly InputState _input = new();

    public SnakeApp(GraphicsDeviceManager graphics, GameWindow window)
    {
        _graphics = graphics;
        _window = window;
    } 

    public void Initialize()
    {
        _board = new Board(GameConfig.Cols, GameConfig.Rows, GameConfig.CellSize);

        _graphics.PreferredBackBufferWidth = _board.PixelWidth;
        _graphics.PreferredBackBufferHeight = _board.PixelHeight + GameConfig.HudHeight;
        
        var start = new Point(GameConfig.Cols / 2, GameConfig.Rows / 2);
        _snake = new Snake(start);
    } 

    public void LoadContent(GraphicsDevice gd, ContentManager content)
    {
        _graphics.ApplyChanges();
        _renderer = new GameRenderer(gd, _board);
    }

    public void Update(GameTime gameTime)
    {
        _input.Update(); 

        if (_input.IsKeyPressed(Keys.Up) || _input.IsKeyPressed(Keys.W))    _snake.SetDirection(new Point(0, -1));
        if (_input.IsKeyPressed(Keys.Down) || _input.IsKeyPressed(Keys.S))  _snake.SetDirection(new Point(0, 1));
        if (_input.IsKeyPressed(Keys.Left) || _input.IsKeyPressed(Keys.A))  _snake.SetDirection(new Point(-1, 0));
        if (_input.IsKeyPressed(Keys.Right) || _input.IsKeyPressed(Keys.D)) _snake.SetDirection(new Point(1, 0));

        _tickAcc += (float)gameTime.ElapsedGameTime.TotalSeconds;

        while (_tickAcc >= TickInterval)
        {
            _tickAcc -= TickInterval;
            _snake.Step();
        }
    }

    public void Draw(GameTime gameTime, GraphicsDevice gd)
    {
        gd.Clear(Color.CornflowerBlue);
        _renderer.Draw(_snake);
    }
}