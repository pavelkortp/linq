namespace ConsoleApp.OutputTypes;

public record AverageGapsInfo
{
    public string StartCity { get; set; }
    public string EndCity { get; set; }
    public double AverageGap { get; set; } // in minutes
}