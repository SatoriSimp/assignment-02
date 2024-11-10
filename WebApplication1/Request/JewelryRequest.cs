using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Request
{
    public class JewelryRequest
    {
        [Key]
        public string Id {  get; set; }

        [Required]
        [RegularExpression("^[A-Z][a-zA-Z0-9\\s]*$", ErrorMessage = "Invalid name format.")]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required]
        public decimal MetalWeight { get; set; }

        [Required]
        [Range(1900, int.MaxValue, ErrorMessage = "ProductionYear must be >= 1900.")]
        public int ProductionYear { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string CategoryId { get; set; }
    }
}
