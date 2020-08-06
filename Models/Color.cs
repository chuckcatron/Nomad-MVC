using System.ComponentModel.DataAnnotations;

namespace Nomad_MVC.Models
{
    public class Color
    {
        [Key]
        [Display(Name = "Color")]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
