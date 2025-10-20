using System;

public interface IModular
{
    public float Module();
}

public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular
{
    private float re;
    private float im;

    public ComplexNumber(float real)
    {
        re = real;
        im = 0;
    }

    public ComplexNumber(float real, float imag)
    {
        re = real;
        im = imag;
    }

    public float Re
    {
        get => re;
        set => re = value;
    }

    public float Im
    {
        get => im;
        set => im = value;
    }

    public override string ToString()
    {
        string sign = im >= 0 ? "+" : "";
        return $"{re} {sign}{im}i";
    }

    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.re + b.re, a.im + b.im);
    }

    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.re - b.re, a.im - b.im);
    }

   
    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
    {
        float real = a.re * b.re - a.im * b.im;
        float imag = a.re * b.im + a.im * b.re;
        return new ComplexNumber(real, imag);
    }

 
    public static ComplexNumber operator -(ComplexNumber a)
    {
        return new ComplexNumber(a.re, -a.im);
    }

 
    public object Clone()
    {
        return new ComplexNumber(re, im);
    }

   
    public bool Equals(ComplexNumber other)
    {
        if (other is null) return false;
        return re == other.re && im == other.im;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as ComplexNumber);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(re, im);
    }

   
    public static bool operator ==(ComplexNumber a, ComplexNumber b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

 
    public static bool operator !=(ComplexNumber a, ComplexNumber b)
    {
        return !(a == b);
    }

 
    public float Module()
    {
        return (float)Math.Sqrt(re * re + im * im);
    }
}

public class Program
{
    public static void Main()
    {
    
        ComplexNumber c1 = new ComplexNumber(3, 4);
        ComplexNumber c2 = new ComplexNumber(1, -2);
        ComplexNumber c3 = new ComplexNumber(0, 1);

        Console.WriteLine("Liczby zespolone:");
        Console.WriteLine($"c1: {c1}");
        Console.WriteLine($"c2: {c2}");
        Console.WriteLine($"c3: {c3}");

        Console.WriteLine("\nOperatory:");
        Console.WriteLine($"c1 + c2 = {c1 + c2}");
        Console.WriteLine($"c1 - c2 = {c1 - c2}");
        Console.WriteLine($"c1 * c2 = {c1 * c2}");
        Console.WriteLine($"-c1 = {-c1}");

       
        ComplexNumber c1Clone = (ComplexNumber)c1.Clone();
        Console.WriteLine($"\nClone c1: {c1Clone}");

      
        Console.WriteLine($"\nc1.Equals(c2): {c1.Equals(c2)}");
        Console.WriteLine($"c1 == c2: {c1 == c2}");
        Console.WriteLine($"c1 == c1Clone: {c1 == c1Clone}");
       
        Console.WriteLine($"\nModuł c1: {c1.Module()}");
        Console.WriteLine($"Moduł c2: {c2.Module()}");
        Console.WriteLine($"Moduł c3: {c3.Module()}");
    }
}