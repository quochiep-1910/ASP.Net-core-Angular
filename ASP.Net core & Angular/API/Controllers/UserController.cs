using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Interfaces.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
      [Authorize]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
    }
}