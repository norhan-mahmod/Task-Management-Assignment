using Assignment.Core.Dtos.AuthDtos;
using Assignment.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService authService;

        public UserController(IAuthService authService)
        {
            this.authService = authService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register(SignUpDto signupDto)
        {
            var result = await authService.SignUp(signupDto);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await authService.Login(loginDto);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var result = await authService.DeleteUser(email);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetCurrentUserProfile")]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            var result = await authService.GetCurrentUserProfile(User);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await authService.GetAllUsers();
            return Ok(result);
        }

    }
}
