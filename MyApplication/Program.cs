using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyApplicationDataLayer.DataContext;
using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer;
using MyApplicationServiceLayer.AccountService;
using MyApplicationServiceLayer.Authenticate;
using MyApplicationServiceLayer.Authenticate.Login;
using MyApplicationServiceLayer.Authenticate.Registration;
using MyApplicationServiceLayer.ProjectService;
using MyApplicationServiceLayer.RequestService;
using MyApplicationServiceLayer.RequestService.EditStatus;
using MyApplicationServiceLayer.RequestService.List;
using MyApplicationServiceLayer.RequestService.PostRequest;
using MyApplicationServiceLayer.Tokens.RefreshTokenService;
using MyApplicationServiceLayer.Tokens.TokenGenerators;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace MyApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors();
            string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;

            builder.Services.Configure<AuthenticationConfiguration>(builder.Configuration.GetSection("Authentication"));

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "My Application API"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter 'Bearer [jwt]'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                options.AddSecurityRequirement(new OpenApiSecurityRequirement { { scheme, Array.Empty<string>() } });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });


            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.User.AllowedUserNameCharacters += " ";
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddTransient<IRequestService, RequestService>();
            builder.Services.AddTransient<IRequestListService, RequestListService>();
            builder.Services.AddTransient<IEditStatusService, EditStatusService>();
            builder.Services.AddTransient<IPostRequestService, PostRequestService>();
            builder.Services.AddTransient<ILoginService, LoginService>();
            builder.Services.AddTransient<IRegistrationService, RegistrationService>();
            builder.Services.AddTransient<IAuthenticator, Authenticator>();
            builder.Services.AddSingleton<AccessTokenGenerator>();
            builder.Services.AddSingleton<RefreshTokenGenerator>();
            builder.Services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            builder.Services.AddTransient<IRefreshTokenDbAccess, RefreshTokenDbAccess>();
            builder.Services.AddTransient<IRefreshTokenValidator, RefreshTokenValidator>();
            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<IPostProjectService, PostProjectService>();
            builder.Services.AddTransient<RoleInitializer>();
            builder.Services.AddTransient<RequestInitializer>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            var secret = builder.Configuration["Authentication:Secret"] ?? throw new InvalidOperationException("Secret not configured.");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Authentication:ValidIssuer"],
                    ValidAudience = builder.Configuration["Authentication:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ClockSkew = new TimeSpan(0, 0, 5)
                };
            });

            var app = builder.Build();

            app.UseCors(builder => builder.AllowAnyOrigin()
                             .AllowAnyHeader()
                            .AllowAnyMethod());

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var roleInitializer = services.GetRequiredService<RoleInitializer>();
                    var requestInitializer = services.GetRequiredService<RequestInitializer>();

                    roleInitializer.Initialize().Wait();
                    requestInitializer.Initialize().Wait();

                }
                catch (Exception ex)
                {
                    //var logger = services.GetRequiredService<ILogger>();
                    //logger.LogError(ex, "An error occurred while seeding the database");
                }
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}