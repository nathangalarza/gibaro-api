#warning This is generated Code. Please check and update UserDevice Model in Entities.Models. Once you do the check remove this line. 

using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;
using Shared.DataTransferObjects.UserDevice;

namespace Service
{

    public class UserDeviceService : IUserDeviceService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;
        public UserDeviceService(IMapper mapper, IRepositoryManager repository)
        {
            _mapper = mapper;

            _repository = repository;
        }
        public async Task<UserDeviceDto?> GetUserDevice(Guid id, bool trackChanges)
        {
            //UserDeviceDto userDevice = await GetUserDeviceById(id, trackChanges);
            //return _mapper.Map<UserDeviceDto>(userDevice);
            throw new NotImplementedException();

        }


        public async Task UpdateUserDevice(Guid id, UserDeviceInfoDto userDeviceUpdateDto)

        {
            //UserDevice userDevice = await GetUserDeviceById(id, false);



            //_mapper.Map(userDeviceUpdateDto,userDevice);

            //_repository.UserDevices.UpdateUserDevice(userDevice);

            //await _repository.SaveAsync();

            throw new NotImplementedException();


        }


        public async Task<UserDeviceDto> CreateUserDevice(UserDeviceInfoDto userDeviceCreationDto)

        {

            //UserDeviceDto userDevice = _mapper.Map<UserDeviceDto>(userDeviceCreationDto);

            //_repository.UserDevice.CreateUserDevice(userDevice);

            //await _repository.UserDeviceDto();



            //return _mapper.Map<UserDeviceDto>(userDevice);

            throw new NotImplementedException();

        }



        public async Task DeleteUserDevice(Guid id)

        {

            //UserDevice userDevice = await GetUserDeviceById(id, false);

            //_repository.UserDevice.DeleteUserDevice(userDevice);

        }




        public async Task<(IEnumerable<UserDeviceDto> userDevices, MetaData metaData)> GetUserDevices(RequestParameters requestParameters, bool trackChanges)

        {

            PagedList<UserDevice?> userDevices = await _repository.UserDevice.GetUserDevices(requestParameters, trackChanges);

            return (userDevices: _mapper.Map<IEnumerable<UserDeviceDto>>(userDevices), metaData: userDevices.MetaData);

        }

        private async Task<UserDevice> GetUserDeviceById(Guid id, bool trackChanges)
        {
            UserDevice? userDevice = await _repository.UserDevice.GetUserDevice(id, trackChanges);

            if (userDevice == null)
                throw new Exception("userDevice not found");

            return userDevice;
        }
    }
}


