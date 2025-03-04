namespace Account.Accounts;
/// <summary>
/// Абстрактный класс, описывающий учетную запись
/// </summary>
public abstract class Account
{
    // баланс учетной записи
    public double Balance { get; set; }

    // процентная ставка
    public double Interest { get; set; }

    public Account(double balance)
    {
        Balance = balance;
    }

}
