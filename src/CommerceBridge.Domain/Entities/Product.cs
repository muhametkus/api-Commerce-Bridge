using CommerceBridge.Domain.Common;

namespace CommerceBridge.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public bool IsActive { get; set; } = true;
}