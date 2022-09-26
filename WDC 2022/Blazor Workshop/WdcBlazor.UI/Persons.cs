using System.ComponentModel.DataAnnotations;

namespace WdcBlazor.UI;

public class Person
{
    [Required, MaxLength(10), MinLength(3)]
    public string Vorname { get; set; }

    [Required, MaxLength(10), MinLength(3)]
    public string Nachname { get; set; }
}