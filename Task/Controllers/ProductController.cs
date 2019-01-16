using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Interfaces;
using Task.Application.Products.Model;
using Task.Domain;
using Task.Domain.Entities;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : GenericController<Products, ProductDTO>
    {
        public ProductController(IServiceQuery<Products,
            ProductDTO> queryService, ICommandService<Products, ProductDTO> commandService) 
            : base(queryService, commandService)
        {
        }
    }
}