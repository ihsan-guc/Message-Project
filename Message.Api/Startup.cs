using FluentValidation.AspNetCore;
using Message.Api.Core;
using Message.Api.Validation;
using Message.Data.DAL;
using Message.Data.DAL.Repository;
using Message.Data.DAL.Repository.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            
            //var messageDB = Configuration.GetConnectionString("MessageDB");
            //services.AddDbContext<MessageContext>(opt => opt.UseSqlServer(messageDB));
            //services.AddDbContext<InMemoryContext>(opt => opt.UseInMemoryDatabase("MessageInMemory"));
            services.AddScoped<MessageContext, MessageContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IUploadFile, UploadFile>();
            services.AddSwaggerDocument(config => {
                config.PostProcess = (doc =>
                {
                    doc.Info.Title = "Message Api";
                    doc.Info.Version = "1.0.5";
                    doc.Info.Contact = new NSwag.OpenApiContact() {
                        Email = "ihsanguc.33@gmail.com",
                        Name = "Ýhsan Güç",
                        Url = "https://www.linkedin.com/in/ihsan-g%C3%BC%C3%A7-873024156/",
                    };
                });
            });

            services.AddControllers();

            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                fv.RegisterValidatorsFromAssemblyContaining<RegisterValidation>();
                fv.RegisterValidatorsFromAssemblyContaining<ApplicationUserUpdateValidation>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseOpenApi();

            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
