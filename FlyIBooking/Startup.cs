using FlyIBooking.DbContext;
using FlyIBooking.Generators;
using FlyIBooking.Repositories;
using FlyIBooking.Services.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace FlyIBooking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FlyIBookingDbContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("DatabaseContext")),
                ServiceLifetime.Transient);

            services.AddTransient<AccountRepository>();
            
            services.AddTransient(_ =>
                new HashGenerator(Configuration.GetSection("HashOptions").GetValue<string>("Salt")));
            services.AddTransient<JwtGenerator>();
            services.AddTransient<AuthService>();
            services.AddTransient<AccountService>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "FlyIBooking", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                opts.DocumentTitle = "Fly booking API";
                opts.DisplayRequestDuration();
                opts.DisplayOperationId();
                opts.DefaultModelRendering(ModelRendering.Example);
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}