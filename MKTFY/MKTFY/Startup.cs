using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MKTFY.App;
using MKTFY.App.Helpers;
using MKTFY.App.Repositories;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Middleware;
using MKTFY.Models.Entities;
using MKTFY.Swashbuckle;
using Stripe;
using System.IO;

namespace MKTFY
{
    /// <summary>
    /// start up program
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StripeConfiguration.ApiKey = Configuration.GetValue<string>("StripeSettings:PrivateKey");
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>        
        public void ConfigureServices(IServiceCollection services)
        {

            // Set up the database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                b =>
                {
                    b.MigrationsAssembly("MKTFY.App");
                })
            );

            // Add Indentity Framework
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Adding Stripe configuration
            services.Configure<StripeSettings>(Configuration.GetSection("StripeSettings"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration.GetSection("Identity").GetValue<string>("Authority");

                    // name of the API resource
                    options.ApiName = "mktfyapi";
                    options.RequireHttpsMetadata = false;
                });
            //services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllers();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "MKTFY API", Version = "v1" });

                s.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
                s.OperationFilter<AuthHeaderOperationFilter>();

                var pathApi = Path.Combine(System.AppContext.BaseDirectory, "MKTFY.xml");
                var modelsPath = Path.Combine(System.AppContext.BaseDirectory, "MKTFY.Models.xml");
                s.IncludeXmlComments(pathApi);
                s.IncludeXmlComments(modelsPath);
            });

            //Adding Other Repository Controller -services
            services.AddTransient<IMailService, SendGridMailService>();     // SendGridMail services
            services.AddScoped<IListingRepository, ListingRepository>();  // Listing Services
            services.AddScoped<IUserRepository, UserRepository>();      //User Services
            services.AddScoped<IFAQRepository, FAQRepository>();        //FAQ
            services.AddScoped<ICategoryRepository, CategoryRepository>();  //Adding category services  
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            if (!env.IsProduction()) // use swagger for documentation
            {
                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "MKTFY API V1");
                });
            }

            // Global error handler
            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // My other endpoints are as follows
        }
    }
}
