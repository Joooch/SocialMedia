using Microsoft.Extensions.FileProviders;
using SocialMedia.API.Extensions;
using SocialMedia.API.Middleware;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

var services = builder.Services;


services
    .AddAPIServices(config)
    .AddControllers();

services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});


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

app.UseExceptionMiddleware();

app.UseRouting();
app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<LoggerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseDatabaseTransaction();

app.MapControllers();

app.Run();
