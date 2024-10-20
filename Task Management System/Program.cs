
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Task_Management_System.Model;
using Task_Management_System.Reprository;

namespace Task_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            builder.Services.AddDbContext<TaskManagementContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("TasksDB"));
            });
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<TaskManagementContext>();
            builder.Services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer( option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://localhost:7055/",
                    ValidateAudience = true,
                    ValidAudience = "https://localhost:4200/",
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfghjllrtyyuuqwqeretrtqweerttyuuioplkjhgfdsaaZZXCCCVVBBN"))
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(MyAllowSpecificOrigins);

            app.MapControllers();

            app.Run();
        }
    }
}
