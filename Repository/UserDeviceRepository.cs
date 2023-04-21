#warning This is generated Code. Please check and update UserDevice Model in Entities.Models. Once you do the check remove this line. 

using Repository.Context;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository
{
    internal sealed class UserDeviceRepository : RepositoryBase<UserDevice>, IUserDeviceRepository

    {

        public UserDeviceRepository(RepositoryContext repositoryContext)

            : base(repositoryContext)

        {

        }



        public void CreateUserDevice(UserDevice userDevice) => Create(userDevice);

        public void DeleteUserDevice(UserDevice userDevice) => Delete(userDevice);

        public void UpdateUserDevice(UserDevice userDevice) => Update(userDevice);

        public async Task<UserDevice?> GetUserDevice(Guid id, bool trackChanges)
        {
            throw new Exception("UserDevice not found");
        }

        public async Task<PagedList<UserDevice?>> GetUserDevices(RequestParameters requestParameters, bool trackChanges)
        {
            return PagedList<UserDevice?>.ToPagedList(null, requestParameters.PageNumber, requestParameters.PageSize);
        }

        public async Task<UserDevice?> GetUserDeviceByDeviceId(string id, bool trackChanges)
        {
            var res = await RepositoryContext.UserDevices?.FirstOrDefaultAsync(x => x.DeviceId == id && x.Revoked == false);
            return res;
        }

        public async Task<UserDevice?> GetUserDeviceByToken(string token, bool trackChanges)
        {
            var res = await RepositoryContext.UserDevices?.FirstOrDefaultAsync(x => x.RefreshToken == token && x.Revoked == false);
            return res;
        }
    }

}
