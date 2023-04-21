using Contracts;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repository.Context;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<User>> GetUsers(Guid currentLoggedUserId, RequestParameters requestParameters, bool trackChanges)
        {
            var query = await
                (
                    from us in RepositoryContext.Users
                    where us.UserName.StartsWith(requestParameters.Search) && us.DeletedAt == null && us.Id != currentLoggedUserId
                    select new User
                    {
                        Id = us.Id,
                        UserName = us.UserName,
                        Name = us.Name,
                      
                    }
                )
                .AsNoTracking()
                .ToListAsync();

            return PagedList<User>.ToPagedList(query, requestParameters.PageNumber, requestParameters.PageSize);
        }

        public async Task<User?> GetUserByUsername(Guid currentLoggedUserId, string username, bool trackChanges)
        {
            return await
                 (
                     from us in RepositoryContext.Users
                     where us.UserName == username
                     select new User
                     {
                         Id = us.Id,
                         UserName = us.UserName,
                         Name = us.Name,
                       
                     }
                 )
                 .AsNoTracking()
                 .SingleOrDefaultAsync();
        }
    }
}
