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
    public class FamiliesController:ControllerBase

    {
        //private IMemberService memberService;
        private IFamilyService familyService;

        public FamiliesController(IFamilyService familyService)
        {
            this.familyService = familyService;
        }

      
        [HttpGet]
        public async Task<ActionResult<IList<Family>>> GetFamilies([FromQuery] string? streetname,[FromQuery] int? housenumber)
        {
            IList<Family> families;
            try
            {
                if (streetname != null && housenumber != null)
                {
                    IList<Family> filteredFamilies = await familyService.GetFamiliesAsync();
                    families = filteredFamilies.Where(f =>
                        f.StreetName.Equals(streetname, StringComparison.OrdinalIgnoreCase) &&
                        f.HouseNumber == housenumber).ToList();
                }
                else
                {
                    families = await familyService.GetFamiliesAsync();
                }
                
                return Ok(families);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete]
        public async Task<ActionResult> DeleteFamilies([FromQuery] string streetname,[FromQuery] int housenumber)
        {
            try
            {
                await familyService.RemoveFamilyAsync(streetname,housenumber);
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
        public async Task<ActionResult<Family>> AddAFamily([FromBody] Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await familyService.AddFamilyAsync(family);
                return Created($"/streetname={family.StreetName}&housenumber={family.HouseNumber}", family);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
