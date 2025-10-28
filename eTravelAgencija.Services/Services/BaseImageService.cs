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

        public virtual async Task<TResponse> GetMainImageAsync(int referenceId)
        {
            // Dinamički filter po referenceId
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var left = Expression.Invoke(_referenceSelector, parameter);
            var right = Expression.Constant(referenceId);
            var equals = Expression.Equal(left, right);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            // Select imageUrl
            var imageUrl = await _context.Set<TEntity>()
                .Where(lambda)
                .Select(x => new
                {
                    Id = EF.Property<int>(x, "Id"), // reflektivno dohvaća "Id" polje
                    ImageUrl = _imageUrlSelector.Compile()(x)
                })
        .FirstOrDefaultAsync();

            if (imageUrl == null)
                return null;

            return new TResponse
            {
                Id = imageUrl.Id,
                referenceId = referenceId,
                ImageUrl = imageUrl.ImageUrl
            };
        }

        public virtual async Task<List<TResponse>> GetAllImagesAsync(int referenceId)
        {
            // Dinamički filter
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var left = Expression.Invoke(_referenceSelector, parameter);
            var right = Expression.Constant(referenceId);
            var equals = Expression.Equal(left, right);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            // Dohvati sve slike
            var list = await _context.Set<TEntity>()
                .Where(lambda)
                .Select(x => new TResponse
                {
                    Id = EF.Property<int>(x, "Id"), // dohvaća ID iz baze
                    referenceId = referenceId,
                    ImageUrl = EF.Property<string>(x, GetPropertyName(_imageUrlSelector))
                })
                .ToListAsync();

            return list;
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
