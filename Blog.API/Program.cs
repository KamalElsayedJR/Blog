using Blog.Application.Interfaces;
using Blog.Application.Mapping;
using Blog.Application.Services;
using Blog.Domain.Interfaces;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Blog.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<BlogDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICommentService, CommentServices>();
            builder.Services.AddScoped<IPostServices,PostService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var sercive = scope.ServiceProvider;
            var loggerFactory = sercive.GetService<ILoggerFactory>();
            try
            {
                var DbContext = sercive.GetService<BlogDbContext>();
                await AppDbContextDataSeeding.DataSeed(DbContext);
            }
            catch (Exception ex) 
            {
                var Logger = loggerFactory.CreateLogger<Program>();
                Logger.LogError(ex.Message);
            }


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
