using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TodoAppServer.Contexts;
using Microsoft.EntityFrameworkCore;
using TodoAppServer.Services;

namespace TodoAppServer
{
    public class Startup
    {
        private const string TodoDb = "TodoDb";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            services.AddDbContext<TodoDbContext>(options =>
            {
                string connectionString = Configuration.GetConnectionString(TodoDb);
                options.UseSqlServer(connectionString);
            });
            services.AddCors(options =>
            {
                string allowedOriginsStr = Configuration.GetValue<string>("AllowedOrigins");
                string[] allowedOrigins = new string[0];
                if (!string.IsNullOrEmpty(allowedOriginsStr))
                {
                    allowedOrigins = allowedOriginsStr.Split(",");
                }
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins(allowedOrigins)
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
            services.AddControllers();
            services.AddScoped<IRepositoryService, RepositoryService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoAppServer", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoAppServer v1"));
            }

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
