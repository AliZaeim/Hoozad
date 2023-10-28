﻿using Core.Services.Interfaces;
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private readonly MyContext _context;
        public GenericService(MyContext context)
        {
            _context = context;
        }
        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public void CreateRange(List<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void Delete(int id)
        {
            TEntity? entityToDelete = _context.Set<TEntity>().Find(id);
            if (entityToDelete != null)
            {
                _context.Set<TEntity>().Remove(entityToDelete);
            }
        }
        public void Edit(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        public IEnumerable<TEntity> Filter()
        {
            return _context.Set<TEntity>();
        }
        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            foreach (Microsoft.EntityFrameworkCore.Metadata.INavigation property in _context.Model.FindEntityType(typeof(TEntity))!.GetNavigations())
            {
                query = query.Include(property.Name);
            }

            return query.ToList();
        }

        public Task<IEnumerable<TEntity>> FilterAsync()
        {
            return FilterAsync(_context);
        }

        public async Task<IEnumerable<TEntity>> FilterAsync(MyContext _context)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
            foreach (Microsoft.EntityFrameworkCore.Metadata.INavigation property in _context.Model.FindEntityType(typeof(TEntity))!.GetNavigations())
            {
                query = query.Include(property.Name);
            }

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            foreach (Microsoft.EntityFrameworkCore.Metadata.INavigation property in _context.Model.FindEntityType(typeof(TEntity))!.GetNavigations())
            {
                query = query.Include(property.Name);
            }

            return await query.ToListAsync();
        }
        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
            foreach (Microsoft.EntityFrameworkCore.Metadata.INavigation property in _context.Model.FindEntityType(typeof(TEntity))!.GetNavigations())
            {
                query = query.Include(property.Name);
            }

            return query;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
            foreach (Microsoft.EntityFrameworkCore.Metadata.INavigation property in _context.Model.FindEntityType(typeof(TEntity))!.GetNavigations())
            {
                query = query.Include(property.Name);
            }

            return await query.ToListAsync();
        }
        public TEntity? GetById(int id)
        {
            TEntity? entity = _context.Set<TEntity>().Find(id);
            return entity;
        }
        public bool ExistEntity(int id)
        {
            TEntity? entity = _context.Set<TEntity>().Find(id);
            return entity != null;
        }
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            TEntity? query = await _context.Set<TEntity>().FindAsync(id);
            return query;
        }
        public bool DetachEntity(TEntity entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Entry(entity).State = EntityState.Detached;
            return true;
        }
       

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity?> GetByGIdAsync(Guid id)
        {
            TEntity? query = await _context.Set<TEntity>().FindAsync(id);
            return query;
        }
    }
}
