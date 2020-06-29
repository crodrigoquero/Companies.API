using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Companies.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Companies.API
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
            services.AddCors();  //Cross-Origin Requests: enable cross origin resources sharing on my ASP.NET Core Web API
            services.AddControllers();

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("CompaniesAPISpec",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Companies API ",
                        Version = "1",
                        Description = "This is the API description",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "Carlos@dominio.com",
                            Url = new Uri("https://findCarlosRodrigo.com")

                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://en.wipidepedia.org/wiki/MIT_License")
                        }
                    });

                //options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //{
                //    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                //    In = ParameterLocation.Header,
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //}); // other authentication Methods can be added


                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new List<string>()

                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

            }); // SWAGGER SETUP




            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddMvc().AddXmlSerializerFormatters(); // CONTENT NEGOTIATION SETUP: in order to be able to return content in XML or Json format
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));


            // connection string for mysql
            //"DefaultConnection": "Server=localhost,1433; Database=testFromMacOsDb2; User=sa; Password=reallyStrongPwd123;",

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Configuration["Tokens:Issuer"],
                   ValidAudience = Configuration["Tokens:Issuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                   ClockSkew = TimeSpan.Zero,
               };
           });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbCojones, IDbInitializer dbInit)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //dbCojones.Database.EnsureCreated(); // another addition for mySql

            app.UseHttpsRedirection();
            app.UseSwagger(); // SWAGGER SETUP
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/CompaniesAPISpec/swagger.json", "Companies API");
                options.RoutePrefix = "";

            });

            app.UseRouting();

            dbInit.Initialize(); // INTIALIZES THE DATABASE

            // enable cross origin resources sharing on my ASP.NET Core Web API (Cross-Origin Requests)
            app.UseCors(X => X
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            ); // That's it. Every client has access to my ASP.NET Core Website/API.

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }

    }
}
