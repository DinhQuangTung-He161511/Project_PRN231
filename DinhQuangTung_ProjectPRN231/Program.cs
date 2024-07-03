using Data.EF;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Products;
using Service.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Data.Entities;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Identity;
using Service.Catalog.Categories;
using Service.System.Language;
using Service.System.Roles;
using Service.System.Users;
using Service.Ulitities;

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
            builder.Services.AddTransient<IStorageService, FileStorageService>();

            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();

            builder.Services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            builder.Services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            builder.Services.AddTransient<ILanguageService, LanguageService>();
            builder.Services.AddTransient<ISlideService, SlideService>();

            builder.Services.AddTransient<IRoleService, RoleService>();
            builder.Services.AddTransient<IUserService, UserService>();

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
