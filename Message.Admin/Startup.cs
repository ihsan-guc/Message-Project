using Message.Api.Core;
using Message.Data.DAL;
using Message.Data.DAL.Repository;
using Message.Data.DAL.Repository.Core;
using Message.Data.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Message.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var messageDB = Configuration.GetConnectionString("MessageDB");
            services.AddDbContext<MessageContext>(opt => opt.UseSqlServer(messageDB));
            services.AddDbContext<InMemoryContext>(opt => opt.UseInMemoryDatabase("MessageInMemory"));
            services.AddScoped<MessageContext, MessageContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IApplicationAdminRepository, ApplicationAdminRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IUserMessageRepository, UserMessageRepository>();
            services.AddScoped<IUploadFile, UploadFile>();

            services.AddControllersWithViews();
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", x =>
                {
                    x.Cookie.Name = "Grandmas.Cookie";
                    x.LoginPath = "/Account/SignIn";
                });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MessageContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            bool itCreated = db.Database.EnsureCreated();
            if (itCreated)
            {
                db.ApplicationAdmins.Add(new ApplicationAdmin()
                {
                    Id = Guid.NewGuid(),
                    Email = "ihsanguc.33@gmail.com",
                    FirstName = "Ýhsan",
                    LastName = "Güç",
                    Password = "123",
                });
                db.SaveChanges();
            }
            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
