using DogTestApi.Misc.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogTestApi.Misc.Checks
{
    public class OrderAndAttributeCheck
    {
        public static string orderAtributeCheck(string attribute, string order)
        {
            if (attribute == null || order == null)
            {
                return ("ERROR: Field attributes OR order is Null");
            }
            if (!AttributesL.attributes.Contains(attribute))
            {
                return ($"ERROR: Field attrinutes is wrong. Accepteble: \n{String.Join(", ", AttributesL.attributes.ToArray())}");
            }
            if (!OrdersL.orders.Contains(order))
            {
                return ($"ERROR: Field order is wrong. Accepteble: \n{String.Join(", ", OrdersL.orders.ToArray())}");
            }
            return null;
        }
    }
}
