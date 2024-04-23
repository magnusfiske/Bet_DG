using Bet.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Bet.Data.Contexts;
using Bet.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Bet.API.Services;
using Bet.Data.Services;
using BetUser = Bet.Data.Entities.BetUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy => {
    policy.AddPolicy(name: "CorsAllAccessPolicy", opt =>
        opt.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowAnyOrigin()
    );
});

builder.Services.AddIdentity<BetUser, IdentityRole>()
 .AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<BetContext>()
 .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SigningSecret"]));

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ClockSkew = TimeSpan.Zero
        };
        options.RequireHttpsMetadata = false;
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Registered", policy => policy.RequireClaim("Registered", "true"));
    options.AddPolicy("Admin", policy => policy.RequireClaim("Admin", "true"));
});

builder.Services.AddDbContext<BetContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("BetConnection")));

//builder.Services.AddDbContext<BetUserContext>(
//    options =>
//        options.UseSqlServer(
//            builder.Configuration.GetConnectionString("BetUserConnection")));


ConfigureAutomapper(builder.Services);
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

void ConfigureAutomapper(IServiceCollection services)
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Bet.Data.Entities.Bet, BetDTO>()
        .ForMember(dest => dest.BetRows, src => src.MapFrom(s => s.BetRows))
        .ForMember(dest => dest.User, src => src.MapFrom(s => s.User))
        .ReverseMap()
        .ForMember(dest => dest.BetRows, src => src.Ignore())
        .ForMember(dest => dest.User, src => src.Ignore());
        cfg.CreateMap<BetRow, BetRowDTO>()
        .ReverseMap();
        cfg.CreateMap<Team, TeamDTO>().ReverseMap();
        cfg.CreateMap<User, UserDTO>()
        .ForMember(dest => dest.Bets, src => src.MapFrom(s => s.Bets.Select(y => y.Id)))
        .ReverseMap()
        .ForMember(dest => dest.Bets, src => src.Ignore());
    });

    var mapper = config.CreateMapper();

    services.AddSingleton(mapper);
}

void RegisterServices(IServiceCollection services)
{
    services.AddScoped<IDbService, DbService>();
    services.AddScoped<IUserService, UserService>();
    services.AddTransient<ITokenService, TokenService>();
}