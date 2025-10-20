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
       
        Zwierze zwyk = new Zwierze("Zwierzak");
        Pies pies = new Pies("Burek");
        Kot kot = new Kot("Mruczek");
        Waz waz = new Waz("Kaa");

        
        Helpers.powiedz_cos(zwyk);
        Console.WriteLine($"Typ: {zwyk.GetType()}");

        Helpers.powiedz_cos(pies);
        Console.WriteLine($"Typ: {pies.GetType()}");

        Helpers.powiedz_cos(kot);
        Console.WriteLine($"Typ: {kot.GetType()}");

        Helpers.powiedz_cos(waz);
        Console.WriteLine($"Typ: {waz.GetType()}");

        Console.WriteLine();
       
        Piekarz piekarz = new Piekarz();
        piekarz.Pracuj();

      

        Console.WriteLine();
        
        A a = new A();
        B b = new B();
        C c = new C();
    }
}
