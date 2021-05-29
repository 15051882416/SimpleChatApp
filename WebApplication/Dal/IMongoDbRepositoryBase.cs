using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Dal
{
    public interface IMongoDbRepositoryBase<T> where T : class
    {
        Task Create(T obj);
        Task<List<T>> Get();
    }
}