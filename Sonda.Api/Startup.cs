using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sonda.Core;
using Sonda.Core.Services;
using Sonda.Data;
using Sonda.Data.Security;
using Sonda.Services;

namespace Sonda.Api
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {

                        //builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("Content-Disposition");
                        builder.WithOrigins("http://sonda-front-end.000webhostapp.com").AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("Content-Disposition");
                    });

            });
            services.AddControllers();
            services.AddDbContext<AplicationDbContext>(options
              => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"),
               x => x.MigrationsAssembly("Sonda.Data")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<AplicationDbContext>().AddDefaultTokenProviders();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer=false,
                    ValidateAudience=false,
                    ValidateLifetime=true,
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"])),
                    ClockSkew=TimeSpan.Zero
                });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<ITipoClienteService, TipoClienteService>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Sonda", 
                    Version = "v1",
                    Description="Clientes",
                    TermsOfService= new Uri("https://foundation.wikimedia.org/wiki/Terms_of_Use/en"),
                    License = new OpenApiLicense()
                    {
                        Name="MIT",
                        Url= new Uri ("https://opensource.org/licenses/MIT")
                    },
                    Contact=new OpenApiContact()
                    {
                        Name="Hernan Arturo cama Chamorro",
                        Email="hernan.arturo.cama.chamorro@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/hernan-cama-886b60147/")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddAutoMapper(typeof(Startup));

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
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sonda Api V1");
            });
           
        }
    }
}
