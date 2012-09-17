﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mychess
{
    public class KnightMovePolitics : MovePolitics
    {
        public List<Position> GetMoves(Figure figure, ChessField cf)
        {
            Position pos = figure.Position;
            List<Position> l = new List<Position>();
            int x = pos.GetX(), y = pos.GetY();
            try
            {
                l.Add(new Position(x + 1, y + 2));
            }
            catch{}
            try
            {
                l.Add(new Position(x + 1, y + -2));
            }
            catch { }
            try
            {
                l.Add(new Position(x - 1, y + 2));
            }
            catch { }
            try
            {
                l.Add(new Position(x - 1, y - 2));
            }
            catch { }
            try
            {
                l.Add(new Position(x + 2, y + 1));
            }
            catch { }
            try
            {
                l.Add(new Position(x + 2, y - 1));
            }
            catch { }
            try
            {
                l.Add(new Position(x - 2, y +1));
            }
            catch { }
            try
            {
                l.Add(new Position(x -2, y - 1));
            }
            catch { }
            return l;
        }
    }
}
