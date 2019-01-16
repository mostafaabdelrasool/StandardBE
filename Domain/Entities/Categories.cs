using System;
using System.Collections.Generic;
using Task.Domain;

namespace Task.Domain.Entities
{
    public partial class Categories : Entity
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
