using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Domain.Entities
{
    public class BlockUnblockReason
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public IEnumerable<DemandPacket> DemandPackets { get; set; }
    }
}
