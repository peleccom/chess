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
            try
            {
                view.Message(server);
                TcpClient client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse(server), 12000));
                NetworkStream ns = client.GetStream();
                WriteString(ns, game.Player2.Name);
                client.Close();
                ns.Close();
            }
            finally
            { 
            }

            //Console.ReadKey();

        }
    }
}
