using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lr1
{
    class Program
    {
        static string enAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static void Main(string[] args)
        {
            //Файлы с данными хранятся возле .exe
            
            bool checker = true;
            while(checker)
            {
                Console.WriteLine("Выберите нужную опцию: \n" +
                "1 - Шифр Цезаря\n" +
                "2 - Шифр Виженера\n" +
                "3 - Выход");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                    {
                        Console.WriteLine();
                        int key = 0;
                        string message = "";
                        using (var stream = new StreamReader("caesar.txt"))
                        {
                            var text = stream.ReadToEnd().Split('\n');
                            key = Convert.ToInt32(text[1]);
                            message = text[0];
                        }

                        var codeMessage = Caesar(message, key);
                        var uncodeMessage = Caesar(codeMessage, -key);

                        Console.WriteLine($"Изначальное сообщение: {message}");
                        Console.WriteLine($"Ключ: {key}");
                        Console.WriteLine($"Зашифрованное сообщение: {codeMessage}");
                        Console.WriteLine($"Дешифрованное сообщение: {uncodeMessage}\n");

                        break;
                    }

                    case 2:
                    {
                        Console.WriteLine();
                        string password = "";
                        string message = "";

                        using(var sr = new StreamReader("vigener.txt"))
                        {
                            var text = sr.ReadToEnd().Split('\n');
                            message = text[0];
                            password = text[1];
                        }

                        var codedMessage = VigenerEncode(message, password);
                        var uncodedMessage = VigenerDecode(codedMessage, password);

                        Console.WriteLine($"Изначальное сообщение: {message}");
                        Console.WriteLine($"Пароль: {password}");
                        Console.WriteLine($"Зашифрованное сообщение: {codedMessage}");
                        Console.WriteLine($"Дешифрованное сообщение: {uncodedMessage}\n");

                        break;
                    }

                    case 3:
                    {
                        Console.WriteLine("Конец!");
                        checker = false;
                        break;
                    }
                        
                }
            }
            

            Console.ReadKey();
        }

        static string Caesar(string message, int key)
        {
            string lowerEnAlphabet = enAlphabet.ToLower();

            string fullEnAlphabet = enAlphabet + lowerEnAlphabet;

            int alphabetAmount = fullEnAlphabet.Length;
            string result = "";

            for(int i = 0; i < message.Length; i++)
            {
                char letter = message[i];
                int letterIndex = fullEnAlphabet.IndexOf(letter);

                if(letterIndex >= 0)
                {
                    //если индекс найден, то шифруем его
                    var codedIndex = (alphabetAmount + letterIndex + key) % alphabetAmount;
                    result += fullEnAlphabet[codedIndex];
                }
                else
                {
                    //если индекса нет, то так его и добавляем без шифрования
                    result += letter.ToString();
                }
            }

            return result;
        }

        static string VigenerEncode(string inputText, string password)
        {
            string result = "";
            int alphabetCount = enAlphabet.Length;

            inputText = inputText.ToUpper();
            var message = inputText.Trim(new char[] { '\r' });
            password = password.ToUpper();
            int passwordIndex = 0;

            for(int i = 0; i < message.Length; i++)
            {
                //Находим позицию буквы из сообщения в алфавите, затем буквы из пароля в алфавите
                int position = (enAlphabet.IndexOf(inputText[i]) + enAlphabet.IndexOf(password[passwordIndex])) % alphabetCount;

                result += enAlphabet[position];

                passwordIndex++;

                //Обнуляем индекс, если он достигает длины пароля
                if(passwordIndex == password.Length)
                {
                    passwordIndex = 0;
                }
            }

            return result;
        }

        static string VigenerDecode(string mes, string pas)
        {
            string res = "";
            mes = mes.ToUpper();
            pas = pas.ToUpper();

            int pasIndex = 0;
            int alphCount = enAlphabet.Length;

            for(int i = 0; i < mes.Length; i++)
            {
                int pos = (enAlphabet.IndexOf(mes[i]) + alphCount - enAlphabet.IndexOf(pas[pasIndex])) % alphCount;

                res += enAlphabet[pos];
                pasIndex++;

                if(pasIndex == pas.Length)
                {
                    pasIndex = 0;
                }
            }
 

            return res;
        }
    }
}
