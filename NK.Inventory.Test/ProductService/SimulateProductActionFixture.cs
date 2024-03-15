using NK.Data.Repositories.Entities;
using NK.Inventory.ProductService;
using NSubstitute;


namespace NK.Inventory.Test.ProductService
{
    [TestFixture]
    public class SimulateProductActionFixture
    {
        [Test]
        public void Test()
        {
            // Arrange
            var dataHandler = Substitute.For<ISimulateProductActionData>();
            dataHandler.GetProduct(Arg.Any<int>()).Returns(new Product
            {
                ProductId = 1,
                SimulateServiceProcessProperty = false
            });

            // Act
            new SimulateProductActionProcess(dataHandler).Process(1);

            // Assert
            dataHandler.Received(1).UpdateProduct(Arg.Is<Product>(q => q.SimulateServiceProcessProperty == true));
        }
    }
}
