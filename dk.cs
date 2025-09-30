public class dk : Character
{
    public string? species { get; set; }
    public override string Display()
    {
        return base.Display() + $"species : {species}\n";
    }
}