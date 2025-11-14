using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.Services.Services
{
    public abstract class BaseCRUDService<TResponse, TSearch, TEntity, TInsert, TUpdate>
        : BaseService<TResponse, TSearch, TEntity>, ICRUDService<TResponse, TSearch, TInsert, TUpdate>
        where TResponse : class
        where TEntity : class, new()
        where TSearch : BaseSearchObject
        where TInsert : class
        where TUpdate : class
    {
        protected readonly eTravelAgencijaDbContext _context;

        protected BaseCRUDService(eTravelAgencijaDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _context = context;
        }

        // 🟢 Create
        public virtual async Task<TResponse> CreateAsync(TInsert request)
        {
            var entity = new TEntity();
            _mapper.Map(request, entity);

            await BeforeInsertAsync(entity, request);
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TResponse>(entity);
        }

        public virtual async Task<TResponse?> GetByCompositeKeysAsync(params object[] keyValues)
        {
            var entity = await _context.Set<TEntity>().FindAsync(keyValues);
            if (entity == null)
                return null;

            return _mapper.Map<TResponse>(entity);
        }

        // 🟡 Update
        public virtual async Task<TResponse?> UpdateAsync(int id, TUpdate request)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
                return null;

            await BeforeUpdateAsync(entity, request);
            _mapper.Map(request, entity);

            await _context.SaveChangesAsync();
            return _mapper.Map<TResponse>(entity);
        }

        public virtual async Task<TResponse?> UpdateCompositeAsync(object[] keyValues, TUpdate request)
        {
            var entity = await _context.Set<TEntity>().FindAsync(keyValues);
            if (entity == null)
                return null;

            await BeforeUpdateAsync(entity, request);
            _mapper.Map(request, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TResponse>(entity);
        }

        // 🔴 Delete (Single-key)
        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
                return false;

            await BeforeDeleteAsync(entity);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // 🔵 Delete (Composite-key) → koristi kad imaš 2 ključa
        public virtual async Task<bool> DeleteCompositeAsync(params object[] keyValues)
        {
            var entity = await _context.Set<TEntity>().FindAsync(keyValues);
            if (entity == null)
                return false;

            await BeforeDeleteAsync(entity);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ⚙️ Hooks
        public virtual Task BeforeInsertAsync(TEntity entity, TInsert request) => Task.CompletedTask;
        public virtual Task BeforeUpdateAsync(TEntity entity, TUpdate request) => Task.CompletedTask;
        public virtual Task BeforeDeleteAsync(TEntity entity) => Task.CompletedTask;

        
    }
}
