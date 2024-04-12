using DeratizacijaAPP.Data;
using Microsoft.EntityFrameworkCore;
using DeratizacijaAPP.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDeratizacijaSwaggerGen();
builder.Services.AddDeratizacijaCORS();


// dodavanje baze podataka
builder.Services.AddDbContext<DeratizacijaContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString(name: "DeratizacijaContext"))
);

// SECURITY

// https://www.youtube.com/watch?v=mgeuh8k3I4g&ab_channel=NickChapsas
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MojKljucKojijeJakoTajan i dovoljno dugačak da se može koristiti")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false
    };
});

builder.Services.AddAuthorization();


// END SECURITY

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
// mogućnost generiranja poziva rute u CMD i Powershell
app.UseSwaggerUI(opcije =>
{
    opcije.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    opcije.ConfigObject.
    AdditionalItems.Add("requestSnippetsEnabled", true);

});
//}

app.UseHttpsRedirection();

// SECURITY
app.UseAuthentication();
app.UseAuthorization();
// END SECURITY

app.MapControllers();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseDefaultFiles();

app.UseDeveloperExceptionPage();

app.MapFallbackToFile("index.html");

app.Run();