using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mychess
{
    public class Game
    {
        private Player player1;
        private Player player2;
        private GameState state;
        private ChessField field;
        private MyList<Position> moves, attacks;
        Position highlightedfigurepos;

        public Game(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
            Field = new ChessField(player1, player2);
        }
    
        public Player Player1
        {
            get
            {
                return player1;
            }
        }

        public Player Player2
        {
            get
            {
                return player2;
            }
        }

        public ChessField Field
        {
            get
            {
                return field;
                
            }
            private set
            {
                field = value;
            }
        }

        public GameState GetState()
        {
            return state;
        }
        public bool Hightlight(Position pos, out MyList<Position> moves, out MyList<Position> attacks)
        {
            MyList<Position> lattacks, lmoves;
            moves = new MyList<Position>();
            attacks = new MyList<Position>();
            if (state != GameState.WaitWhite && state!= GameState.WaitBlack)
                return false;
            Figure fig = Field.GetFigureAt(pos);
            if (fig != null)
            {
                if ((state == GameState.WaitBlack && fig.Side == Side.Black) ||
                    (state == GameState.WaitWhite && fig.Side == Side.White))
                {
                    // highlighting code
                    lattacks = fig.GetAttacks();
                    lmoves = fig.GetMoves();
                    foreach (Position attack in lattacks) //для всех направлений удара проверям есть ли там фигура
                        if (Field.GetFigureAt(attack) != null && Field.GetFigureAt(attack).Side != fig.Side 
                                && Field.GetFigureAt(attack).GetFigureType() != FigureTypes.King)
                            attacks.Add(attack);
                    foreach (Position move in lmoves)
                        if (!attacks.Contains(move) && Field.GetFigureAt(move) == null)
                            moves.Add(move);
                    if (state == GameState.WaitBlack)
                        state = GameState.HighlightedBlack;
                    if (state == GameState.WaitWhite)
                    state = GameState.HighlightedWhite;
                    this.moves = moves;
                    this.attacks = attacks;
                    highlightedfigurepos = pos;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }


        }
        public MyList<Position> Escape()
        {

            if (state == GameState.HighlightedBlack)
            {
                state = GameState.WaitBlack;
            }
            if (state == GameState.HighlightedWhite)
                state = GameState.WaitWhite;
            MyList<Position> movs = new MyList<Position>();
            movs.AddRange(moves);
            movs.AddRange(attacks);
            movs.Add(highlightedfigurepos);
            moves = null;
            attacks = null;
            return movs;
            
        }
        // находимся в состоянии выделения
        public bool isHighlighted()
        {
            if (state == GameState.HighlightedBlack || state == GameState.HighlightedWhite)
                return true;
            else
                return false;
        }
        // эта клетка была выделена?
        public bool isCorrectMove(Position pos)
        {
            if (!isHighlighted())
                return false;
            if (moves.Contains(pos) || attacks.Contains(pos))
                return true;
            else
                return false;
        }
        // является ли фигура той которая инициировала выделение?
        public bool isHighlightedFigure(Position pos)
        {
            if (!isHighlighted())
                return false;
            if (highlightedfigurepos == pos)
                return true;
            else
                return false;

        }

        public bool Move(Position pos)
        {
            if (!isHighlighted())
                return false;
            if (!isCorrectMove(pos))
                return false;

            if (moves.Contains(pos) || attacks.Contains(pos))
            {

                Figure fig = Field.GetFigureAt(highlightedfigurepos);
                fig.SetPosition(pos);
            }

            switch (state)
            {
                case GameState.HighlightedBlack:
                    state = GameState.WaitWhite;
                    break;
                case GameState.HighlightedWhite:
                    state = GameState.WaitBlack;
                    break;
            }
            return true;
        }
        }
}
