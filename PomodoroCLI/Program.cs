using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomodoro.Interfaces;
using PomodoroCLI.Commands;

namespace PomodoroCLI;

static class Program
{
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional:false, reloadOnChange:true)
            .AddEnvironmentVariables()
            .Build();

        ISettings settings = config.GetRequiredSection("PomodoroSettings").Get<PomodoroSettings>();

        ITaskWorkerMethod worker = new WorkerMethod.Pomodoro(settings);
        
        IHost host = Host.CreateDefaultBuilder().ConfigureServices(
            (_, services) =>
            {
                services.AddSingleton(settings);
                services.AddScoped<ITaskWorkerMethod>(_ => new WorkerMethod.Pomodoro(settings));
            }
                
            )
            .Build();

        var command = new Commander(host.Services);

        _ = command.SetCommand(args);
    }
}
