using ConsoleApp.Model;
using ConsoleApp.Model.Enum;
using ConsoleApp.OutputTypes;

namespace ConsoleApp;

public interface IQueryHelper
{
    IEnumerable<Delivery> Paid(IEnumerable<Delivery> deliveries);
    IEnumerable<Delivery> NotFinished(IEnumerable<Delivery> deliveries);
    IEnumerable<DeliveryShortInfo> DeliveryInfosByClient(IEnumerable<Delivery> deliveries, string clientId);
    IEnumerable<Delivery> DeliveriesByCityAndType(IEnumerable<Delivery> deliveries, string cityName, DeliveryType type);
    IEnumerable<Delivery> OrderByStatusThenByStartLoading(IEnumerable<Delivery> deliveries);
    int CountUniqCargoTypes(IEnumerable<Delivery> deliveries);
    Dictionary<DeliveryStatus, int> CountsByDeliveryStatus(IEnumerable<Delivery> deliveries);
    IEnumerable<AverageGapsInfo> AverageTravelTimePerDirection(IEnumerable<Delivery> deliveries);

    public IEnumerable<TElement> Paging<TElement, TOrderingKey>(IEnumerable<TElement> elements,
        Func<TElement, TOrderingKey> ordering,
        Func<TElement, bool>? filter = null,
        int countOnPage = 100,
        int pageNumber = 1);
}