using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mychess
{
    public class ChessField
    {
        private MovePolitics horizontalmovepolitics = new HorizontalMovePolitics();
        private MovePolitics verticalmovepolitics = new VerticalMovePolitic();
        private MovePolitics diagmovepolitics = new DiagMovePolitics();
        private MovePolitics knightmovepolitics = new KnightMovePolitics();
        private Player pl1, pl2;
        private Figure[,] field;


        public MovePolitics HorizontalMovePolitics
        {
            get{
            return horizontalmovepolitics;
            }
        }

        public MovePolitics VerticalMovePolitics
        {
            get{
            return verticalmovepolitics;
            }
        }

                public MovePolitics DiagMovePolitics
        {
            get{
            return diagmovepolitics;
            }
        }

                public MovePolitics KnightMovePolitics
        {
            get{
            return knightmovepolitics;
            }
        }



        public ChessField(Player p1, Player p2)
        {
            field = new Figure[8, 8];
            
            p1.alivefigures.Clear();
            p2.alivefigures.Clear();
            p1.deadfigures.Clear();
            p2.deadfigures.Clear();
            p1.ArrangeFigures(this);
            p2.ArrangeFigures(this);

            pl1 = p1;
            pl2 = p2;

            foreach (Figure fig in p1.alivefigures)
                field[fig.Position.GetX() - 1, fig.Position.GetY() - 1] = fig;
            foreach (Figure fig in p2.alivefigures)
                field[fig.Position.GetX() - 1, fig.Position.GetY() - 1] = fig;

        }

        public Figure GetFigureAt(Position pos)
        {
            try
            {
                return field[pos.GetX() - 1, pos.GetY() - 1];
            }
            catch{
                return null;
            }
        }

        public void MoveFigureAt(Position pos1, Position pos2)
        {
            Figure fig = GetFigureAt(pos1);
            Figure victim = GetFigureAt(pos2);
            if (victim != null)
            {
                pl1.KillFigure(victim);
                pl2.KillFigure(victim);
                field[pos2.GetX() - 1, pos2.GetY() - 1] = null;
            }
            pl1.MoveFigure(fig,pos2);
            pl2.MoveFigure(fig,pos2);

            


        }
        public void PositionChanged(Position from, Position newposition)
        {
            MoveFigureAt(from, newposition);
            Figure fig = field[from.GetX() - 1, from.GetY() - 1];
            field[from.GetX() - 1, from.GetY() - 1] = null;
            field[newposition.GetX() - 1, newposition.GetY() - 1] = fig;

        }

    }
}
