using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public abstract class BaseImageService<TEntity, TResponse> : IBaseImageService<TResponse>
        where TEntity : class, new()
        where TResponse : BaseImageResponse, new()
    {
        protected readonly eTravelAgencijaDbContext _context;
        private readonly Expression<Func<TEntity, int>> _referenceSelector;
        private readonly Expression<Func<TEntity, string>> _imageUrlSelector;

        protected BaseImageService(
            eTravelAgencijaDbContext context,
            Expression<Func<TEntity, int>> referenceSelector,
            Expression<Func<TEntity, string>> imageUrlSelector)
        {
            _context = context;
            _referenceSelector = referenceSelector;
            _imageUrlSelector = imageUrlSelector;
        }

        public virtual async Task<List<TResponse>> GetImagesAsync(int referenceId, bool isMain = false)
        {
            var entityType = typeof(TEntity);
            IQueryable<TEntity> query = _context.Set<TEntity>();

            // 📌 Dinamički pronađi property koji pokazuje na referenceId (OfferId ili HotelId)
            string referenceProperty = GetPropertyName(_referenceSelector);

            query = query.Where(x => EF.Property<int>(x, referenceProperty) == referenceId);

            // 📌 Ako se traže samo glavne slike (IsMain == true)
            if (isMain && entityType.GetProperty("IsMain") != null)
            {
                query = query.Where(x => EF.Property<bool>(x, "IsMain") == true);
            }

            // 📸 Izvuci slike u response model
            var result = await query
                .Select(x => new TResponse
                {
                    Id = EF.Property<int>(x, "Id"),
                    referenceId = referenceId,
                    ImageUrl = EF.Property<string>(x, GetPropertyName(_imageUrlSelector))
                })
                .ToListAsync();

            return result;
        }


        public virtual async Task<int> AddImageAsync(int referenceId, string imageUrl)
        {
            var entity = new TEntity();

            // Postavi reference ID
            typeof(TEntity).GetProperty(GetPropertyName(_referenceSelector))
                ?.SetValue(entity, referenceId);

            typeof(TEntity).GetProperty(GetPropertyName(_imageUrlSelector))
                ?.SetValue(entity, imageUrl);

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            var idProp = typeof(TEntity).GetProperty("Id");
            return idProp != null ? (int)idProp.GetValue(entity) : 0;
        }

        public virtual async Task<bool> DeleteImageByIdAsync(int imageId)
        {
            var entity = await _context.Set<TEntity>().FindAsync(imageId);
            if (entity == null)
                return false;

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private string GetPropertyName<TValue>(Expression<Func<TEntity, TValue>> expression)
        {
            if (expression.Body is MemberExpression memberExpr)
                return memberExpr.Member.Name;

            if (expression.Body is UnaryExpression unary && unary.Operand is MemberExpression member)
                return member.Member.Name;

            throw new InvalidOperationException("Cannot get property name from expression.");
        }
    }
}
