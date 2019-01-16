using Task.Domain;
using System;
using System.Collections.Generic;

namespace Task.Domain.Entities
{
    public partial class Region : Entity
    {
        public Region()
        {
            Territories = new HashSet<Territories>();
        }

        public string RegionDescription { get; set; }

        public ICollection<Territories> Territories { get; set; }
    }
}
