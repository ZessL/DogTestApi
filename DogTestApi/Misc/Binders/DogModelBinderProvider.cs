using DogTestApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
