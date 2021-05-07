using AVA.ForBrain.ApplicationCore.AppSetting;
using AVA.ForBrain.ApplicationCore.Interfaces.Repositories;
using AVA.ForBrain.ApplicationCore.Interfaces.Services;
using AVA.ForBrain.ApplicationCore.ModelAssistant;
using AVA.ForBrain.ApplicationCore.Services;
using AVA.ForBrain.ApplicationCore.Services.Common;
using AVA.ForBrain.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AVA.ForBrain.Infrastructure.Persistence;
using AVA.ForBrain.ApplicationCore.Interfaces.Repositories.Common;
using AVA.ForBrain.Infrastructure.Context;
using AVA.ForBrain.Infrastructure.Uow;
using Microsoft.OpenApi.Models;

namespace AVA.ForBrain
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string CorsPolicy = "AVAForBrainCorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettingsSecret");

            services.Configure<AppSettingsSecret>(appSettingsSection);
            services.AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions(opt =>
                {
                    var serializerOptions = opt.JsonSerializerOptions;
                    serializerOptions.IgnoreNullValues = true;
                    serializerOptions.IgnoreReadOnlyProperties = false;
                    serializerOptions.WriteIndented = true;
                });

            var appSettings = appSettingsSection.Get<AppSettingsSecret>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            MongoDbPersistence.Configure();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MongoDB Repository Pattern and Unit of Work - AVA ForBrain",
                    Description = "Swagger surface",
                    Contact = new OpenApiContact
                    {
                        Name = "Atylla Macedo",
                        Email = "projetointegrado@gmail.com",                       
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",                       
                    }
                });
            });

            RegisterServices(services);

            services.AddControllers()
                .AddNewtonsoftJson(options => options.UseMemberCasing());
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

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Repository Pattern and Unit of Work API v1.0");
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();

        }
    }
}
