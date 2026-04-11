using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Namespace
{
    public class ProductModel : PageModel
    {
        private readonly MyDbContext _context;

        /// Constructor injects the application's DbContext.
        public ProductModel(MyDbContext context)
        {
            _context = context;
        }

        // Public list of products so the view can access Model.Products.
        public IList<Product> Products { get; set; } = new List<Product>();

        public class ProductViewModel
        {
            [Required(ErrorMessage = "Product Name is required.")]
            [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
            public string Name { get; set; } = string.Empty;

            [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01$ and 100000$")]
            public decimal Price { get; set; }

            [Range(0, 100000, ErrorMessage = "Stock quantity must be between 0 and 100000")]
            public int StockQuantity { get; set; }

            [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
            public string Description { get; set; } = string.Empty;
        }

        // BindProperty to ensures Input is populated from (form POST) values.
        [BindProperty]
        public ProductViewModel Input { get; set; } = new ProductViewModel();

        public async Task OnGetAsync()
        {
            // Load Product entities into the DbContext.
            Products = await _context.Products.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Server-side validation using Data Annotations
            if (!ModelState.IsValid)
            {
                // Reload products so the page can display the list and the validation errors at the same time
                Products = await _context.Products.ToListAsync();
                return Page();
            }

            // Map the validated input from the user to the Product entity
            var product = new Product
            {
                Name = Input.Name,
                Price = Input.Price,
                Description = Input.Description,
                StockQuantity = Input.StockQuantity
            };

            // Add the new product to the DbContext and save changes
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Redirect to avoid duplicate form submissions.
            return RedirectToPage();
        }
    }
}