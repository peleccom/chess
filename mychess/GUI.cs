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
        private Control ctrl;
        Game game;

        public GUI(Control ctrl)
        {
            this.ctrl = ctrl;
            buttons = new Button[8, 8];
        }
    
        public void NewGame(Player p1, Player p2, ChessField cf, Game g)
        {
            game = g;

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Button btn = new Button();
                    btn.Parent = ctrl;
                    btn.Height = 50;
                    btn.Width = 50;
                    btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    btn.BackgroundImageLayout = ImageLayout.Center;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    if ((i + j) % 2 == 0)
                        btn.BackColor = Color.Gray;
                    else
                        btn.BackColor = Color.White;
                    btn.Left = 50 * i;
                    btn.Top = 50 * (7 - j);
                    buttons[i, j] = btn;
                    btn.Click += new System.EventHandler(Cell_Click);
                    btn.Tag = (Object)(new Position(i + 1, j + 1));
                    
            }

            DrawField();
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
            List<Position> moves , attacks;
            Button btn = (Button)sender;
            Position pos = (Position)btn.Tag;
            Figure fig = game.Field.GetFigureAt(pos);
                if (!game.isHighlighted())
                {
                    if (game.Hightlight(pos, out moves, out attacks))
                    {
                        foreach (Position move in moves)
                        {
                            buttons[move.GetX() - 1, move.GetY() - 1].FlatAppearance.BorderSize = 4;
                            buttons[move.GetX() - 1, move.GetY() - 1].FlatAppearance.BorderColor = Color.LightGreen;
                        }
                        buttons[pos.GetX() - 1, pos.GetY() - 1].FlatAppearance.BorderSize = 4;
                        buttons[pos.GetX() - 1, pos.GetY() - 1].FlatAppearance.BorderColor = Color.LightGreen;
                        foreach (Position move in attacks)
                        {
                            buttons[move.GetX() - 1, move.GetY() - 1].FlatAppearance.BorderSize = 4;
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
                            List<Position> needunhighlight = game.Escape();
                            foreach (Position unhpos in needunhighlight)
                            {
                                buttons[unhpos.GetX() - 1, unhpos.GetY() - 1].FlatAppearance.BorderSize = 0;
                                buttons[unhpos.GetX() - 1, unhpos.GetY() - 1].FlatAppearance.BorderColor = Color.Red;
                            }
                        }
                    
            }
            
            //if (game.GetState() == Side.Black)
            //else 
        }
        public void PositionChanged(Figure fig, Position newposition)
        {
            throw new System.NotImplementedException();
        }
    }
}
