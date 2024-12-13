using Aspire.Hosting;
using DeviceAPI.Context;
using DeviceAPI.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
//var builder = DistributedApplication.CreateBuilder(args);

//// Add services to the container.
//var postgres = builder.AddPostgres("postgres").WithPgAdmin().WithDataVolume(isReadOnly: false);
//var postgresdb = postgres.AddDatabase("postgresdb");

//var exampleProject = builder.AddProject<Projects.DeviceAPI>()
//                            .WithReference(postgresdb);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("DeviceService", client =>
{
    client.BaseAddress = new Uri("http://localhost:7054/");
});

//builder.Services.AddScoped<IQueueService, QueueService>();

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7045/") });

//var connUser = builder.Configuration.GetConnectionString("UserDbConnString");
//builder.Services.AddEntityFrameworkNpgsql().AddDbContext<UserContext>(opt => opt.UseNpgsql(connUser));

var connDevice = builder.Configuration.GetConnectionString("DeviceDbConnString");
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DeviceContext>(opt => opt.UseNpgsql(connDevice));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(op =>
{
    op.AllowAnyHeader();
    op.AllowAnyMethod();
    op.AllowAnyOrigin();

});

app.Run();