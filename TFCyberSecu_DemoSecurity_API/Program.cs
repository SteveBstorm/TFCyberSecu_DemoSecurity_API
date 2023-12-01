using DemoSecurity_BLL.Interface;
using DemoSecurity_BLL.Services;
using DemoSecurity_DAL.Interface;
using DemoSecurity_DAL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.Text;
using TFCyberSecu_DemoSecurity_API.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Enregistrement des services (Injection de dépendance)

builder.Services.AddTransient(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("default")));

builder.Services.AddScoped<IUserRepository, UserService>();
builder.Services.AddScoped<IUserBLLService, UserBLLService>();
builder.Services.AddScoped<JwtGenerator>();

builder.Services.AddScoped<IArticleRepository, ArticleService>();
builder.Services.AddScoped<IArticleBLLService, ArticleBLLService>();

#endregion

#region Authentification
//package nuget => Microsoft.AspNetCore.Authentication.JwtBearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("tokenInfo").GetSection("secretKey").Value)),
            ValidateLifetime = true,
            ValidateIssuer = false,
            ValidAudience = "adresseDuSiteClient.com"
        };
    }
    );

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adminPolicy", policy => policy.RequireRole("Admin")); //Lire la valeur du claim Role dans le token
    //options.AddPolicy("modoPolicy", policy => policy.RequireRole("Modo", "Admin")); // multiple role
    options.AddPolicy("connectedPolicy", policy => policy.RequireAuthenticatedUser()); //Vérifier que le token est bien valide
});

#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//Bien mettre ces 2 méthodes dans cet ordre précis !!!
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
