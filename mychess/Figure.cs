using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mychess
{
    public abstract class Figure
    {
        private Side side;
        private Position pos;
        protected MovePolitics[] movepolitics;
        protected /*readonly*/ FigureTypes FigureType;
        protected ChessField chessfield;

        public Figure(Position pos, Side side, ChessField chessfield)
        {
            this.pos = pos;
            this.Side = side;
            this.chessfield = chessfield;
        }

        public Position Position
        {
            get
            {
                return pos;
            }
            private set
            {
                pos = value;
            }
        }

        public Side Side
        {
            get
            {
                return side;
            }
            private set
            {
                side = value;
            }
        }

        public List<Position> GetMoves()
        {
            List<Position> l = new List<Position>();
            foreach (MovePolitics mv in movepolitics)
                l.AddRange(mv.GetMoves(this,chessfield));
            return l;
        }

        /// <remarks>Возвращает позицию дополненную по y</remarks>
        public void SetPosition(Position pos)
        {
            Position = pos;
            //chessfield.PositionChanged(this, pos);
        }

        public virtual List<Position> GetAttacks()
        {
            return GetMoves();
        }

        public Position Reverse(Position pos)
        {
            byte x = pos.GetX();
            byte y = pos.GetY();
            y = (byte)(9 - y);
            return new Position(x, y);
            
        }

        public FigureTypes GetFigureType() // запрет на переопределение метода
        {
            return FigureType;
        }

        virtual public string GetImage()
        {
            throw new System.NotImplementedException();
        }
    }
}
