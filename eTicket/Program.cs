using eTicket.Data;
using eTicket.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace eTicket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Dbcontext configuration
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
            builder.Services.AddDbContext<AppDbcontext>(options => options.UseSqlServer(connectionString));

            //Service configuration
            builder.Services.AddScoped<IActorsServices, ActorsService>();
            builder.Services.AddScoped<IProducersService, ProducerService>();
            builder.Services.AddScoped<ICinemasService, CinemaService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //Seed database
            AppDbInitializer.Seed(app);

            app.Run();
        }
    }
}