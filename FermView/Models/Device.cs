using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FermView.Data.Migrations;

namespace FermView.Models
{
    public class Device
    {
        public Guid ID { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public string SetupCode { get; set; }

        public static string CreateSetupCode()
        {
            var builder = new StringBuilder();
            var random = new Random();
            char ch;
            for (int i = 0; i < 5; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            
            return builder.ToString();
        }
    }
}
