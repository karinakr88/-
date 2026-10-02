
using System.Text;

namespace pr2;

public class BankAccount
{
    static private int s_accountNumberSeed = 1000000000;
    public string Number { get;  }
    public string Owner { get; private set;  }
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

    private List<Transactionn> _allTransactions = new List<Transactionn>(); 

    public BankAccount(string name, decimal initialBalance) //какой то конструктор хз
    {
        Owner = name; // this.Owner = name
        MakeDeposit(initialBalance, DateTime.UtcNow, "Initial Balance");
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
    }
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive"); // исключение если сумма отрицательная
        }
        var deposit = new Transactionn(amount, date, note);
        _allTransactions.Add(deposit);
    }
    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
        }
        if (Balance < amount)
        {
            throw new InvalidOperationException("Not sufficient rubls for this withdawal");
        }
        var withdawal = new Transactionn(-amount, date, note);
        _allTransactions.Add(withdawal);
    }

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

    public virtual void PerformMonthAndTransactions()
    {

    }
    //переопределяем метод который унаследовали от object этот метод должен возвращать строку с состоянием объекта 
    public override string ToString()
    //{
    //    return $"Type: {GetType().Name}\tOwner : {Owner}\tNumber of account : {Number}"
    //}
    => $"Type: {GetType().Name}\tOwner : {Owner}\tNumber of account : {Number}";

}
