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
            {
                field[fig.Position.GetX() - 1, fig.Position.GetY() - 1] = fig;
            }
            foreach (Figure fig in p2.alivefigures)
            {

                field[fig.Position.GetX() - 1, fig.Position.GetY() - 1] = fig;
            }

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

        public bool isDangerPosition(Side enemyside, Position pos)
        {
            bool danger = false;
            Player[] players = { pl1, pl2 };
            // To do
            foreach (Player pl in players)
            {
                if (pl.Side == enemyside)
                    // iterate over alive figures
                    foreach (Figure fig in pl.alivefigures)
                        // to do
                        if (fig.GetAttacks().Contains(pos))
                        {
                            danger = true;
                            break;
                        }
            }


            return danger;
        }
        public void MoveFigureHandler(object source, MoveEventArgs args)
        {
            Figure fig = GetFigureAt(args.newpos);
            if (fig != null)
            {
                // бъет другую фигуру
                fig.Kill();
            }
            // устанавивает новую позицию
            field[args.oldpos.GetX() - 1, args.oldpos.GetY() - 1] = null;
            field[args.newpos.GetX() - 1, args.newpos.GetY() - 1] = (Figure)source;


        }
    }
}
