using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task.Domain;

namespace Task.Application.Interfaces
{
    public interface IServiceQuery<TModel,TDto>
        where TModel :class ,IEntity
    {

        Task<List<TDto>> ListAsync();
        Task<TDto> GetAsync(Guid id);
    }

}
