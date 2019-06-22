using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermView.Models
{
    public class TemperatureData
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string BrewName { get; set; }
        public decimal Temperature { get; set; }
        public DateTime Time { get; set; }
    }
}
