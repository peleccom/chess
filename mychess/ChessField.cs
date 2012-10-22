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
            Player pl = (pl1.Side == enemyside) ? pl1 : pl2;
            // To do
            // iterate over alive figures
            foreach (Figure fig in pl.alivefigures)
                if (fig.GetFigureType() == FigureTypes.King)
                {
                    if (((King)fig).GetMovesWithOutChecks().Contains(pos))
                    {
                        danger = true;
                        break;
                    }
                }
                else
                { 
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
            Figure target = GetFigureAt(args.newpos);
            if (target != null)
            {
                // бъет другую фигуру
                target.Kill();
            }
            // устанавивает новую позицию
            Figure fig = (Figure) source;
            field[args.oldpos.GetX() - 1, args.oldpos.GetY() - 1] = null;
            field[args.newpos.GetX() - 1, args.newpos.GetY() - 1] = fig;
            // шах
            if (isShahedKing(fig.GetEnemySide()))
                SideToPlayer(fig.GetEnemySide()).ShahAlert();

            else
            {
                SideToPlayer(fig.GetEnemySide()).ResetShahSituation();
                if (isShahedKing(fig.Side))
                    SideToPlayer(fig.Side).ShahAlert();
                else
                    SideToPlayer(fig.Side).ResetShahSituation();
            }

        }

        public void SetFiguresMoveListener(MoveEventHandler handler)
        {
            foreach (Figure fig in pl1.alivefigures)
                fig.MoveEvent += handler;
            foreach (Figure fig in pl2.alivefigures)
                fig.MoveEvent += handler;
        }

        public void SetFiguresKillListener(KillEventHandler handler)
        {
            foreach (Figure fig in pl1.alivefigures)
                fig.KillEvent += handler;
            foreach (Figure fig in pl2.alivefigures)
                fig.KillEvent+= handler;
        }

        public void SetPawnSuperiousListener(PawnSuperiorityHandler handler)
        {
            foreach (Figure fig in pl1.alivefigures)
                if (fig.GetFigureType() == FigureTypes.Pawn)
                    fig.PawnSuperiorityEvent += handler;
            foreach (Figure fig in pl2.alivefigures)
                if (fig.GetFigureType() == FigureTypes.Pawn)
                    fig.PawnSuperiorityEvent += handler;
        }

        public void SetKingShahHandler(KingShahHandler handler)
        {
            pl1.King.KingShahEvent += handler;
            pl2.King.KingShahEvent += handler;
        }

        public void SetKingStalemateHandler(KingStalemateHandler handler)
        {
            pl1.King.KingStalemateEvent += handler;
            pl2.King.KingStalemateEvent += handler;
        }
        // Возвращает игрока заданного цвета
        public Player SideToPlayer(Side side)
        {
            return (side == pl1.Side) ? pl1 : pl2;
        }


        public bool isShahedKing(Side side)
        {
            return isDangerPosition(SideToPlayer(side).King.GetEnemySide(), SideToPlayer(side).King.Position);
        }
    }
}
