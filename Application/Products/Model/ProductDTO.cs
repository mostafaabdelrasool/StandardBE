using System;
using System.Collections.Generic;
using System.Text;
using Task.Application.Category.Model;
using Task.Application.Supplier.Model;

namespace Task.Application.Products.Model
{
    public class ProductDTO
    {
        public string ProductName { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public CategoryDTO Category { get; set; }
        public SupplierDTO Supplier { get; set; }
    }
}
