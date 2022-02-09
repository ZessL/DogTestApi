using DogTestApi.Misc.Checks;
using DogTestApi.Misc.Order;
using DogTestApi.Misc.OrderOperations;
using DogTestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogTestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        DogsContext db;
        DbSet<Dog> dbTable;

        public DogsController(DogsContext context)
        {
            db = context;
            dbTable = db.Dogs;
            if (!dbTable.Any())
            {
                dbTable.Add(new Dog { name = "Neo", color = "red & amber", tail_length = 22, weight = 32 });
                dbTable.Add(new Dog { name = "Jessy", color = "black & white", tail_length = 7, weight = 14 });
                db.SaveChanges();
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dog>>> Get()
        {
            return await dbTable.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dog>> Get(int id)
        {
            Dog dog = await dbTable.FirstOrDefaultAsync(x => x.Id == id);
            if (dog == null)
            {
                return BadRequest($"ERROR: no such entry at Id -- {id}");
            }
            return new ObjectResult(dog);
        }

        [HttpGet("sorted")]
        public async Task<ActionResult<Dog>> GetSorted([FromQuery] string attribute, [FromQuery] string order)
        {
            string checkFields = OrderAndAttributeCheck.orderAtributeCheck(attribute, order);
            if (checkFields != null)
            {
                return BadRequest(checkFields);
            }

            List<Dog> dogList = new List<Dog>();
            dogList = dbTable.ToList();
            string isSorted = doOrder.orderBy(dogList, order, attribute, out dogList);
            if (isSorted != null)
            {
                return BadRequest(isSorted);
            }

            return new ObjectResult(dogList);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<Dog>> GetPaged([FromQuery] int pageNumber, [FromQuery] int pageLimit)
        {
            string checkFields = PageNumberAndLimitCheck.pageNumberLimitcheck(pageNumber, pageLimit);
            if (checkFields != null)
            {
                return BadRequest(checkFields);
            }
            List<Dog> pagedList = new List<Dog>();
            string isPaged = doPagination.paging(dbTable.ToList(), pageNumber, pageLimit, out pagedList);
            if (isPaged != null)
            {
                return BadRequest(isPaged);
            }
            return new ObjectResult(pagedList);
        }

        [HttpGet("pagedAndSorted")]
        public async Task<ActionResult<Dog>> GetPagedAndSorted([FromQuery] int pageNumber, [FromQuery] int pageLimit, [FromQuery] string attribute, [FromQuery] string order)
        {
            string checkNumberLimit = PageNumberAndLimitCheck.pageNumberLimitcheck(pageNumber, pageLimit);
            string checkAttributeOrder = OrderAndAttributeCheck.orderAtributeCheck(attribute, order);
            if (checkNumberLimit != null)
            {
                return BadRequest(checkNumberLimit);
            }
            if (checkAttributeOrder != null)
            {
                return BadRequest(checkAttributeOrder);
            }

            List<Dog> dogList = new List<Dog>();
            string isSorted = doOrder.orderBy(dbTable.ToList(), order, attribute, out dogList);
            if (isSorted != null)
            {
                return BadRequest(isSorted);
            }
            List<Dog> outDogList = new List<Dog>();
            string isPaged = doPagination.paging(dogList, pageNumber, pageLimit, out outDogList);
            if (isPaged != null)
            {
                return BadRequest(isPaged);
            }
            return new ObjectResult(outDogList);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Dog>> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("ERROR: Chosen Id is 0");
            }
            Dog deleteDog = await dbTable.FindAsync(id);
            if (deleteDog == null)
            {
                return BadRequest($"ERROR: No such entry at Id -- {id}");
            }
            dbTable.Remove(deleteDog);
            await db.SaveChangesAsync();
            return Ok(deleteDog);
        }
    }
}
