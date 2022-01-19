using Pomodoro.Interfaces;

namespace PomodoroCLI;

public class PomodoroSettings : ISettings
{
    public int TimerLength { get; set; }
    public int ShortBreak { get; set; }
    public int LongBreak { get; set; }
}