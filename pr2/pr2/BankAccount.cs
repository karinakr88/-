
namespace pr2;

internal class BankAccount
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
}
