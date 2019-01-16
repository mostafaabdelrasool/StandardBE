using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Interfaces;
using Task.Application.Supplier.Model;
using Task.Domain.Entities;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : GenericController<Suppliers, SupplierDTO>
    {
        public SupplierController(IServiceQuery<Suppliers, SupplierDTO> queryService, 
            ICommandService<Suppliers, SupplierDTO> commandService) : base(queryService, commandService)
        {
        }
    }
}