using System.ComponentModel.DataAnnotations;

namespace Showcase_Contactpagina.Models
{
    public class Contactform
    {
        [Required]
        [StringLength(60)]
        public string FirstName {  get; set; }

        [Required]
        [StringLength(60)]
        public string LastName {  get; set; }

        [Required]
        [StringLength(80)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        [Phone]
        public string Phone { get; set; }
    }
}
