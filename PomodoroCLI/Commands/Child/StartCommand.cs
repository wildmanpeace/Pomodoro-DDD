using System.CommandLine;
using Pomodoro.Interfaces;

namespace PomodoroCLI.Commands;

internal class StartCommand : ChildCommand
{
    private ITaskWorkerMethod Worker { get; set; }

    public StartCommand(ISettings settings, ITaskWorkerMethod worker) : base(settings)
    {
        Worker = worker;
    }
    
    public override Command Create()
    {
        var command = new Command("start")
        {
            Description = "Start a pomodoro with your settings"
        };
        
        command.SetHandler(StartWorkerMethod);
        return command;
    }

    private async void StartWorkerMethod()
    {
        Worker.Start();
        Console.BackgroundColor = ConsoleColor.Cyan;
        await WorkerRun.WaitUntil(WorkerEnd, CancellationToken.None);
        
        ConsoleExtension.ConsoleColorPulse(ConsoleColor.Gray, ConsoleColor.Cyan);
    }

    private bool WorkerEnd() => Worker.EndDate == DateTime.Now;
}