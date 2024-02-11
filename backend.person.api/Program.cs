using backend.person.api.Services;
using backend.person.api.Services.Interfaces;
using backend.person.datalibrary.DataContext;
using backend.person.datalibrary.Repository;
using backend.person.datalibrary.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PersonDataStringConnection");

// Add services to the container.
builder.Services.AddDbContext<IPersonDataContext, PersonDataContext>(opt =>
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#region Repository

builder.Services.AddTransient<IPersonRepository, PersonRepository>();

#endregion

#region Services

builder.Services.AddTransient<IPersonService, PersonService>();

#endregion

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();