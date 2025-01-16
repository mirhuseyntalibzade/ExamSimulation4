using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Absractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task AddAsync(T entity);
        public Task<ICollection<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int Id);
        public Task<T> GetByConditionAsync(Expression<Func<T,bool>> condition);
        public void Update(T entity);
        public void Delete(T entity);
        public Task<int> SaveChangesAsync();
    }
}
