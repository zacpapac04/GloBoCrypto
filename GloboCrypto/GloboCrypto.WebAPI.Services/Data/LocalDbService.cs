using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GloboCrypto.WebAPI.Services.Data
{
    public class LocalDbService:ILocalDbService
    {
        private readonly LiteDatabase Local;
        
        private string GetFilePath(string fileName)
        {
            return Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                            fileName
                        );
        }
        public LocalDbService(string filename)
        {
            Local = new LiteDatabase($"filename={GetFilePath(filename)};mode=Exclusive");
        }
        ~LocalDbService()
        {
            Local.Dispose();
        }

        public IEnumerable<T> All<T>()
        {
            var collection = Local.GetCollection<T>();

            return collection.FindAll().Skip(0).Take(100);
        }

        public void Delete<T>(Expression<Func<T, bool>> query)
        {
            var collection = Local.GetCollection<T>();
            collection.DeleteMany(query);
        }

        public void Insert<T>(T item)
        {
            var collection = Local.GetCollection<T>();
            collection.Insert(item);
        }

        public IEnumerable<T> Query<T>(Expression<Func<T, bool>> query)
        {
            var collection = Local.GetCollection<T>();
            return collection.Find(query);
        }

        public void Upsert<T>(T item)
        {
            var collection = Local.GetCollection<T>();
            collection.Upsert(item);
        }
    }
}
