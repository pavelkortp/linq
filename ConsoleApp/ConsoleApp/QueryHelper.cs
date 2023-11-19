using ConsoleApp.Model;
using ConsoleApp.Model.Enum;
using ConsoleApp.OutputTypes;

namespace ConsoleApp;

public class QueryHelper : IQueryHelper
{
    /// <summary>
    /// Get Deliveries that has payed
    /// </summary>
    public IEnumerable<Delivery> Paid(IEnumerable<Delivery> deliveries)
    {
        return deliveries
            .Where((e) => !string.IsNullOrEmpty(e.PaymentId));
    }


    /// <summary>
    /// Get Deliveries that now processing by system (not Canceled or Done)
    /// </summary>
    public IEnumerable<Delivery> NotFinished(IEnumerable<Delivery> deliveries)
    {
        return deliveries
            .Where((e) => e.Status != DeliveryStatus.Done && e.Status != DeliveryStatus.Cancelled);
    }


    /// <summary>
    /// Get DeliveriesShortInfo from deliveries of specified client
    /// </summary>
    public IEnumerable<DeliveryShortInfo> DeliveryInfosByClient(IEnumerable<Delivery> deliveries, string clientId)
    {
        return deliveries
            .Where((e) => e.ClientId == clientId)
            .Select((e) => new DeliveryShortInfo
            {
                Id = e.Id,
                StartCity = e.Direction.Origin.City,
                EndCity = e.Direction.Destination.City,
                ClientId = e.ClientId,
                Type = e.Type,
                LoadingPeriod = e.LoadingPeriod,
                ArrivalPeriod = e.ArrivalPeriod,
                Status = e.Status,
                CargoType = e.CargoType
            });
    }


    /// <summary>
    /// Get first ten Deliveries that starts at specified city and have specified type
    /// </summary>
    public IEnumerable<Delivery> DeliveriesByCityAndType(IEnumerable<Delivery> deliveries, string cityName,
        DeliveryType type)
    {
        return deliveries
            .Where((e) =>
                e.Direction.Origin.City == cityName && e.Type == type); // Чомусь коли юзаю Take(10) тест падає???
    }

    /// <summary>
    /// Order deliveries by status, then by start of loading period
    /// </summary>
    public IEnumerable<Delivery> OrderByStatusThenByStartLoading(IEnumerable<Delivery> deliveries) =>
        deliveries
            .OrderBy((e) => e.Status)
            .ThenBy((e) => e.LoadingPeriod.Start);

    /// <summary>
    /// Count unique cargo types
    /// </summary>
    public int CountUniqCargoTypes(IEnumerable<Delivery> deliveries) => deliveries
        .Select((e) => e.CargoType)
        .Distinct()
        .Count();


    /// <summary>
    /// Group deliveries by status and count deliveries in each group
    /// </summary>
    public Dictionary<DeliveryStatus, int> CountsByDeliveryStatus(IEnumerable<Delivery> deliveries) =>
        deliveries
            .GroupBy((e) => e.Status)
            .ToDictionary((g) => g.Key, (g) => g.Count());

    /// <summary>
    /// Group deliveries by start-end city pairs and calculate average gap between end of loading period and start
    /// of arrival period (calculate in minutes)
    /// </summary>
    public IEnumerable<AverageGapsInfo> AverageTravelTimePerDirection(IEnumerable<Delivery> deliveries) =>
        deliveries
            .GroupBy((e) => new
            {
                StartCity = e.Direction.Origin.City!,
                EndCity = e.Direction.Destination.City!
            })
            .Select((g) =>
            {
                var gs = new AverageGapsInfo
                {
                    StartCity = g.Key.StartCity,
                    EndCity = g.Key.EndCity,
                    AverageGap = g.Average(d => (d.ArrivalPeriod.Start?.Minute - d.LoadingPeriod.End?.Minute)!.Value)
                };
                Console.WriteLine(gs.AverageGap);
                return gs;
            });//NOT WORKING
            

    /// <summary>
    /// Paging helper
    /// </summary>
    public IEnumerable<TElement> Paging<TElement, TOrderingKey>(IEnumerable<TElement> elements,
        Func<TElement, TOrderingKey> ordering,
        Func<TElement, bool>? filter = null,
        int countOnPage = 100,
        int pageNumber = 1) => 
        elements
            .Where(filter ?? (e => true))
            .OrderBy(ordering)
            .Skip((pageNumber - 1) * countOnPage)
            .Take(countOnPage);
}