using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagement.DatabaseConnection
{
    public interface IRepository<T> where T : class
    {

        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);

        Task UpdateAsync(string id, T entity);

        Task DeleteAsync(string id);
    }
}
