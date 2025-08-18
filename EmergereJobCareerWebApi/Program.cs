using Dapper;
using EmergereJobCareerWebApi;
using EmergereJobCareerWebApi.Authentication;
using EmergereJobCareerWebApi.Services;
using NLog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
using EmergereJobCareerWebApi.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore;
//using MySql.EntityFrameworkCore;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);
DefaultTypeMap.MatchNamesWithUnderscores = true;
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.AddCors();
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddRazorPages();
// Set up configuration sources.
var config = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json", optional: false)
       .Build();
var _conn = config.GetConnectionString("SqlConnection");
//For CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "http://localhost:8080", "https://emergertech.com", "https://emergerewebsite.azurewebsites.net", "https://testemergere.azurewebsites.net")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});
Console.WriteLine("Connection String: " + builder.Configuration.GetConnectionString("SqlConnection"));
//For Entity Framework 
builder.Services.AddDbContext<emergerecareerdbContext>(options => options.UseMySql(config.GetConnectionString("SqlConnection"), ServerVersion.AutoDetect(config.GetConnectionString("SqlConnection"))));
//For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<emergerecareerdbContext>()
    .AddDefaultTokenProviders();
//Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer  
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});
builder.Services.AddSingleton<IDBService, DBService>();
builder.Services.AddScoped<IJobCareerService, JobCareerService>();
builder.Services.AddScoped<IUploadResumeService, UploadResumeService>();
builder.Services.AddScoped<IContactUsService, ContactUsService>();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Place Info Service API",
        Version = "v2",
        Description = "Sample service for Learner",
    });
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.UseSwagger();
app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "PlaceInfo Services"));
app.MapRazorPages();

app.Run();
