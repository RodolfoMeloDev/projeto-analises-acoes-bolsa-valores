using System;
using System.Collections.Generic;
using System.Linq;
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
        protected readonly AnaliseDeAcoesContext _context;
        private DbSet<T> _dataSet;

        public BaseRepository(AnaliseDeAcoesContext context){
            _context = context;
            _dataSet = context.Set<T>();
        }       

        public async Task<bool> ExistAsync(int id)
        {
            return await _dataSet.AnyAsync(obj => obj.Id.Equals(id));
        }

        private int RetunrNextId(){
            try{
                var lastId = _dataSet.Max(x => x.Id);

                return lastId + 1;

            } catch (Exception ex){
                throw ex;
            }
        }

        public async Task<T> SelectAsync(int id)
        {
            try{
                var result = await _dataSet.SingleOrDefaultAsync(obj => obj.Id.Equals(id));

                if (result == null)
                    throw new IntegrityException("ID não encontrado");

                return result;
            }catch (Exception ex){
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAllAsync()
        {
            try{
                return await _dataSet.ToListAsync();
            }catch (Exception ex){
                throw ex;
            }
        }        

        public async Task<T> InsertAsync(T item)
        {
            try{
                if (item.Id == 0)
                    item.Id = RetunrNextId();

                item.DataCadastro = DateTime.UtcNow;
                item.Ativo = true;

                _dataSet.Add(item);
                await _context.SaveChangesAsync();

                return item;
            }catch (Exception ex){
                throw ex;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try{
                var result = await SelectAsync(item.Id);

                if (result == null)
                    throw new IntegrityException("ID não encontrado");

                item.DataCadastro = result.DataCadastro;
                item.DataAlteracao = DateTime.UtcNow;

                _context.Entry(result).CurrentValues.SetValues(item);

                await _context.SaveChangesAsync();

                return item;

            }catch (Exception ex){
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try{
                var result = await SelectAsync(id);

                if (result == null)
                    return false;

                _dataSet.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }catch (Exception ex){
                throw ex;
            }
        }
    }
}