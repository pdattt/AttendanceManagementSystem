using AttendanceManagement.Common;
using AttendanceManagement.Common.Constants;
using AttendanceManagement.Domain;
using AttendanceManagement.Domain.Interfaces.IRepos;
using AttendanceManagement.Domain.Interfaces.IServices;
using AttendanceManagement.Domain.Repositories;
using AttendanceManagement.Infrastructure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AttendanceManagementDBContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString(Constants.ConnectionString)));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IMapper, Mapper>();

// Repositories
builder.Services.AddScoped(typeof(IRepo<>), typeof(Repository<>));
builder.Services.AddScoped<IAttendeeRepo, AttendeeRepo>();
builder.Services.AddScoped<IEventRepo, EventRepo>();
builder.Services.AddScoped<IClassRepo, ClassRepo>();
builder.Services.AddScoped<ISessionRepo, SessionRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

// Services
builder.Services.AddScoped<IAttendeeService, AttendeeService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IUserService, UserService>();

// CORS
builder.Services.AddCors();

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JWT.issuer,
            ValidAudience = JWT.audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT.key))
        };
    });

// Serilog
var config = new ConfigurationBuilder().AddJsonFile(Constants.Json_AppSettings).Build();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(option =>
{
    option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();