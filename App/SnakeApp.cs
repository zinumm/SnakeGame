using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SnakeGame.Core;
using SnakeGame.Rendering;
using SnakeGame.Input;
using Microsoft.Xna.Framework.Input;
using System;

namespace SnakeGame.App;

public sealed class SnakeApp
{
    private readonly GraphicsDeviceManager _graphics;
    private readonly GameWindow _window;

    private Board _board = null!;
    private Snake _snake = null!;
    private GameRenderer _renderer = null!;

    private float _tickAcc = 0f;
    private readonly Speed _speed = new (0.15f, 0.06f, 0.005f);

    private readonly InputState _input = new();

    private GameState _state = GameState.Playing;

    public SnakeApp(GraphicsDeviceManager graphics, GameWindow window)
    {
        _graphics = graphics;
        _window = window;
    } 

    private readonly Random _rng = new();
    private Food _food = null!;
    private readonly Score _score = new();

    public void Initialize()
    {
        _board = new Board(GameConfig.Cols, GameConfig.Rows, GameConfig.CellSize);

        _graphics.PreferredBackBufferWidth = _board.PixelWidth;
        _graphics.PreferredBackBufferHeight = _board.PixelHeight + GameConfig.HudHeight;
        
        var start = new Point(GameConfig.Cols / 2, GameConfig.Rows / 2);
        Reset();
    } 

    public void LoadContent(GraphicsDevice gd, ContentManager content)
    {
        _graphics.ApplyChanges();
        _renderer = new GameRenderer(gd, _board);
    }

    public void Update(GameTime gameTime)
    {
        _input.Update(); 

        if (_state == GameState.GameOver)
        {
            if (_input.IsKeyPressed(Keys.R))
                Reset();
            return;
        }


        if (_input.IsKeyPressed(Keys.Up) || _input.IsKeyPressed(Keys.W))    _snake.SetDirection(new Point(0, -1));
        if (_input.IsKeyPressed(Keys.Down) || _input.IsKeyPressed(Keys.S))  _snake.SetDirection(new Point(0, 1));
        if (_input.IsKeyPressed(Keys.Left) || _input.IsKeyPressed(Keys.A))  _snake.SetDirection(new Point(-1, 0));
        if (_input.IsKeyPressed(Keys.Right) || _input.IsKeyPressed(Keys.D)) _snake.SetDirection(new Point(1, 0));

        _tickAcc += (float)gameTime.ElapsedGameTime.TotalSeconds;

        while (_tickAcc >= _speed.TickInterval)
        {
            _tickAcc -= _speed.TickInterval;
            
            var nextHead = _snake.NextHead();

            //colisão com a parede
            if (Rules.HitsWall(nextHead, _board)) { _state = GameState.GameOver; break; }
            
            //Comer?
            bool willEat = nextHead == _food.Position;

            _snake.Step(grow: willEat);

            //colisão com o corpo
            if (Rules.HitSelf(_snake.Body)) { _state = GameState.GameOver; break; }

            if (willEat)
            {
                _score.Add(1);
                _speed.OnEat();

                var foodPos = FoodSpawner.Spawn(_board, _snake.Body,_rng);
                _food.SetPosition(foodPos);
            }
        }
    }

    public void Draw(GameTime gameTime, GraphicsDevice gd)
    {
        gd.Clear(Color.CornflowerBlue);
        _renderer.Draw(_snake, _food, _state, _score.Value);
    }

    private void Reset()
    {
        var start = new Point(GameConfig.Cols / 2, GameConfig.Rows / 2);
        _snake = new Snake(start);
        
        _score.Reset();

        _speed.Reset();

        var foodPos = FoodSpawner.Spawn(_board, _snake.Body, _rng);
        _food = new Food(foodPos);

        _state = GameState.Playing;
        _tickAcc = 0f;
    }
}