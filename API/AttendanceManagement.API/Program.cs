using AttendanceManagement.Domain;
using AttendanceManagement.Domain.Interfaces.IRepos;
using AttendanceManagement.Domain.Interfaces.IServices;
using AttendanceManagement.Domain.Repositories;
using AttendanceManagement.Infrastructure.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AttendanceManagementDBContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AMSConnectionString")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IMapper, Mapper>();

// Repositories
builder.Services.AddScoped(typeof(IRepo<>),typeof(Repository<>));
builder.Services.AddScoped<IAttendeeRepo, AttendeeRepo>();

// Services
builder.Services.AddScoped<IAttendeeService, AttendeeService>();


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

app.Run();
