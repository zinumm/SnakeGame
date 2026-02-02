using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SnakeGame.Core;

public sealed class Snake
{
    public List<Point> Body { get; } = new();
    public Point Direction { get; private set;} = new(1, 0);

    public Snake(Point start)
    {
        Body.Add(start);
        Body.Add(new Point(start.X - 1, start.Y));
        Body.Add(new Point(start.X - 2, start.Y));
    }

    public void Step(bool grow = false)
    {
        var head = Body[0];
        var next = new Point(head.X + Direction.X, head.Y + Direction.Y);

        Body.Insert(0, next);
        if (!grow ) Body.RemoveAt(Body.Count - 1);
    }
}