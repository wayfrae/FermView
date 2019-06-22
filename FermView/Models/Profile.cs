using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermView.Models
{
    public class Profile
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TempPeriod> Details { get; set; }
    }
}
