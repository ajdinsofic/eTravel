using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Interfaces
{
    public interface ICommentService : ICRUDService<Model.model.Comment, CommentSearchObject, CommentUpsertRequest, CommentUpsertRequest>
    {
        
    }
}