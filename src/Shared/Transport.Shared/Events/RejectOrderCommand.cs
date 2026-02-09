using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Shared.Events
{
    public class RejectOrderCommand
    {
        public Guid OrderId { get; set; }
        public List<string> Reasons { get; set; } = new();
    }
}
