using System.Text.Json;
using ConsoleApp;
using ConsoleApp.Model;
using ConsoleApp.Model.Enum;
using ConsoleApp.OutputTypes;

var deliveries = JsonSerializer.Deserialize<List<Delivery>>(FileText("Dataset")) ?? throw new ArgumentException();
QueryHelper helper = new QueryHelper();

var task1 = JsonSerializer.Deserialize<List<Delivery>>(FileText("task1")) ?? throw new ArgumentException();
PrintResult("task1",helper.Paid(deliveries).SequenceEqual(task1));

var task2 = JsonSerializer.Deserialize<List<Delivery>>(FileText("task2")) ?? throw new ArgumentException();
PrintResult("task2",helper.NotFinished(deliveries).SequenceEqual(task2));

var task3_1 = JsonSerializer.Deserialize<List<DeliveryShortInfo>>(FileText("task3_1")) ?? throw new ArgumentException();
PrintResult("task3_1",helper.DeliveryInfosByClient(deliveries, "f2467cf2-2f3e-4fc9-b6d8-6678ba2a29f3").SequenceEqual(task3_1));

var task3_2 = JsonSerializer.Deserialize<List<DeliveryShortInfo>>(FileText("task3_2")) ?? throw new ArgumentException();
PrintResult("task3_2",helper.DeliveryInfosByClient(deliveries, "0e0b7048-cb7c-4046-9d7c-2051b07c7e05").SequenceEqual(task3_2));

var task4 = JsonSerializer.Deserialize<List<Delivery>>(FileText("task4")) ?? throw new ArgumentException();
PrintResult("task4",helper.DeliveriesByCityAndType(deliveries, "Kryvyi Rih", DeliveryType.TruckOnly).SequenceEqual(task4));

var task5 = JsonSerializer.Deserialize<List<Delivery>>(FileText("task5")) ?? throw new ArgumentException();
PrintResult("task5",helper.OrderByStatusThenByStartLoading(deliveries).SequenceEqual(task5));

var task6 = JsonSerializer.Deserialize<int>(FileText("task6"));
PrintResult("task6",helper.CountUniqCargoTypes(deliveries) == task6);

var task7 = JsonSerializer.Deserialize<Dictionary<DeliveryStatus, int>>(FileText("task7")) ?? throw new ArgumentException();
PrintResult("task7",helper.CountsByDeliveryStatus(deliveries).SequenceEqual(task7));

var task8 = JsonSerializer.Deserialize<List<AverageGapsInfo>>(FileText("task8")) ?? throw new ArgumentException();
PrintResult("task8",helper.AverageTravelTimePerDirection(deliveries).SequenceEqual(task8));

var task9_1 = JsonSerializer.Deserialize<List<Delivery>>(FileText("task9_1")) ?? throw new ArgumentException();
PrintResult("task9_1",helper.Paging(
    deliveries, 
    x => x.CargoType, 
    x => x.Status is DeliveryStatus.InProgress).SequenceEqual(task9_1));

var task9_2 = JsonSerializer.Deserialize<List<Delivery>>(FileText("task9_2")) ?? throw new ArgumentException();
PrintResult("task9_2",helper.Paging(
    deliveries, 
    x => x.ArrivalPeriod.Start, 
    x => x.Direction.Origin.City == "Lviv").SequenceEqual(task9_2));

var task9_3 = JsonSerializer.Deserialize<List<Delivery>>(FileText("task9_3")) ?? throw new ArgumentException();
PrintResult("task9_3",helper.Paging(
    deliveries, 
    x => x.ArrivalPeriod.Start, 
    x => x.Direction.Origin.City == "Lviv",
    25,
    3).SequenceEqual(task9_3));

string FileText(string fileName)
{
    using StreamReader reader = new StreamReader($"../../../../{fileName}.json");
    return reader.ReadToEnd();
}
void PrintResult(string text, bool result)
{
    Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
    Console.WriteLine(text + ":" + (result ? "Passed" : "Failed"));
}