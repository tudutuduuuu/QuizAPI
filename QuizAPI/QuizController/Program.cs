using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using QuizAPI.QuizDataAccess.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<QuizDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

//Hangfire
builder.Services.AddHangfire(x => x.UseSqlServerStorage(@"Data Source=.;Initial Catalog=QuizAppDB;Integrated Security=True;Pooling=False;TrustServerCertificate=True"));
builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseCors(options =>
options.WithOrigins("http://localhost:3000")
.AllowAnyMethod()
.AllowAnyHeader());

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHangfireDashboard();
app.UseAuthorization();

app.MapControllers();

app.Run();
