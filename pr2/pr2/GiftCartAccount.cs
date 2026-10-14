
namespace pr2;
/// <summary>
/// Представляет подарочный счёт, на который ежемесячно может вноситься фиксированная сумма
/// </summary>

public class GiftCartAccount : BankAccount
{
    private readonly decimal _monthlyDeposit = 0m;
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="GiftCartAccount"/>
    /// </summary>
    /// <param name="name"></param>
    /// <param name="initialBalance"></param>
    /// <param name="monthlyDeposit"></param>
    //monthlyDeposit-параметр по умолчанию(принимает 0), при создании GitCartAccount("Yana,1000");=> monthlyDeposit=0
    //new GitCartAccount("Yana,1000,5000");=> monthlyDeposit=5000
    public GiftCartAccount(string  name, decimal initialBalance, decimal monthlyDeposit=0) 
        : base (name, initialBalance)=> _monthlyDeposit= monthlyDeposit;
    /// <summary>
    /// Выполняет ежемесячное пополнение счёта, если задана ненулевая сумма ежемесячного депозита
    /// </summary>
    public override void PerformMonthAndTransactions()
    {
        if (_monthlyDeposit!=0)
        {
            MakeDeposit(_monthlyDeposit, DateTime.UtcNow, "Add monthly deposit", OperationType.Interest);
        }
    }
    //base.ToString()-вызов базовой реализации => реализация из класса BankAccount
    /// <summary>
    /// Возвращает строковое представление счёта, включая базовую информацию и размер ежемесячного пополнения
    /// </summary>
    /// <returns>Строка с информацией о счёте</returns>
    public override string ToString() => $"{base.ToString()} Monthluy deposit: {_monthlyDeposit}";
}
