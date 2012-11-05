using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace mychess
{
    public class BaseClientServer
    {
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
    }
}
