using System.ComponentModel.DataAnnotations;

namespace Nomad_MVC.Models
{
    public class Category
    {
        [Key] 
        [Display(Name = "Category")] 
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
