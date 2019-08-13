using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FermView.Models
{
    public class TemperatureData
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        [ForeignKey("Brew")]
        public Guid BrewId { get; set; }
        [Column(TypeName = "Decimal(4,2)")]
        public decimal Temperature { get; set; }
        public DateTime Time { get; set; }
    }
}
