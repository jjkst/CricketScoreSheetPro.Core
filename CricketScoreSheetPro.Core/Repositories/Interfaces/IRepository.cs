using Firebase.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        T Create(T obj);
        void CreateWithId(string id, T obj);

        void Update(string uid, string fieldName, object val);

        IList<T> GetList();
        IList<T> GetFilteredList(string fieldname, string value);
        T GetItem(string uid);

        void DeleteById(string uid);
        void Delete();
    }
}
