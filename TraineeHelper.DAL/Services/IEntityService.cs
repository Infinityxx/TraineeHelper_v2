using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;

namespace TraineeHelper.DAL.Services
{
    public interface IEntityService<T> where T: IMongoEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetOneAsync(T context);
        Task<T> GetOneAsync(string id);
        Task<T> GetManyAsync(IEnumerable<T> contexts);
        Task<T> GetManyAsync(IEnumerable<string> ids);
        //Task<T> SaveOneAsync(T Context);
        Task<T> SaveManyAsync(IEnumerable<T> contexts);
        Task<bool> RemoveOneAsync(T context);
        //Task<bool> RemoveOneAsync(string id);
        Task<bool> RemoveManyAsync(IEnumerable<T> contexts);
        Task<bool> RemoveManyAsync(IEnumerable<string> ids);

        Task<T> SaveOneAsync(T entity);

        Task<bool> Delete(string id);

        Task<T> GetById(string id);

        //Task<bool> Update(T entity);

    }
}
