using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class CommentService : BaseCRUDService<Model.model.Comment, CommentSearchObject, Database.Comment, CommentUpsertRequest, CommentUpsertRequest>, ICommentService
    {
        public CommentService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override IQueryable<Comment> ApplyFilter(IQueryable<Comment> query, CommentSearchObject search)
        {
            if (!string.IsNullOrWhiteSpace(search.pearsonName))
            {
                var name = search.pearsonName.ToLower();

                query = query.Where(c =>
                    c.user.FirstName.ToLower().Contains(name) ||
                    c.user.LastName.ToLower().Contains(name)
                );
            }

            return base.ApplyFilter(query, search);

        }

        public override IQueryable<Comment> AddInclude(IQueryable<Comment> query, CommentSearchObject? search = null)
        {
            if (search.offerId > 0)
                query = query.Where(c => c.offerId == search.offerId);

            query = query
                .Include(c => c.offer)
                .Include(c => c.user);

            return base.AddInclude(query, search);
        }

    }
}