using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Threading;

namespace mychess
{
    public class BaseClientServer
    {

        protected bool newmove;
        protected bool hasdefeat;
        protected Side defeatside;
        Position from;
        Position to;
        protected const string commov = "Move";
        protected const string comend = "End";
        protected const string comdef = "Defeat";
        protected object lockobj = new object();
        protected BaseClientServer(){
            newmove = false;
            hasdefeat = false;
        }

        protected string ReadString(NetworkStream ns)
        {
            byte[] buffer = new byte[4];
            int len;
            ns.Read(buffer, 0, 4);
            len = BitConverter.ToInt32(buffer, 0);
            buffer = new byte[len];
            ns.Read(buffer, 0, len);
            return Encoding.UTF8.GetString(buffer);
        }

        protected void WriteString(NetworkStream ns, string s)
        {
            int len;
            byte[] buffer;
            buffer = Encoding.UTF8.GetBytes(s);
            len = buffer.Length;
            ns.Write(BitConverter.GetBytes(len), 0, 4);
            ns.Write(buffer, 0, len);
        }

        protected void WriteInt(NetworkStream ns, int n)
        {
            byte[] buffer;
            buffer = BitConverter.GetBytes(n);
            ns.Write(buffer, 0, 4);
        }

        protected int ReadInt(NetworkStream ns)
        {
            byte []buffer = new byte[4];
            ns.Read(buffer, 0, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        public void NewMove(Position from, Position to)
        {
            //MessageBox.Show("new move");
            lock (lockobj)
            {
                newmove = true;
                this.from = from;
                this.to = to;
            }
        }

        public void GetMove(NetworkStream ns, View view, Game game )
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Position from = (Position)formatter.Deserialize(ns);
            Position to = (Position)formatter.Deserialize(ns);
            // view 
            view.Invoke(new Action(
                () => { game.Cell_Click(from); Thread.Sleep(100); game.Cell_Click(to); }));
        }

        public void SendMove(NetworkStream ns, View view, Game game)
        {
            //MessageBox.Show("Send move");
            lock (from)
            {
                WriteString(ns, commov);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ns, from);
                formatter.Serialize(ns, to); 
                newmove = false;
            }
        }
        public void NewDefeat(Side side)
        {
            lock (lockobj)
            {
                hasdefeat = true;
                defeatside = side;
            }

        }

        protected void SendDefeat(NetworkStream ns,Side side)
        {
            hasdefeat = false;
            WriteString(ns, comdef);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ns, side);
        }

        protected void GetDefeat(NetworkStream ns, View view, Game game)
        {

            BinaryFormatter formatter = new BinaryFormatter();
            Side side = (Side)formatter.Deserialize(ns);

            view.Invoke(new Action(
            () =>{game.EndGame(game.Field.SideToPlayer(side).King);}));
        }
    }
}
