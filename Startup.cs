using System.Threading.Tasks;
using cs_api.Models;
using cs_api.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace cs_api {
    public class Startup {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        readonly string My_CORS_Policy = "My_CORS_Policy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            services.AddCors(option => option.AddPolicy("My_CORS_Policy", builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }));
            services
                .AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddDbContext<MainContext>(item => item.UseSqlServer(Configuration.GetConnectionString("mydb")));
            services.AddScoped<IPersonRepository, PersonRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseCors(My_CORS_Policy);

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseHsts();

            app.UseMvc();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}