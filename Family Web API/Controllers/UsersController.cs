using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family_Web_API.Data;
using Family_Web_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Family_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController :ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IList<User>>> GetUsers([FromQuery] string usertype,[FromQuery] string username)
        {
            IList<User> users;
            try
            {
                IList<User> filteredUsers = await userService.GetUsersAsync();
                
                if(username!=null)
               {
                    
                   users= filteredUsers.Where(a => a.UserName.Equals(username,StringComparison.OrdinalIgnoreCase)).ToList();
               }
                else if (usertype != null)
                {
                    users= filteredUsers.Where(a => a.UserType.Equals(usertype,StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else
                {
                    users = await userService.GetUsersAsync();
                }
              
                return Ok(users); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        
        [HttpDelete]
        public async Task<ActionResult> DeleteUser([FromQuery] string username)
        {
            try
            {
                User user=await userService.RemoveUserAsync(username);
                return Ok(user);
            }
            catch
                (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPut]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await userService.AddUserAsync(user);
                return Created($"/{user.UserName}", user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}