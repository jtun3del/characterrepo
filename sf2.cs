public class sf2 : Character
{
    public List<string> Specials { get; set; } = [];
    public override string Display()
    {
        return base.Display() + $"Specials : {string.Join(", ", Specials)}\n";
    }
}