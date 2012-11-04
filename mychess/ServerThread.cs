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
            try
            {
                listener = new TcpListener(IPAddress.Any, 12000);
                listener.Start(1);
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream ns = client.GetStream();
                string s = ReadString(ns);
                view.HideServerBanner();
                view.Message(s);
                ns.Close();
                client.Close();
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
