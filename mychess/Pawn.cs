using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mychess
{
    public class Pawn : Figure
    {
        private MovePolitics[] attackpolitics;
        public Pawn(Position pos, Side side, ChessField chessfield):base(pos,side, chessfield)
        {
            FigureType = FigureTypes.Pawn;
            movepolitics = new MovePolitics[] { chessfield.VerticalMovePolitics};
            attackpolitics = new MovePolitics[] { chessfield.DiagMovePolitics};
            
        }
    
        public override List<Position> GetAttacks()
        {
            List<Position> l = new List<Position>();
            foreach (MovePolitics mp in attackpolitics)
                    l.AddRange(mp.GetMoves(this, chessfield));
            return l;
            
        }
        public override string GetImage()
        {
            if (Side == Side.Black)
                return "♟";
            else
                return "♙";
        }
    }
}
