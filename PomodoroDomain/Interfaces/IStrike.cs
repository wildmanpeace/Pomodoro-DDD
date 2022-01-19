namespace Pomodoro.Interfaces;

public interface IStrike
{
    Guid ID { get; set; }
    string  Category { get; set; }
    string Description { get; set; }
    DateTime Date { get; set; }
}