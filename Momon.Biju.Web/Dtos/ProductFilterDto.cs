using Momon.Biju.App.Domain.Entities;

namespace Momon.Biju.Web.Models;

public class ProductFilterDto
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }

    public List<CategoryViewModel> Categories { get; set; } = [];
}