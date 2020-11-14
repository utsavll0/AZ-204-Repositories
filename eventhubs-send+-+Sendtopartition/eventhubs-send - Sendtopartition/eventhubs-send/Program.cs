﻿using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace eventhubs_send
{
    class Program
    {
        private static string connstring = "Endpoint=sb://appname.servicebus.windows.net/;SharedAccessKeyName=demo;SharedAccessKey=Dz60v7IXFFmFGWVMGSfmONyouCHa0S3/IvQINDZ3rAw=;EntityPath=apphub";
        private static string hubname = "apphub";

        static String[] courses = new String[]
        {
            "AZ-900","AZ-400","AZ-300","AZ-301","AZ-204"
        };
        static DataTable dt_table;
        static void Main(string[] args)
        {
             SendData().Wait();
        }
        
        private static async Task SendData()
        {
            
            EventHubProducerClient client = new EventHubProducerClient(connstring, hubname);
            string _partition = (await client.GetPartitionIdsAsync()).First();

            var _options = new CreateBatchOptions
            {
                PartitionId=_partition
            };

                EventDataBatch batch_obj = await client.CreateBatchAsync(_options);
                Random _rnd=new Random();    
            for (int i = 1; i <= 10; i++)
            {
                Order obj = new Order(i, courses[_rnd.Next(0, 4)], _rnd.Next(1, 100));
                batch_obj.TryAdd(new EventData(Encoding.UTF8.GetBytes(obj.ToString())));
             
            }
            await client.SendAsync(batch_obj);

            Console.WriteLine("Sent all Orders");

            }
        }
    }
    
