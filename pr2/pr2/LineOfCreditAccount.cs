namespace pr2;
/// <summary>
/// Представляет кредитный счёт с возможностью ухода в минус (овердрафт) и начислением процентов на отрицательный баланс
/// </summary>
internal class LineOfCreditAccount : BankAccount
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="LineOfCreditAccount"/>
    /// </summary>
    /// <param name="name">Имя владельца счёта</param>
    /// <param name="initialBalance">Начальный баланс счёта</param>
    /// <param name="creditLimit">Кредитный лимит (максимально допустимая отрицательная сумма)</param>
    public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit)
        : base (name, initialBalance, -creditLimit) { }
    /// <summary>
    /// Начисляет ежемесячные проценты на отрицательный баланс, если счёт ушёл в минус
    /// </summary>
    public override void PerformMonthAndTransactions()
    {
        if (Balance < 0)
        {
            decimal interest = -Balance *  0.07m;
            MakeWithdrawal(interest, DateTime.UtcNow, "Charge monthly interest");
        }
    }
    /// <summary>
    /// Проверяет, превышен ли кредитный лимит, и при необходимости создаёт транзакцию за овердрафт
    /// </summary>
    /// <param name="isOverdrawn">Флаг, указывающий, что баланс уходит в минус</param>
    /// <returns>Транзакция за овердрафт или значение по умолчанию, если овердрафта нет</returns>
    protected override Transactionn? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (!isOverdrawn)
        {
            return default;
        }

        var overdraft = new Transactionn(-20, DateTime.UtcNow, "apply overdraft");
        AuditLog.Record(Number, OperationType.Withdrawal, 20m, DateTime.UtcNow); // ← новое
        return overdraft;
    }
}

