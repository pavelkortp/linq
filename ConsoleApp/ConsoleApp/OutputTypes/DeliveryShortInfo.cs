using ConsoleApp.Model;
using ConsoleApp.Model.Enum;

namespace ConsoleApp.OutputTypes;

public record DeliveryShortInfo
{
    public string Id { get; set; }
    public string? StartCity { get; set; }
    public string? EndCity { get; set; }
    public string ClientId { get; set; }
    public DeliveryType Type { get; set; }
    public TimePeriod LoadingPeriod { get; set; }
    public TimePeriod ArrivalPeriod { get; set; }
    public DeliveryStatus Status { get; set; }
    public string CargoType { get; set; }
}