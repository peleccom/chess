using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mychess
{
    public class Player
    {
        string name;
        Side side;
        private int win, lose;
        public List<Figure> alivefigures = new List<Figure>();
        public List<Figure> deadfigures = new List<Figure>();
        

        public Player(string name, Side side)
        {
            Name = name;
            Side = side;
            win = 0;
            lose = 0;


            
        }

        private Position SidePosition(int x, int y)
        {
            // для расстановки фигур позволяет писать один и тот же код для черных и белых
            // координаты от 0 до 7
            if (side == Side.Black)
            {
                x = 7 - x;
                y = 7 - y;
            }
            return new Position(x+1, y+1);
        }

        public void ArrangeFigures(ChessField chessfield){
            // расстановка на доске
                for (int i = 0; i < 8; i++)
                    alivefigures.Add(new Pawn(SidePosition(i, 1), side, chessfield));
                alivefigures.Add(new Rook(SidePosition(0, 0), side, chessfield));
                alivefigures.Add(new Rook(SidePosition(7, 0), side, chessfield));
                alivefigures.Add(new Knight(SidePosition(1, 0), side, chessfield));
                alivefigures.Add(new Knight(SidePosition(6, 0), side, chessfield));
                alivefigures.Add(new Bishop(SidePosition(2, 0), side, chessfield));
                alivefigures.Add(new Bishop(SidePosition(5, 0), side, chessfield));
            // король с ферзем ставяться зеркально
                Position pos = SidePosition(3, 0); // отображаем координату y если надо
                alivefigures.Add(new King(new Position(4+1, pos.GetY()), side, chessfield));
                alivefigures.Add(new Queen(new Position(3+1, pos.GetY()), side, chessfield));        
        }
        

        public void MoveFigure(Figure fig, Position newpos)
    {
            int index;
            if (alivefigures.Contains(fig))
            {
                index = alivefigures.IndexOf(fig);
                alivefigures[index].SetPosition(newpos);
            }
    }

        public void KillFigure(Figure fig)
        {
            if (alivefigures.Contains(fig))
            {
                alivefigures.Remove(fig);
                deadfigures.Add(fig);

            }
        }


        public int GetCount()
        {
            return alivefigures.Count;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public Side Side
        {
            get
            {
                return side;
            }
            set
            {
                side = value;
            }
        }

        public void Lose()
        {
            lose++;
        }

        public void Win()
        {
            win++;
        }
        public int GetLose()
        {
            return lose;
        }

        public int GetWin()
        {
            return win;
        }

    }
}
