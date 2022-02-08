using DogTestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogTestApi.Misc.Order
{
    public class Order
    {
        public static bool orderBy(DogsContext dogs, string order, string attribute, out List<Dog> collectionOut)
        {
            var dogList = from s in dogs.Dogs select s;
            if (order == "upsc")
            {
                if (attribute == "name")
                {
                    dogList = dogList.OrderBy(x => x.name);
                    collectionOut = dogList.ToList();
                    return true;
                }
                else if (attribute == "color")
                {
                    dogList = dogList.OrderBy(x => x.color);
                    collectionOut = dogList.ToList();
                    return true;
                }
                else if (attribute == "tail_length")
                {
                    dogList = dogList.OrderBy(x => x.tail_length);
                    collectionOut = dogList.ToList();
                    return true;
                }
                else if (attribute == "weight")
                {
                    dogList = dogList.OrderBy(x => x.weight);
                    collectionOut = dogList.ToList();
                    return true;
                }
                else
                {
                    collectionOut = null;
                    return false;
                }
            }
            else if (order == "desc")
            {
                if (attribute == "name")
                {
                    dogList = dogList.OrderByDescending(x => x.name);
                    collectionOut = dogList.ToList();
                    return true;
                }
                else if (attribute == "color")
                {
                    dogList = dogList.OrderByDescending(x => x.color);
                    collectionOut = dogList.ToList();
                    return true;
                }
                else if (attribute == "tail_length")
                {
                    dogList = dogList.OrderByDescending(x => x.tail_length);
                    collectionOut = dogList.ToList();
                    return true;
                }
                else if (attribute == "weight")
                {
                    dogList = dogList.OrderByDescending(x => x.weight);
                    collectionOut = dogList.ToList();
                    return true;
                }
            }
            collectionOut = null;
            return false;
        }
    }
        
}
