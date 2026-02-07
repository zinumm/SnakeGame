using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SnakeGame.Core;

public static class FoodSpawner
{
    public static Point Spawn (Board board, IReadOnlyList<Point> occupied, Random rng)
    {
        // brute force simples (tabuleiro pequeno). Sem overengineering.
        while (true)
        {
            var p = new Point(rng.Next(0,board.Cols), rng.Next(0,board.Rows));

            bool hit = false;
            for (int i = 0; i < occupied.Count; i++)
            {
                if (occupied[i] == p) {hit = true; break;}
            }
            
            if (!hit) return p;
        }
    }
}