using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Infrastructure.Config
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Refrigerator API",
                    Version = "v1",
                    Description = @"<H2>API for Managing Refrigerator Items</H2>
                                    <p> Developed by Marina Varela.</p>
                                    <br>
                                    <H4>IDE and Framework Version </H4>
                                    <p> The project was developed using Visual Studio, with .NET 8.</p>
                                    <H4>Unit Tests</H4>
                                    <p> Unit tests are written using the xUnit framework, with AAA pattern. These tests focus on validating the core functionality of the service, covering CRUD operations and exception handling.</p>
                                    <H4>Persistence and Database</H4>
                                    <p> Migration added with the following commands:</p>
                                    <ul>
                                        <li> Add-Migration: Creates the migration based on the model.</li>
                                        <li> Update-Database: Updates the database with the changes defined in the migration.</li>
                                        <li> Script-Migration: Generates a SQL script for the migration.</li>
                                    </ul> 
                                    <H4>User Authentication</H4>
                                    <p>This API uses ASP.NET Core Identity for user management and authentication. Users are authenticated using Bearer Tokens (JWT - JSON Web Tokens), which are issued upon successful login and must be included in the Authorization header for protected endpoints.</p>
                                    <H4> HTTP Codes Mapped in This Application: </H4>
                                    <ul>
                                        <li><b>200:</b> Request was successful.</li>
                                        <li><b>201:</b> Created a new resource.</li>
                                        <li><b>204:</b> Request was successful, but there is no content to return.</li>
                                        <li><b>400:</b> Bad request, for example, when there is a validation error or incorrect information.</li>
                                        <li><b>401:</b> Unauthorized, for example, when the user is not authenticated.</li>
                                        <li><b>404:</b> Not found, for example, when an item is not found in the refrigerator.</li>
                                        <li><b>500:</b> Internal server error, for example, when an unhandled exception occurs.</li>
                                    </ul>",
                    Contact = new OpenApiContact
                    {
                        Name = "Marina Varela",
                        Email = "marinavareladev@gmail.com",
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insert the JWT token like this: Bearer {your token}",
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
                        new string[] { }
                    }
                });
            });

            return services;
        }
    }
}
