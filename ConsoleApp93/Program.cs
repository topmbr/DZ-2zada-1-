using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace ConsoleApp93
{
    class Program
    {
        static void Main()
        {
            TcpListener server = null;

            try
            {
                // Встановлюємо порт для слухання
                int port = 8888;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // Створюємо TcpListener для слухання вказаного порту
                server = new TcpListener(localAddr, port);

                // Запускаємо слухання
                server.Start();

                Console.WriteLine("Сервер запущено...");

                while (true)
                {
                    Console.Write("Очікування клієнта... ");

                    // Блокуючий виклик, очікуємо підключення клієнта
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Клієнт підключений!");

                    // Отримуємо дані від клієнта
                    NetworkStream stream = client.GetStream();
                    byte[] data = new byte[256];
                    int bytesRead = stream.Read(data, 0, data.Length);
                    string request = Encoding.ASCII.GetString(data, 0, bytesRead);
                    Console.WriteLine($"Отримано запит: {request}");

                    // Відправляємо відповідь на запит
                    string response = DateTime.Now.ToString();
                    byte[] responseData = Encoding.ASCII.GetBytes(response);
                    stream.Write(responseData, 0, responseData.Length);

                    // Закриваємо з'єднання з клієнтом
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            finally
            {
                // Зупиняємо TcpListener
                server.Stop();
            }

        }
    }
}