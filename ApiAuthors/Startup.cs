using Microsoft.EntityFrameworkCore;

namespace ApiAuthors;

public class Startup
{
    public IConfiguration _config { get; }

    public Startup(IConfiguration config)
    {
        _config = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(_config.GetConnectionString("dockerConnection")));
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
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

