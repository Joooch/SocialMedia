using Microsoft.Extensions.FileProviders;
using SocialMedia.API.Extensions;
using SocialMedia.API.Middleware;
using SocialMedia.Chat;
using SocialMedia.Chat.Extensions;
using System.Net.WebSockets;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.Development.json", optional: true);
builder.Configuration.AddEnvironmentVariables();

var config = builder.Configuration;
var services = builder.Services;

services
    .AddAPIServices(config)
    .AddChat()
    .AddControllers();

services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});


var app = builder.Build();

app.UseWebSockets();
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
// app.UseHttpsRedirection();

var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Static/img"));
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = "/img"
});

app.UseExceptionMiddleware();

app.UseRouting();
app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<LoggerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseDatabaseTransaction();

app.MapControllers();

app.Run();
