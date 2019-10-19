using System;
using System.Threading.Tasks;

namespace EchantionCodeChallenge.Client
{
    class Program
    {
        static readonly Client client = new Client();
        static Task Main(string[] args)
        {
            Console.WriteLine("Send message to server or press Ctrl + C to exit.");

            while (true)
            {

                var message = Console.ReadLine();

                Task.Run(() => SendMessageToServer(message));

            }

        }

        static void SendMessageToServer(string message)
        {
            var reply = client.SendAsync(new RequestMessage() { Message = message });
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==> " + reply.Result.Message);
            Console.ResetColor();

        }
    }



}
