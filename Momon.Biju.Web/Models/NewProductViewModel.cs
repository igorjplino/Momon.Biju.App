using Microsoft.AspNetCore.Mvc.Rendering;

namespace Momon.Biju.Web.Models;

public class NewProductViewModel
{
    public string Name { get; set; }
    public string Price { get; set; }
    public Guid CategoryId { get; set; }
    
    public IEnumerable<SelectListItem> Categories { get; set; }
}