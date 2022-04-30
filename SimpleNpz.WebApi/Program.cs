using Microsoft.EntityFrameworkCore;
using SimpleNpz.Domain.Repositories;
using SimpleNpz.Persistence;
using SimpleNpz.Persistence.Repositories;
using SimpleNpz.Services;
using SimpleNpz.Services.Abstractions;
using SimpleNpz.WebApi;
using SimpleNpz.WebApi.Backgrounds;
using SimpleNpz.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers().AddApplicationPart(typeof(SimpleNpz.Presentation.AssemblyReference).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddDbContext<RepositoryDbContext>(dBuilder =>
{
    var connString = "Host=localhost;Port=5432;Database=SimpleNpz;User Id=erpuser;Password=71567zz";
    dBuilder.UseNpgsql(connString);

});
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddHostedService<TanksUpdaterHostedService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
