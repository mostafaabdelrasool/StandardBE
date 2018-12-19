using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task.Application.Interfaces
{
    public interface IServiceQuery<TModel,TDto>
    {

        Task<List<TDto>> ListAsync();
        Task<TDto> GetAsync(Guid id);
    }

}
