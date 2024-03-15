using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NK.Service.Messages
{
    public class SimulateProductActionMessage : ICommand
    {
        public int ProductId { get; set; }
    }
}
