using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace test
{
    class Program
    {
        static void Main()
        {
            string connStr = "server=localhost;user=root;database=kursach;password=0000;";

            MySqlConnection connect = new MySqlConnection(connStr);

            connect.Open();

            string sql = "SELECT id,name FROM testnumbers";
            string sql2 = "SELECT id,task,answers FROM testtasks WHERE testid=1";

            MySqlCommand comm = new MySqlCommand(sql, connect);

            MySqlDataReader read = comm.ExecuteReader();

            while (read.Read())
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.WriteLine(read[j].ToString());
                }
            }
            connect.Close();

            connect.Open();

            MySqlCommand command = new MySqlCommand(sql2, connect);
            MySqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(reader.GetString(i));
                }
                
                string readerStr = reader[2].ToString();
                string[] str = readerStr.Split(' ');
                Console.WriteLine(str[1]);
            }
            


            connect.Close();

            Console.ReadKey();
        }
    }
}
