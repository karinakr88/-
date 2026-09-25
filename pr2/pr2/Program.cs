

namespace pr2;

internal class Program
{
    static void Main(string[] args)
    {
        BankAccount account1 = new BankAccount("yana", 1000);
        BankAccount account2 = new BankAccount("lena", 10090);
        //account.MakeDeposit();MakeWithdrawal();

        Console.WriteLine($"account{account1.Balance} №{account1.Number} {account1.Owner}");
        Console.WriteLine($"account{account2.Balance} №{account2.Number} {account2.Owner}");

        account1.MakeDeposit(23476,DateTime.UtcNow, ":)");
        Console.WriteLine(account1.Balance);
        account1.MakeWithdrawal(26, DateTime.UtcNow, ":(");
        Console.WriteLine(account1.Balance);

        try
        {
            account2.MakeWithdrawal(3292, DateTime.UtcNow, ":(");

        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
