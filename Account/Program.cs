using Account.Accounts;
using AccountFistOption.Calculators;
namespace Account;

internal class Program
{
    static void Main(string[] args)
    {
        RegularAccount regularAccount = new RegularAccount(30_000);
        Calculator.CalculateInterest(regularAccount);
        Console.WriteLine($"\tRegular Account Interest: {regularAccount.Interest}\n");

        SalaryAccount salaryAccount = new SalaryAccount(30_000);
        Calculator.CalculateInterest(salaryAccount);
        Console.WriteLine($"\tSalary Account Interest: {salaryAccount.Interest}");
    }
}
