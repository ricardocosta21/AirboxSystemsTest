using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using AirboxSystems.API.Persistence.Repositories;
using AirboxSystems.API.Services.UserLocation;
using Microsoft.Extensions.Options;
using AirboxSystems.API.Domain.Models;

namespace AirboxSystems.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing()); ;
            services.AddAutoMapper(typeof(Startup));
      
            services.Configure<AirboxDatabaseSettings>(
                Configuration.GetSection(nameof(AirboxDatabaseSettings)));

            services.AddSingleton<IAirboxDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<AirboxDatabaseSettings>>().Value);

            services.AddSingleton<UserLocationService>();

            services.AddScoped<IUserLocationRepository, UserLocationRepository>();
            services.AddScoped<IUserLocationService, UserLocationService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
