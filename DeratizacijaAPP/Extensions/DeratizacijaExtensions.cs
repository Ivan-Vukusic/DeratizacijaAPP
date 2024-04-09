using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DeratizacijaAPP.Extensions
{
    public static class DeratizacijaExtensions
    {

        public static void AddDeratizacijaSwaggerGen(this IServiceCollection Services)
        {
            // prilagodba za dokumentaciju, čitati https://medium.com/geekculture/customizing-swagger-in-asp-net-core-5-2c98d03cbe52

            Services.AddSwaggerGen(sgo =>
            { // sgo je instanca klase SwaggerGenOptions
              // čitati https://devintxcontent.blob.core.windows.net/showcontent/Speaker%20Presentations%20Fall%202017/Web%20API%20Best%20Practices.pdf
                var o = new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Deratizacija API",
                    Version = "v1",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "ivukusic27@gmail.com",
                        Name = "Ivan Vukušić"
                    },
                    Description = "Ovo je dokumentacija za Deratizacija API"
                };
                sgo.SwaggerDoc("v1", o);

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                sgo.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

            });

        }


        public static void AddDeratizacijaCORS(this IServiceCollection Services)
        {
            // Svi se od svuda na sve moguće načine mogu spojiti na naš API
            // Čitati https://code-maze.com/aspnetcore-webapi-best-practices/

            Services.AddCors(opcije =>
            {
                opcije.AddPolicy("CorsPolicy",
                    builder =>
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );

            });

        }
    }
}
