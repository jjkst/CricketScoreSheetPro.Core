using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services
{
    public interface IService<T> 
    {
        Task<T> Create(T obj);        
        Task<T> Update(string uid, string fieldName, object val);
        Task<List<T>> GetList();        
        Task<T> GetItem(string uid);        
        Task<bool> Delete(string uid);
        Task<bool> DropTable();

        Task<T> CreateWithId(string id, T obj);
    }

}
