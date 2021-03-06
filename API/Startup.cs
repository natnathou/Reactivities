using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;


namespace API
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
      services.AddDbContext<DataContext>(opt =>
      {
        opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
      });
      // services.AddControllersWithViews();
      services.AddControllers();

      services.AddSpaStaticFiles(configuration =>
           {
             configuration.RootPath = "../Client/build";
           });
    }


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // else
      // {
      //   app.UseExceptionHandler("/Error");
      //   // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
      //   app.UseHsts();
      // }

      // app.UseHttpsRedirection();

      app.UseRouting();
      app.UseStaticFiles();
      app.UseSpaStaticFiles();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.UseSpa(spa =>
            {
              spa.Options.SourcePath = "../Client";

              if (env.IsDevelopment())
              {
                spa.UseReactDevelopmentServer(npmScript: "start");
              }
            });
    }
  }
}
