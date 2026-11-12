namespace game;

public class FlightLogger
{
    private readonly string _filePath = "docking_log.txt";
    private readonly IShipConsole _console;

    public FlightLogger(IShipConsole console)
    {
        _console = console;
    }

    // Сохраняем результат стыковки
    public void SaveResult(int target, bool success, int attempts, string computerName)
    {
        string status = success ? "УСПЕХ" : "ПРОВАЛ";
        string line = $"[{DateTime.Now:HH:mm:ss}] Компьютер: {computerName} | Цель: {target} | {status} | Попыток: {attempts}\n";
        File.AppendAllText(_filePath, line);
    }

    // Показываем бортовой журнал
    public void ShowLog()
    {
        if (!File.Exists(_filePath))
        {
            _console.PrintMessage("Бортовой журнал пуст.");
            return;
        }

        string[] lines = File.ReadAllLines(_filePath);
        _console.PrintMessage(" БОРТОВОЙ ЖУРНАЛ ");
        _console.PrintMessage($"Всего стыковок: {lines.Length}");

        int successCount = 0;
        foreach (string line in lines)
        {
            if (line.Contains("УСПЕХ")) successCount++;
        }
        _console.PrintMessage($"Успешных: {successCount}");
    }
}

