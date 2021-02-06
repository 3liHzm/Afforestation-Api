using Afforestation.App;
using Afforestation.Database.Managers;
using Afforestation.Domain.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace AfforestationApi
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {

            var serviceType = typeof(Service);
            var defineTypes = serviceType.Assembly.DefinedTypes;

            var services = defineTypes.Where(s => s.GetTypeInfo().GetCustomAttribute<Service>() != null);

            foreach (var service in services)
            {
                @this.AddTransient(service);
            }

           
           @this.AddTransient<IUserManager, UsersManager>();
           @this.AddTransient<IPostManager, PostManager>();
            



            return @this;
        }
    }
}
