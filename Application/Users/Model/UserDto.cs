using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Application.Users.Model
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
    }
}
