namespace pr2;
/// <summary>
/// Представляет одну финансовую транзакцию по счёту
/// Является неизменяемым (immutable) типом
/// </summary>
/// <param name="Amount">Сумма транзакции. Положительная — пополнение, отрицательная — снятие</param>
/// <param name="date">Дата и время совершения транзакции</param>
/// <param name="Note">Комментарий к транзакции</param>

public record Transactionn(decimal Amount, DateTime date, string Note); // record - Состояние объектов этого класса нельзя изменить

// это то же самое 

//internal record Transactionn
//{
//    public decimal Amount { get; }
//    public DateTime Date { get; }
//    public string Note { get; }
//     public Transactionn(decimal Amount, DateTime date, string Note)
//    { 
//        this.Amount = Amount;
//        this.Note = Note;
//        this.Date = Date;
//    }
//}

