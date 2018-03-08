using CricketScoreSheetPro.Core.Repositories.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        internal ChildQuery _reference { get; set; }

        public async virtual Task<T> CreateAsync(T obj)
        {
            if (obj == null) throw new ArgumentNullException($"Object is null");
            var item = await _reference.PostAsync<T>(obj);
            await UpdateAsync(item.Key, "Id", item.Key);
            return item.Object;
        }

        public async virtual Task CreateWithIdAsync(string id, T obj)
        {
            if (obj == null) throw new ArgumentNullException($"Object is null");
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Given ID is null");
            await _reference.Child(id).PutAsync(obj);
        }

        public async virtual Task DeleteAsync()
        {
            await _reference.DeleteAsync();
        }

        public async virtual Task DeleteByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Given ID is null");
            await _reference.Child(id).DeleteAsync();
        }

        public async virtual Task<T> GetItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Given ID is null");
            var item = await _reference.Child(id).OnceSingleAsync<T>();
            return item;
        }

        public async virtual Task<IList<T>> GetFilteredListAsync(string fieldname, string value)
        {
            var items = await _reference.OrderBy(fieldname).EqualTo(value).OnceAsync<T>();
            return ConvertFirebaseObjectCollectionToReadOnlyCollections(items);
        }

        public async virtual Task<IList<T>> GetListAsync()
        {
            var items = await _reference.OrderByKey().OnceAsync<T>();
            return ConvertFirebaseObjectCollectionToReadOnlyCollections(items); 
        }

        public async virtual Task UpdateAsync(string id, string fieldName, object val)
        {
            if (val == null) throw new ArgumentNullException($"Object is null");
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(fieldName)) throw new ArgumentException($"Given ID is null");
            await _reference.Child(id).Child(fieldName).PutAsync(val);
        }

        private static IList<T> ConvertFirebaseObjectCollectionToReadOnlyCollections(IReadOnlyCollection<FirebaseObject<T>> firebaseObjectCollection) 
        {
            var val = new List<T>();
            foreach (var item in firebaseObjectCollection)
            {
                val.Add(item.Object);
            }
            val.Reverse();
            return val;
        }
    }
}
