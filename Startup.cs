using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using AgileTaskMaster.Helpers;
using AgileTaskMaster.Models;
using AgileTaskMaster.Repositories;
using AgileTaskMaster.Services;
using AgileTaskMaster.Data;
using AgileTaskMaster.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MongoDB.Driver;
using Microsoft.AspNetCore.Identity;

namespace AgileTaskMaster
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<MongoDBSettings>(_configuration.GetSection(nameof(MongoDBSettings)));
            services.Configure<JwtSettings>(_configuration.GetSection(nameof(JwtSettings)));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AgileTaskMaster API", Version = "v1" });
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "JWT Authorization header using the Bearer scheme.",
                };
                c.AddSecurityDefinition("Bearer", jwtSecurityScheme);

                var jwtBearerScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme,
                    },
                };
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    { jwtBearerScheme, Array.Empty<string>() },
                });

                
                c.OperationFilter<AuthenticationRequirementOperationFilter>();
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IMongoDatabase>(provider =>
            {
                var settings = provider.GetService<IOptions<MongoDBSettings>>();
                var client = new MongoClient(settings.Value.ConnectionString);
                return client.GetDatabase(settings.Value.DatabaseName);
            });
            services.AddSignalR(); 

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IDependencyRepository, DependencyRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();

            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IDependencyService, DependencyService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); 

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AgileTaskMaster API V1");
            });
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                AdminSeed.SeedAdmin(serviceProvider).Wait();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notificationHub");
            });
        }
    }
}
