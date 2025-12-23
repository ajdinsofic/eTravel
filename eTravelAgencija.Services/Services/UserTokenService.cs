using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Model.ResponseObject;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace eTravelAgencija.Services.Services
{

    public class UserTokenService
    : BaseCRUDService<
        Model.model.UserToken,
        UserTokenSearchObject,
        Database.UserToken,
        UserTokenUpsertRequest,
        UserTokenUpsertRequest>,
      IUserTokenService
    {
        public UserTokenService(
            eTravelAgencijaDbContext context,
            IMapper mapper
        ) : base(context, mapper)
        {
        }

        public override IQueryable<UserToken> ApplyFilter(
    IQueryable<UserToken> query,
    UserTokenSearchObject search)
        {
            if (search?.UserId != null)
            {
                query = query.Where(x => x.UserId == search.UserId);
            }

            return base.ApplyFilter(query, search);
        }

    }

}