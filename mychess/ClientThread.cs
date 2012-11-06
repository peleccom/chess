using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace mychess
{
    
    class ClientThread:BaseClientServer
    {
        private View view;
        private Game game;
        private int port;
        private string server;
        public ClientThread(View view,Game game, string server, int port)
        {
            this.view = view;
            this.port = port;
            this.server = server;
            this.game = game;
        }



        public void Run(){
            Player player = game.Player2;
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse(server), port));
                NetworkStream ns = client.GetStream();
                view.AddToLog(String.Format("Подключен к серверу {0}:{1}", server, port));
                WriteString(ns, player.Name);
                WriteInt(ns, player.GetWin());
                WriteInt(ns, player.GetLose());
                // get first player
                player = game.Player1;
                string playername = ReadString(ns);
                int lose, win;
                win = ReadInt(ns);
                lose = ReadInt(ns);
                player.Name = playername;
                player.SetStatistic(win, lose);
                view.Invoke(new Action(() => game.ClientGameView()));
                client.Close();
                ns.Close();
            }
            catch (Exception e) {
                view.Message(e.Message);
            }
            finally
            { 
            }

            //Console.ReadKey();

        }
    }
}
