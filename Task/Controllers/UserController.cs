using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Interfaces;
using Task.Application.User.Model;
using Task.Domain;
using Task.Web.Controllers;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : GenericController<User, UserDto>
    {
        public UserController(IServiceQuery<UserDto, User> queryService,
            ICommandService<User, UserDto> commandService) : base(queryService, commandService)
        {
        }

    }
}