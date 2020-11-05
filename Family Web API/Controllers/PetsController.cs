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
    public class PetsController :ControllerBase
    {
        private IMemberService memberService;
        

        public PetsController(IMemberService memberService)
        {
            this.memberService = memberService;
           
        }
        
        [HttpGet]
        public async Task<ActionResult<IList<Pet>>> GetPets([FromQuery] int? id,[FromQuery] string name)
        {
            IList<Pet> pets;
            try
            {
                IList<Pet> filteredPets = await memberService.GetPetsAsync();
                if (id!= null)
                {
                    pets=filteredPets.Where(a => a.Id == id).ToList();
                }
                else if(name!=null)
                {
                    pets=filteredPets.Where(a => a.Name.Equals(name,StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else
                {
                    pets = await memberService.GetPetsAsync();
                   
                }
                return Ok(pets); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeletePets([FromRoute] int id)
        {
            try
            {
                await memberService.RemovePetAsync(id);
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
        public async Task<ActionResult<Pet>> AddPet([FromBody] Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Pet added=await memberService.AddPetAsync(pet);
                return Created($"/{added.Id}",added);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}