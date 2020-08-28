using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NotepadBackend.JWS;
using NotepadBackend.Model;
using NotepadBackend.Model.Repository;

namespace NotepadBackend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration config) => Configuration = config;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers().AddNewtonsoftJson();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<INoteRepository, NoteRepository>();
            services.AddTransient<IJwtService, JwtService>();
            string conString = Configuration["ConnectionString:DefaultConnection"];
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(conString));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = TokenConfiguration.Issuer,
                    ValidateAudience = true,
                    ValidAudience = TokenConfiguration.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = TokenConfiguration.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers());
        }
    }
}
