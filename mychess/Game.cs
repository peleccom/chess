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
        View view;
        public Game(View view)
        {
            this.view = view;
            view.ShowgbHUD(false);
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

        /// <summary>
        /// Получить текущее состояние игры
        /// </summary>
        /// <returns></returns>
        public GameState GetState()
        {
            return state;
        }

        /// <summary>
        /// Подсветка хода
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="moves"></param>
        /// <param name="attacks"></param>
        /// <returns></returns>
        public bool Hightlight(Position pos, out MyList<Position> moves, out MyList<Position> attacks)
        {
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
                    attacks = GetAttacks(fig);
                    moves = GetMoves(fig, attacks);
                    // Если король под шахом и возможных ходов нет то конец игры FIX IT
                    if (fig.GetFigureType() == FigureTypes.King & moves.Count == 0 & attacks.Count == 0)
                        EndGame(fig);
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

        /// <summary>
        /// Отмена подсветки хода
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        ///  Находимся в состоянии выделения?
        /// </summary>
        /// <returns></returns>
        public bool isHighlighted()
        {
            if (state == GameState.HighlightedBlack || state == GameState.HighlightedWhite)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Эта клетка была выделена?
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool isCorrectMove(Position pos)
        {
            if (!isHighlighted())
                return false;
            if (moves.Contains(pos) || attacks.Contains(pos))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Является ли фигура той которая инициировала выделение?
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Конец игры
        /// </summary>
        private void EndGame(Figure king)
        {
            //
            if (state == GameState.WaitBlack)
                state = GameState.LoseBlack;
            if (state == GameState.WaitWhite)
                state = GameState.LoseWhite;
            king.Stalemate();
            
        }

        public MyList<Position> GetAttacks(Figure fig)
        {
            MyList<Position> lattacks = new MyList<Position>();
            MyList<Position> attacks = new MyList<Position>();
            lattacks = fig.GetAttacks();
            foreach (Position attack in lattacks) //для всех направлений удара проверям есть ли там фигура
                if (Field.GetFigureAt(attack) != null && Field.GetFigureAt(attack).Side != fig.Side
                        && Field.GetFigureAt(attack).GetFigureType() != FigureTypes.King)
                    attacks.Add(attack);
            return attacks;
        }

        public MyList<Position> GetMoves(Figure fig, MyList<Position> attacks)
        {
            MyList<Position> lmoves = new MyList<Position>();
            MyList<Position> moves = new MyList<Position>();
            lmoves = fig.GetMoves();
            foreach (Position move in lmoves)
                if (!attacks.Contains(move) && Field.GetFigureAt(move) == null)
                    moves.Add(move);
            return moves;

        }

        /// <summary>
        /// Новая игра
        /// </summary>
        public void NewGame(){
            this.player1 = new Player(view.GetUserName(Side.White), Side.White);
            this.player2 = new Player(view.GetUserName(Side.Black), Side.Black);
            //this.player1 = new Player("", Side.White);
            //this.player2 = new Player("", Side.Black);
            Field = new ChessField(player1, player2);
            view.ShowgbChessField(true);
            view.ShowrtbLog(true);
            view.EnableDefeat(true);
            view.EnableSave(true);
            view.EnableUndo(true);
            view.EnableNewGame(false);
            view.EnableNewLanGame(false);
            view.EnableLoad(false);
            view.ShowgbHUD(true);
            field.SetPawnSuperiousListener(view.PawnSuperiorityHandler);
            field.SetKingShahListener(view.KingShahHandler);
            field.SetKingStalemateListener(view.KingStalemateHandler);
            view.DrawField();
            view.SetWhiteName(player1.Name);
            view.SetBlackName(player2.Name);
        }

        public void NewServerGame()
        {

        }

        public void Close()
        {
            view.Close();
        }

        public void Cell_Click(Position pos)
        {
            MyList<Position> moves, attacks;
            Figure fig = Field.GetFigureAt(pos);
            if (!isHighlighted())
            {
                if (Hightlight(pos, out moves, out attacks))
                {
                    foreach (Position move in moves)
                    {
                        view.CellMove(move);
                    }
                    view.CellMove(pos);
                    foreach (Position move in attacks)
                    {
                        view.CellAttack(move);
                    }
                }


            }
            else
            {
                if (isCorrectMove(pos))
                {
                    view.AddToLog(Field.GetFigureAt(highlightedfigurepos).GetImage() + " " + pos.ToString());
                    Move(pos);
                    view.DrawField();
                }
                // снять выделение
                if (isHighlightedFigure(pos))
                {
                    MyList<Position> needunhighlight = Escape();
                    foreach (Position unhpos in needunhighlight)
                    {
                        view.CellDefault(unhpos);
                    }
                }

            }

            view.SetTurnText();

            view.WhiteCount(player1.GetCount());
            view.BlackCount(player2.GetCount());


        }
    }

}
