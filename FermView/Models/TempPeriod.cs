using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FermView.Models
{
    public class TempPeriod
    {
        public Guid ID { get; set; }
        [Column(TypeName = "Decimal(4,2)")]
        public decimal Temperature { get; set; }
        public decimal Duration { get; set; }
    }
}
