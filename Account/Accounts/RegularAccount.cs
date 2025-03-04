using Account.Interfests;
namespace Account.Accounts;
/// <summary>
/// Класс, описывающий обычную учетную запись
/// </summary>
public class RegularAccount : Account, ICalculator
{
    public RegularAccount(double balance) : base(balance)
    {
    }

    public void CalculateInterest()
    {
        Interest = Balance * 0.4;

        if (Balance < 1000)
            Interest -= Balance * 0.2;
        else
            Interest -= Balance * 0.4;
    }
}
