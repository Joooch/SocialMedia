using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SocialMedia.API.Extensions;
using SocialMedia.API.Middleware;
using SocialMedia.Infrastructure;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var services = builder.Services;
var authOptions = new AuthOptions(config["Secret"]);


services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddAuthorization();

services.AddSingleton<AuthOptions>(authOptions);
services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
services.AddScoped(typeof(IProfileRepository), typeof(ProfileRepository));
services.AddAutoMapper(typeof(ApplicationDbContext));

services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(config["DefaultConnection"]);
});
services.AddJwtAuthentication(authOptions);
services.AddControllers();

services.AddCors(c => { c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()); });

var app = builder.Build();

app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Static/img"));
var requestPath = "/img";

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});

//app.UseCookiePolicy();
//app.UseRouting();
app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
