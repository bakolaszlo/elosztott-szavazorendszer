using Microsoft.EntityFrameworkCore;
using Osztott_szavazasi_rendszer;
using Osztott_szavazasi_rendszer.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var connectionString = StartupHelper.GetConnectionString(builder.Configuration);
Console.WriteLine(connectionString);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

try
{
    StartupHelper.ApplyMigrations();
}
catch (Exception ex)
{
    throw new Exception("Failed to apply migrations. Stopping service.", ex);
}

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSpaStaticFiles(configuration =>
 {
     configuration.RootPath = "wwwroot";
 });

 var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(
    options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
);
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseRouting();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<VoteHub>("/votehub");
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "wwwroot";
});
app.Run();
