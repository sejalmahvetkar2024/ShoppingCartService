
using Microsoft.EntityFrameworkCore;
using System;
using WalletService.Interface;
using WalletService.Models;
using WalletService.Repository;

namespace WalletService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ShoppingCartContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("mycon")));

            builder.Services.AddScoped<IStatement, StatementRepository>();
            builder.Services.AddScoped<IWallet, WalletRepository>();    


            builder.Services.AddControllers();

           

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //////////////////////////////////////////////CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins("http://localhost:5056")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var app = builder.Build();
            app.UseCors("AllowSpecificOrigin");
            ////////////////////////////////////

            //var app = builder.Build();

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
        }
    }
}
