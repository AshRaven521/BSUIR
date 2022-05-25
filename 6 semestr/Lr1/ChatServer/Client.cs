using ChatServer.NetWork.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Client
    {
        public string Username { get; set; }
        public Guid UId { get; set; }
        public TcpClient ClientSocket { get; set; }

        private PacketReader packetReader; 
        public Client(TcpClient client)
        {
            ClientSocket = client;
            UId = Guid.NewGuid();

            packetReader = new PacketReader(ClientSocket.GetStream());

            var opcode = packetReader.ReadByte();
            Username = packetReader.ReadMessage();

            Console.WriteLine($"[{DateTime.Now}] : Клиент присоединился с именем пользователя : {Username}");

            Task.Run(() => Process());
        }

        private void Process()
        {
            while(true)
            {
                try
                {
                    var opcode = packetReader.ReadByte();
                    switch(opcode)
                    {
                        case 5:
                            var message = packetReader.ReadMessage();
                            Console.WriteLine($"[{DateTime.Now}] : Сообщение принято! {message}");
                            Program.BroadCastMessage($"[{DateTime.Now}] : [{Username}] : {message}");
                            break;
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine($"[{UId}] : Отключен!");
                    Program.BroadCastDisconnect(UId.ToString());
                    ClientSocket.Close();
                    break;
                }
            }
        }

    }
}
