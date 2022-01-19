using System.CommandLine;
using System.CommandLine.Binding;
using Microsoft.Extensions.DependencyInjection;
using Pomodoro.Interfaces;

namespace PomodoroCLI.Commands;

public class Commander
{
    private ISettings Settings { get; set; }
    private ITaskWorkerMethod Worker { get; set; }

    public Commander(IServiceProvider services)
    {
        Settings = services.GetRequiredService<ISettings>();
        Worker = services.GetRequiredService<ITaskWorkerMethod>();
    }
    
    public int SetCommand(string[] args)
    {
        var command = new RootCommand("A Pomodoro Counter")
        {
            new Option<int>("--timerLength", getDefaultValue:() => Settings.TimerLength)
            {
                Description = $"Set the Pomodoro Length.  Set to {Settings.TimerLength}",
                IsRequired = false,
                
            },
            new Option<int>("--breakTimer", getDefaultValue:() => Settings.ShortBreak)
            {
                Description = $"Set how long the break time will be.  Set to {Settings.ShortBreak}",
                IsRequired = false
            },
            new Option<bool>("--longBreak", getDefaultValue:() => false)
            {
                Description = "Set if this is a long break.  Long breaks should happen after your 4th pomodoro",
                IsRequired = false,
                
            },
            new Option<int>("--longBreakTimer", getDefaultValue: () => Settings.LongBreak)
            {
                Description = $"Set how long the break time will be.  Set to {Settings.LongBreak}",
                IsRequired = false,
            },
            
            new SettingCommand(Settings).Create(),
            new StartCommand(Settings, Worker).Create()
        };
        
        command.SetHandler<int, int, bool, int>(( timerLength, breakTimer, longBreak, longBreakTimer) =>
        {
            Settings.TimerLength = timerLength;
            Settings.ShortBreak = breakTimer;
            Settings.LongBreak = longBreakTimer;

            Worker.IsLongBreak = longBreak;
            
            Worker.Start();

        }, command.Children.OfType<IValueDescriptor>().ToArray());

        return command.Invoke(args);
    }
}