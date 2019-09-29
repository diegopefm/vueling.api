using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vueling.Api.Models;
using Vueling.Data;
using Vueling.Data.Models;
using System.Linq;
using Vueling.Api.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AspNet.Security.OAuth.Validation;

namespace Vueling.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication(OAuthValidationDefaults.AuthenticationScheme).AddOAuthValidation();

            services.AddHttpContextAccessor();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddCors(o => o.AddPolicy("Cors", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowCredentials()
                       .AllowAnyHeader();
            }));

            //Add repository to scope
            services.AddScoped<UserRepository>();
            services.AddScoped<PassengerRepository>();

            //sql connection and context (with crypted pass)
            var connection = getConnectionString();
            services.AddDbContext<Context>(options => options.UseSqlServer(connection));

            //jwt
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisisasupersecuresecretkey")),
                        RequireSignedTokens = false,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://localhost:55909",
                        ValidAudience = "http://localhost:55909"
                    };
                });
        }

        private string getConnectionString() {

            var connection = Configuration.GetSection("ConnectionStrings").GetChildren().First().Value;
            IConfigurationSection crypto = Configuration.GetSection("Cryptography");
            var digit16 = crypto.GetChildren().ElementAt(0).Value;
            var digit32 = crypto.GetChildren().ElementAt(1).Value;
            var decryptoPass = Crypto.AES_decrypt("e8ExDkQlL4H7sac7GPgzqg==", digit32, digit16);

            return connection.Replace("xxx", decryptoPass);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
