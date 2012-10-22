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

        public override MyList<Position> GetMoves()
        {
            MyList<Position> moves = base.GetMoves();
           MyList<Position> newmoves = new MyList<Position>();
           Side oppositeside  = (this.Side == Side.Black)? Side.White:Side.Black;
            foreach (Position pos in moves)
                if (!chessfield.isDangerPosition(oppositeside, pos,Position))
                // Fix situation of cyclic checking of two kings 
                newmoves.Add(pos);
            return newmoves;
        }

        public  MyList<Position> GetMovesWithOutChecks()
        {
            return base.GetMoves();
        }
        public override string GetImage()
        {
            if (Side == Side.Black)
                return "♚";
            else
                return "♔";
        }
    }
}
