﻿using NK.Data.Repositories.Entities;
using NK.Service.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NK.Inventory.ProductService
{

    public class SimulateProductActionHandler : IHandleMessages<SimulateProductActionMessage>
    {
        public Task Handle(SimulateProductActionMessage message, IMessageHandlerContext context)
        {
            // Handle the received message
            Console.WriteLine($"Received message: {message.ProductId}");

            // Perform any necessary processing
            new SimulateProductActionProcess().Process(message.ProductId);


            return Task.CompletedTask;
        }
    }
}
