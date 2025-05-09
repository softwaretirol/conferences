namespace DNDF25.Components
{
    public class EvenCalculator
    {
        public static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        public static string IsEvenColor(int i)
        {
            return IsEven(i) ? "color: green" : "color: red";
        }

        public static string IsEvenColorClass(int i)
        {
            return IsEven(i) ? "even" : "odd";
        }
    }
}
