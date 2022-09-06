using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities;

namespace App.Domain.Interfaces.Services
{
    public interface IRepository<T> where T: BaseEntity
    {
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
        Task<T> SelectAsync(int id);
        Task<IEnumerable<T>> SelectAllAsync();
        Task<bool> ExistAsync(int id);
    }
}