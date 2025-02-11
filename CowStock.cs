namespace KSAApi;

public class CowStock
{
    public DateOnly Date { get; set; }

    public string? Type { get; set; }

    public int Quantity { get; set; }

    public int Days { get; set; }

    public string? Summary { get; set; }
}
