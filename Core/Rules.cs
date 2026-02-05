using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SnakeGame.Core;

public static class Rules
{
    public static bool HitsWall(Point head, Board board) 
    => head.X < 0 || head.X >= board.Cols || head.Y < 0 || head.Y >= board.Rows;

    public static bool HitSelf(IReadOnlyList<Point> body)
    {
        var head = body[0];
        for (int i = 1; i < body.Count; i++)
            if (body[i] == head) return true;
        return false;
    }
}