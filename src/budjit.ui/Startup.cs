using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ElectronNET.API;
using budjit.core.data.Contracts;
using budjit.core.data.SQLite;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using AutoMapper;

namespace budjit.ui
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<BudjitContext>(cfg =>
            {
                //Need to do connecting string initialization in code as this requires using the assemblies executing location at runtime
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SQLite");
                if(!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                
                string fullPath = Path.Combine(path, Configuration.GetConnectionString("budjit"));

                cfg.UseSqlite($"DataSource={fullPath}");
            });
            
            services.AddAutoMapper();

            services.AddTransient<ITransactionsRepository, TransactionRepository>(
                (IServiceProvider provider) => new TransactionRepository(provider.GetService<BudjitContext>())
            );

            services.AddTransient<ITagRepository, TagRepository>(
                (IServiceProvider provider) => new TagRepository(provider.GetService<BudjitContext>())
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Open the Electron-Window here
            Task.Run(async () => await Electron.WindowManager.CreateWindowAsync());
        }
    }
}
