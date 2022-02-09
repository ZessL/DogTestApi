using DogTestApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace DogTestApi.Misc.Order
{
    public class doOrder
    {
        public static string orderBy(List<Dog> dogs, string order, string attribute, out List<Dog> collectionOut)
        {
            var dogList = dogs;
            if (order == "upsc")
            {
                if (attribute == "name")
                {
                    dogList = dogList.OrderBy(x => x.name).ToList();
                    collectionOut = dogList.ToList();
                    return null;
                }
                else if (attribute == "color")
                {
                    dogList = dogList.OrderBy(x => x.color).ToList();
                    collectionOut = dogList.ToList();
                    return null;
                }
                else if (attribute == "tail_length")
                {
                    dogList = dogList.OrderBy(x => x.tail_length).ToList();
                    collectionOut = dogList.ToList();
                    return null;
                }
                else if (attribute == "weight")
                {
                    dogList = dogList.OrderBy(x => x.weight).ToList();
                    collectionOut = dogList.ToList();
                    return null;
                }
                else
                {
                    collectionOut = null;
                    return "ERROR: Sorting attribute does not belong to model Dog. Accepteble attributes: \n name, weight, tail_length, weight";
                }
            }
            else if (order == "desc")
            {
                if (attribute == "name")
                {
                    dogList = dogList.OrderByDescending(x => x.name).ToList();
                    collectionOut = dogList.ToList();
                    return null;
                }
                else if (attribute == "color")
                {
                    dogList = dogList.OrderByDescending(x => x.color).ToList();
                    collectionOut = dogList.ToList();
                    return null;
                }
                else if (attribute == "tail_length")
                {
                    dogList = dogList.OrderByDescending(x => x.tail_length).ToList();
                    collectionOut = dogList.ToList();
                    return null;
                }
                else if (attribute == "weight")
                {
                    dogList = dogList.OrderByDescending(x => x.weight).ToList();
                    collectionOut = dogList.ToList();
                    return null;
                }
                else
                {
                    collectionOut = null;
                    return "ERROR: Sorting attribute does not belong to model Dog. Accepteble attributes: \n name, weight, tail_length, weight";
                }
            }
            collectionOut = null;
            return "ERROR: Sorting style nor desc(descending), nor upsc(upscending)";
        }
    }

}
