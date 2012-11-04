using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace mychess
{
    
    class ClientThread
    {
        private Game game;
        private int port;
        private string server;
        public ClientThread(string server, int port, Game game)
        {
            this.port = port;
            this.server = server;
            this.game = game;
        }



        public void Run(){
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12000));

                StreamWriter sw = new StreamWriter(client.GetStream());
                sw.AutoFlush = true;
                StreamReader sr = new StreamReader(client.GetStream());
                sw.WriteLine(game.Player2.Name);
                client.Close();
            }
            finally
            { 
            }

            //Console.ReadKey();

        }
    }
}
