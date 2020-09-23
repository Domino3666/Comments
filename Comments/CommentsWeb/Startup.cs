using Comments.DB.Models;
using Comments.Implementation;
using Comments.Implementation.Helpers;
using Comments.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetEscapades.Extensions.Logging.RollingFile;
using Newtonsoft.Json.Serialization;

namespace CommentsWeb
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
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddLogging(config =>
            {
                config.ClearProviders();
                config.AddConfiguration(Configuration.GetSection("Logging"));
                config.AddDebug();
                config.AddFile(options =>
                {
                    options.FileName = "logFile-"; // The log file prefixes
                    options.LogDirectory = "LogFiles"; // The directory to write the logs
                    options.FileSizeLimit = 20 * 1024 * 1024; // The maximum log file size (20MB here)
                    options.Extension = "log"; // The log file extension
                    options.Periodicity = PeriodicityOptions.Daily; // Roll log files hourly instead of daily.
                });
            });

            services.AddEntityFrameworkSqlite().AddDbContext<CommentsContext>();
            services.AddScoped<IComment, Comment>();
            services.AddScoped<ICommentsRepo, CommentsRepo>();
            services.AddScoped<ICommentLogger, CommentLogger>();            

            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                             .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)                             
                             .AddNewtonsoftJson(opt =>
                             {
                                 if (opt.SerializerSettings.ContractResolver != null)
                                 {
                                     var resolver = opt.SerializerSettings.ContractResolver as DefaultContractResolver;
                                     resolver.NamingStrategy = null;
                                 }
                                 //opt.SerializerSettings.DateFormatString = "dd.MM.yyyy";   
                                 // rekurzivna serializacia
                                 //opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
