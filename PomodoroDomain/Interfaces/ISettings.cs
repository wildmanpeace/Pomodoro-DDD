namespace Pomodoro.Interfaces;

public interface ISettings
{
    int TimerLength { get; set; }
    int ShortBreak { get; set; }
    int LongBreak { get; set; }
}