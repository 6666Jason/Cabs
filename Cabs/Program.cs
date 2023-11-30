using Cabs.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cabs.Areas.Website.Models;
using Cabs.Areas.Website.Services.Authenication;
using Cabs.Areas.Website.Repositories.Authenication;
using Cabs.Areas.Website.Repositories.Company;
using Cabs.Areas.Website.Services.Company;
using Cabs.Areas.Website.Repositories.UploadFileRepo;
using Cabs.Areas.Website.Services.UploadFileService;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Cabs.Areas.Website.Requests.Program;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ProgramProfile));

// config identity
builder.Services.AddIdentity<User, IdentityRole<int>>(options => options.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

// config dbcontext
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectDb"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});



// config DI
builder.Services.AddScoped<IAuthRepository, AuthServiceImp>();
builder.Services.AddScoped<ICompanyRepository, CompanyService>();
builder.Services.AddScoped<IFileRepository, FileServiceImp>();

// config cors dung tren trinh duyet
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

// config authenicate
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    //options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    //RequestPath = "/"
});
app.MapControllers();

app.Run();
