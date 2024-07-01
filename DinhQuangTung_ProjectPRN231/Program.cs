using Data.EF;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Products;
using Service.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DinhQuangTung_ProjectPRN231
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<EshopDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Declare DI 
            builder.Services.AddTransient<IStorageService, FileStorageService>();
            builder.Services.AddTransient<IPublicProduct, PublicProductService>();
            builder.Services.AddTransient<IManageProductService, ManagerProductService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            // Kiểm tra và thiết lập WebRootPath nếu chưa có
            var env = app.Services.GetRequiredService<IWebHostEnvironment>();
            if (string.IsNullOrEmpty(env.WebRootPath))
            {
                env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            // Ghi log đường dẫn WebRootPath
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation($"WebRootPath: {env.WebRootPath}");

            app.Run();
        }
    }
}
