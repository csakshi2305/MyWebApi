
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyWebApi.Core.Dto;
using MyWebApi.Infrastructure.Data;
using MyWebApi.Infrastructure.Repository;
using MyWebApi.Infrastructure.Repository___service;
using MyWebApi.Interfaces;


namespace MyWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //   builder.Services.AddScoped<CompanyMapper>();

           builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<StuffMapping>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped<IFabricService, FabricService>();

            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
        
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IDyeTypesRepository, DyeTypesRepository>();
            builder.Services.AddScoped<IDyeTypesService,DyeTypesService>();
            builder.Services.AddScoped<IStuffService, StuffService>();
            builder.Services.AddScoped<IStuffRepo, StuffRepository>();
            builder.Services.AddScoped<IStuffService, StuffService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IAPIUserContext, APIUserContext>();
            builder.Services.AddScoped<IFabricRepository, FabricRepository>();

            // JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                            .AddJwtBearer(options =>
                  {
                 options.TokenValidationParameters = new TokenValidationParameters
                {
                      
                     ValidateIssuer = false,
                      ValidateAudience = false,
                      ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,

          ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });


            //this is for authorization button
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your token in the text input below.\n\nExample: Bearer 12345abcdef"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });


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

        
            app.UseCors("AllowAll");           // Enable CORS
            app.UseAuthentication();          // Enable JWT Authentication
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
