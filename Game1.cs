using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnakeGame.App;
using SnakeGame.Core;

namespace SnakeGame;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SnakeApp _app = null!;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _app = new SnakeApp(_graphics,Window);
        _app.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _app.LoadContent(GraphicsDevice,Content);
    }

    protected override void Update(GameTime gameTime)
    {
        _app.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _app.Draw(gameTime,GraphicsDevice);
        base.Draw(gameTime);
    }
}
