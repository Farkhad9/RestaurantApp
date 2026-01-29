using RestaurantApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantApp.DAL.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, bool isTracking = false, params string[] includes);
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null);
        IQueryable<T> GetAll(bool isTracking = false, int page = 1, int take = 10, params string[] includes);
        IQueryable<T> GetAll(bool isTracking = false, Expression<Func<T, bool>> filter = null, params string[] includes);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> filter);
        Task SaveChangeAsync();
    }
}
