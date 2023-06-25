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
using SmartClassAPI.Data;
using SmartClassAPI.Repository;
using SmartClassAPI.Repository.DevicesRepo;
using SmartClassAPI.Repository.LopHocRepo;
using SmartClassAPI.Repository.MonHocRepo;
using SmartClassAPI.Repository.PhongHocRepo;
using SmartClassAPI.Repository.TaiLieuRepo;
using SmartClassAPI.Repository.TKBRepo;
//using SmartClassAPI.Repository.ThoiKhoaBieu;
using SmartClassAPI.Repository.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartClassAPI.HubConfig;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SmartClassAPI
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllers();


            // enable Cors
            services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                );
            });
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;


            }).AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                //options.LoginPath = "/Home/Login";
                //options.AccessDeniedPath = "/Home/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddHttpContextAccessor();
            // end
            // realTime singalIR
            //services.AddSignalR(options =>
            //{
            //    options.EnableDetailedErrors = true;
            //});
            services.AddSignalR();
            //services.AddSingleton<TimerManager>();

            //end SingalIR

            services.AddDbContext<MyDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("MyDB"));
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILopHocRepository, LopHocRepository>();
            services.AddScoped<IMonHocRepository, MonHocRepository>();
            services.AddScoped<ITaiLieuRepository, TaiLieuRepository>();
            services.AddScoped<IPhongHocRepository, PhongHocRepository>();
            services.AddScoped<ITkbRepository, TkbRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartClassAPI", Version = "v1" });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartClassAPI v1"));
            }

            //app.UseHttpsRedirection();

            app.UseCors("CorsPolicy"); // enable Cors

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<BroadcastHub>("/notify");
                endpoints.MapHub<BroadcastHub>("/detect");
            });
        }
    }
}
