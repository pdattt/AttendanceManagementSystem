using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Interfaces.IRepos
{
    public interface IRepo<TEntity> where TEntity : class
    {
        void SaveChanges();

        List<TEntity> GetAll();

        TEntity GetById(int id);

        void Add(TEntity newEntity);

        void Update(TEntity newEntity);

        bool Delete(int id);

        IQueryable<TEntity> Query();
    }
}