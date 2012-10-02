using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mychess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        private void tsNewGame_Click(object sender, EventArgs e)
        {
            gbChessField.Visible = true;
            tsDefeat.Enabled = true;
            tsSave.Enabled = true;
            tsUndo.Enabled = true;
            tsNewGame.Enabled = false;
            tsNewLanGame.Enabled = false;
            tsLoad.Enabled = false;
            rtbLog.Visible = true;
            //Form2 form2 = new Form2();
            //form2.Show();
            Player p1, p2;
            p1 = new Player("Игрок 1", Side.White);
            p2 = new Player("Player2", Side.Black);
            Game gm = new Game(p1, p2);
            ChessField cf = gm.Field;
            GUI gui = new GUI(gbChessField);

            gui.NewGame(p1, p2, cf, gm);
        }

        private void gbChessField_Enter(object sender, EventArgs e)
        {

        }
    }
}
