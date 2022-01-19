namespace PomodoroCLI;

public interface IFileWriter
{ 
    void WriteToFile<T>(T value);
}