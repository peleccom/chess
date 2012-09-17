using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mychess
{
    public class King : Figure
    {
        public King(Position pos, Side side, ChessField chessfield)
            : base(pos, side, chessfield)
        {
            FigureType = FigureTypes.King;
            movepolitics = new MovePolitics[] {
                                        chessfield.HorizontalMovePolitics,
                                        chessfield.VerticalMovePolitics,
                                        chessfield.DiagMovePolitics};
        }

        public override string GetImage()
        {
            string s;
            if (Side == Side.Black)
                s = "ч";
            else
                s = "б";
            return "Король " + s;
        }
    }
}
