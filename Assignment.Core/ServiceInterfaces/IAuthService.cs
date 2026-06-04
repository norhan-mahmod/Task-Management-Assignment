using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Assignment.Core.Dtos.AuthDtos;

namespace Assignment.Core.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<ResponseDto> SignUp(SignUpDto signupDto);
        Task<LoginResultDto> Login(LoginDto loginDto);
        Task<bool> DeleteUser(string email);
        Task<UserInfoDto> GetCurrentUserProfile(ClaimsPrincipal userClaims);
        Task<List<UserFullInfoDto>> GetAllUsers();
    }
}
