using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.User;

namespace Shared.DataTransferObjects
{
    public record TokenDto(string AccessToken, string RefreshToken, UserDto user = null);
}
