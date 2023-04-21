using Microsoft.AspNet.Identity;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface IUserService
    {
        Task UpdateUser(Guid id, UserUpdateDto userUpdateDto, bool trackChanges);
        Task<IdentityResult> CreateUser(UserCreationDto userCreateDto);
        Task<UserDto> GetUser(Guid id, bool trackChanges);
        Task<bool> ValidateUsernameExists(string username, bool trackChanges);
        Task<bool> ValidateEmailExists(string email, bool trackChanges);
        Task<UserDto> GetUserByUsername(Guid userId, string username, bool trackChanges);
        Task<(IEnumerable<UserDto> users, MetaData metaData)> GetUsers(Guid currentLoggedUserId, RequestParameters requestParameters, bool trackChanges);
        Task DeleteUser(Guid id, string password, bool trackChanges);
        Task DeactivateUser(Guid id, string password, bool trackChanges);
    }
}