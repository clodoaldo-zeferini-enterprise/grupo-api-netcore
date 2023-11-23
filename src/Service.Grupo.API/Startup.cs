using Service.Grupo.Infrastructure.Config;
using Service.Grupo.Infrastructure.IoC;

namespace Service.Grupo.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        private readonly Configuration _myConfiguration;

        public Startup(IConfiguration configuration)
        {
            var env = Ambiente.GetAmbiente();
                      
            var configApp = new ConfigurationBuilder()
                .AddJsonFile($@"appsettings.{env}.json")
                .Build();           

            _configuration = configApp;

            _myConfiguration = new Configuration();
            _configuration.Bind("Configuration", _myConfiguration);


        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins(
                                            "https://192.168.15.10:5001"
                                          , "https://Server-DES"
                                          , "https://Server-HOM"
                                          , "https://Server-PRO"

                                          ).AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                  });
            });


            services.AddSingleton(_myConfiguration);
            services.AddSingleton(_configuration);

            services.AddControllers();

            Registry.RegisterApplication(services);
            Registry.RegisterDatabase(services);

 /*
            var key = System.Text.Encoding.ASCII.GetBytes(Configuration["Client:Secret"]);

           
            services.AddAuthentication(
                    x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer(
                    x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
            */

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_myConfiguration.Swagger.SwaggerDoc.Name, new Microsoft.OpenApi.Models.OpenApiInfo { Title = _myConfiguration.Swagger.SwaggerDoc.OpenApiInfo.Title, Version = _myConfiguration.Swagger.SwaggerDoc.OpenApiInfo.Version });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "dev")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(_myConfiguration.Swagger.SwaggerEndpoint.Url, _myConfiguration.Swagger.SwaggerEndpoint.Name));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
