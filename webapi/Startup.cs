using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BetContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BetConnection")));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var signingKey = new SymmetricSecurityKey(Convert.FromBase64String(Configuration["Jwt:SigningSecret"]));

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


            services.AddAuthorization(options =>
            {
                options.AddPolicy("Registered", policy => policy.RequireClaim("Registered", "true"));
                options.AddPolicy("Admin", policy => policy.RequireClaim("Admin", "true"));
            });
        }
    }
}
