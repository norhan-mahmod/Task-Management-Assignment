using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Assignment.Core.Dtos.AuthDtos;
using Assignment.Core.Entities.Identity;
using Assignment.Core.ServiceInterfaces;
using Assignment.Service.ExceptionHandling;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AuthService(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager,
            ITokenService tokenService , IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        public async Task<ResponseDto> SignUp(SignUpDto signupDto)
        {
            var oldUser = await userManager.FindByEmailAsync(signupDto.Email);
            if (oldUser is not null)
                throw new BadRequestException("The Email is already exist!");
            var user = new AppUser()
            {
                UserName = signupDto.UserName,
                Email = signupDto.Email,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            var result = await userManager.CreateAsync(user, signupDto.Password);
            if (!result.Succeeded)
                throw new BadRequestException($"Registration Failed! {result.Errors.Select(e=>e.Description).Aggregate((acc,next) => acc + next)}");
            return new ResponseDto()
            {
                IsSucceded = true,
                Message = "user signed up successfully!"
            };
        }


        public async Task<LoginResultDto> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null || user.IsDeleted)
                throw new UnauthorizedAccessException("There is no user with this credentials");
            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                throw new UnauthorizedAccessException("wrong Email or Password!");
            var token = await tokenService.CreateTokenAsync(user, userManager);
            return new LoginResultDto()
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = token
            };
        }

        public async Task<bool> DeleteUser(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
                throw new NotFoundException("User Not Found!");
            user.IsDeleted = true;
            await userManager.UpdateAsync(user);
            return true;
        }

        public async Task<UserInfoDto> GetCurrentUserProfile(ClaimsPrincipal userClaims)
        {
            var user = await userManager.GetUserAsync(userClaims);
            var result = mapper.Map<UserInfoDto>(user);
            return result;
        }

        public async Task<List<UserFullInfoDto>> GetAllUsers()
        {
            var users = await userManager.Users.ToListAsync();
            var result = mapper.Map<List<UserFullInfoDto>>(users);
            return result;
        }
    }
}
