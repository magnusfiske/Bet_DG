using Bet.Token.API.Services;
using Bet.Users.Database.Contexts;
using Bet.Users.Database.Entities;
using Bet.Users.Database.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy => {
    policy.AddPolicy("CorsAllAccessPolicy", opt =>
        opt.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SigningSecret"]));//(Convert.FromBase64String(builder.Configuration["Jwt:SigningSecret"]));

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ClockSkew = TimeSpan.Zero
        };
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddDbContext<BetUserContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("BetUserConnection")));

builder.Services.AddIdentity<BetUser, IdentityRole>()
 .AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<BetUserContext>()
 .AddDefaultTokenProviders();

RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsAllAccessPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();


void RegisterServices(IServiceCollection services)
{
    services.AddScoped<IUserService, UserService>();
    services.AddTransient<ITokenService, TokenService>();
}