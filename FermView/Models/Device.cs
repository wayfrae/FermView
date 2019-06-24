using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermView.Models
{
    public class Device
    {
        public Guid ID { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public string SetupCode { get; set; }
    }
}
