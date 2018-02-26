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

        public async Task<FirebaseObject<T>> CreateAsync(T obj)
        {
            if (obj == null) throw new ArgumentNullException($"{typeof(T).Name} is null");
            var item = await _reference.PostAsync<T>(obj);
            await UpdateAsync(item.Key, "Id", item.Key);
            return item;
        }

        public async Task CreateWithIdAsync(string id, T obj)
        {
            if (obj == null) throw new ArgumentNullException($"{typeof(T).Name} is null");
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException($"Given ID is null");
            await _reference.Child(id).PutAsync(obj);
        }

        public async Task DeleteAsync()
        {
            await _reference.DeleteAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException($"Given ID is null");
            await _reference.Child(id).DeleteAsync();
        }

        public async Task<T> GetItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException($"Given ID is null");
            var item = await _reference.Child(id).OnceSingleAsync<T>();
            return item;
        }

        public async Task<IReadOnlyCollection<FirebaseObject<T>>> GetFilteredListAsync(string orderby, string value)
        {
            return await _reference.OrderBy(orderby).EqualTo(value).OnceAsync<T>();
        }

        public async Task<IReadOnlyCollection<FirebaseObject<T>>> GetListAsync()
        {
            var items = await _reference.OnceAsync<T>();
            return items;
        }

        public async Task UpdateAsync(string id, string fieldName, object val)
        {
            if (val == null || string.IsNullOrEmpty(id) || string.IsNullOrEmpty(fieldName))
                throw new ArgumentNullException($"Given ID is null");
            await _reference.Child(id).Child(fieldName).PutAsync(val);
        }
    }
}
