namespace PomodoroCLI;

public static class ConsoleExtension
{
    public static void ConsoleColorPulse(ConsoleColor color1, ConsoleColor color2, int intervals = 1, int numOfPulses = 5)
    {
        for (int i = 0; i < numOfPulses; i++)
        {
            Console.BackgroundColor = color1;
            Thread.Sleep(TimeSpan.FromSeconds(intervals));
            Console.BackgroundColor = color2;
            Thread.Sleep(TimeSpan.FromSeconds(intervals));
        }
    }
}