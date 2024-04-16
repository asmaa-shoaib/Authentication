using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using Microsoft.AspNetCore.Builder;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Humanizer.Configuration;
using Microsoft.Extensions.Options;
using BusinessObjects.Interfaces;
using Data_Access_Layer.Repository;
using BusinessObjects.Entities;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusinessObjects.Helper;
using BusinessObjects.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IGenericRepository<Car>, GenericRepository <Car>>();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICar, CarRepository>();
builder.Services.AddScoped<IBrand, BrandRepository>();
builder.Services.AddScoped<IBranch, BranchRepository>();
builder.Services.AddScoped<IPhoto, PhotoRepository>();
builder.Services.AddScoped<IDetail, DetailRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var myAllowSpecificOrigin = "_myAllowSpecificOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigin,
    policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

//builder.Services.AddDbContext<Data_Base>(opt =>
//opt.UseInMemoryDatabase("ConnStr"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddDbContext<Data_Base>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<Data_Base>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
.AddJwtBearer(options =>
{
    options.SaveToken = false;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey=true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime= true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider=new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath,"Uploads")),RequestPath= "/Uploads"
});

app.UseCors(myAllowSpecificOrigin);
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
