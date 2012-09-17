using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mychess
{
    public class Bishop : Figure
    {
        public Bishop(Position pos, Side side, ChessField chessfield)
            : base(pos, side, chessfield)
        {
            FigureType = FigureTypes.Bishop;
            movepolitics = new MovePolitics[] {
                                        chessfield.DiagMovePolitics};
        }

        public override string GetImage()
        {
            string s;
            if (Side == Side.Black)
                s = "ч";
            else
                s = "б";
            return "Слон " + s;
        }
    }
}
