using System.CommandLine;
using Pomodoro.Interfaces;

namespace PomodoroCLI.Commands;

public class Break : ChildCommand
{
    private ITaskWorkerMethod Worker { get; set; }

    public Break(ISettings settings, ITaskWorkerMethod worker) : base(settings)
    {
        Worker = worker;
        
    }

    public override Command Create()
    {
        var command = new Command("break", description: "Start a break with you default settings or with a custom length")
        {
            new Argument(name: "IsLongBreak", "Identify if this is going to be a long break or not (Y/N)")
            {
                Arity = ArgumentArity.ExactlyOne,
                ValueType = typeof(string)
            },
            new Option<int?>("--breakTimer", getDefaultValue: () => Settings.ShortBreak)
            {
                Description = $"Set how long your break will be.  " +
                              $"If timer is set it will be used.  Otherwise your timer settings will be used",
                IsRequired = false
            },
        };
        
        command.SetHandler((string isLongTimer, int breakTimer) =>
        {
            if (breakTimer == Settings.ShortBreak)
            {
                Worker.IsLongBreak = isLongTimer == "Y";
                Worker.BreakStart();
            }
            
            Worker.BreakStart(breakTimer);
            Console.BackgroundColor = ConsoleColor.Green;
            
            WorkerRun.WaitUntil(HasBreakEnded, CancellationToken.None).RunSynchronously();
            
            ConsoleExtension.ConsoleColorPulse(ConsoleColor.Green, ConsoleColor.Gray);
        });

        return command;
    }

    private bool HasBreakEnded() => DateTime.Now == Worker.BreakTimeEnd;
}