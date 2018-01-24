using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using MiddlewareApp.Middlewares;

namespace MiddlewareApp
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
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /*中间件添加顺序
             1.Exception / error handling
             2.Static file server
             3.Authentication
             4.MVC
             */

            // 类
            app.UseRequesetBegin();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // 匿名方法中间件
            app.Map("/map1", HandleMapTest1);
            app.Map("/map2", HandleMapTest2);
            app.Map("/map", HandleMapMultiSet);
            app.MapWhen(context => context.Request.Query.ContainsKey("from"), HandleMapWhen);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void HandleMapTest1(IApplicationBuilder app)
        {
            app.Run(async context => {
                await context.Response.WriteAsync("Map Test1");
            });
        }

        private static void HandleMapTest2(IApplicationBuilder app)
        {
            app.Run(async context => {
                await context.Response.WriteAsync("Map Test2");
            });
        }

        private static void HandleMapMultiSet(IApplicationBuilder app)
        {
            app.Map("/sub1", subApp1 => {
                subApp1.Run(async c => { await c.Response.WriteAsync("Map Sub1"); });
            });
            app.Map("/sub2", subApp2 => {
                subApp2.Run(async c => { await c.Response.WriteAsync("Map Sub2"); });
            });
        }

        private static void HandleMapWhen(IApplicationBuilder app)
        {
            app.Run(async context => {
                var from = context.Request.Query["from"];
                await context.Response.WriteAsync($"FromSite is {from}");
            });
        }
    }
}
