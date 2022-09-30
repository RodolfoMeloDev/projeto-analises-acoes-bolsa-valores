using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Data.Context;
using App.Data.Repository.Exceptions;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly StockAnalysisContext _context;
        private DbSet<T> _dataSet;

        public BaseRepository(StockAnalysisContext context)
        {
            _context = context;
            _dataSet = context.Set<T>();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _dataSet.AnyAsync(obj => obj.Id.Equals(id));
        }

        public async Task<T> SelectAsync(int id)
        {
            try
            {
                var result = await _dataSet.FirstOrDefaultAsync(obj => obj.Id.Equals(id));

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAllAsync()
        {
            try
            {
                return await _dataSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                item.DateCreated = DateTime.UtcNow;
                item.Active = true;

                _dataSet.Add(item);
                await _context.SaveChangesAsync();

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await SelectAsync(item.Id);

                if (result == null)
                    throw new IntegrityException("A chave de identificação do objeto não foi encontrada, não foi possível atualizar as informações.");

                item.DateCreated = result.DateCreated;
                item.DateUpdated = DateTime.UtcNow;

                _context.Entry(result).CurrentValues.SetValues(item);

                await _context.SaveChangesAsync();

                return item;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var result = await SelectAsync(id);

                if (result == null)
                    return false;

                _dataSet.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
