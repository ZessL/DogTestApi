using DogTestApi.Misc.Checks;
using DogTestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DogTestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        DogsContext db;
        DbSet<Dog> dbTable;

        public DogController(DogsContext context)
        {
            db = context;
            dbTable = db.Dogs;
        }
        public DogController(DogsContext context, bool test)
        {
            db = context;
            dbTable = db.TestDogs;
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
            presenceDog = await dbTable.FirstOrDefaultAsync(dogEnt => dogEnt.name == dog.name);
            if (presenceDog != null)
            {
                if (presenceDog.name == dog.name)
                {
                    return BadRequest("ERROR: Entity with this name already exists");
                }
            }
            dbTable.Add(dog);
            await db.SaveChangesAsync();
            return Ok(dog);

        }
    }
}
