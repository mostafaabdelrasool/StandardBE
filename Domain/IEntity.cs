using System;

namespace Task.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }       
        bool IsActive { get; set; }
    }
}
