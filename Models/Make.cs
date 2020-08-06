using System.ComponentModel.DataAnnotations;

namespace Nomad_MVC.Models
{
    public class Make
    {
        [Key]
        [Display(Name = "Make")]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
