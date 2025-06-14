namespace Momon.Biju.App.Infra.Repositories;

internal class ProductRow
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public string ImagePath { get; set; } = "";
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = "";
    public int Total { get; set; }
}
