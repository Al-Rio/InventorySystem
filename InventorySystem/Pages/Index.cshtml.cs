using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Pages;

public class IndexModel : PageModel
{
    
    private readonly MyDbContext _context;
    // Constructor injects the application's DbContext.
    public IndexModel(MyDbContext context)
    {
        _context = context;
    }

    // Public property the Razor view can access via Model.Products
    public IList<Product> Products { get; set; } = new List<Product>();

    // Load the entities from the database into the Products property.
    public async Task OnGetAsync()
    {
        Products = await _context.Products.ToListAsync();
    }
}
