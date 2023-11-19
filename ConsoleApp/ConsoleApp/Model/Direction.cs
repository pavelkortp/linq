namespace ConsoleApp.Model;

public record Direction
{
    public Address Origin { get; set; }
    public Address Destination { get; set; }
}