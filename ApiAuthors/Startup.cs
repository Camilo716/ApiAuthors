using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace ApiAuthors;

public class Startup
{
    private readonly IConfiguration _config;

    public Startup(IConfiguration config)
    {
        _config = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
                .AddJsonOptions(
                    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        // services.AddDbContext<ApplicationDbContext>(
        //     options => 
        //         options.UseInMemoryDatabase(Guid.NewGuid().ToString()),
        //     contextLifetime:ServiceLifetime.Transient
        //     );
        
        services.AddDbContext<ApplicationDbContext>(
            options => 
                options.UseSqlServer(_config.GetConnectionString("dockerConnection")),
            contextLifetime:ServiceLifetime.Scoped
            );
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAutoMapper(typeof(Startup));
    }

    public void ConfigureMiddlewares(IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
