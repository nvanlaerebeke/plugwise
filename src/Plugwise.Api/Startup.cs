using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Plugwise.Config;

namespace Plugwise.Api;

public class Startup {
    public void Start(WebApplicationBuilder builder) {
        // Add services to the container.
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Initialize
        var settings = new SettingsProvider().Get();
        builder.Services.AddSingleton(settings);

        try {
            new Actions.Startup().Setup(builder.Services, settings.SerialPort);
        } catch (Exception ex) {
            Console.WriteLine(ex);
        }

        var app = builder.Build();

        //Configure Error Handler
        app.UseExceptionHandler("/Error");
        app.UseHsts();

        //Enable swagger 
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthorization();

        app.MapControllers();
        app.MapGet("/", () => "Hello World");

        app.Run();
    }
}
