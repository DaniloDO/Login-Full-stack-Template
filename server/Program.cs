using LoginApp.Data;
using LoginApp.Models;
using LoginApp.Services.Interfaces;
using LoginApp.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using DotNetEnv; 

var builder = WebApplication.CreateBuilder(args);

//Loading env variables
Env.Load();

//Add access to configuration
var configuration = builder.Configuration;

//Build connection string and add DbContext
var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
var port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
var database = Environment.GetEnvironmentVariable("POSTGRES_DATABASE");
var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

var connectionString = $"Host={host};Port={port};Database={database};Username={user};Password={password}"; 

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

//Add IdentityCore 
builder.Services.AddIdentityCore<User>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false; 
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

//Add JWT
var jwtSettings = configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

//Add Controllers + DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Add Swagger support JWT
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "LoginApp API", Version = "v1" });
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT bearer token in the textbox below. Example: 'Bearer {token}'",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    config.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

//Add application services
builder.Services.AddScoped<IAuthService, AuthService>(); 
builder.Services.AddScoped<ITokenService, TokenService>(); 

//Add Cors services
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowFrontend", 
        policy => 
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod(); 
        }
    );
});

var app = builder.Build();

// Migrate DB at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); 

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>(); 
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(); 
    await IdentityDataSeeder.SeedAsync(userManager, roleManager); 
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
//Use Cors policy
app.UseCors("AllowFrontend"); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); 

app.MapGet("/", () => "Hello World!");

app.Run();
