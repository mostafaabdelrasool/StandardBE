using Task.Domain;
using System;
using System.Collections.Generic;

namespace Task.Domain.Entities
{
    public partial class Territories : Entity
    {
        public Territories()
        {
            EmployeeTerritories = new HashSet<EmployeeTerritories>();
        }

        public string TerritoryDescription { get; set; }
        public Guid RegionId { get; set; }

        public Region Region { get; set; }
        public ICollection<EmployeeTerritories> EmployeeTerritories { get; set; }
    }
}
