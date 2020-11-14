﻿using Microsoft.Azure.ServiceBus;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace topic___receive
{
    class Program
    {
        private static string _bus_connectionstring = "Endpoint=sb://appnamespace2000.servicebus.windows.net/;SharedAccessKeyName=topicpolicy;SharedAccessKey=b1Y5V79pd2tFk4b+g7MTWytFz61H7gzSccqFhgs3xnI=;EntityPath=apptopic";
        
        private static ISubscriptionClient _client;
        private static string _subscription_name = "SubscriptionA";

        static void Main(string[] args)
        {
            TopicFunction().GetAwaiter().GetResult();
        }

        static async Task TopicFunction()
        {


            ServiceBusConnectionStringBuilder builder = new ServiceBusConnectionStringBuilder(_bus_connectionstring);
            _client = new SubscriptionClient(builder, _subscription_name);

            var _options = new MessageHandlerOptions(ExceptionReceived)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _client.RegisterMessageHandler(Process_Message, _options);
            Console.ReadKey();
        }


        static async Task Process_Message(Message _message, CancellationToken _token)
        {
            Console.WriteLine(Encoding.UTF8.GetString(_message.Body));
            await _client.CompleteAsync(_message.SystemProperties.LockToken);
        }

        static Task ExceptionReceived(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine(args.Exception);
            return Task.CompletedTask;
        }

    }
}
