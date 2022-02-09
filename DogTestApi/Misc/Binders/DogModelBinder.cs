using DogTestApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogTestApi.Misc.Checks;

namespace DogTestApi.Misc.Binders
{
    public class DogModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            string sTailLength = bindingContext.ValueProvider.GetValue("tail_length").FirstValue;
            string sWeight = bindingContext.ValueProvider.GetValue("weight").FirstValue;

            sTailLength = sTailLength == null ? "-1" : sTailLength;
            sWeight = sWeight == null ? "-1" : sWeight;

            short tail_length = 0;
            short weight = 0;
            try
            {
                tail_length = short.Parse(sTailLength);
                weight = short.Parse(sWeight);
            }
            catch(FormatException e)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.FromException(new ArgumentException("ERROR: Field tail_length OR weight contains symbols"));
            }

            Dog newDog = new Dog()
            {
                name = bindingContext.ValueProvider.GetValue("name").FirstValue,
                color = bindingContext.ValueProvider.GetValue("color").FirstValue,
                tail_length = tail_length,
                weight = weight
            };

            string checkFiledsError = DogFieldsCheck.FieldsPassOrNot(newDog);
            if(checkFiledsError != null)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.FromException(new ArgumentException(checkFiledsError));
            }
               

            bindingContext.Result = ModelBindingResult.Success(newDog);
            return Task.CompletedTask;
        }
    }
}