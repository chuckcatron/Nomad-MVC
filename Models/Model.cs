using System.ComponentModel.DataAnnotations;

namespace Nomad_MVC.Models
{
    public class Model
    {
        [Key]
        [Display(Name = "Model")]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
