using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.NetWork.IO
{
    class PacketReader : BinaryReader
    {
        private NetworkStream ns;

        public PacketReader(NetworkStream ns) : base(ns)
        {
            this.ns = ns;
        }

        public string ReadMessage()
        {
            byte[] messageBuffer;

            var lenght = ReadInt32();
            messageBuffer = new byte[lenght];

            ns.Read(messageBuffer, 0, lenght);

            var message = Encoding.ASCII.GetString(messageBuffer);

            return message;
        }
    }
}
