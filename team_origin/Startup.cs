using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using team_origin.Contracts;
using team_origin.Entities;
using team_origin.Entities.Notifications;
using team_origin.Services;

namespace team_origin
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
            services.AddApplicationInsightsTelemetry(Configuration);

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddJwtBearer(options => {
            //            options.TokenValidationParameters =
            //                 new TokenValidationParameters
            //                 {
            //                     ValidateIssuer = true,
            //                     ValidateAudience = true,
            //                     ValidateLifetime = true,
            //                     ValidateIssuerSigningKey = true,

            //                     ValidIssuer = "Fiver.Security.Bearer",
            //                     ValidAudience = "Fiver.Security.Bearer",
            //                     IssuerSigningKey =
            //                      JwtSecurityKey.Create("fiversecret ")
            //                 };
            //        });
            services.AddAuthentication()
                  .AddJwtBearer(cfg =>
                  {
                      cfg.RequireHttpsMetadata = false;
                      cfg.SaveToken = true;

                      cfg.TokenValidationParameters = new TokenValidationParameters()
                      {
                          ValidIssuer = "https://google.com",
                          ValidAudience = "https://google.com",
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Qas2ty9qqRuwekfg$ytty7j874&32iILOpqu@ayghqpyrbslid52abwtys%"))
                      };

                  });

            services.AddMvc();

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<TeamOriginContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<TeamOriginContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFriendshipRepository, FriendshipRespository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IVerificationCodeSenderService, VerificationCodeSenderService>();

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

        }
    }
}
