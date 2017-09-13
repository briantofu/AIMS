using AIMS.Models.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AIMS.Models.Repositories
{
    public class Repository<T> where T : class
    {
        private InventoryDbContext context = null;
        protected DbSet<T> DbSet
        {
            get; set;
        }

        public Repository()
        {
           context = new InventoryDbContext();
            DbSet = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T GetID(int id)
        {
            return DbSet.Find(id);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }
}