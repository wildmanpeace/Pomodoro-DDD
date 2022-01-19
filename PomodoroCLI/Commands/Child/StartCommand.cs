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
            Description = "Start with your settings"
        };
        
        command.SetHandler(StartWorkerMethod);
        

        return command;
    }

    private void StartWorkerMethod()
    {
        Worker.Start();
    }
}