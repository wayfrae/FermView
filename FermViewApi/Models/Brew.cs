using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FermViewApi.Models
{
    public class Brew
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string BrewName { get; set; }
        public Profile Profile { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Date
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
