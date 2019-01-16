using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Application.Category.Model
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
    }
}
