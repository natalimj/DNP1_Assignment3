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
    public class ChildrenController:ControllerBase
    {
        private IMemberService memberService;

        public ChildrenController(IMemberService memberService)
        {
            this.memberService = memberService;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<IList<Child>>> 
            GetChildren([FromQuery] int? id,[FromQuery] String firstname,[FromQuery] String lastname)
        {
            IList<Child> children;
            try
            {
                IList<Child> filteredChildren = await memberService.GetChildrenAsync();

                if (id != null)
                {
                    children=filteredChildren.Where(a => a.Id == id).ToList();
                }
                else if(firstname!=null)
                {
                   
                    children=filteredChildren.Where(a => a.FirstName.Equals(firstname,StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else if(lastname!=null)
                {
                  
                    children=filteredChildren.Where(a => a.LastName.Equals(lastname,StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else
                {
                    children = await memberService.GetChildrenAsync();
                  
                }
                return Ok(children);
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
                
            }
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteChild([FromRoute] int id)
        {
            try
            {
                await memberService.RemoveChildAsync(id);
                return Ok();
            }
            catch
                (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        
        [HttpPut]
        public async Task<ActionResult<Child>> AddChild([FromBody] Child child)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await memberService.AddChildAsync(child);
                return Created($"/{child.Id}", child);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
    }
}