using Pomodoro.Interfaces;

namespace WorkerMethod;


public class Pomodoro : ITaskWorkerMethod, ISettings
{
    public Guid ID { get; set; }
    public DateTime StartDate => _StartDate;
    public DateTime EndDate => _EndDate;
    public DateTime BreakTimeStart => _BreakTimeStart;
    public DateTime BreakTimeEnd => _BreakTimeEnd;
    public bool IsActive { get; set; }
    public List<IStrike> Strikes { get; set; }
    
    public int TimerLength { get; set; }
    public int ShortBreak { get; set; }
    public int LongBreak { get; set; }
    
    
    private DateTime _BreakTimeStart { get; set; }
    private DateTime _BreakTimeEnd { get; set; }
    
   public ISettings Settings { get; set; }
    public bool IsLongBreak { get; set; }
    private DateTime _StartDate { get; set; }
    private DateTime _EndDate { get; set; }

    public Pomodoro()
    {
        ID = Guid.NewGuid();
        Settings.TimerLength = 25;
        Settings.ShortBreak = 5;
        Settings.LongBreak = 25;
        Strikes = new List<IStrike>();
    }

    public Pomodoro(int pomodoroLength, int shortBreakLength, int longBreakLength)
    {
        ID = Guid.NewGuid();
        Settings.TimerLength = pomodoroLength;
        Settings.ShortBreak = shortBreakLength;
        Settings.LongBreak = longBreakLength;
        Strikes = new List<IStrike>();
    }
    
    public Pomodoro(int pomodoroLength, int shortBreakLength, int longBreakLength, bool isLongBreak)
    {
        ID = Guid.NewGuid();
        Settings.TimerLength = pomodoroLength;
        Settings.ShortBreak = shortBreakLength;
        Settings.LongBreak = longBreakLength;
        Strikes = new List<IStrike>();
        IsLongBreak = isLongBreak;
    }

    public Pomodoro(ISettings settings)
    {
        ID = Guid.NewGuid();
        Settings = settings;
    }

    private DateTime SetEndBreakTime()
    {
        return BreakTimeStart.Add(IsLongBreak
            ? TimeSpan.FromMinutes(Settings.LongBreak)
            : TimeSpan.FromMinutes(Settings.ShortBreak));
    }

    public void Start()
    {
        _StartDate = DateTime.Now;
        _EndDate = _StartDate.Add(TimeSpan.FromMinutes(Settings.TimerLength));
        IsActive = true;
    }

    public void End()
    {
        IsActive = false;
        _EndDate = DateTime.Now;
    }

    public void BreakStart()
    {
        _BreakTimeStart = DateTime.Now;
        _BreakTimeEnd = SetEndBreakTime();
    }

    public void BreakStart(int timerLength)
    {
        _BreakTimeStart = DateTime.Now;
        _BreakTimeEnd = _BreakTimeStart.AddMinutes(timerLength);
    }

    public void BreakEnd()
    {
        _BreakTimeEnd = DateTime.Now;
    }

    public void AddStrike(IStrike strike)
    {
        Strikes.Add(strike);
    }
}