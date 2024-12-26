using System.ComponentModel.DataAnnotations;
namespace Entities.Models;

public class Product
{
       
        public int ProductId{ get; set; }

        [Required(ErrorMessage="ProductName is required")]
        public String? ProductName{ get; set; } =String.Empty;
        
        [Required(ErrorMessage="ProductPrice is required")]
        public decimal ProductPrice{ get; set; }

[Required(ErrorMessage="Duration is required")]
        public TimeSpan Duration { get; set; }

[Required(ErrorMessage="ProductDescription is required")]
        public string ProductDescription { get; set; }

[Required(ErrorMessage="CategoryId is required")]
        public int? CategoryId { get; set; } //Foreign Key

        public Category? Category { get; set; } //Navigation property

      

// Navigation Property
   
   

   public ICollection<EmployeeProduct> EmployeeProducts { get; set; } = new List<EmployeeProduct>(); // Many-to-Many Relationship
}
