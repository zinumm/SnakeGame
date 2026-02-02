using Microsoft.Xna.Framework.Input;

namespace SnakeGame.Input;

public sealed class InputState
{
    private KeyboardState _prev;
    private KeyboardState _cur;

    public void Update()
    {
        _prev = _cur;
        _cur = Keyboard.GetState();
    }

    public bool IsKeyPressed(Keys key) => _cur.IsKeyDown(key) && !_prev.IsKeyDown(key);
}