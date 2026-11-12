using game;

// Реализация консоли
public class ShipConsole : IShipConsole
{
    public void PrintMessage(string text) => Console.WriteLine(text);
    public string ReadCommand() => Console.ReadLine() ?? "";
}

class Program
{
    static void Main()
    {
        IShipConsole console = new ShipConsole();
        FlightSettings settings = new FlightSettings();
        FlightLogger logger = new FlightLogger(console);

        console.PrintMessage(" СИСТЕМА СТЫКОВКИ 'ORBITAL-1' ");

        console.PrintMessage("Выберите режим:");
        console.PrintMessage("1 - Новичок");
        console.PrintMessage("2 - Стандартный");
        console.PrintMessage("3 - Эксперт");
        console.PrintMessage("4 - Свои настройки (Custom)");

        string diffChoice = console.ReadCommand();

        if (diffChoice == "1")
        {
            settings.MinThrust = 1;
            settings.MaxThrust = 100;
            settings.MaxAttempts = 10;
        }
        else if (diffChoice == "3")
        {
            settings.MinThrust = 50;
            settings.MaxThrust = 60;
            settings.MaxAttempts = 3;
        }
        else if (diffChoice == "4") // ПОЛЬЗОВАТЕЛЬСКИЕ НАСТРОЙКИ
        {
            console.PrintMessage("Введите минимальную тягу (например, 10):");
            settings.MinThrust = int.Parse(console.ReadCommand());

            console.PrintMessage("Введите максимальную тягу (например, 100):");
            settings.MaxThrust = int.Parse(console.ReadCommand());

            console.PrintMessage("Введите количество попыток (например, 5):");
            settings.MaxAttempts = int.Parse(console.ReadCommand());
        }
 

        console.PrintMessage("\nВыберите навигационный компьютер:");
        console.PrintMessage("1 - Штатный (все в порядке)");
        console.PrintMessage("2 - Аварийный (сломан, выдает сбои)");

        string compChoice = console.ReadCommand();

        IDockingComputer computer;
        if (compChoice == "2")
        {
            computer = new BrokenComputer();
        }
        else
        {
            computer = new NormalComputer();
        }

        DockingModule module = new DockingModule(console, computer, settings);



        // 1. Подписка логгера на завершение игры (сохранение в файл)
        module.OnDockingFinished += logger.SaveResult;

        // 2. Подписка на событие предупреждения двигателей (во время игры)
        module.OnThrusterWarning += (warningMessage) =>
        {
            // Это уведомление для "подписчика". Например, мы можем мигнуть лампочкой.
            // В консоли просто выводим системное сообщение, чтобы препод увидел работу события.
            //Console.ForegroundColor = ConsoleColor.Yellow;
            console.PrintMessage($"[СИСТЕМА] Датчики зафиксировали: {warningMessage}");
            Console.ResetColor();
        };
 

        bool retry = true;
        while (retry)
        {
            module.StartDocking();
            logger.ShowLog();

            console.PrintMessage("\nПопробовать снова? (y/n)");
            retry = console.ReadCommand().ToLower() == "y";
        }
    }
}