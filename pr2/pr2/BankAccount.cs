
using System.Text;

namespace pr2;
/// <summary>
/// Представляет базовый банковский счёт, поддерживающий операции пополнения и снятия.
/// </summary>

public class BankAccount
{
    private readonly decimal _minimumBalance;
    static private int s_accountNumberSeed = 1000000000;
    /// <summary>
    /// Уникальный номер счёта.
    /// </summary>
    public string Number { get;  }
    /// <summary>
    /// Имя владельца счёта.
    /// </summary>
    public string Owner { get; private set;  }
    /// <summary>
    /// Текущий баланс счёта, рассчитанный на основе всех транзакций.
    /// </summary>
    public decimal Balance 
    {
        get
        { 
            decimal balance = 0;
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }
            return balance;
        }
    }
    /// <summary>
    /// Текущий статус клиента, влияющий на ежемесячные начисления и списания.
    /// </summary>
    public AccountStatus Status { get; set; } = AccountStatus.Regular;

    private List<Transactionn> _allTransactions = new List<Transactionn>();
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="BankAccount"/> с минимальным балансом по умолчанию (0).
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>

    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0) { }
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="BankAccount"/> с заданным минимальным балансом.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    /// <param name="minimumBalance">Минимально допустимый баланс счёта.</param>

    public BankAccount(string name, decimal initialBalance, decimal minimumBalance) //какой то конструктор хз
    {
        Owner = name; // this.Owner = name
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
        _minimumBalance = minimumBalance;
        if (initialBalance > 0)
        {

            MakeDeposit(initialBalance, DateTime.UtcNow, "Initial Balance");
        }
        
    }
    /// <summary>
    /// Пополняет счёт на указанную сумму.
    /// </summary>
    /// <param name="amount">Сумма пополнения. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Бросается, если <paramref name="amount"/> меньше или равен нулю.
    /// </exception>
    public void MakeDeposit(decimal amount, DateTime date, string note, OperationType operationType = OperationType.Deposit)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive"); // исключение если сумма отрицательная
        }
        var deposit = new Transactionn(amount, date, note);
        _allTransactions.Add(deposit);
        AuditLog.Record(Number, OperationType.Deposit, amount, date); 
    }
    /// <summary>
    /// Снимает указанную сумму со счёта.
    /// </summary>
    /// <param name="amount">Сумма снятия. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Бросается, если <paramref name="amount"/> меньше или равен нулю.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Бросается, если на счете недостаточно средств для снятия.
    /// </exception>
    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Transactionn? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transactionn? withdrawal = new(-amount, date, note);
        _allTransactions.Add(withdrawal);
        AuditLog.Record(Number, OperationType.Withdrawal, amount, date);
        if (overdraftTransaction is not null)
        {
            _allTransactions.Add(overdraftTransaction);
            AuditLog.Record(Number, OperationType.Withdrawal, -overdraftTransaction.Amount, date);

        }
    }
        //if (amount <= 0)
        //{
        //    throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
        //}
        //if (Balance < amount)
        //{
        //    throw new InvalidOperationException("Not sufficient rubls for this withdawal");
        //}
        //var withdawal = new Transactionn(-amount, date, note);
        //_allTransactions.Add(withdawal);
        /// <summary>
        /// Переводит указанную сумму с текущего счёта на другой счёт.
        /// </summary>
        /// <param name="destination">Счёт-получатель.</param>
        /// <param name="amount">Сумма перевода. Должна быть положительной.</param>
        /// <param name="date">Дата операции.</param>
        /// <param name="note">Комментарий к операции.</param>
        /// <exception cref="ArgumentNullException">
        /// Бросается, если <paramref name="destination"/> равен <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Бросается, если <paramref name="amount"/> меньше или равен нулю.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Бросается, если на текущем счёте недостаточно средств.
        /// </exception>
    public void Transfer(BankAccount destination, decimal amount, DateTime date, string note)
    {
        if (destination is null)
            throw new ArgumentNullException(nameof(destination));

        MakeWithdrawal(amount, date, $"Transfer to {destination.Number}: {note}");
        destination.MakeDeposit(amount, date, $"Transfer from {Number}: {note}");
    }
    
    /// <summary>
    /// Проверяет, превышен ли лимит снятия (овердрафт), и при необходимости создаёт транзакцию за овердрафт.
    /// </summary>
    /// <param name="v">Флаг, указывающий, что баланс уходит в минус (овердрафт).</param>
    /// <returns>Транзакция за овердрафт или значение по умолчанию, если овердрафта нет.</returns>
    /// <exception cref="InvalidOperationException">
    /// Бросается, если овердрафт не разрешён для данного типа счёта.
    /// </exception>
    protected virtual Transactionn? CheckWithdrawalLimit(bool v)
    {
        if (v)
        {
            throw new InvalidOperationException
                ("Not sufficient rubls for this withdrawal");
        }
        return default;
    }
    /// <summary>
    /// Возвращает историю всех транзакций по счёту в виде отформатированной строки.
    /// </summary>
    /// <returns>Строка, содержащая дату, сумму, баланс и комментарий для каждой транзакции.</returns>

    public string GetAccountHistory()
    {
        var report = new StringBuilder();
        decimal balance = 0;
        report.AppendLine("Data\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.date.ToShortDateString()}\t" + $"{balance}\t{item.Note}");
        }
        return report.ToString();
    }
    /// <summary>
    /// Вычисляет суммарную сумму всех списаний за текущий месяц.
    /// </summary>
    /// <returns>Сумма списаний (положительное число).</returns>
    private decimal CalculateMonthlyWithdrawals()
    {
        decimal total = 0;
        var now = DateTime.UtcNow;
        foreach (var t in _allTransactions)
        {
            if (t.Amount < 0 && t.date.Month == now.Month && t.date.Year == now.Year)
            {
                total += -t.Amount; // берём модуль
            }
        }
        return total;
    }
    /// <summary>
    /// Выполняет ежемесячные транзакции в соответствии со статусом клиента.
    /// </summary>

    public virtual void PerformMonthAndTransactions()
    {

        switch (Status)
        {
            case AccountStatus.Regular:
                MakeWithdrawal(50m, DateTime.UtcNow, "Monthly fee");
                break;

            case AccountStatus.Premium:
                if (Balance > 10_000m)
                {
                    decimal interest = (Balance - 10_000m) * 0.01m;
                    MakeDeposit(interest, DateTime.UtcNow, "Premium interest");
                }
                break;

            case AccountStatus.VIP:
                if (Balance > 10_000m)
                {
                    decimal vipInterest = (Balance - 10_000m) * 0.003m;
                    MakeDeposit(vipInterest, DateTime.UtcNow, "VIP interest", OperationType.Interest);
                }
                decimal cashback = CalculateMonthlyWithdrawals() * 0.05m;
                MakeDeposit(cashback, DateTime.UtcNow, "VIP cashback", OperationType.Interest);
                break;
        }
    }
    //переопределяем метод который унаследовали от object этот метод должен возвращать строку с состоянием объекта 
    /// <summary>
    ///  Возвращает строковое представление счёта, включающее тип, владельца и номер счёта
    /// </summary>
    /// <returns>Строка с информацией о счёте</returns>
    public override string ToString()
    //{
    //    return $"Type: {GetType().Name}\tOwner : {Owner}\tNumber of account : {Number}"
    //}
    => $"Type: {GetType().Name}\tOwner : {Owner}\tNumber of account : {Number}";

}
