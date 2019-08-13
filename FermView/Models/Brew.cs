using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FermView.Models
{
    public class Brew
    {
        private readonly ILazyLoader _lazyLoader;
        private Profile _profile;

        public Guid ID { get; set; }
        public string Username { get; set; }
        public string BrewName { get; set; }
        public Profile Profile
        {
            get => _lazyLoader.Load(this, ref _profile);
            set => _profile = value;
        }
        [ForeignKey("Device")]
        public Guid DeviceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Date => DateTime.Now;

        public Brew(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public Brew()
        {
        }
    }
}
