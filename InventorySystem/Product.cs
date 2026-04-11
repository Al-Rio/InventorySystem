// Data Annotations provide server-side validation for the forms.
using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }
    // Required product name
    [Required]
    public string Name { get; set; } = string.Empty;
    // Price required
    [Required]
    public decimal Price { get; set; }
    // Short description
    [Required]
    public string Description { get; set; } = string.Empty;
    // Stock quantity 
    [Range(0, 1000)]
    public int StockQuantity { get; set; }
}
