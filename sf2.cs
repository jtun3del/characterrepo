public class sf2 : Character
{
    public List<string> Moves { get; set; } = [];
    public override string Display()
    {
        return base.Display() + $"Moves : {string.Join(", ", Moves)}\n";
    }
}