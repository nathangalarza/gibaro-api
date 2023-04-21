using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IUserRepository
{
    Task<PagedList<User>> GetUsers(Guid currentLoggedUserId, RequestParameters requestParameters, bool trackChanges);
    Task<User?> GetUserByUsername(Guid currentLoggedUserId, string userName, bool trackChanges);
}