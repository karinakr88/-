

namespace pr2;

internal record Transactionn(decimal Amount, DateTime date, string Note); // record - Состояние объектов этого класса нельзя изменить

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

