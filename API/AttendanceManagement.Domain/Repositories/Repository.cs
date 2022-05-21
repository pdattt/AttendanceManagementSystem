using AttendanceManagement.Domain.Interfaces.IRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Repositories
{
    public class Repository<TEntity> : IRepo<TEntity> where TEntity : class
    {
        private readonly AttendanceManagementDBContext _context;
        private readonly DbSet<TEntity> _set;

        public Repository(AttendanceManagementDBContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual List<TEntity> GetAll()
        {
            //return _context.Set<TEntity>().ToListAsync().Result;
            return _set.ToListAsync().Result;
        }

        public virtual TEntity GetById(int id)
        {
            return _set.FindAsync(id).Result;
        }

        public virtual void Add(TEntity newEntity)
        {
            _context.Add(newEntity);
        }

        public virtual void Delete(int id)
        {
            _context.Remove(id);
        }

        public virtual void Update(TEntity newEntity)
        {
            // Update
        }
        public IQueryable Query()
        {
            return _set;
        }
    }
}
