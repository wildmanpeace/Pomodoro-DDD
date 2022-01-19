using System.CommandLine;
using System.CommandLine.Binding;
using Microsoft.Extensions.Configuration;
using Pomodoro.Interfaces;

namespace PomodoroCLI.Commands;

internal class SettingCommand : ChildCommand
{
    private static  IFileWriter writer = new JsonWriter( "appsettings.json");

    public SettingCommand(ISettings settings) : base(settings)
    {
    }
    
    public override Command Create()
    {
        var command = new Command("settings", description: "You can change the default setting using these options")
        {
            new Option<int?>("--timerLength")
            {
                Description = $"Set the Pomodoro Length.  Currently set to {Settings.TimerLength}",
                IsRequired = false
            },
            new Option<int?>("--breakTimer")
            {
                Description = $"Set how long the break time will be.  Currently set to {Settings.ShortBreak}",
                IsRequired = false
            },
            new Option<int?>("--longBreakTimer")
            {
                Description = $"Set how long the break time will be.  Currently set to {Settings.LongBreak}",
                IsRequired = false
            },
        };
        
        command.SetHandler<int?, int?, int?>(HandleSettingsChange, command.Children.OfType<IValueDescriptor>().ToArray());

        return command;
    }

    private void HandleSettingsChange(int? timerLength, int? breakTimer, int? longBreakTimer)
    {
        try
        {
            SetSettings(timerLength, breakTimer, longBreakTimer);
            SaveSettings();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void SetSettings(int? timerLength, int? breakTimer, int? longBreakTimer)
    {
        if (timerLength.HasValue)
        {
            Settings.TimerLength = timerLength.Value;
        }
        if (breakTimer.HasValue)
        {
            Settings.ShortBreak = breakTimer.Value;
        }
        if (longBreakTimer.HasValue)
        {
            Settings.LongBreak = longBreakTimer.Value;
        }
    }

    private void SaveSettings()
    {
        Console.WriteLine(Settings);
        writer.WriteToFile(Settings);
    } 
}