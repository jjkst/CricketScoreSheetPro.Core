using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services
{
    public class BaseService<T> where T : class 
    {
        
        public static FirebaseClient Client { get; set; } = new FirebaseClient("https://xamarinfirebase-4a90e.firebaseio.com/");

        internal ChildQuery _reference { get; set; }

        public virtual async Task<T> Create(T obj) 
        {
            if (obj == null) return null;            
            var item = await _reference.PostAsync<T>(obj);                  
            return await Update(item.Key, "Id", item.Key);
        }

        public virtual async Task<T> CreateWithId(string id, T obj)
        {
            if (obj == null || string.IsNullOrEmpty(id)) return null;
            await _reference.Child(id).PutAsync(obj);
            return obj;
        }

        public virtual async Task<T> Update(string uid, string fieldName, object val)
        {
            if (val == null || string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(fieldName))
                return null;
            await _reference.Child(uid).Child(fieldName).PutAsync(val);
            return await _reference.Child(uid).OnceSingleAsync<T>();
        }

        public virtual async Task<List<T>> GetList()
        {
            var items = await _reference.OnceAsync<T>();
            var val = new List<T>();
            foreach (var item in items)
                val.Add(item.Object);
            return val;
        }

        public virtual async Task<T> GetItem(string uid)
        {
            if (string.IsNullOrEmpty(uid)) return null;
            var item =  await _reference.Child(uid)
                .OnceSingleAsync<T>();
            return item;
        }

        public virtual async Task<bool> Delete(string uid)
        {
            if (string.IsNullOrEmpty(uid)) return false;
            try
            {
                await _reference.Child(uid).DeleteAsync();
                return true;
            }
            catch(Exception e)
            {
                var error = e.Message;
                return false;
            }
        }

        public virtual async Task<bool> DropTable()
        {
            try
            {
                var items = await _reference.OnceAsync<T>();
                foreach (var item in items)
                    await _reference.Child(item.Key).DeleteAsync();
                return true;
            }
            catch(Exception e)
            {
                var error = e.Message;
                return false;
            }
        }
    }
}
