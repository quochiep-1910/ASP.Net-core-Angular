using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTO;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces.IRepository;
using AutoMapper;

namespace API.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {

        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly DataContext _Context;
        private readonly IMapper _mapper;

        public AccountController(IUserRepository userRepository, DataContext context,
         ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _Context = context;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
        {
            var user = await _userRepository.GetUsersAsync();
            var usersToReturn = _mapper.Map<IEnumerable<MemberDTO>>(user);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDTO>> GetUsers(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            var usermapper = _mapper.Map<MemberDTO>(user);
            return Ok(usermapper);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO register)
        {
            if (await UserExists(register.UserName))
                return BadRequest("UserName is taken");
            var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = register.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmac.Key
            };
            _Context.Users.Add(user);
            await _Context.SaveChangesAsync();
            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
        {
            var user = await _Context.Users
                .SingleOrDefaultAsync(x => x.UserName == login.UserName);
            if (user == null)
                return Unauthorized("Invalid Username");
            var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid password");
            }
            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }

        private async Task<bool> UserExists(string username)
        {
            return await _Context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}