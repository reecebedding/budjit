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

namespace budjit.ui
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

            services.AddSingleton<BudjitContext, BudjitContext>(
                (IServiceProvider provider) => 
                {
                    string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var builder = new DbContextOptionsBuilder<BudjitContext>().UseSqlite($"DataSource={path}\\SQLite\\budjit.db");
                    
                    var context = new BudjitContext(builder.Options);

                    //Need to call migrate on construction as this allows the embedded db to be migrated
                    context.Database.Migrate();

                    return context;
                }
            );

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
