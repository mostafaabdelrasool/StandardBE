using MKMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task.Application.Interfaces;
using Task.Domain;
using Task.Persistance.Interfaces;

namespace Task.Application.Service
{
    public class CommandService<TModel, TDto> : ICommandService<TModel, TDto> 
        where TModel : class, IEntity, new()
        where TDto : class, new()
    {
        protected IRepository<TModel> repository;
        public CommandService(IRepository<TModel> repository)
        {
            this.repository = repository;
        }
        public async Task<TDto> CreateAsync(TDto value, string createdBy)
        {
            try
            {
                TModel model = Map(value);
                var result = await repository.CreateAsync(model, createdBy);
                await repository.SaveAsync();
                return MapDto(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static TModel Map(TDto value)
        {
            MapProperties mapProperties = new MapProperties();
            var model = mapProperties.Map<TDto, TModel>(value, new TModel());
            return model;
        }
        private static TDto MapDto(TModel value)
        {
            MapProperties mapProperties = new MapProperties();
            var model = mapProperties.Map<TModel,TDto>(value, new TDto());
            return model;
        }
        public async Task<TDto> Update(TDto value, string updatedBy)
        {
            try
            {
                TModel model = Map(value);
                var result =  repository.Update(model, updatedBy);
                await repository.SaveAsync();
                return MapDto(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<TDto> DeleteAsync(Guid id, String deletedBy)
        {
            try
            {
                var result= await repository.DeleteAsync(id, deletedBy);
                await repository.SaveAsync();
                return MapDto(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TDto> RemoveAsync(Guid id, string removedBy)
        {
            try
            {
                var result = await repository.RemoveAsync(id, removedBy);
                await repository.SaveAsync();
                return MapDto(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
