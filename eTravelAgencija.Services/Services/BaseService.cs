using eTravelAgencija.Services.Database;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace eTravelAgencija.Services.Services
{
    public abstract class BaseService<TResponse, TSearch, TEntity> 
        where TResponse : class 
        where TSearch : BaseSearchObject 
        where TEntity : class
    {
        protected readonly eTravelAgencijaDbContext _context;
        protected readonly IMapper _mapper;

        protected BaseService(eTravelAgencijaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<PagedResult<TResponse>> GetAsync(TSearch search)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            query = ApplyFilter(query, search);

            query = AddInclude(query, search);

            int? totalCount = null;
            if (search.IncludeTotalCount)
            {
                totalCount = await query.CountAsync();
            }

            if (!search.RetrieveAll)
            {
                if (search.Page.HasValue)
                    query = query.Skip(search.Page.Value * (search.PageSize ?? 10));

                if (search.PageSize.HasValue)
                    query = query.Take(search.PageSize.Value);
            }

            var list = await query.ToListAsync();

            var processedEntities = await AfterGetAsync(list, search);

            return new PagedResult<TResponse>
            {
                Items = list.Select(MapToResponse).ToList(),
                TotalCount = totalCount ?? 0
            };
        }

        public virtual async Task<TResponse?> GetByIdAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
                return null;

            return MapToResponse(entity);
        }
        public virtual async Task<TResponse?> GetByCompositeKeysAsync(params object[] keyValues)
        {
            var entity = await _context.Set<TEntity>().FindAsync(keyValues);
            if (entity == null)
                return null;

            return _mapper.Map<TResponse>(entity);
        }

        public virtual IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TSearch search)
        {
            return query;
        }

        public virtual IQueryable<TEntity> AddInclude(IQueryable<TEntity> query, TSearch? search = null)
        {
            return query;
        }

        public virtual async Task<TEntity> AddIncludeId(IQueryable<TEntity> query, int id)
        {
            return (TEntity)query;
        }

        public virtual Task<IEnumerable<TEntity>> AfterGetAsync(IEnumerable<TEntity> entities, TSearch? search = null)
        {
            return Task.FromResult(entities);
        }

        protected virtual TResponse MapToResponse(TEntity entity)
        {
            return _mapper.Map<TResponse>(entity);
        }
    }
}
