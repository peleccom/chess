using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mychess
{
    public class Knight : Figure
    {
        public Knight(Position pos, Side side, ChessField chessfield)
            : base(pos, side, chessfield)
        {
            FigureType = FigureTypes.Knight;
            movepolitics = new MovePolitics[] {
                                        chessfield.KnightMovePolitics};
        }
        public override string GetImage()
        {
            string s;
            if (Side == Side.Black)
                s = "ч";
            else
                s = "б";
            return "Конь " + s;
        }
    }
}
