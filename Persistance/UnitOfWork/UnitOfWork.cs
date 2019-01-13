using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Task.Persistance.Repositories;
using Task.Domain;
using Persistance.Models;

namespace MK.UnitOfWork
{
    /// <summary>  
    /// Unit of Work class responsible for DB transactions  
    /// </summary>  
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Private member variables...

        public TaskContext _context = null;

        #endregion

        public UnitOfWork()
        {
            this._context = new TaskContext();
        }

        #region Public member methods...
        /// <summary>  
        /// Save method.  
        /// </summary>  
        public int Save()
        {
            try

            {

              return  _context.SaveChanges();

            }

            catch (DbUpdateConcurrencyException ex)

            {

                ex.Entries.ToList().ForEach(x=>x.Reload());
                return _context.SaveChanges();

            }
        }
        public async Task<int> SaveAsync()
        {
            try

            {

                return await _context.SaveChangesAsync();

            }

            catch (DbUpdateConcurrencyException ex)

            {

                ex.Entries.ToList().ForEach(x => x.Reload());
                return await _context.SaveChangesAsync();

            }
        }
      
        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>  
        /// Protected Virtual Dispose method  
        /// </summary>  
        /// <param name="disposing"></param>  
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>  
        /// Dispose method  
        /// </summary>  
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public Repository<T> Init<T>() where T : class, IEntity,new ()
        {
            Repository<T> gr = new Repository<T>(_context);
            return gr;
        }
    }

}