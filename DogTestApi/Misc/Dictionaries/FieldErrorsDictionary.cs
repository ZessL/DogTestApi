using System.Collections.Generic;

namespace DogTestApi.Misc.Dictionaries
{
    public class FieldErrorsDictionary
    {
        public static Dictionary<string, string> fieldErrorPairs = new Dictionary<string, string>
        {
            {"name", "ERROR: Field --name-- is NULL" },
            {"color", "ERROR: Field --color-- is NULL" },
            {"tail_length", "ERROR: Field --tail_length-- is <1 OR Null"},
            {"weight", "ERROR: Field --weight-- is <1 OR Null" }
        };
    }
}
