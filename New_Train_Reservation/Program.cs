using Microsoft.EntityFrameworkCore;
using New_Train_Reservation.Data;
public class Program
{
    public static void Main(string[] args)
    {


        var builder = WebApplication.CreateBuilder(args);

        //Session
        
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        //Configration
        builder.Services.AddDbContext<ApplicationDBcontext>
            (Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection"));
            });



        builder.Services.AddSession(
            options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            }

            );

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
            pattern: "{controller=User}/{action=Index}/{id?}");
        app.UseSession();
        app.Run();
    }
}
