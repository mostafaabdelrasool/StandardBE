using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Interfaces;
using Task.Domain;

namespace Task.Web.Controllers
{
    public class GenericController<TModel,TDto> : ControllerBase
           where TModel : class, IEntity, new()
             where TDto : class, new()
    {
        private IServiceQuery<TModel, TDto> queryService;
        private ICommandService<TModel, TDto> commandService;

        public GenericController(IServiceQuery<TModel,TDto> queryService, 
            ICommandService<TModel, TDto> commandService)
        {
            this.queryService = queryService;
            this.commandService = commandService;
        }
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var result = await queryService.ListAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public virtual async Task<IActionResult> Get(Guid id)
        {
            var result = await queryService.GetAsync(id);
            return Ok();
        }
        [HttpPut]
        public virtual  IActionResult Put(TDto entity)
        {
            commandService.Update(entity, User.Identity.Name);
            return Ok();
        }
        [HttpPost]
        public virtual async Task<IActionResult> Post(TDto entity)
        {
            await commandService.CreateAsync(entity, User.Identity.Name);
            return Ok();
        }
        [HttpDelete]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            await commandService.DeleteAsync(id, User.Identity.Name);
            return Ok();
        }
    }
}