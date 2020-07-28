using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rotativa.AspNetCore;
using WebApplication7.Areas.Identity.Data;
using WebApplication7.Data;
using WebApplication7.Models;

namespace WebApplication7
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ThesisDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("ThesisDbContextConnection")));

            services.AddIdentity<ThesisDbUser, IdentityRole>()
                .AddEntityFrameworkStores<ThesisDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WebAppDb")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ThesisDbUser> um, RoleManager<IdentityRole> rm)
        {

            CultureInfo customCulture = new CultureInfo("de-DE");
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            CultureInfo.DefaultThreadCurrentCulture = customCulture;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            RotativaConfiguration.Setup(env);

            CreateUserRoles(um, rm).Wait();
        }

        private async Task CreateUserRoles(UserManager<ThesisDbUser> um, RoleManager<IdentityRole> rm)
        {

            ThesisDbUser user = await um.FindByNameAsync("samil-gencaslan@outlook.de");
            if (user == null)
            {
                user = new ThesisDbUser() { Email = "samil-gencaslan@outlook.de", UserName = "samil-gencaslan@outlook.de" };
                await um.CreateAsync(user, "_Samil123");
            }

            IdentityRole role = await rm.FindByNameAsync("Administrator");
            if (role == null)
            {
                role = new IdentityRole("Administrator");
                await rm.CreateAsync(role);
            }

            bool inrole = await um.IsInRoleAsync(user, "Administrator");
            if (!inrole)
                await um.AddToRoleAsync(user, "Administrator");

            return;

        }
    }
}
