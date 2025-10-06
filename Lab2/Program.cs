using System;

public class Zwierze
{
    protected string nazwa;

    public Zwierze(string nazwa)
    {
        this.nazwa = nazwa;
    }

    public virtual void daj_glos()
    {
        Console.WriteLine("...");
    }
}

public class Pies : Zwierze
{
    public Pies(string nazwa) : base(nazwa) { }

    public override void daj_glos()
    {
        Console.WriteLine($"{nazwa} robi woof woof!");
    }
}

public class Kot : Zwierze
{
    public Kot(string nazwa) : base(nazwa) { }

    public override void daj_glos()
    {
        Console.WriteLine($"{nazwa} robi miau miau!");
    }
}

public class Waz : Zwierze
{
    public Waz(string nazwa) : base(nazwa) { }

    public override void daj_glos()
    {
        Console.WriteLine($"{nazwa} robi ssssssss!");
    }
}

public static class Helpers
{
    // "Global" helper to call daj_glos on any Zwierze
    public static void powiedz_cos(Zwierze z)
    {
        z.daj_glos();
    }
}

public abstract class Pracownik
{
    public abstract void Pracuj();
}

public class Piekarz : Pracownik
{
    public override void Pracuj()
    {
        Console.WriteLine("Trwa pieczenie...");
    }
}

public class A
{
    public A()
    {
        Console.WriteLine("To jest konstruktor A");
    }
}

public class B : A
{
    public B() : base()
    {
        Console.WriteLine("To jest konstruktor B");
    }
}

public class C : B
{
    public C() : base()
    {
        Console.WriteLine("To jest konstruktor C");
    }
}

public class Program
{
    public static void Main()
    {
        // 7. Create one object of each: Zwierze, Pies, Kot, Waz
        Zwierze zwyk = new Zwierze("Zwierzak");
        Pies pies = new Pies("Burek");
        Kot kot = new Kot("Mruczek");
        Waz waz = new Waz("Kaa");

        // Call powiedz_cos and print runtime type
        Helpers.powiedz_cos(zwyk);
        Console.WriteLine($"Typ: {zwyk.GetType()}");

        Helpers.powiedz_cos(pies);
        Console.WriteLine($"Typ: {pies.GetType()}");

        Helpers.powiedz_cos(kot);
        Console.WriteLine($"Typ: {kot.GetType()}");

        Helpers.powiedz_cos(waz);
        Console.WriteLine($"Typ: {waz.GetType()}");

        Console.WriteLine();
        // 10. Create Piekarz and call Pracuj
        Piekarz piekarz = new Piekarz();
        piekarz.Pracuj();

        // 11. Trying to create Pracownik would fail at compile time because it's abstract:
        // Pracownik p = new Pracownik(); // <- error: cannot create an instance of the abstract class 'Pracownik'

        Console.WriteLine();
        // 15. Create A, B, C and observe constructor order
        A a = new A();
        B b = new B();
        C c = new C();
    }
}
