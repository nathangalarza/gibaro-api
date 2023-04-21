using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IRepositoryManager _repository;

        public UserService(IMapper mapper, UserManager<User> userManager, IRepositoryManager repository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<(IEnumerable<UserDto> users, MetaData metaData)> GetUsers(Guid currentLoggedUserId, RequestParameters requestParameters, bool trackChanges)
        {
            PagedList<User> users = await _repository.User.GetUsers(currentLoggedUserId, requestParameters, trackChanges);
            return (users: _mapper.Map<IEnumerable<UserDto>>(users), metaData: users.MetaData);
        }

        public async Task<UserDto> GetUser(Guid id, bool trackChanges)
        {
            var user = await GetUserAndCheckIfExists(id, trackChanges);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> ValidateUsernameExists(string username, bool trackChanges)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return false;

            return true;
        }

        public async Task<bool> ValidateEmailExists(string email, bool trackChanges)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return false;

            return true;
        }
        
        public async Task<UserDto> GetUserByUsername(Guid userId, string username, bool trackChanges)
        {
            var user = await _repository.User.GetUserByUsername(userId, username, trackChanges);

            if (user == null)
                throw new Exception("User is not found");

            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUser(Guid id, UserUpdateDto userUpdateDto, bool trackChanges)
        {
            var user = await GetUserAndCheckIfExists(id, trackChanges);
            _mapper.Map(userUpdateDto, user);
            await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> CreateUser(UserCreationDto userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);

            var result = await _userManager.CreateAsync(user, userCreateDto.Password);

            return result;
        }
        
        public async Task DeleteUser(Guid id, string password, bool trackChanges)
        {
            var user = await GetUserAndCheckPassword(id, password, trackChanges);
            await _userManager.DeleteAsync(user);
        }

        public async Task DeactivateUser(Guid id, string password, bool trackChanges)
        {
            var user = await GetUserAndCheckPassword(id, password, trackChanges);
            user.DeletedAt = DateTime.Now;
            await _userManager.UpdateAsync(user);
        }


        private async Task<User> GetUserAndCheckIfExists(Guid id, bool trackChanges)
        {
            var user = !trackChanges
                ? await _userManager.Users.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.DeletedAt == null)
                : await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.DeletedAt == null);

            if (user is null)
                throw new Exception("User is not found");

            //throw new NotFoundException("User not found");

            return user;
        }

        private async Task<User> GetUserAndCheckPassword(Guid id, string password, bool trackChanges)
        {
            var user = await GetUserAndCheckIfExists(id, trackChanges);

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordCorrect)
                throw new Exception("Password is incorrect");
            //throw new BadRequestException("Password is incorrect");

            return user;
        }

        Task<Microsoft.AspNet.Identity.IdentityResult> IUserService.CreateUser(UserCreationDto userCreateDto)
        {
            throw new NotImplementedException();
        }
    }
}