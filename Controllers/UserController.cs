using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Models;
using AgileTaskMaster.Services;

namespace AgileTaskMaster.Controllers
{
    [Authorize(Roles = "BusinessOwner, DepartmentManager, ProjectManager")] 
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDTO>> GetUserById(string userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        
        [Authorize(Roles = "BusinessOwner, DepartmentManager, ProjectManager")]
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO userDTO)
        {
            var user = await _userService.CreateUser(userDTO);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, user);
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<UserDTO>> UpdateUser(string userId, UserDTO userDTO)
        {
            if (userId != userDTO.Id)
                return BadRequest();

            var updatedUser = await _userService.UpdateUser(userDTO);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }

        [Authorize(Roles = "BusinessOwner")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var deletedUser = await _userService.DeleteUser(userId);
            if (!deletedUser)
                return NotFound();

            return NoContent();
        }
    }
}
