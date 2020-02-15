using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Conduit.ApplicationCore.Entities;
using Conduit.ApplicationCore.Errors;
using Conduit.ApplicationCore.Interfaces.Account;
using Conduit.ApplicationCore.Services;
using Conduit.Infrastructure.Data;
using Conduit.Infrastructure.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Conduit.Web
{
    public class Startup
    {
        private readonly string _allowSpecificOrigins = "allowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ConduitDbContext>()
                .Services
                .Configure<IdentityOptions>(o => o.User.RequireUniqueEmail = true);

            services.AddAuthentication(a =>
                {
                    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Secret"])),
                        ValidIssuer = Configuration["Token:Issuer"],
                        ValidAudience = Configuration["Token:Audience"]
                    };
                });

            services.AddAutoMapper(Assembly.GetAssembly(typeof(BaseMapper)));

            services.AddCors(options =>
            {
                options.AddPolicy(_allowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services
                .AddMvc()
                .ConfigureApiBehaviorOptions(options =>
                {                    
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        return new BadRequestObjectResult(ToErrorsList(actionContext.ModelState))
                        {
                            StatusCode = 422
                        };
                    };
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ConduitDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConduitApplicationConnection")));

            services.Configure<AppConfiguration>(Configuration);

            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(_allowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }

        private ErrorsDtoRoot ToErrorsList(ModelStateDictionary modelState)
        {
            return new ErrorsDtoRoot
            {
                Errors = new Errors
                {
                    Body = new List<string>(modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)))
                }
            };
        }
    }
}