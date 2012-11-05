using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace mychess
{
    public class ServerThread:BaseClientServer
    {

        private int port;
        private View view;
        private Game game;

        public ServerThread(View view, Game game, int port=12000)
        {
            this.view = view;
            this.game = game;
            this.port = port;
        }

        public void Run()
        {
            TcpListener listener = null;
            Player player = game.Player2;
            try
            {
                listener = new TcpListener(IPAddress.Any, 12000);
                listener.Start(1);
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream ns = client.GetStream();
                string playername = ReadString(ns);
                int lose, win;
                win = ReadInt(ns);
                lose = ReadInt(ns);
                player.Name = playername;
                player.SetStatistic(win, lose);
                // send player info
                player = game.Player1;
                WriteString(ns, player.Name);
                WriteInt(ns, player.GetWin());
                WriteInt(ns, player.GetLose());
                view.HideServerBanner();
                ns.Close();
                client.Close();
            }
            catch (Exception e)
            {
                view.Message(e.Message);
            }
            finally
            {
                if (listener != null)
                {
                    // Остановим его
                    listener.Stop();
                }
            }
        }
    }
}
