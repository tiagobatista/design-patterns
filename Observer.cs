public interface IObserver
{
    void Update(string message);
}

public class Customer : IObserver
{
    public string Name { get; set; }

    public Customer(string name) { Name = name; }

    public void Update(string message)
    {
        Console.WriteLine($"{Name} received notification: {message}");
    }
}

public class Store
{
    private List<IObserver> customers = new List<IObserver>();

    public void AddCustomer(IObserver observer)
    {
        customers.Add(observer);
    }

    public void Notify(string message)
    {
        foreach (var customer in customers)
        {
            customer.Update(message);
        }
    }

    public void NewArrival(string item)
    {
        Console.WriteLine($"New arrival: {item}");
        Notify($"New {item} has arrived in the store!");
    }
}