namespace pr2;

/// <summary>
/// Тип операции для журнала аудита.
/// </summary>
public enum OperationType
{
    /// <summary>Пополнение счёта.</summary>
    Deposit,

    /// <summary>Списание со счёта.</summary>
    Withdrawal,

    /// <summary>Начисление процентов или кэшбэка.</summary>
    Interest
}

/// <summary>
/// Ведёт журнал аудита всех операций по банковским счетам.
/// </summary>
public static class AuditLog
{
    private static readonly List<string> _entries = new();

    /// <summary>
    /// Записывает операцию в журнал аудита.
    /// </summary>
    /// <param name="accountNumber">Номер счёта.</param>
    /// <param name="operationType">Тип операции.</param>
    /// <param name="amount">Сумма операции.</param>
    /// <param name="date">Дата операции.</param>
    public static void Record(string accountNumber, OperationType operationType, decimal amount, DateTime date)
    {
        _entries.Add($"[{date:yyyy-MM-dd HH:mm:ss}] Account: {accountNumber} | " +
                     $"Operation: {operationType} | Amount: {amount}");
    }

    /// <summary>
    /// Возвращает все записи журнала аудита.
    /// </summary>
    /// <returns>Список строк с записями.</returns>
    public static IReadOnlyList<string> GetEntries() => _entries;

    /// <summary>
    /// Очищает журнал аудита (полезно для тестов).
    /// </summary>
    public static void Clear() => _entries.Clear();
}