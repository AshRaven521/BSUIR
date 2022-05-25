using System;
using System.IO;
using System.Text;

namespace ChatClient.NetWork.IO
{


    class PacketBuilder
    {
        private MemoryStream ms;

        public PacketBuilder()
        {
            ms = new MemoryStream();
        }

        public void WriteOpCode(byte opCode)
        {
            ms.WriteByte(opCode);
        }

        public void WriteMessage(string message)
        {
            var messageLenght = message.Length;
            ms.Write(BitConverter.GetBytes(messageLenght));
            ms.Write(Encoding.ASCII.GetBytes(message));
        }

        public byte[] GetPacketBytes()
        {
            return ms.ToArray();
        }
    }
}
