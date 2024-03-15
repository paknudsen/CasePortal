using System;
using System.Net;
using NK.Data.Repositories.Extensions;
using NK.DataRepositories;
using NK.Inventory.ProductService;
using NServiceBus;

public static class Program
{
    public static void Main(string[] args)
    {
        var endpointConfiguration = new EndpointConfiguration("NK.Inventory.ProductService");
        endpointConfiguration.UseTransport<LearningTransport>();

        // Populate product repo with dummy data
        using (var repo = new ProductRepository())
        {
            repo.Products.FillProductRepositoryMock();
            repo.Commit();
        }

        var endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();


        Console.WriteLine("Ready to process message. Input options: simulate = send message, exit = exit program.");
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            if(line == "simulate")
            {
                var message = new SimulateProductActionMessage { ProductId = 3218130 };
                endpointInstance.SendLocal(message).GetAwaiter().GetResult();
            }

            if (line == "exit")
            {
                endpointInstance.Stop().GetAwaiter().GetResult();
            }
        }
    }

}