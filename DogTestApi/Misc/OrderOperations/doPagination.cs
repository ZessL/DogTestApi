using DogTestApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace DogTestApi.Misc.OrderOperations
{
    public class doPagination
    {
        public static string paging(List<Dog> dogsList, int pageNumber, int pageLimit, out List<Dog> outList)
        {
            var dogs = dogsList;
            if (pageLimit * pageNumber >= dogsList.Count)
            {
                outList = null;
                return ("ERROR: too huge number of pageNumber OR/AND pageLimit");
            }
            dogsList = dogsList.Skip(pageNumber * pageLimit).Take(pageLimit).ToList();
            outList = dogsList;
            return null;
        }
    }
}
