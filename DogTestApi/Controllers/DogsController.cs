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
using DogTestApi.Misc.OrderOperations;

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
            dogList = (from s in db.Dogs select s).ToList();
            string isSorted = doOrder.orderBy(dogList, order, attribute,out dogList);
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
            string isPaged = doPagination.paging(db.Dogs.ToList(), pageNumber, pageLimit, out pagedList);
            if(isPaged != null)
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
            if(checkNumberLimit != null)
            {
                return BadRequest(checkNumberLimit);
            }
            if (checkAttributeOrder != null)
            {
                return BadRequest(checkAttributeOrder);
            }
            
            List<Dog> dogList = new List<Dog>();
            string isSorted = doOrder.orderBy(db.Dogs.ToList(), order, attribute, out dogList);
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
