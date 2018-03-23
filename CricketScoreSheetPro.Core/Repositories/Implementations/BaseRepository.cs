using CricketScoreSheetPro.Core.Repositories.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        internal ChildQuery _reference { get; set; }

        public virtual T Create(T obj)
        {
            if (obj == null) throw new ArgumentNullException($"Object to create is null");
            var item = _reference.PostAsync<T>(obj).Result;
            Update(item.Key, "Id", item.Key);
            return GetItem(item.Key);
        }

        public virtual void CreateWithId(string id, T obj)
        {
            if (obj == null) throw new ArgumentNullException($"Object to create is null");
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Given ID is null");
            _reference.Child(id).PutAsync(obj).Wait();
        }

        public virtual void Delete()
        {
            _reference.DeleteAsync().Wait();
        }

        public virtual void DeleteById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Given ID is null");
            _reference.Child(id).DeleteAsync().Wait();
        }

        public virtual T GetItem(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Given ID is null");
            var item = _reference.Child(id).OnceSingleAsync<T>().Result;
            return item;
        }

        public virtual IList<T> GetFilteredList(string fieldname, string value)
        {
            if (string.IsNullOrEmpty(fieldname)) throw new ArgumentException($"FieldName is null");
            if (string.IsNullOrEmpty(value)) throw new ArgumentException($"Value is null");
            var items = _reference.OrderBy(fieldname).EqualTo(value).OnceAsync<T>().Result;
            return ConvertFirebaseObjectCollectionToReadOnlyCollections(items);
        }

        public virtual IList<T> GetList()
        {
            var items = _reference.OrderByKey().OnceAsync<T>().Result;
            return ConvertFirebaseObjectCollectionToReadOnlyCollections(items); 
        }

        public virtual void Update(string id, string fieldName, object val)
        {
            if (val == null) throw new ArgumentNullException($"Object is null");
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(fieldName)) throw new ArgumentException($"Given fieldname or id is invalid");
            _reference.Child(id).Child(fieldName).PutAsync(val).Wait();
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
