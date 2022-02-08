using DogTestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogTestApi.Misc.Dictionaries;

namespace DogTestApi.Misc.Checks
{
    public class DogFieldsCheck
    {

        public static string FieldsPassOrNot(Dog dog)
        {
            if (dog.name == null)
            {
                return FieldErrorsDictionary.fieldErrorPairs["name"];
            } else if(dog.color == null)
            {
                return FieldErrorsDictionary.fieldErrorPairs["color"];
            } else if(dog.tail_length < 1)
            {
                return FieldErrorsDictionary.fieldErrorPairs["tail_length"];
            } else if(dog.weight < 1)
            {
                return FieldErrorsDictionary.fieldErrorPairs["weight"];
            }
            return null;
        }
    }
}
