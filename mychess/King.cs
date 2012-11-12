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

        public MyList<Position> GetCastling()
        {
            MyList<Position> castling = new MyList<Position>();
            Figure fig;
            if (ismoved())
                return castling;
            Side oppside = (this.Side == Side.Black)?Side.White:Side.Black;
            if (chessfield.isDangerPosition(oppside, Position))
                return castling;
            if (Side == Side.White)
            {
                fig = chessfield.GetFigureAt(new Position(1, 1));
                if (fig != null && !chessfield.isDangerPosition(oppside, fig.Position) && !fig.ismoved() && chessfield.GetFigureAt(new Position(2, 1)) == null && chessfield.GetFigureAt(new Position(3, 1)) == null
                    && chessfield.GetFigureAt(new Position(4, 1)) == null)
                    castling.Add(fig.Position);
                fig = chessfield.GetFigureAt(new Position(8, 1));
                if (fig != null && !chessfield.isDangerPosition(oppside, fig.Position) && !fig.ismoved() && chessfield.GetFigureAt(new Position(7, 1)) == null && chessfield.GetFigureAt(new Position(6, 1)) == null)
                    castling.Add(fig.Position);

            }
            else
            {
                fig = chessfield.GetFigureAt(new Position(1, 8));
                if (fig != null && !chessfield.isDangerPosition(oppside, fig.Position) && !fig.ismoved() && chessfield.GetFigureAt(new Position(2, 8)) == null && chessfield.GetFigureAt(new Position(3, 8)) == null
                    && chessfield.GetFigureAt(new Position(4, 8)) == null)
                    castling.Add(fig.Position);
                fig = chessfield.GetFigureAt(new Position(8, 8));
                if (fig != null && !chessfield.isDangerPosition(oppside, fig.Position) && !fig.ismoved() && chessfield.GetFigureAt(new Position(7, 8)) == null && chessfield.GetFigureAt(new Position(6, 8)) == null)
                    castling.Add(fig.Position);
            }
            return castling;

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
