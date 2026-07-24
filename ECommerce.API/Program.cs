
using ECommerce.API.Extentions;
using ECommerce.Application;
using ECommerce.Application.Profiles;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Threading.Tasks;

namespace ECommerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddInfrastuctureServices(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddApplicationServices();

            builder.Services.Configure<UrlSettings>(builder.Configuration.GetSection(nameof(UrlSettings)));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            await app.MigrationAndSeedAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Files")),
                RequestPath = "/Files"
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
