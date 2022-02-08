using DogTestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogTestApi.Misc.Lists;
using DogTestApi.Misc.Order;
using DogTestApi.Misc;
using DogTestApi.Misc.Checks;

namespace DogTestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        DogsContext db;
        public DogsController(DogsContext context)
        {
            db = context;
            if (!db.Dogs.Any())
            {
                db.Dogs.Add(new Dog { name = "Neo", color = "red & amber", tail_length = 22, weight = 32 });
                db.Dogs.Add(new Dog { name = "Jessy", color = "black & white", tail_length = 7, weight = 14 });
                db.Dogs.Add(new Dog { name = "Nika", color = "black & white", tail_length = 18, weight = 27 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dog>>> Get()
        {
            return await db.Dogs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dog>> Get(int id)
        {
            Dog dog = await db.Dogs.FirstOrDefaultAsync(x => x.Id == id);
            if (dog == null)
            {
                return BadRequest($"ERROR: no such entry at Id -- {id}");
            }
            return new ObjectResult(dog);
        }

        [HttpGet("sorted")]
        public async Task<ActionResult<Dog>> GetSorted([FromQuery]string attribute, [FromQuery]string order)
        {
            string checkFields = OrderAndAttributeCheck.orderAtributeCheck(attribute, order);
            if(checkFields != null)
            {
                return BadRequest(checkFields);
            }

            List<Dog> dogList = new List<Dog>();
            bool isSorted = Order.orderBy(db, order, attribute,out dogList);
            if (!isSorted)
            {
                return BadRequest("ERROR: error while perfroming order");
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
            var dogs = from d in db.Dogs select d;
            var dogsList = await dogs.ToListAsync();
            if (pageLimit * pageNumber >= dogsList.Count)
            {
                return BadRequest("ERROR: too huge number of pageNumber OR/AND pageLimit");
            }
            dogsList = dogsList.Skip(pageNumber * pageLimit).Take(pageLimit).ToList();
            return new ObjectResult(dogsList);
        }

        [HttpGet("pagedAndSorted")]
        public async Task<ActionResult<Dog>> GetPagedAndSorted([FromQuery] int pageNumber, [FromQuery] int pageLimit, [FromQuery] string attribute, [FromQuery] string order)
        {
            string checkNumberLimit = PageNumberAndLimitCheck.pageNumberLimitcheck(pageNumber, pageLimit);
            string checkAttributeOrder = OrderAndAttributeCheck.orderAtributeCheck(attribute, order);
            if(checkNumberLimit != null)
            {
                return BadRequest(checkNumberLimit);
            }
            if (checkAttributeOrder != null)
            {
                return BadRequest(checkAttributeOrder);
            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Dog>> Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest("ERROR: Chosen Id is 0");
            }
            Dog deleteDog = await db.Dogs.FindAsync(id);
            if(deleteDog == null)
            {
                return BadRequest($"ERROR: No such entry at Id -- {id}");
            }
            db.Dogs.Remove(deleteDog);
            await db.SaveChangesAsync();
            return Ok(deleteDog);
        }
    }
}
