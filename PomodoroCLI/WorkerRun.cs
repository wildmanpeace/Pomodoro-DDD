namespace PomodoroCLI;

public class WorkerRun
{
    public static Task WaitWhile(Func<bool> condition, CancellationToken cancellationToken, int frequency = 25)
    {
        try
        {
            var awaitTask = Task.Run(async () =>
            {
                while (condition() && cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(frequency, cancellationToken).ConfigureAwait(true);
                }
            }, cancellationToken);
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Task.CompletedTask;
    }
    
    
    public static Task WaitUntil(Func<bool> condition, CancellationToken cancellationToken, int frequency = 25)
    {
        try
        {
            var awaitTask = Task.Run(async () =>
            {
                while (!condition() && cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(frequency, cancellationToken).ConfigureAwait(true);
                }
            }, cancellationToken);
            return awaitTask;
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine(e);
            throw;
        }

        
    }
}