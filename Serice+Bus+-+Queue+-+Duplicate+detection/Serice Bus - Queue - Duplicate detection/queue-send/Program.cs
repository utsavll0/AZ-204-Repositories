using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace queue_send
{
    class Program
    {
        private static string _bus_connectionstring= "Endpoint=sb://appnamespace3000.servicebus.windows.net/;SharedAccessKeyName=demo;SharedAccessKey=r8LrBjOu2zPbZquuwVSnnqsJqbAicOXPT4pYeGJ8pag=";
        private static string _queue_name = "qppqueue";
        static async Task Main(string[] args)
        {
            IQueueClient _client;
            _client = new QueueClient(_bus_connectionstring, _queue_name);
            Console.WriteLine("Sending Messages");
            for (int i = 1; i <=10; i++)
            {
                Order obj = new Order();
                
                var _message = new Message(Encoding.UTF8.GetBytes(obj.ToString()));
                _message.MessageId = i.ToString();
                await _client.SendAsync(_message);
                Console.WriteLine($"Sending Message : {obj.Id} ");
            }
        }
        }
}
