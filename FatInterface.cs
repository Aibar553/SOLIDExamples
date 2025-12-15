/*public interface IDevice
{
    void TurnOn();
    void TurnOff();
    void Print(string text);
    void ChangeInkCartridge();
}

public class Printer : IDevice
{
    public void TurnOn() => Console.WriteLine("Printer on");
    public void TurnOff() => Console.WriteLine("Printer off");
    public void Print(string text) => Console.WriteLine($"Printing: {text}");
    public void ChangeInkCartridge() => Console.WriteLine("Ink changed");
}

public class Projector : IDevice
{
    public void TurnOn() => Console.WriteLine("Projector on");
    public void TurnOff() => Console.WriteLine("Projector off");

    public void Print(string text)
    {
        // ❌ Проектор не умеет печатать
        throw new NotImplementedException();
    }

    public void ChangeInkCartridge()
    {
        // ❌ У проектора нет картриджа
        throw new NotImplementedException();
    }
}*/

public interface ITurnOn
{
   void TurnOn();
}
public interface ITurnOff
{
    void TurnOff();
}
public interface IPrint
{
    void Print(string text);
}
public interface IChangeInkCartridge
{
    void ChangeInkCartridge();
}
public class Printer : ITurnOn, ITurnOff, IPrint, IChangeInkCartridge
{
    public void TurnOn() => Console.WriteLine("Printer on");
    public void TurnOff() => Console.WriteLine("Printer off");
    public void Print(string text) => Console.WriteLine($"Printing: {text}");
    public void ChangeInkCartridge() => Console.WriteLine("Ink changed");
}
public class Projector : ITurnOn, ITurnOff
{
    public void TurnOn() => Console.WriteLine("Projector on");
    public void TurnOff() => Console.WriteLine("Projector off");
}

/*public interface IAnimal
{
    void Eat();
    void Walk();
    void Fly();
    void Swim();
}

public class Dog : IAnimal
{
    public void Eat() => Console.WriteLine("Dog eats");
    public void Walk() => Console.WriteLine("Dog walks");
    public void Fly() => throw new NotImplementedException();    // ❌
    public void Swim() => Console.WriteLine("Dog swims");
}

public class Fish : IAnimal
{
    public void Eat() => Console.WriteLine("Fish eats");
    public void Walk() => throw new NotImplementedException();   // ❌
    public void Fly() => throw new NotImplementedException();    // ❌
    public void Swim() => Console.WriteLine("Fish swims");
}

public class Eagle : IAnimal
{
    public void Eat() => Console.WriteLine("Eagle eats");
    public void Walk() => Console.WriteLine("Eagle walks");
    public void Fly() => Console.WriteLine("Eagle flies");
    public void Swim() => throw new NotImplementedException();   // ❌
}
*/
public interface IEatable
{
    void Eat();
}
public interface IWalkable
{
    void Walk();
}
public interface IFlyable
{
    void Fly();
}
public interface ISwimable
{
    void Swim();
}
public class Dog : IEatable, IWalkable, ISwimable
{
    public void Eat() => Console.WriteLine("Dog eats");
    public void Walk() => Console.WriteLine("Dog walks");
    public void Swim() => Console.WriteLine("Dog swims");
}
public class Fish : IEatable, ISwimable
{
    public void Eat() => Console.WriteLine("Fish eats");
    public void Swim() => Console.WriteLine("Fish swims");
}
public class Eagle : IEatable, IWalkable, IFlyable
{
    public void Eat() => Console.WriteLine("Eagle eats");
    public void Walk() => Console.WriteLine("Eagle walks");
    public void Fly() => Console.WriteLine("Eagle flies");
}

/*public interface IMultiFunctionDevice
{
    void Print(string content);
    void Scan(string destination);
    void Fax(string number);
    void Copy(string sourceTray, string destinationTray);
}*/

public interface IPrintable
{
    void Print(string content);
}
public interface IScanable
{
    void Scan(string destination);
}

public interface IFaxable
{
    void Fax(string number);
}

public interface ICopyable
{
    void Copy(string sourceTray, string destinationTray);
}
