public abstract class Logistics
{
    public abstract ITransport CreateTransport();
}

public class RoadLogistics : Logistics
{
    public override ITransport CreateTransport()
    {
        return new Truck();
    }
}

public class SeaLogistics : Logistics
{
    public override ITransport CreateTransport()
    {
        return new Ship();
    }
}

public interface ITransport
{
    void Deliver();
}

public class Truck : ITransport
{
    public void Deliver()
    {
        Console.WriteLine("Delivered by Truck.");
    }
}

public class Ship : ITransport
{
    public void Deliver()
    {
        Console.WriteLine("Delivered by Ship.");
    }
}