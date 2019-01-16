using MKMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Application.Interfaces;
using Task.Domain;
using Task.Persistance.Interfaces;

namespace Task.Application
{
    public class QueryService<TModel, TDto> : IServiceQuery<TModel, TDto> 
        where TModel : class, IEntity, new()
        where TDto : class, new()
    {
        protected IRepository<TModel> repository;
        public QueryService(IRepository<TModel> repository)
        {
            this.repository = repository;
        }
        public async Task<TDto> GetAsync(Guid id)
        {
            var result= await repository.ReadOneAsync(id);
            return MapDto(result);
        }
        public async Task<List<TDto>> ListAsync()
        {
            var result = await repository.ReadAllAsync();
            MapProperties mapProperties = new MapProperties();
            var model = mapProperties.Map<List<TModel>, List<TDto>>
                (result.ToList(), new List<TDto>());
            return model;
        }
        private static TDto MapDto(TModel value)
        {
            MapProperties mapProperties = new MapProperties();
            var model = mapProperties.Map<TModel, TDto>(value, new TDto());
            return model;
        }
    }
}
