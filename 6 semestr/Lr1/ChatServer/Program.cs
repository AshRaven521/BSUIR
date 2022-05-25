using ChatServer.NetWork.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    class Program
    {
        private static TcpListener listener;
        private static List<Client> users;
        static void Main(string[] args)
        {
            users = new List<Client>();

            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
            listener.Start();

            while (true)
            {
                var client = new Client(listener.AcceptTcpClient());
                users.Add(client);
                BroadCastConnection();
            }

        }

        private static void BroadCastConnection()
        {
            foreach (var user in users)
            {
                foreach (var usr in users)
                {
                    var broadcastPacket = new PacketBuilder();
                    broadcastPacket.WriteOpCode(1);
                    broadcastPacket.WriteMessage(usr.Username);
                    broadcastPacket.WriteMessage(usr.UId.ToString());
                    user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                }
            }
        }

        public static void BroadCastMessage(string message)
        {
            foreach (var user in users)
            {
                var messagePacket = new PacketBuilder();
                messagePacket.WriteOpCode(5);
                messagePacket.WriteMessage(message);
                user.ClientSocket.Client.Send(messagePacket.GetPacketBytes());
            }
        }

        public static void BroadCastDisconnect(string uid)
        {
            var disconnectUser = users.Where(a => a.UId.ToString() == uid).FirstOrDefault();
            users.Remove(disconnectUser);
            foreach(var user in users)
            {
                var broadcastMessage = new PacketBuilder();
                broadcastMessage.WriteOpCode(10);
                broadcastMessage.WriteMessage(uid);
                user.ClientSocket.Client.Send(broadcastMessage.GetPacketBytes());
            }

            BroadCastMessage($"[{disconnectUser.Username}] Отключился!");
        }
    }
}
