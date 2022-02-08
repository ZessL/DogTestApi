using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogTestApi.Models
{
    public class Dog
    {
        public Dog() { }
        public Dog(string name, string color, short tail_length, short weight)
        {
            this.name = name;
            this.color = color;
            this.tail_length = tail_length;
            this.weight = weight;
        }
        
        public int Id { get; set; }
        
        [BindRequired]
        public string name { get; set; }
        
        [BindRequired]
        public string color { get; set; }
        
        [BindRequired]
        public short tail_length { get; set; }

        [BindRequired]
        public short weight { get; set; }
    }
}
