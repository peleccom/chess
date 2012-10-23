﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mychess
{
    /// <summary>
    /// Представляет собой шахматную доску
    /// </summary>
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

        /// <summary>
        ///  Получить фигуру в позиции pos
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Установить фигуру позиции pos
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="fig"></param>
        private void SetFigureAt(Position pos, Figure fig)
        {
            try
            {
                field[pos.GetX() - 1, pos.GetY() - 1] = fig;
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Проверяет находится ли позиция под ударом
        /// </summary>
        /// <param name="enemyside"></param>
        /// <param name="pos"></param>
        /// <param name="originalpos"></param>
        /// <returns></returns>
        public bool isDangerPosition(Side enemyside, Position pos,Position originalpos = null)
        {
            bool danger = false;
            Figure oldfig = null;
            if (originalpos != null)
            {
                oldfig = GetFigureAt(originalpos);
                SetFigureAt(originalpos, null);
            }
            Player pl = this.SideToPlayer(enemyside);
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
            if (originalpos != null)
                SetFigureAt(originalpos, oldfig);
            return danger;
        }

        /// <summary>
        /// Обработчик MoveEvent
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        public void MoveFigureHandler(object source, MoveEventArgs args)
        {
            Figure target = GetFigureAt(args.newpos);
            if (target != null)
            {
                // бьет другую фигуру
                target.Kill();
            }
            // устанавливает новую позицию
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
        
        /// <summary>
        /// Установить обработчик MoveEvent
        /// </summary>
        /// <param name="handler"></param> 
        public void SetFiguresMoveListener(MoveEventHandler handler)
        {
            foreach (Figure fig in pl1.alivefigures)
                fig.MoveEvent += handler;
            foreach (Figure fig in pl2.alivefigures)
                fig.MoveEvent += handler;
        }
        
        /// <summary>
        /// Установить обработчик KillEvent
        /// </summary>
        /// <param name="handler"></param>
        public void SetFiguresKillListener(KillEventHandler handler)
        {
            foreach (Figure fig in pl1.alivefigures)
                fig.KillEvent += handler;
            foreach (Figure fig in pl2.alivefigures)
                fig.KillEvent+= handler;
        }
        
        /// <summary>
        /// Установить обработчик PawnSuperiousEvent
        /// </summary>
        /// <param name="handler"></param>
        public void SetPawnSuperiousListener(PawnSuperiorityHandler handler)
        {
            foreach (Figure fig in pl1.alivefigures)
                if (fig.GetFigureType() == FigureTypes.Pawn)
                    fig.PawnSuperiorityEvent += handler;
            foreach (Figure fig in pl2.alivefigures)
                if (fig.GetFigureType() == FigureTypes.Pawn)
                    fig.PawnSuperiorityEvent += handler;
        }
        
        /// <summary>
        /// Установить обработчик KingShahEvent
        /// </summary>
        /// <param name="handler"></param>
        public void SetKingShahListener(KingShahHandler handler)
        {
            pl1.King.KingShahEvent += handler;
            pl2.King.KingShahEvent += handler;
        }

        /// <summary>
        /// Установить обработчик KingStalemateEvent
        /// </summary>
        /// <param name="handler"></param>
        public void SetKingStalemateListener(KingStalemateHandler handler)
        {
            pl1.King.KingStalemateEvent += handler;
            pl2.King.KingStalemateEvent += handler;
        }

        /// <summary>
        /// Возвращает игрока заданного цвета
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
        public Player SideToPlayer(Side side)
        {
            return (side == pl1.Side) ? pl1 : pl2;
        }

        /// <summary>
        ///  Находится ли король цвета под ударом
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
        public bool isShahedKing(Side side)
        {
            return isDangerPosition(SideToPlayer(side).King.GetEnemySide(), SideToPlayer(side).King.Position);
        }
    }
}
