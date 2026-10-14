namespace pr2;
/// <summary>
/// Представляет сберегательный счёт, на который ежемесячно начисляются проценты
/// </summary>
public class InterestEarningAccount : BankAccount
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="InterestEarningAccount"/>.
    /// </summary>
    /// <param name="name">Имя владельца счёта</param>
    /// <param name="initialBalance">Начальный баланс счёта</param>
    public InterestEarningAccount(string name, decimal initialBalance) : base(name, initialBalance) { }
    /// <summary>
    /// Начисляет ежемесячные проценты на остаток, если баланс превышает 500
    /// </summary>
    public override void PerformMonthAndTransactions()
    {
        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposit(interest, DateTime.UtcNow, "Apply month interest", OperationType.Interest);
        }
    }
}


