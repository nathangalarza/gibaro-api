using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Contracts;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Shared.DataTransferObjects.User;
using Shared.DataTransferObjects;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Entities.Exceptions;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<JwtConfiguration> _configuration;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IRepositoryManager _repository;

        private User? _user;


        public AuthenticationService(ILoggerManager logger, IMapper mapper,
        UserManager<User> userManager, IOptions<JwtConfiguration> configuration, IRepositoryManager repository)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _jwtConfiguration = configuration.Value;
            _repository = repository;
        }

        public async Task<TokenDto> AuthenticateUser(UserAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByEmailAsync(userForAuth.Email);
            var isCorrectPassword =
                (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));

            if (!isCorrectPassword)
            {
                const string message = "Incorrect username or password";
                _logger.LogWarn($"{nameof(ValidateUser)}: {message}");
                throw new UnauthorizedException(message);
            }

            // If user had the account deactivated, the login will reactivate the account
            if (_user!.DeletedAt != null)
            {
                _user.DeletedAt = null;
                await _userManager.UpdateAsync(_user!);
            }

            var token = await CreateToken(true, null);

            var session = await _repository.UserDevice.GetUserDeviceByDeviceId(userForAuth.DeviceInfo.DeviceId, false);

            if (session == null)
            {

                _repository.UserDevice.CreateUserDevice(new UserDevice
                {
                    CreatedAt = DateTime.Now,
                    Model = userForAuth.DeviceInfo.Model,
                    LastUsedAt = DateTime.Now,
                    DeviceId = userForAuth.DeviceInfo.DeviceId,
                    Platform = (Platform)Enum.Parse(typeof(Platform), userForAuth.DeviceInfo.Platform),
                    RefreshToken = token.RefreshToken,
                    UserId = _user.Id,
                   

                });

                await _repository.SaveAsync();
            }
            else
            {
                session.Revoked = true;
                await _repository.SaveAsync();
                throw new UnauthorizedException("Error");
            }

            return token;
        }

        public async Task<bool> ValidateUser(UserAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.Email);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));

            if (!result)
                _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");

            return result;
        }

        public async Task<TokenDto> CreateToken(bool populateExp, UserDevice? device)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var refreshToken = GenerateRefreshToken();

            if (device != null)
            {
                device.RefreshToken = refreshToken;
                device.LastUsedAt = DateTime.Now;
                _repository.UserDevice.UpdateUserDevice(device);
                await _repository.SaveAsync();

            }
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            var user = _mapper.Map<UserDto>(_user);
            return new TokenDto(accessToken, refreshToken, user);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
             var  secret = "kibd&2m9cp*56f8i&^+wp8s6ei5pgl0#yihzxk6u4#po6=fze5";

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!)),
                ValidateLifetime = false,
                ValidIssuer = _jwtConfiguration.ValidIssuer,
                ValidAudience = _jwtConfiguration.ValidAudience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
         
 
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = "kibd&2m9cp*56f8i&^+wp8s6ei5pgl0#yihzxk6u4#po6=fze5";
            var bytes = Encoding.UTF8.GetBytes(key!);
            var secret = new SymmetricSecurityKey(bytes);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, _user!.Id.ToString())
                };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken
            (
                issuer: _jwtConfiguration.ValidIssuer,
                audience: _jwtConfiguration.ValidAudience,
                claims: claims,
                expires:  DateTime.UtcNow.AddMinutes(1),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(claim!.Value);
            var session = await _repository.UserDevice.GetUserDeviceByToken(tokenDto.RefreshToken, false);

            if (user == null ||
                session?.Revoked == true ||
                session?.RefreshToken !=
                tokenDto.RefreshToken ||
                session?.LastUsedAt <= DateTime.Now.AddDays(-7))
                throw new UnauthorizedAccessException("Invalid client request. The tokenDto has some invalid values.");

            _user = user;

            return await CreateToken(populateExp: false, session);
        }

    }


}
