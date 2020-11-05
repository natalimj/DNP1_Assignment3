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
    public class AdultsController : ControllerBase
    {
        private IMemberService memberService;

        public AdultsController(IMemberService memberService)
        {
            this.memberService = memberService;
        } 
        
        [HttpGet]
        public async Task<ActionResult<IList<Adult>>> 
            GetAdults([FromQuery] int? id,[FromQuery] String firstname,[FromQuery] String lastname)
        {
           IList<Adult> adults;
           try 
           {
               IList<Adult> filteredAdults=await memberService.GetAdultsAsync();
                if (id!= null)
                {
                    
                    adults = filteredAdults.Where(a => a.Id == id).ToList();
                }
                else if(firstname!=null)
                {
                    
                    adults = filteredAdults.Where(a => a.FirstName.Equals(firstname,StringComparison.OrdinalIgnoreCase)).ToList();
               }
               else if(lastname!=null)
               {
                  
                   adults = filteredAdults.Where(a => a.LastName.Equals(lastname,StringComparison.OrdinalIgnoreCase)).ToList();
               }
                else
                {
                   adults = await memberService.GetAdultsAsync();
                }
                return Ok(adults); 
           }
           catch (Exception e)
           {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
                
           }
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteAdult([FromRoute] int id)
        {
            try
            {
                await memberService.RemoveAdultAsync(id);
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
        public async Task<ActionResult<Adult>> AddAdult([FromBody] Adult adult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await memberService.AddAdultAsync(adult);
                return Created($"/{adult.Id}", adult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
    }
}