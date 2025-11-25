using System.Linq;

public interface IModular
{
    public double Module();
}

public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular, IComparable<ComplexNumber>
{
    private double re;
    private double im;
    public double Re { get => re; set => re = value; }
    public double Im { get => im; set => im = value; }
    public ComplexNumber(double re, double im)
    {
        this.re = re; this.im = im;
    }
    public override string ToString()
    {
        string sign = im >= 0 ? "+" : "-";
        return $"{re} {sign} {Math.Abs(im)}i";
    }
    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re + b.re, a.im + b.im);
    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re - b.re, a.im - b.im);
    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re * b.re - a.im * b.im, a.re * b.im + a.im * b.re);
    public static ComplexNumber operator -(ComplexNumber a)
        => new ComplexNumber(a.re, -a.im);
    public object Clone() => new ComplexNumber(re, im);
    public bool Equals(ComplexNumber other)
    {
        if (other == null) return false;
        return re == other.re && im == other.im;
    }
    public override bool Equals(object obj)
        => obj is ComplexNumber other && Equals(other);
    public override int GetHashCode()
        => HashCode.Combine(re, im);
    public static bool operator ==(ComplexNumber a, ComplexNumber b)
        => a?.Equals(b) ?? b is null;
    public static bool operator !=(ComplexNumber a, ComplexNumber b)
        => !(a == b);
    public double Module()
        => Math.Sqrt(re * re + im * im);
    public int CompareTo(ComplexNumber other)
    {
        if (other == null) return 1;
        return Module().CompareTo(other.Module());
    }
}

public class Program
{
    public static void Main()
    {
        // Zadanie 2
        ComplexNumber[] numbers = new ComplexNumber[]
        {
            new ComplexNumber(1, 2),
            new ComplexNumber(3, -4),
            new ComplexNumber(0, 5),
            new ComplexNumber(-2, 3),
            new ComplexNumber(4, 0)
        };

        // a. Wypisz je wykorzystując pętlę foreach
        Console.WriteLine("Task 2a:");
        foreach (var num in numbers)
        {
            Console.WriteLine(num);
        }

        // b. Posortuj w oparciu o moduł liczby zespolonej i wypisz jeszcze raz
        Console.WriteLine("Task 2b:");
        Array.Sort(numbers);
        foreach (var num in numbers)
        {
            Console.WriteLine(num);
        }

        // c. Wypisz minimum i maksimum tablicy
        Console.WriteLine("Task 2c:");
        Console.WriteLine("Min: " + numbers.Min());
        Console.WriteLine("Max: " + numbers.Max());

        // d. Odfiltruj z tablicy wszystkie liczby zespolone o ujemnej części urojonej i wypisz jeszcze raz
        Console.WriteLine("Task 2d:");
        var filtered = numbers.Where(n => n.Im >= 0).ToArray();
        foreach (var num in filtered)
        {
            Console.WriteLine(num);
        }

        // Zadanie 3
        List<ComplexNumber> list = new List<ComplexNumber>
        {
            new ComplexNumber(1, 2),
            new ComplexNumber(3, -4),
            new ComplexNumber(0, 5),
            new ComplexNumber(-2, 3),
            new ComplexNumber(4, 0),
            new ComplexNumber(5, 6)
        };

        // a. Usuń drugi element z listy i wypisz całą listę
        Console.WriteLine("Task 3a:");
        list.RemoveAt(1);
        foreach (var num in list) Console.WriteLine(num);

        // b. Usuń najmniejszy element z listy i wypisz całą listę
        Console.WriteLine("Task 3b:");
        var min = list.Min();
        list.Remove(min);
        foreach (var num in list) Console.WriteLine(num);

        // c. Usuń wszystkie elementy z listy i wypisz całą listę
        Console.WriteLine("Task 3c:");
        list.Clear();
        foreach (var num in list) Console.WriteLine(num); // empty

        // Zadanie 4
        HashSet<ComplexNumber> set = new HashSet<ComplexNumber>
        {
            new ComplexNumber(6, 7),
            new ComplexNumber(1, 2),
            new ComplexNumber(6, 7), // duplicate
            new ComplexNumber(1, -2),
            new ComplexNumber(-5, 9)
        };

        // a. Sprawdź zawartość zbioru wypisując wszystkie wartości
        Console.WriteLine("Task 4a:");
        foreach (var num in set) Console.WriteLine(num);

        // b. Sprawdź możliwość wykonania operacji z poprzednich zadań na zbiorze (minimum, maksimum, sortowanie, filtrowanie)
        Console.WriteLine("Task 4b:");
        Console.WriteLine("Min: " + set.Min());
        Console.WriteLine("Max: " + set.Max());
        var sorted = set.OrderBy(n => n.Module()).ToList();
        Console.WriteLine("Sorted:");
        foreach (var num in sorted) Console.WriteLine(num);
        var filteredSet = set.Where(n => n.Im >= 0).ToList();
        Console.WriteLine("Filtered (Im >= 0):");
        foreach (var num in filteredSet) Console.WriteLine(num);

        // Zadanie 5
        Dictionary<string, ComplexNumber> dict = new Dictionary<string, ComplexNumber>
        {
            {"z1", new ComplexNumber(6, 7)},
            {"z2", new ComplexNumber(1, 2)},
            {"z3", new ComplexNumber(6, 7)},
            {"z4", new ComplexNumber(1, -2)},
            {"z5", new ComplexNumber(-5, 9)}
        };

        // a. Wypisz wszystkie elementy słownika w postaci (klucz, wartość)
        Console.WriteLine("Task 5a:");
        foreach (var kvp in dict)
        {
            Console.WriteLine($"({kvp.Key}, {kvp.Value})");
        }

        // b. Wypisz osobno wszystkie klucze i wszystkie wartości ze słownika
        Console.WriteLine("Task 5b:");
        Console.WriteLine("Keys:");
        foreach (var key in dict.Keys) Console.WriteLine(key);
        Console.WriteLine("Values:");
        foreach (var val in dict.Values) Console.WriteLine(val);

        // c. Sprawdź, czy w słowniku istnieje element o kluczu z6
        Console.WriteLine("Task 5c:");
        Console.WriteLine(dict.ContainsKey("z6"));

        // d. Wykonaj na słowniku zadania 2c i 2d
        Console.WriteLine("Task 5d:");
        Console.WriteLine("Min: " + dict.Values.Min());
        Console.WriteLine("Max: " + dict.Values.Max());
        var filteredDict = dict.Where(kvp => kvp.Value.Im >= 0).Select(kvp => kvp.Value).ToList();
        foreach (var num in filteredDict) Console.WriteLine(num);

        // e. Usuń ze słownika element o kluczu „z3”
        Console.WriteLine("Task 5e:");
        dict.Remove("z3");

        // f. Usuń drugi element ze słownika
        var secondKey = dict.Keys.ElementAt(1);
        dict.Remove(secondKey);

        // g. Wyczyść słownik
        dict.Clear();
    }
}
