using System.ComponentModel.DataAnnotations;

namespace ServerSideSample.Data
{
    public class Person
    {
        public int Id { get; set; }

        [MyRequired]
        [MinLength(1)]
        [MaxLength(10)]
        [DoesNotContain(ErrorMessage = "Nix gute")]
        public string Vorname { get; set; }

        [MyRequired]
        [MinLength(1)]
        [MaxLength(10)]
        public string Nachname { get; set; }
    }

    public class MyRequired : RequiredAttribute
    {
        public MyRequired()
        {
            ErrorMessage = "Pflichtfeld";
        }
    }

    public class DoesNotContainAttribute : ValidationAttribute
    {
        public DoesNotContainAttribute()
        {
            ErrorMessage = "Eingabe ist mangelhaft...";
        }

        public override bool IsValid(object value)
        {
            if (value is string text)
            {
                return !text.Contains("<") &&
                       !text.Contains(">");
            }

            return true;
        }
    }
}