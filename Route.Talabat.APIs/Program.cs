
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Infrastructure;
using Route.Talabat.Infrastructure.Data;

namespace Route.Talabat.APIs
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Services
            webApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();
            webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options => 
            { 
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("defaultConnection"));
                options.UseLazyLoadingProxies();
            });
            webApplicationBuilder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion
            var app = webApplicationBuilder.Build();

            #region Apply Migrations & Data Seeding
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<ApplicationDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync();
                await ApplicationContextSeed.DataSeed(_dbContext);
            }
            catch (Exception)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError("An error occured during applying pending migrations");
            } 
            #endregion

            #region Kestrel Middlewares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
