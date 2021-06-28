using System.ComponentModel.DataAnnotations;

namespace DwxBlazor.RazorLib.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string CompanyName { get; set; }

        [Required]
        [Url]
        public string WebAddress { get; set; }

        [Required]
        public string Location { get; set; }
    }
}