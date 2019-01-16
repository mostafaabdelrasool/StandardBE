using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Category.Model;
using Task.Application.Interfaces;
using Task.Domain.Entities;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : GenericController<Categories, CategoryDTO>
    {
        public CategoryController(IServiceQuery<Categories, CategoryDTO> queryService, 
            ICommandService<Categories, CategoryDTO> commandService) : base(queryService, commandService)
        {
        }
    }
}