using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PassagensAereasAPI.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PassagensAereasAPI.Dominio.Contratos;
using PassagensAereasAPI.Dominio.Servicos;
using PassagensAereasAPI.Infra;
using PassagensAereasAPI.Infra.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace PassagensAereasAPI.Api
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(Configuration["SecuritySettings:SigningKey"]))
                    };
                });

            services.Configure<SecuritySettings>(options => Configuration.GetSection("SecuritySettings").Bind(options));

            services.AddMvc();

            var connectionString = Configuration.GetConnectionString("PassagensAereas");

            services.AddScoped<ClasseDeVooService>();
            services.AddScoped<LocalService>();
            services.AddScoped<OpcionalService>();
            services.AddScoped<ReservaService>();
            services.AddScoped<TrechoService>();
            services.AddScoped<UsuarioService>();

            services.AddDbContext<PassagensAereasContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IClasseDeVooRepository, ClasseDeVooRepository>();
            services.AddScoped<ILocalRepository, LocalRepository>();
            services.AddScoped<IOpcionalRepository, OpcionalRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<ITrechoRepository, TrechoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Passagens Aereas API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());

            // Diz que a aplicação utiliza os atributos de [Authorize]
            app.UseAuthentication();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Passagens Aereas API");
            });

            app.UseMvc();

            app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
        }
    }
}
