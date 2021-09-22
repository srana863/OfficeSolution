using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.API.Helpers
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
