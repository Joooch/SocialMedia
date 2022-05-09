using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SocialMedia.API;
using SocialMedia.API.Exceptions;
using SocialMedia.API.Extensions;
using SocialMedia.API.Middleware;
using SocialMedia.Common.Dtos.User;
using SocialMedia.Infrastructure;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
/*builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureEndpointDefaults(cfg => {})
});*/


var services = builder.Services;
var authOptions = new AuthOptions(config["Secret"]);


services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddAuthorization();
services.AddAutoMapper(typeof(AutoMapperConfiguration));

services.AddSingleton<AuthOptions>(authOptions);
services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
services.AddScoped(typeof(IProfileRepository), typeof(ProfileRepository));

services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(config["DefaultConnection"]);
});
services.AddJwtAuthentication(authOptions);
services.AddControllers();

services.AddCors(c => { c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()); });

var app = builder.Build();

app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}
app.UseHttpsRedirection();


var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Static/img"));
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = "/img"
});

//app.UseCookiePolicy();
app.UseRouting();
app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<LoggerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
