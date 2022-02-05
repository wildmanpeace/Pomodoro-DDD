namespace Pomodoro.Interfaces;

public interface ITaskWorkerMethod
{
    Guid ID { get; set; }
    DateTime StartDate { get; }
    DateTime EndDate { get; }
    DateTime BreakTimeStart { get; }
    DateTime BreakTimeEnd { get; }
    bool IsLongBreak { get; set; }
    bool IsActive { get; set; }
    
    ISettings Settings { get; set; }
    

    void Start();
    void End();
    void BreakStart();
    void BreakStart(int timerLength);
    void BreakEnd();

}