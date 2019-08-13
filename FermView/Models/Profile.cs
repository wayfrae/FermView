using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FermView.Models
{
    public class Profile
    {
        private readonly ILazyLoader _lazyLoader;
        private List<TempPeriod> _details;

        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<TempPeriod> Details
        {
            get => _lazyLoader.Load(this, ref _details);
            set => _details = value;
        }
        public int Hearts { get; set; }
        public Guid Creator { get; set; }

        public Profile(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public Profile() { }
    }
}
