using System;
using Pomodoro.Interfaces;

namespace PomodoroDomainTests.Models;

public class TestWorkerMethod : ITaskWorkerMethod
{
    public Guid ID { get; set; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public DateTime BreakTimeStart { get; }
    public DateTime BreakTimeEnd { get; }
    public bool IsLongBreak { get; set; }
    public bool IsActive { get; set; }
    public ISettings Settings { get; set; }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void End()
    {
        throw new NotImplementedException();
    }

    public void BreakStart()
    {
        throw new NotImplementedException();
    }

    public void BreakStart(int timerLength)
    {
        throw new NotImplementedException();
    }

    public void BreakEnd()
    {
        throw new NotImplementedException();
    }
}