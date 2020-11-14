using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using queue_send;

namespace topic_send
{
    class Program
    {
        private static string _bus_connectionstring = "Endpoint=sb://appnamespace2000.servicebus.windows.net/;SharedAccessKeyName=topicpolicy;SharedAccessKey=b1Y5V79pd2tFk4b+g7MTWytFz61H7gzSccqFhgs3xnI=";
        private static string _topic_name = "apptopic";
        private static ITopicClient _client;
        private static string[] Exams = new string []{ "AZ-900", "AZ-300", "AZ-400", "AZ-500", "AZ-204" };

        static async Task Main(string[] args)
        {
            _client = new TopicClient(_bus_connectionstring, _topic_name);

            for(int i=0;i<5;i++)
            {
                Order obj = new Order();
                var _message = new Message(Encoding.UTF8.GetBytes(obj.ToString()));
                _message.MessageId = $"{i}";
                _message.UserProperties.Add("Category", Exams[i]);           
                await _client.SendAsync(_message);
                Console.WriteLine($"Sending Message : {obj.Id} ");


            }
            Console.ReadKey();
            await _client.CloseAsync();

        }
    }
}
