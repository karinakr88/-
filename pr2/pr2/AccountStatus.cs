
namespace pr2;
/// <summary>
/// Статус клиента банка, определяющий условия обслуживания.
/// </summary>
public enum AccountStatus
{
    /// <summary>Обычный клиент. Комиссия 50 руб./мес.</summary>
    Regular,

    /// <summary>Премиум-клиент. Без комиссии, 1% на остаток свыше 10 000 руб.</summary>
    Premium,

    /// <summary>VIP-клиент. 0,3% на остаток свыше 10 000 руб. и 5% кэшбэка на списания.</summary>
    VIP
}   
