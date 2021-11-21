using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Talkish.Dal;
using Talkish.Dal.Repositories;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Profiles;
using Talkish.Services;

namespace Talkish.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Talkish.API", Version = "v1" });
            });
            services.AddCors(o => o.AddPolicy("Policy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));

            var connectionString = Configuration.GetConnectionString("Default");

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Talkish.API v1"));
            }

            // Call CORS policy last
            app.UseCors("Policy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
