using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace mychess
{
    public class GUI
    {
        private Button[,] buttons;
        private Control gbChessField;
        private Label lTurn, lLost, lKilled, lWhite, lBlack;
        private RichTextBox rtbLog;
        private Form form;
        Game game;

        public GUI(GroupBox gbChessField, Label lTurn, Label lLost, Label lKilled, Label lWhite, Label lBlack, RichTextBox rtbLog, Form form)
        {
            this.gbChessField = gbChessField;
            this.lTurn = lTurn;
            this.lLost = lLost;
            this.lKilled = lKilled;
            this.lWhite = lWhite;
            this.lBlack = lBlack;
            this.rtbLog = rtbLog;
            this.form = form;
            buttons = new Button[8, 8];
        }
    
        public void NewGame(Player p1, Player p2, ChessField cf, Game g)
        {
            form.SuspendLayout();
            game = g;
            Font font = new System.Drawing.Font("Arial Unicode MS", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    int size = 70;
                    Button btn = new Button();
                    btn.Parent = gbChessField;
                    btn.Height = size;
                    btn.Width = size;
                    btn.Font = font;
                    btn.BackgroundImageLayout = ImageLayout.Center;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    if ((i + j) % 2 == 0)
                        btn.BackColor = Color.Gray;
                    else
                        btn.BackColor = Color.White;
                    btn.Left = size * i;
                    btn.Top = size * (7 - j);
                    buttons[i, j] = btn;
                    btn.Click += Cell_Click;
                    btn.Tag = (Object)(new Position(i + 1, j + 1));   
            }

            cf.SetPawnSuperiousListener(PawnSuperiorityHandler);
            cf.SetKingCheckHandler(KingCheckHandler);
            cf.SetKingStalemateHandler(KingStalemateHandler);
            DrawField();
            form.ResumeLayout(false);
        }
        
        private void DrawField()
        {
            Figure figure;
            Button btn;
            for (int i = 0 ; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    //btn.Enabled = false;
                    
                    btn = buttons[i, j];
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    figure = game.Field.GetFigureAt(new Position(i+1, j+1));
                    if (figure != null)
                        btn.Text = figure.GetImage();
                    else
                        btn.Text = "";
                }
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            MyList<Position> moves , attacks;
            Button btn = (Button)sender;
            Position pos = (Position)btn.Tag;
            Figure fig = game.Field.GetFigureAt(pos);
                if (!game.isHighlighted())
                {
                    if (game.Hightlight(pos, out moves, out attacks))
                    {
                        foreach (Position move in moves)
                        {
                            buttons[move.GetX() - 1, move.GetY() - 1].FlatAppearance.BorderSize = 2;
                            buttons[move.GetX() - 1, move.GetY() - 1].FlatAppearance.BorderColor = Color.Green;
                        }
                        buttons[pos.GetX() - 1, pos.GetY() - 1].FlatAppearance.BorderSize = 2;
                        buttons[pos.GetX() - 1, pos.GetY() - 1].FlatAppearance.BorderColor = Color.Green;
                        foreach (Position move in attacks)
                        {
                            buttons[move.GetX() - 1, move.GetY() - 1].FlatAppearance.BorderSize = 2;
                            buttons[move.GetX() - 1, move.GetY() - 1].FlatAppearance.BorderColor = Color.Red;
                        }
                    }


                 }
                else
                    {
                        if (game.isCorrectMove(pos))
                        {
                            game.Move(pos);
                            DrawField();
                        }
                        // снять выделение
                        if (game.isHighlightedFigure(pos))
                        {
                            MyList<Position> needunhighlight = game.Escape();
                            foreach (Position unhpos in needunhighlight)
                            {
                                buttons[unhpos.GetX() - 1, unhpos.GetY() - 1].FlatAppearance.BorderSize = 0;
                                buttons[unhpos.GetX() - 1, unhpos.GetY() - 1].FlatAppearance.BorderColor = Color.Red;
                            }
                        }
                    
            }

                switch (game.GetState())
                {
                    case GameState.WaitBlack:
                    case GameState.HighlightedBlack: lTurn.Text = "Ход чёрных"; break;
                    case GameState.HighlightedWhite:
                    case GameState.WaitWhite: lTurn.Text = "Ход белых"; break;
                }

        }

        public void PositionChanged(Figure fig, Position newposition)
        {
            throw new System.NotImplementedException();
        }

        public void PawnSuperiorityHandler(object obj, EventArgs args){
            Figure fig = (Figure) obj;
            MessageBox.Show(fig.Position.ToString()+"\nСказку про Золушку читали, не?");

        }

        public void KingCheckHandler(object source, EventArgs args)
        {
            Figure fig = (Figure)source;
            if (fig.Side == Side.Black)
            MessageBox.Show("Чёрный Король под шахом. Хьюстон, у нас проблема.");
            else
            MessageBox.Show("Белый Король под шахом. Хьюстон, у нас проблема.");

        }
        public void KingStalemateHandler(object source, EventArgs args)
        {
            Figure fig = (Figure)source;
            if (fig.Side == Side.Black)
            MessageBox.Show("Тучи расступились, и с неба засветило яркое солнце. Поверженные чёрные фигуры тускло блестели...");
            else
            MessageBox.Show("Солнце скрылось за тучами, на поле боя пала тень. Поверженные белые фигуры пропали во мраке...");
        }

    }
}
