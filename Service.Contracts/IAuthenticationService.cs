using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<TokenDto> AuthenticateUser(UserAuthenticationDto userForAuth);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
    }
}
