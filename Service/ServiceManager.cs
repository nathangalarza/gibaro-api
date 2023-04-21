using AutoMapper;
using Contracts;
using Microsoft.Extensions.Options;
using Entities.Models;
using Service.Contracts;
using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
       
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        //{{Lazy}}//

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager, ILoggerManager
                logger, IOptions<JwtConfiguration> configuration
        //BlobServiceClient blobService
        )
        {
          

            _userService = new Lazy<IUserService>(() => new
                UserService(mapper, userManager, repositoryManager));

            _authenticationService = new Lazy<IAuthenticationService>(() =>
               new AuthenticationService(logger, mapper, userManager,
                   configuration, repositoryManager));


                        

            //{{constructor}}//
        }


        public IUserService UserService => _userService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
      

        //{service}//

    }
}


