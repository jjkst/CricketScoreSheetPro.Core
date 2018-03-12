using Firebase.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T obj);
        Task CreateWithIdAsync(string id, T obj);

        Task UpdateAsync(string uid, string fieldName, object val);

        Task<IList<T>> GetListAsync();
        Task<IList<T>> GetFilteredListAsync(string fieldname, string value);
        Task<T> GetItemAsync(string uid);

        Task DeleteByIdAsync(string uid);
        Task DeleteAsync();
    }
}
