namespace LowLevelDotNET.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine($"[INFO] {DateTime.Now} - {message}");
        }
    
        public void Warning(string message)
        {
            Console.WriteLine($"[WARN] {DateTime.Now} - {message}");
        }
    
        public void Error(string message)
        {
            Console.WriteLine($"[ERROR] {DateTime.Now} - {message}");
        }
    }
}