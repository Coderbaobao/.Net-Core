using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dnc.DataAccessRepository.Context;
using Dnc.DataAccessRepository.Repositories;
using Dnc.DataAccess.Seeds;
using Dnc.DataAccessRepository.Utilities;
using Dnc.DataAccessRepository.Seeds;

namespace Dnc.Services
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // 添加跨域访问授权处理

            services.AddCors(option => option.AddPolicy("DncDemo", p => p.WithOrigins("http://192.168.0.5:8060",
                "http://192.168.0.5:8060").AllowAnyHeader().AllowAnyMethod()));


            //services.AddCors(option => option.AddPolicy("DncDemo", p => p.WithOrigins("http://192.168.1.106:8060",
            //    "http://192.168.1.106:8060").AllowAnyHeader().AllowAnyMethod()));

            // 注入 DbContext 对应的数据库连接实例
            services.AddDbContext<EntityDbContext>();

            // 注入 数据服务接口实例;
            services.AddTransient<IEntityRepository, EntityRepository>();

            // 注入Session组件
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.CookieName = ".MyCoreApp";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,EntityDbContext context)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            // 启用 Session，添加 Session 的引用
            app.UseSession();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseMvc();
            app.UseCors("DncDemo");

            //种子处理
            DbInitializer.Initialze(context);

            // 初始化一些静态类
            DataAccessUtility.InitialDBContext(context);
        }
    }
}
