using DogTestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogTestApi.Misc.Checks;

namespace DogTestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        DogsContext db;

        public DogController(DogsContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<ActionResult<Dog>> Post(Dog dog)
        {
            if (dog == null)
            {
                return BadRequest();
            }

            string fieldsCheckError = DogFieldsCheck.FieldsPassOrNot(dog);
            if (fieldsCheckError != null)
            {
                return BadRequest(fieldsCheckError);
            }

            Dog presenceDog;
            presenceDog = await db.Dogs.FirstOrDefaultAsync(dogEnt => dogEnt.name == dog.name);
            if (presenceDog != null)
            {
                if (presenceDog.name == dog.name)
                {
                    return BadRequest("ERROR: Entity with this name already exists");
                }
            }
            db.Dogs.Add(dog);
            await db.SaveChangesAsync();
            return Ok(dog);
            
        }
    }
}
