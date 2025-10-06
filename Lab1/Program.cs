using System;

public class Program
{
    public static void Main()
    {
        Zwierze[] zwierzeta = new Zwierze[4];
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Podaj nazwę zwierzęcia #{i + 1}:");
            string nazwa = Console.ReadLine();
            Console.WriteLine($"Podaj gatunek zwierzęcia #{i + 1}:");
            string gatunek = Console.ReadLine();
            Console.WriteLine($"Podaj liczbę nóg zwierzęcia #{i + 1}:");
            int liczbaNog = int.Parse(Console.ReadLine());
            zwierzeta[i] = new Zwierze(nazwa, gatunek, liczbaNog);
        }
        zwierzeta[3] = new Zwierze(zwierzeta[2]);
        Console.WriteLine("Podaj nową nazwę dla sklonowanego zwierzęcia:");
        zwierzeta[3].Nazwa = Console.ReadLine();

        Console.WriteLine("\nInformacje o wszystkich zwierzętach:");
        for (int i = 0; i < zwierzeta.Length; i++)
        {
            Console.WriteLine($"Zwierzę #{i + 1}: Nazwa: {zwierzeta[i].Nazwa}, Gatunek: {zwierzeta[i].GetGatunek()}, Liczba nóg: {zwierzeta[i].GetLiczbaNog()}");
            zwierzeta[i].daj_glos();
        }
        Console.WriteLine($"\nLiczba utworzonych zwierząt: {Zwierze.GetLiczbaZwierzat()}");
    }
}

public class Zwierze
{
    public string Nazwa { get; set; }
    private string gatunek;
    private int liczbaNog;
    private static int liczbaZwierzat = 0;

    public string GetGatunek() => gatunek;
    public int GetLiczbaNog() => liczbaNog;

    public Zwierze()
    {
        Nazwa = "Rex";
        gatunek = "Pies";
        liczbaNog = 4;
        liczbaZwierzat++;
    }

    public Zwierze(string nazwa, string gatunek, int liczbaNog)
    {
        Nazwa = nazwa;
        this.gatunek = gatunek;
        this.liczbaNog = liczbaNog;
        liczbaZwierzat++;
    }

    public Zwierze(Zwierze inne)
    {
        Nazwa = inne.Nazwa;
        gatunek = inne.gatunek;
        liczbaNog = inne.liczbaNog;
        liczbaZwierzat++;
    }

    public void daj_glos()
    {
        switch (gatunek.ToLower())
        {
            case "kot":
                Console.WriteLine("Miau");
                break;
            case "krowa":
                Console.WriteLine("Muuu!");
                break;
            case "pies":
                Console.WriteLine("Hau hau!");
                break;
            default:
                Console.WriteLine("Brak charakterystycznego głosu");
                break;
        }
    }

    public static int GetLiczbaZwierzat() => liczbaZwierzat;

    ~Zwierze()
    {
    }
}



