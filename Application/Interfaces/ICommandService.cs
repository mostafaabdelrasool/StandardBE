using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task.Domain;

namespace Task.Application.Interfaces
{
    public interface ICommandService<TModel, TDto> where TModel:class,IEntity
    {
        Task<TDto> CreateAsync(TDto value, string createdBy);
        Task<TDto> DeleteAsync(Guid id, string deletedBy);
        Task<TDto> RemoveAsync(Guid id, string removedBy);
        TDto Update(TDto value, string updatedBy);

    }
}
