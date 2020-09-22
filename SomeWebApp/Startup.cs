using CustomValidation.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Reflection;

namespace SomeWebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var syncValidatorTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => (type.BaseType?.IsGenericType ?? false)
                               && type.BaseType.GetGenericTypeDefinition() == typeof(SyncValidator<>));

            foreach (var type in syncValidatorTypes)
            {
                var validatorImplType = type.BaseType.GenericTypeArguments.First();
                var validatorToRegisterType = typeof(ISyncValidator<>).MakeGenericType(validatorImplType);
                services.AddScoped(validatorToRegisterType, type);
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
