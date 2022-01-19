using Pomodoro.Interfaces;

namespace WorkerMethod;

public class Strike : IStrike
{
    public Guid ID { get; set; }
    public string  Category { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public Strike(string category, string description)
    {
        ID = new Guid();
        Category = category;
        Description = description;
        Date = DateTime.Now;
    }
}