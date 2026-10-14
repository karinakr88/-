namespace pr2;
/// <summary>
/// Содержит точку входа в приложение и демонстрационные сценарии работы с банковскими счетами
/// </summary>
public class Program
{
    /// <summary>
    /// Точка входа в приложение. Демонстрирует работу с различными типами банковских счетов
    /// </summary>
    /// <param name="args">Аргументы командной строки</param>
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

        InterestEarningAccount interest = new InterestEarningAccount("Yana", 1000);
        interest.PerformMonthAndTransactions();

        LineOfCreditAccount LineOfCredit = new LineOfCreditAccount("Yana", 0, 1000m);
        LineOfCredit.MakeWithdrawal(500m, DateTime.UtcNow, "credit");
        GiftCartAccount giftcart = new GiftCartAccount("Yana", 1000m, 5000m);
        List<BankAccount> accounts = new List<BankAccount>();
        accounts.Add(account1);
        accounts.Add(interest);
        accounts.Add(LineOfCredit);
        accounts.Add(giftcart);

        foreach (BankAccount account in accounts)
        {
            Console.WriteLine(account);// == Console.WriteLine(account.ToString())
            account.PerformMonthAndTransactions();
            Console.WriteLine(account.GetAccountHistory());
        }
        LineOfCredit.MakeWithdrawal(60m,DateTime.UtcNow, "credit");
        Console.WriteLine(LineOfCredit.GetAccountHistory());

        //  Проверка статусов 
        Console.WriteLine("\n Regular ");
        var regular = new BankAccount("Ivan", 20_000m);
        regular.Status = AccountStatus.Regular;
        regular.PerformMonthAndTransactions();
        Console.WriteLine(regular.GetAccountHistory());

        Console.WriteLine("\nPremium ");
        var premium = new BankAccount("Petr", 20_000m);
        premium.Status = AccountStatus.Premium;
        premium.PerformMonthAndTransactions();
        Console.WriteLine(premium.GetAccountHistory());

        Console.WriteLine("\n VIP ");
        var vip = new BankAccount("Anna", 20_000m);
        vip.Status = AccountStatus.VIP;
        vip.MakeWithdrawal(5_000m, DateTime.UtcNow, "Покупка");
        vip.PerformMonthAndTransactions();
        Console.WriteLine(vip.GetAccountHistory());

        // === Проверка аудита ===
        AuditLog.Clear();

        var testAccount = new BankAccount("AuditTest", 20_000m);
        testAccount.MakeDeposit(1_000m, DateTime.UtcNow, "зарплата");
        testAccount.MakeWithdrawal(500m, DateTime.UtcNow, "продукты");

        Console.WriteLine("\n--- Audit Log ---");
        foreach (var entry in AuditLog.GetEntries())
        {
            Console.WriteLine(entry);
        }
        // === Проверка перевода ===
        Console.WriteLine("\n--- Transfer ---");
        var accountA = new BankAccount("Alice", 10_000m);
        var accountB = new BankAccount("Bob", 5_000m);

        accountA.Transfer(accountB, 1_500m, DateTime.UtcNow, "Оплата долга");

        Console.WriteLine($"A balance: {accountA.Balance}");   // 8500
        Console.WriteLine($"B balance: {accountB.Balance}");   // 6500

        Console.WriteLine("\n--- История A ---");
        Console.WriteLine(accountA.GetAccountHistory());

        Console.WriteLine("\n--- История B ---");
        Console.WriteLine(accountB.GetAccountHistory());
    }
}

