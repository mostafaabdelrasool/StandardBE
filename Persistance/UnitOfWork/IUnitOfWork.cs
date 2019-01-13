using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task.Domain;
using Task.Persistance.Repositories;

namespace MK.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save method.
        /// </summary>
        int Save();
        Repository<T> Init<T>() where T : class, IEntity, new();
        void Dispose();
        Task<int> SaveAsync();

    }
}