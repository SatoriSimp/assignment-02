using BusinessObjects;
using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SilverJewelry2023DbContext>();
            services.AddScoped<SilverJewelryDAO>();
            services.AddScoped<BranchAccountDAO>();
            services.AddScoped<ISilverJewelryRepository, SilverJewelryRepository>();
            services.AddScoped<IBranchAccountRepository, BranchAccountRepository>();

            services
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            services.AddSwaggerGen(c =>
            {
                // Add JWT Authentication support to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by a space and the JWT token."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            Array.Empty<string>()
                        }
                    });
                }
            );

            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<SilverJewelry>("SilverJewelry").EntityType.Ignore(c => c.CategoryId);
            odataBuilder.EntitySet<Category>("Category");

            // Add OData services and controllers
            services.AddControllers().AddOData(options =>
            {
                options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100)
                    .AddRouteComponents("api", odataBuilder.GetEdmModel()); 
            });

            services.AddRazorPages();
            services.AddEndpointsApiExplorer();

            return services;
        }
    }
}
