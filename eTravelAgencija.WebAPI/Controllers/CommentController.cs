using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{
    public class CommentController : BaseCRUDController<Model.model.Comment, CommentSearchObject, CommentUpsertRequest, CommentUpsertRequest>
    {
        public CommentController(ILogger<BaseCRUDController<Model.model.Comment, CommentSearchObject, CommentUpsertRequest, CommentUpsertRequest>> logger, ICommentService commentService) : base(logger, commentService)
        {
            
        }
    }
}