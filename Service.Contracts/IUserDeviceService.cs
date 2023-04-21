#warning This is generated Code. Please check and update UserDevice Model in Entities.Models. Once you do the check remove this line. 

using Shared.DataTransferObjects.UserDevice;

using Shared.RequestFeatures;

namespace Service.Contracts

{

    public interface IUserDeviceService

    {

        Task<(IEnumerable<UserDeviceDto> userDevices, MetaData metaData)> GetUserDevices(RequestParameters requestParameters, bool trackChanges);

        Task<UserDeviceDto> GetUserDevice(Guid id, bool trackChanges);

        Task<UserDeviceDto> CreateUserDevice(UserDeviceInfoDto userDeviceCreationDto);

        Task DeleteUserDevice(Guid id);

        Task UpdateUserDevice(Guid id, UserDeviceInfoDto userDeviceUpdateDto);

    }

}

