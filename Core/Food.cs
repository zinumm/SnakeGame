using Microsoft.Xna.Framework;

namespace SnakeGame.Core;

public sealed class Food
{
    public Point Position { get; private set; }

    public Food (Point start) => Position = start;

    public void SetPosition(Point p) => Position = p;
}