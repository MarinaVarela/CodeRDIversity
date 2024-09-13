using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Config
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(SwaggerGenOptions options) => options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Refrigerator API",
            Version = "v1",
            Description =
                        @"<H2>API for Managing Refrigerator Items</H2>
                                <p> Developed by Marina Varela.</p>
                                <br>
                            <H4> IDE and Framework Version </H4>
                                <p> The project was developed using Visual Studio, with .NET 8.</p>
                            <H4> Persistence and Database</H4>
                                <p> Migration added with the following commands:</p>
                                <ul>
                                    <li> Add-Migration CreateRefrigerator: Creates the migration based on the model.</li>
                                    <li> Update-Database CreateRefrigerator: Updates the database with the changes defined in the migration.</li>
                                    <li> Script-Migration: Generates a SQL script for the migration.</li>
                                </ul> 
                            <H4> HTTP Codes Mapped in This Application: </H4>
                                <ul>
                                    <li><b>200:</b> Request was successful.</li>
                                    <li><b>204:</b> Request was successful, but there is no content to return.</li>
                                    <li><b>400:</b> Bad request, for example, when there is a validation error or incorrect information.</li>
                                    <li><b>404:</b> Not found, for example, when an item is not found in the refrigerator.</li>
                                    <li><b>500:</b> Internal server error, for example, when an unhandled exception occurs.</li>
                                </ul>",
            Contact = new OpenApiContact
            {
                Name = "Marina Varela",
                Email = "marinavareladev@gmail.com",
            }
        });
    }
}
