using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeaTime.Api.TeaTimeMapper;
using TeaTime.Services;
using AutoMapper;
using System.Reflection;
using System.IO;
using System;

namespace TeaTime.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
         

            string conn = this.Configuration.GetConnectionString("DBCS");
            services.AddDbContext<TeaTime.Data.TeaTimeContext>(options => options.UseSqlServer(conn, b => b.MigrationsAssembly("TeaTime.Api")));


            services.AddScoped<INationalParkService, NationalParkService>();
            services.AddAutoMapper(typeof(TeaTimeMapping));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("TeaTimeApi",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Mahipal's National Park API",
                        Version = "1",
                        Description = "Mahipal'Api",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "themahipalthakur@gmail.com",
                            Name = "Mahipal Thakur"
                            //Url = new Uri("www.iammahipal.com")
                        },

                    }); 
                //This Code is Written Here for Xml Comment but not working throwing run time error will fix latter
                //var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var cmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                //options.IncludeXmlComments(cmlCommentFullPath);
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>

            options.SwaggerEndpoint("/swagger/TeaTimeApi/swagger.json", "Tea Time Api"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
