#warning This is generated Code. Please check and update UserDevice Model in Entities.Models. Once you do the check remove this line. 

using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts

{

    public interface IUserDeviceRepository

    {

        Task<PagedList<UserDevice?>> GetUserDevices(RequestParameters requestParameters, bool trackChanges);

        Task<UserDevice?> GetUserDevice(Guid id, bool trackChanges);

        Task<UserDevice?> GetUserDeviceByDeviceId(string id, bool trackChanges);

        Task<UserDevice?> GetUserDeviceByToken(string token, bool trackChanges);

        void CreateUserDevice(UserDevice userDevice);

        void DeleteUserDevice(UserDevice userDevice);

        void UpdateUserDevice(UserDevice userDevice);

    }

}

