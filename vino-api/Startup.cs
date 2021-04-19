using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace vino_api
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VinoDbContext>(config =>
            {
                config.UseSqlServer(
                    _configuration.GetConnectionString("LocalDBConnection")
                );
                // config.UseInMemoryDatabase("Memory");
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<VinoDbContext>()
                .AddDefaultTokenProviders();

            // services.AddMvc();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    // options.RequireHttpsMetadata = false;
                    IConfigurationSection googleAuthNSection =
                        _configuration.GetSection("Authentication:Google");

                    options.Authority = googleAuthNSection["ClientId"];
                    options.Audience = googleAuthNSection["ClientId"];
                    // options.ClientSecret = googleAuthNSection["ClientSecret"];

                    // options.ClaimsIssuer = "accounts.google.com";

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidIssuer = "accounts.google.com"
                    };
                    options.RequireHttpsMetadata = false;
                    // options.AutomaticAuthenticate = true;
                    // options.AutomaticChallenge = false;
                });


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
}
