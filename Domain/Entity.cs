using System;

namespace Task.Domain
{
    public class Entity : IEntity
    {
        public Entity()
        {
            this.Id = new Guid();
        }

        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}
