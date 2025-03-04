using Account.Interfests;
namespace Account.Accounts;
/// <summary>
/// // Класс, описывающий зарплатную учетную запись
/// </summary>
public class SalaryAccount : Account, ICalculator
{
    public SalaryAccount(double balance) : base(balance)
    {
    }
    public void CalculateInterest()
    {
        Interest = Balance * 0.5;
    }
}
