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
    public class ServerThread
    {

        private int port = 12000;
        private View view;

        public ServerThread(View view)
        {
            this.view = view;
        }

        public void Run()
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Any, 12000);
                listener.Start(1);
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    StreamReader sr = new StreamReader(client.GetStream());
                    //Console.WriteLine("Client : " + sr.ReadLine());

                    StreamWriter sw = new StreamWriter(client.GetStream());
                    sw.AutoFlush = true;
                    view.Message(sr.ReadLine());
                    client.Close();
                }
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
