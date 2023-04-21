using Contracts;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IUserDeviceRepository> _userDeviceRepository;


        //{{Lazy}}//


        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _userRepository = new Lazy<IUserRepository>(() => new
                UserRepository(repositoryContext));

            _userDeviceRepository = new Lazy<IUserDeviceRepository>(() => new
              UserDeviceRepository(repositoryContext));
            //{{constructor}}//

        }

        public IUserRepository User => _userRepository.Value;
        public IUserDeviceRepository UserDevice => _userDeviceRepository.Value;


        //{service}//




        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }


    }
}














