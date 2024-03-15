namespace NK.Web.CasePortal.NServiceBus
{
    using NK.Service.Messages;
    using NServiceBus;
    using Endpoint = global::NServiceBus.Endpoint;

    public class BusSender
    {
        public void SendSimulateProductActionMessageMessage(int productId)
        {
            var endpointConfiguration = new EndpointConfiguration("SenderEndpoint");
            endpointConfiguration.UseTransport<LearningTransport>();
            var endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

            endpointInstance.Send("NK.Inventory.ProductService", new SimulateProductActionMessage { ProductId = productId }).GetAwaiter().GetResult();
        }
    }
}
