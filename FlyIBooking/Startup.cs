using System.Text;
using FlyIBooking.DbContext;
using FlyIBooking.Generators;
using FlyIBooking.Repositories;
using FlyIBooking.Services;
using FlyIBooking.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
                options => options.UseNpgsql(Configuration.GetSection("DatabaseContext")["ConnectionString"]),
                ServiceLifetime.Transient);

            services.AddTransient<AccountRepository>();
            services.AddTransient<TicketRepository>();
            services.AddTransient<PlaneRepository>();
            
            services.AddTransient(_ =>
                new HashGenerator(Configuration.GetSection("HashOptions")["Salt"]));
            services.AddTransient<JwtGenerator>();
            services.AddTransient<AuthService>();
            services.AddTransient<AccountService>();
            services.AddTransient<PlaneService>();
            services.AddTransient<TicketService>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                },
                            },
                            new string[0]
                        }
                    });

                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.ApiKey,
                        In = ParameterLocation.Header,
                        Scheme = "Bearer",
                        Name = "Authorization",
                        Description = "JWT token",
                        BearerFormat = "JWT"
                    });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClassicTotalizator.API", Version = "v1" });
            });
            
            services.AddCors();
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(Configuration.GetSection("AuthKey").GetValue<string>("Secret"))),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

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