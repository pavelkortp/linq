using ConsoleApp.Model.Enum;

namespace ConsoleApp.Model;

public record Delivery
{
    public string Id { get; set; }
    public Direction Direction { get; set; }
    public string ClientId { get; set; }
    public DeliveryType Type { get; set; }
    public string? PaymentId { get; set; }
    public TimePeriod LoadingPeriod { get; set; }
    public TimePeriod ArrivalPeriod { get; set; }
    public DeliveryStatus Status { get; set; }
    public string CargoType { get; set; }
}