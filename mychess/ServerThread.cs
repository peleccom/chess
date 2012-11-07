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
            string command;
            bool docycle = true;
            try
            {
                listener = new TcpListener(IPAddress.Any, 12000);
                listener.Start(1);
                TcpClient client = listener.AcceptTcpClient();
                view.AddToLog(String.Format("Подключен клиент {0}", client.Client.RemoteEndPoint.ToString()));
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

                while (docycle)
                {
                    if (game.GetState() == GameState.HighlightedWhite || game.GetState() == GameState.WaitWhite){
                        lock (lockobj)
                        {
                            Monitor.Wait(lockobj);
                            if (newmove)
                            {
                                //view.Message("New move detected");
                                SendMove(ns, view, game);
                            }
                        }
                        }
                    else
                    if (game.GetState() == GameState.WaitBlack)
                    {
                        command = ReadString(ns);
                        switch (command)
                        {
                            case commov:
                                {
                                    GetMove(ns, view, game);
                                    break;
                                }
                            case comend: {
                                docycle = false;
                                break;
                            }
                        }
                    }
                    Thread.Sleep(100);
                }
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
                view.Message("ended");
            }
        }
    }
}
