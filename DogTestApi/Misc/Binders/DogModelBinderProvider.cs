using DogTestApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogTestApi.Misc.Binders
{
    public class DogModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinder binder = new DogModelBinder();
        
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(Dog) ? binder : null;
        }
    }
}
