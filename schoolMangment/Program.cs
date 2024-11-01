using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using schoolMangment.Data;
using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;
using schoolMangment.Repository.RepoClass;
using schoolMangment.Profiles;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using schoolMangment.UnitOfWork;
using schoolMangment.UnitOfWork.BaseClass;

namespace schoolMangment
{
    public class Program
    {
        public static async Task Main(string[] args) // Change here
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Repo service
            builder.Services.AddScoped<IUnitOfWork, Unit>();
            builder.Services.AddScoped<IRepsitory<Student>, StudentRepository>();
            builder.Services.AddScoped<IStudent, StudentRepository>();
            builder.Services.AddScoped<ITeacher, TeacherRepository>();
            builder.Services.AddScoped<IRepsitory<Course>, CourseRepostiory>();
            builder.Services.AddScoped<IExamStudent, ExamRepository>();
            builder.Services.AddScoped<IRepsitory<Teacher>, TeacherRepository>();
            builder.Services.AddScoped<IRepsitory<Department>, DepartmentReository>();
            builder.Services.AddScoped<IRepsitory<Class>, ClassRepository>();
            builder.Services.AddScoped<IRepsitory<Exam>, ExamRepository>();
            builder.Services.AddScoped<IClass, ClassRepository>();
            builder.Services.AddScoped<IDepartment, DepartmentReository>();


            // Disable automatic validation
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Add AutoMapper services
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(
                builder.Configuration.GetConnectionString("conn")));
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            // Authentication
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:valid_issur"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:valdid_audiance"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration["JWT:secret"].PadRight(48)))
                };
            });

            builder.Services.AddCors(x =>
            {
                x.AddPolicy("mypolicy", y =>
                {
                    y.AllowAnyOrigin().AllowAnyMethod();
                });
            });

            // Swagger configuration
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("mypolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seedData = new SeedData();
                await seedData.InitializeAsync(services);
            }

            await app.RunAsync(); // Make sure to await RunAsync
        }
    }
}
