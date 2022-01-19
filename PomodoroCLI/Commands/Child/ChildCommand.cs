using System.CommandLine;
using Pomodoro.Interfaces;

namespace PomodoroCLI.Commands;

public abstract class ChildCommand
{
    public ISettings Settings { get; set; }

    protected ChildCommand(ISettings settings)
    {
        Settings = settings;
    }

    public abstract Command Create();
}