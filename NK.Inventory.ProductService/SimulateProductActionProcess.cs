using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NK.Inventory.ProductService
{
    public class SimulateProductActionProcess
    {
        private readonly ISimulateProductActionData _simulateProductActionData;

        public SimulateProductActionProcess() : this(new SimulateProductActionData()) { }
        public SimulateProductActionProcess(ISimulateProductActionData simulateProductActionData)
        {
            _simulateProductActionData = simulateProductActionData;
        }

        public void Process(int productId)
        {
            var productd = _simulateProductActionData.GetProduct(productId);

            productd.SimulateServiceProcessProperty = true;

            _simulateProductActionData.UpdateProduct(productd);

        }
    }
}
