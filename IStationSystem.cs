namespace game;


// Интерфейс для общения с пультом
public interface IShipConsole
{
    void PrintMessage(string text);
    string ReadCommand();
}

// Интерфейс генератора координат узла
public interface IDockingComputer
{
    int GenerateTargetThrust(int min, int max);
    string GetComputerName();
}