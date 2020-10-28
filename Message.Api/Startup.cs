using FluentValidation.AspNetCore;
using Message.Api.Core;
using Message.Api.Validation;
using Message.Data.DAL;
using Message.Data.DAL.Repository;
using Message.Data.DAL.Repository.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace Message.Api
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
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IUploadFile, UploadFile>();



            services.AddControllers();

            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                fv.RegisterValidatorsFromAssemblyContaining<RegisterValidation>();
                fv.RegisterValidatorsFromAssemblyContaining<ApplicationUserUpdateValidation>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MessageContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            db.Database.EnsureCreated();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
