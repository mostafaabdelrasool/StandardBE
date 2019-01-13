using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Interfaces;
using Task.Application.Users.Model;
using Task.Domain;
using Task.Web.Controllers;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : GenericController<User, UserDto>
    {
        public UserController(IServiceQuery<User, UserDto> queryService,
            ICommandService<User, UserDto> commandService) :
            base(queryService, commandService)
        {
        }
       
    }
}