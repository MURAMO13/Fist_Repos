using Account.Interfests;
namespace AccountFistOption.Calculators;
/// <summary>
/// Класс для калькулятора
/// </summary>
public static class Calculator
{
   public static void CalculateInterest( ICalculator calculator)
   {
        calculator.CalculateInterest();
   }
}