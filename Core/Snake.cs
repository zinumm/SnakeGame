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

        Direction = new Point(1,0);
    }

    public void Step(bool grow = false)
    {
        var head = Body[0];
        var next = new Point(head.X + Direction.X, head.Y + Direction.Y);

        Body.Insert(0, next);
        if (!grow ) Body.RemoveAt(Body.Count - 1);
    }

    public void SetDirection(Point dir)
    {
        // Só aceita 4 direções
        if (!IsCardinal(dir)) return;

        //Anti-reversão: não pode inverter
        if (dir.X == -Direction.X && dir.Y == -Direction.Y) return;

        Direction = dir;
    }

    private static bool IsCardinal(Point d)
    {
        // (1,0) , (-1,0), (0,1), (0,-1)
        int ax = d.X < 0 ? -d.X : d.X;
        int ay = d.Y < 0 ? -d.Y : d.Y;
        return (ax + ay) == 1;
    }

    public Point NextHead()
    {
        var head = Body[0];
        return new Point(head.X + Direction.X, head.Y + Direction.Y);
    }
}