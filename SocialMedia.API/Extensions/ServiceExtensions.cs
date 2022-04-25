using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace SocialMedia.API.Extensions
{
    public class AuthOptions
    {
        public string Issuer = "SocialMedia"; // издатель токена
        public string Audience = "SocialMediaClient"; // потребитель токена
        public int LIFETIME = 60; // время жизни токена - 1 минута

        public TokenValidationParameters TokenValidation;
        public string Key { get; private set; }
        public SymmetricSecurityKey SymmetricSecurityKey { get => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key)); }
        public SigningCredentials Сredentials { get => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature); }

        public AuthOptions(string key)
        {
            Key = key;

            TokenValidation = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = Issuer,

                ValidateAudience = true,
                ValidAudience = Audience,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SymmetricSecurityKey,
            };
        }

    }

    public static class ServiceExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, AuthOptions authOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = authOptions.TokenValidation;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Web API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
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
                    }
                );
            });
        }
    }
}
