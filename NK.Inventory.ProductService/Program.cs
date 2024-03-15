using System;
using System.Net;
using NK.Inventory.ProductService;
using NServiceBus;

//class Program
//{
//    static async Task Main(string[] args)
//    {
//        var endpointConfiguration = new EndpointConfiguration("NK.Inventory.ProductService");
//        endpointConfiguration.UseTransport<LearningTransport>();

//        var endpointInstance = await Endpoint.Start(endpointConfiguration)
//            .ConfigureAwait(false);



//        Console.WriteLine("Ready.");
//        string line;
//        while ((line = Console.ReadLine()) != null)
//        {
//            var message = new SimulateProductActionMessage { ProductId = 1234 };
//            await endpointInstance.SendLocal(message);
//        }

//        await endpointInstance.Stop()
//            .ConfigureAwait(false);
//    }
//}


public static class Program
{
    public static void Main(string[] args)
    {
        var endpointConfiguration = new EndpointConfiguration("NK.Inventory.ProductService");
        endpointConfiguration.UseTransport<LearningTransport>();

        var endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

        Console.WriteLine("Ready to process message. Input options: simulate = send message, exit = exit program.");
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            if(line == "simulate")
            {
                var message = new SimulateProductActionMessage { ProductId = 1234 };
                endpointInstance.SendLocal(message).GetAwaiter().GetResult();
            }

            if (line == "exit")
            {
                endpointInstance.Stop().GetAwaiter().GetResult();
            }
        }
    }
}