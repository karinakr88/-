

namespace pr2;

public class Program
{
    static void Main(string[] args)
    {
        BankAccount account1 = new BankAccount("yana", 1000);
        BankAccount account2 = new BankAccount("lena", 10090);
        //account.MakeDeposit();MakeWithdrawal();

        Console.WriteLine($"account{account1.Balance} №{account1.Number} {account1.Owner}");
        Console.WriteLine($"account{account2.Balance} №{account2.Number} {account2.Owner}");

        account1.MakeDeposit(23476, DateTime.UtcNow, ":)");
        Console.WriteLine(account1.Balance);
        account1.MakeWithdrawal(26, DateTime.UtcNow, ":(");
        Console.WriteLine(account1.Balance);
        Console.WriteLine(account1.GetAccountHistory());

        try
        {
            account2.MakeWithdrawal(3292, DateTime.UtcNow, ":(");

        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
        InterestEarningAccount interestEarning = new("Yana", 1000m); //m-decimal
        interestEarning.MakeDeposit(100m, DateTime.UtcNow, ";");
        interestEarning.MakeWithdrawal(10m, DateTime.UtcNow, ";");
        interestEarning.PerformMonthAndTransactions();

        Console.WriteLine(interestEarning);//    ==    Console.WriteLine(interestEarning.ToString());
        Console.WriteLine(interestEarning.GetAccountHistory());

        GiftCartAccount giftCart = new("Yana", 1000m, 5000m);
        giftCart.MakeDeposit(100m, DateTime.UtcNow, ";");
        giftCart.MakeWithdrawal(10m, DateTime.UtcNow, ";");
        giftCart.PerformMonthAndTransactions();
        Console.WriteLine(giftCart);
        Console.WriteLine(giftCart.GetAccountHistory());
    }
}
