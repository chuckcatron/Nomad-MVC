using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nomad_MVC.Models
{
    public class Car: IValidatableObject
    {
        [Key]
        public int Id { get; set; }
       
        [Required(ErrorMessage = "Category is required!")]
        [Display(Name = "Category")]
        public Category Category { get; set; }
        
        [Required(ErrorMessage = "Make is required!")]
        [Display(Name = "Make")]
        public Make Make { get; set; }
        
        [Required(ErrorMessage = "Model is required!")]
        [Display(Name = "Model")]
        public Model Model { get; set; }
        
        [Required(ErrorMessage = "Color is required!")]
        [Display(Name = "Color")]
        public Color Color { get; set; }

        [Required(ErrorMessage = "Year is required!")]
        [Display(Name = "Year")]
        [Range(1990, 2021)]
        public int Year { get; set; } = 2021;

        [Required(ErrorMessage = "Price is required!")]
        [Display(Name = "Price")]
        [Range(100, 80000)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; } = 10000;
        [NotMapped]
        public IEnumerable<SelectListItem> Categories { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> Makes { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> Models { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> Colors { get; set; }

        public byte[] Image { get; set; }
        [NotMapped]
        public string ImageString { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Image== null && Id <= 0)
            {
                yield return new ValidationResult("Image is required", new List<string> { "Image" });
            }
        }
    }

    [NotMapped]
    public class CarImage
    {
        [Display(Name = "Image")]
        public int Id { get; set; }
        public byte[] Image { get; set; }
        [NotMapped]
        public string ImageString { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
