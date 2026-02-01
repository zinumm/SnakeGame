using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnakeGame.Core;

namespace SnakeGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Board _board = null!;

    private Texture2D _pixel = null!;
    private Rectangle _hudRect;
    private Rectangle _boardRect;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _board = new Board(GameConfig.Cols, GameConfig.Rows, GameConfig.CellSize);

        _graphics.PreferredBackBufferWidth  = _board.PixelWidth;
        _graphics.PreferredBackBufferHeight = _board.PixelHeight + GameConfig.HudHeight;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _pixel = new Texture2D(GraphicsDevice,1,1);
        _pixel.SetData(new [] {Color.White});

        _hudRect = new Rectangle(0,0,_board.PixelWidth,_board.PixelHeight);
        _boardRect = new Rectangle(0,GameConfig.HudHeight,_board.PixelWidth,_board.PixelHeight);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
           Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        Window.Title = $"W:{Window.ClientBounds.Width} H:{Window.ClientBounds.Height}";

        _spriteBatch.Begin();
        
        //HUD (Faixa Superior)
        _spriteBatch.Draw(_pixel,_hudRect,Color.DarkSlateGray);
        
        // Área do Board
        _spriteBatch.Draw(_pixel,_boardRect,Color.Black);
        
        //Linha separando HUD do Board
        _spriteBatch.Draw(_pixel,new Rectangle(0,GameConfig.HudHeight-1,_board.PixelWidth,1),Color.Gray);
        

        int boardTop = GameConfig.HudHeight;
        //linhas verticais
        for (int x = 0; x <= _board.Cols; x++)
        {
            int px = x * _board.CellSize;
            _spriteBatch.Draw(_pixel,
                                new Rectangle(px,boardTop,1,_board.PixelHeight),
                                Color.DimGray);
        }

        //linhas horizontais
        for (int y = 0; y <= _board.Rows; y++)
        {
            int py = boardTop + y * _board.CellSize;
            _spriteBatch.Draw(_pixel, 
                                new Rectangle(0,py,_board.PixelWidth,1),
                                Color.DimGray);
        }
       
       
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
