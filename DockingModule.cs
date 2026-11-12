namespace game;

// Базовый класс модуля (LSP)
public abstract class BaseDockingComputer : IDockingComputer
{
    public abstract int GenerateTargetThrust(int min, int max);
    public abstract string GetComputerName();
}
public class NormalComputer : BaseDockingComputer
{
    private Random _rand = new Random();

    public override int GenerateTargetThrust(int min, int max)
    {
        return _rand.Next(min, max + 1);
    }

    public override string GetComputerName()
    {
        return "Штатный компьютер (норма)";
    }
}

// Сломанный компьютер (LSP: выдает мусор, но программа не падает)
public class BrokenComputer : BaseDockingComputer
{
    public override int GenerateTargetThrust(int min, int max)
    {
        return -999; // Некорректное значение
    }

    public override string GetComputerName()
    {
        return "Аварийный компьютер (сбой)";
    }
}
// Главный класс управления стыковкой
public class DockingModule
{
    private readonly IShipConsole _console;
    private readonly IDockingComputer _computer;
    private readonly FlightSettings _settings;

    // События 
    public event Action<int, bool, int, string>? OnDockingFinished; // (цель, успех, попытки, имя ПК)
    public event Action<string>? OnThrusterWarning; // Предупреждение двигателей

    // DIP: Зависим от абстракций
    public DockingModule(IShipConsole console, IDockingComputer computer, FlightSettings settings)
    {
        _console = console;
        _computer = computer;
        _settings = settings;
    }

    public void StartDocking()
    {
        int targetThrust = _computer.GenerateTargetThrust(_settings.MinThrust, _settings.MaxThrust);

        // OCP + LSP: Если компьютер сломан и выдал бред, мы его обрабатываем
        if (targetThrust < _settings.MinThrust || targetThrust > _settings.MaxThrust)
        {
            _console.PrintMessage("ВНИМАНИЕ Сбой навигации! Компьютер выдал неверную тягу.");
            _console.PrintMessage("Включаем резервный контур. Целевая тяга: 50.");
            targetThrust = 50;
        }

        int attempts = 0;
        bool isDocked = false;

        _console.PrintMessage($"\nСвязь со станцией установлена. Компьютер: {_computer.GetComputerName()}");
        _console.PrintMessage($"Введите тягу двигателей (от {_settings.MinThrust} до {_settings.MaxThrust}). Попыток: {_settings.MaxAttempts}.");

        while (attempts < _settings.MaxAttempts)
        {
            attempts++;
            _console.PrintMessage($"\nПопытка {attempts}. Введите значение тяги:");

            string input = _console.ReadCommand();
            int userThrust;

            if (!int.TryParse(input, out userThrust))
            {
                _console.PrintMessage("ОШИБКА Пульт не распознал команду. Введите число.");
                attempts--; // не тратим попытку
                continue;
            }

            if (userThrust == targetThrust)
            {
                _console.PrintMessage("СТЫКОВКА УСПЕШНА! Добро пожаловать на станцию.");
                isDocked = true;
                break;
            }

            // Подсказки (больше/меньше)
            if (userThrust < targetThrust)
            {
                _console.PrintMessage("Станция: 'Тяни сильнее!' (отклонение назад)");
                OnThrusterWarning?.Invoke("Слишком мало");
            }
            else
            {
                _console.PrintMessage("Станция: 'Сбавь обороты!' (отклонение вперед)");
                OnThrusterWarning?.Invoke("Слишком много");
            }
        }

        if (!isDocked)
        {
            _console.PrintMessage($"\nСТЫКОВКА ПРОВАЛЕНА. Станция ушла с орбиты. Нужная тяга была: {targetThrust}");
        }

        // Вызов главного события
        OnDockingFinished?.Invoke(targetThrust, isDocked, attempts, _computer.GetComputerName());
    }
}