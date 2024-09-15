# design-patterns

### **Title**  
**Top 5 Design Patterns in C# Every Developer Should Master – Real Examples & Applications**

---

### **Section 1: Singleton Pattern **

**Explanation**:  
“Let’s start with the **Singleton pattern**. This is one of the simplest design patterns, but also one of the most misunderstood. The Singleton pattern ensures that a class has only one instance and provides a global point of access to it. Imagine you’re working on an application that needs to interact with a database. You don’t want multiple instances of your database class floating around, because that could cause all sorts of problems—like resource contention and memory issues. The Singleton pattern ensures that the database class, for example, is instantiated only once and shared across the entire application.”

**Detailed Explanation**:  
- **How it works**:  
  “The Singleton pattern restricts the instantiation of a class to a single object. It also provides a global point of access, so whenever you need that instance, you just call a method that gives you the already-created instance.”
  
- **Thread Safety**:  
  “In multi-threaded environments, you need to ensure that your Singleton instance is created only once, even if multiple threads try to instantiate it at the same time. That’s where things like `lock` and `double-checked locking` come in.”

**Code Example**:
```csharp
public class Database
{
    private static Database _instance;
    private static readonly object _lock = new object();

    private Database() { /* Private constructor to prevent instantiation */ }

    public static Database GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
            }
        }
        return _instance;
    }
}
```

**Real-World Applicability**:
- **When to use it**:  
  “Use the Singleton pattern when you need exactly one instance of a class to control system-wide behavior, like database connections, logging, or managing configuration settings.”

**Scenario – What can go wrong**:  
“If you don’t implement the Singleton pattern and instead create multiple instances of your database class, you could end up with inconsistencies in how your application interacts with the database. Imagine multiple components all making changes to the same data source but with different instances. This would lead to resource contention, memory leaks, or worse—corrupted data.”

---

### **Section 2: Factory Method Pattern **

**Explanation**:  
“Next up is the **Factory Method pattern**. This pattern is used to create objects without specifying the exact class of object that will be created. Think about an application where you have different types of transport options—like a truck for land transport or a ship for sea transport. Based on the need, you want your application to instantiate the correct transport method, but without modifying your core logic every time you add a new transport type. That’s where the Factory Method pattern shines.”

**Detailed Explanation**:  
- **How it works**:  
  “In this pattern, you define an interface or abstract class for creating an object, but you allow subclasses to decide which class to instantiate. The Factory Method pattern is all about creating objects in a way that makes your code more flexible and easier to extend.”
  
- **Why it’s useful**:  
  “It decouples the client code from the actual instantiation of the objects. If you need to add new types of objects in the future, you don’t have to change your client code, just the factory class.”

**Code Example**:
```csharp
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
```

**Real-World Applicability**:
- **When to use it**:  
  “The Factory Method pattern is ideal when the exact type of object needed isn’t known until runtime, or when you want to avoid coupling the client code to specific classes.”

**Scenario – What can go wrong**:  
“Without the Factory Method, you’d have to write conditionals in your code to create `Truck` or `Ship` objects. Every time you add a new type of transport—like an airplane—you’d have to modify your client code, which could introduce bugs. With the Factory Method, you just extend the factory to handle new transport types, keeping the client code clean.”

---

### **Section 3: Observer Pattern **

**Explanation**:  
“Now let’s talk about the **Observer pattern**. This pattern is useful when you have one object that needs to notify other objects about changes in its state. For example, let’s say you have a store, and when a new product arrives, you want to notify all your customers. Instead of manually keeping track of each customer and sending updates to them, the Observer pattern lets you do this dynamically.”

**Detailed Explanation**:  
- **How it works**:  
  “The Observer pattern defines a one-to-many relationship between objects. When the state of one object changes, all its dependents, or ‘observers’, are automatically notified and updated. This pattern is widely used in event-driven systems and GUI applications.”

- **Advantages**:  
  “One of the biggest advantages is decoupling. The subject and the observers are loosely coupled, meaning you can easily add or remove observers without modifying the subject itself.”

**Code Example**:
```csharp
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
```

**Real-World Applicability**:
- **When to use it**:  
  “Use the Observer pattern when one object needs to notify multiple objects of changes, like in event systems, GUIs, or notifications.”

**Scenario – What can go wrong**:  
“Without the Observer pattern, you’d have to manually notify each observer, which could lead to tight coupling between objects. Imagine a situation where you need to notify 50 customers—hardcoding this would result in a mess. The Observer pattern makes this process dynamic and scalable.”

---

### **Section 4: Decorator Pattern **

**Explanation**:  
“Next, we have the **Decorator pattern**. This is great for adding functionality to an object dynamically without altering its structure. Think of it like adding toppings to a pizza—you start with a basic pizza and then add cheese, peppers, or mushrooms based on what you need. In software, you might have a core object, like a coffee order, and you want to add options like milk, sugar, or flavor shots at runtime. Instead of modifying the core object, you use decorators to add this behavior.”

**Detailed Explanation**:  
- **How it works**:  
  “The Decorator pattern involves wrapping an object with another object that adds additional responsibilities. Each decorator implements the same interface as the original object, which allows you to chain them together seamlessly.”

- **Why it’s useful**:  
  “The Decorator pattern is useful when you need to add functionality in a flexible and scalable way. Instead of creating subclasses for every possible combination, you create decorator classes that can be composed.”

**Code Example**:
```csharp
public interface ICoffee
{
    string GetDescription();
    double GetCost();
}

public class SimpleCoffee : ICoffee
{
    public string GetDescription() => "Simple coffee";
    public double GetCost

() => 5.0;
}

public abstract class CoffeeDecorator : ICoffee
{
    protected ICoffee _coffee;

    public CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public virtual string GetDescription() => _coffee.GetDescription();
    public virtual double GetCost() => _coffee.GetCost();
}

public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee) { }

    public override string GetDescription() => _coffee.GetDescription() + ", with Milk";
    public override double GetCost() => _coffee.GetCost() + 1.5;
}

public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee) : base(coffee) { }

    public override string GetDescription() => _coffee.GetDescription() + ", with Sugar";
    public override double GetCost() => _coffee.GetCost() + 0.5;
}
```

**Real-World Applicability**:
- **When to use it**:  
  “Use the Decorator pattern when you want to add responsibilities to individual objects dynamically, without affecting other objects of the same class.”

**Scenario – What can go wrong**:  
“If you don’t use the Decorator pattern, you might end up creating subclasses for every combination of features. Imagine a coffee shop app where you need to manage different combinations of coffee with milk, sugar, or both. Without decorators, you’d have to create separate classes for every combination, which becomes unmanageable as options grow.”

---

### **Section 5: Strategy Pattern **

**Explanation**:  
“The last pattern is the **Strategy pattern**. This pattern allows you to define a family of algorithms, encapsulate them, and make them interchangeable. A good example of this is a file compression tool where you may want to choose between ZIP, RAR, or 7-Zip compression algorithms. Instead of hardcoding these algorithms into your class, you can use the Strategy pattern to select which algorithm to use at runtime.”

**Detailed Explanation**:  
- **How it works**:  
  “The Strategy pattern defines a set of algorithms, encapsulates each one in a separate class, and makes them interchangeable. This means you can change the behavior of an object at runtime without modifying its code. It’s especially useful when you have multiple ways of doing something and you want to be able to switch between them easily.”

- **Advantages**:  
  “The Strategy pattern promotes code reusability and avoids the use of complex conditionals inside a class. Instead of cluttering your code with `if-else` or `switch` statements to choose algorithms, the pattern lets you pass in the desired behavior.”

**Code Example**:
```csharp
public interface ICompressionStrategy
{
    void Compress(string filePath);
}

public class ZipCompression : ICompressionStrategy
{
    public void Compress(string filePath)
    {
        Console.WriteLine($"Compressing {filePath} using ZIP compression.");
    }
}

public class RarCompression : ICompressionStrategy
{
    public void Compress(string filePath)
    {
        Console.WriteLine($"Compressing {filePath} using RAR compression.");
    }
}

public class Compressor
{
    private ICompressionStrategy _compressionStrategy;

    public Compressor(ICompressionStrategy compressionStrategy)
    {
        _compressionStrategy = compressionStrategy;
    }

    public void CompressFile(string filePath)
    {
        _compressionStrategy.Compress(filePath);
    }
}
```

**Real-World Applicability**:
- **When to use it**:  
  “Use the Strategy pattern when you want to switch between different algorithms dynamically, like sorting methods, file compression techniques, or payment gateways in an e-commerce platform.”

**Scenario – What can go wrong**:  
“Without the Strategy pattern, you’d likely use a lot of `if-else` statements to choose the right algorithm, leading to bloated and hard-to-maintain code. By using the Strategy pattern, you separate the logic for each algorithm and keep your main class clean.”

---

### **Conclusion **

- Recap the **Top 5 Design Patterns** covered:
  - **Singleton Pattern**: Global, single-instance objects.
  - **Factory Method Pattern**: Flexible object creation.
  - **Observer Pattern**: Dynamic event handling.
  - **Decorator Pattern**: Dynamic behavior modification.
  - **Strategy Pattern**: Interchangeable algorithms.
