using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;

namespace Cwiczenie5_Pliki
{
    // ZADANIE 5: Klasa Student
    [Serializable]
    public class Student
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public List<int> Oceny { get; set; }

        public Student() { } // Wymagany do XML

        public Student(string imie, string nazwisko, List<int> oceny)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Oceny = oceny;
        }

        public override string ToString()
        {
            return $"{Imie} {Nazwisko}, Oceny: [{string.Join(", ", Oceny)}]";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Sprawdzenie czy plik Iris.csv istnieje przed operacjami na nim
            bool irisExists = File.Exists("Iris.csv");

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- MENU ĆWICZENIE 5 ---");
                Console.WriteLine("2. Zapisz tekst do pliku (Zadanie 2)");
                Console.WriteLine("3. Odczytaj plik tekstowy (Zadanie 3)");
                Console.WriteLine("4. Dopisz do pliku (Zadanie 4)");
                Console.WriteLine("6. Serializacja JSON (Zadanie 6)");
                Console.WriteLine("7. Deserializacja JSON (Zadanie 7)");
                Console.WriteLine("8. Serializacja XML (Zadanie 8)");
                Console.WriteLine("9. Deserializacja XML (Zadanie 9)");
                Console.WriteLine("10. Odczyt CSV (Zadanie 10)");
                Console.WriteLine("11. Statystyki CSV (Zadanie 11)");
                Console.WriteLine("12. Filtrowanie CSV (Zadanie 12)");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz zadanie: ");

                string wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "2": Zadanie2(); break;
                    case "3": Zadanie3(); break;
                    case "4": Zadanie4(); break;
                    case "6": Zadanie6(); break;
                    case "7": Zadanie7(); break;
                    case "8": Zadanie8(); break;
                    case "9": Zadanie9(); break;
                    case "10":
                        if (irisExists) Zadanie10();
                        else Console.WriteLine("Błąd: Brak pliku Iris.csv w folderze programu.");
                        break;
                    case "11":
                        if (irisExists) Zadanie11();
                        else Console.WriteLine("Błąd: Brak pliku Iris.csv w folderze programu.");
                        break;
                    case "12":
                        if (irisExists) Zadanie12();
                        else Console.WriteLine("Błąd: Brak pliku Iris.csv w folderze programu.");
                        break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Nieznana opcja."); break;
                }
            }
        }

        // --- ZADANIA 2-4: PLIKI TEKSTOWE ---

        static void Zadanie2()
        {
            string sciezka = "tekst.txt";
            Console.WriteLine("Podaj 3 linie tekstu do zapisania:");
            using (StreamWriter sw = new StreamWriter(sciezka))
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.Write($"Linia {i + 1}: ");
                    sw.WriteLine(Console.ReadLine());
                }
            }
            Console.WriteLine("Zapisano.");
        }

        static void Zadanie3()
        {
            string sciezka = "tekst.txt";
            if (!File.Exists(sciezka)) { Console.WriteLine("Najpierw wykonaj zadanie 2."); return; }

            string[] linie = File.ReadAllLines(sciezka);
            foreach (var linia in linie) Console.WriteLine(linia);
        }

        static void Zadanie4()
        {
            string sciezka = "tekst.txt";
            Console.WriteLine("Podaj tekst do dopisania (wpisz 'koniec' aby przerwać):");
            using (StreamWriter sw = new StreamWriter(sciezka, append: true))
            {
                while (true)
                {
                    string tekst = Console.ReadLine();
                    if (tekst.ToLower() == "koniec") break;
                    sw.WriteLine(tekst);
                }
            }
        }

        // --- ZADANIA 6-9: JSON i XML ---

        static void Zadanie6()
        {
            var studenci = PrzygotujStudentow();
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText("studenci.json", JsonSerializer.Serialize(studenci, options));
            Console.WriteLine("Zapisano studenci.json");
        }

        static void Zadanie7()
        {
            if (!File.Exists("studenci.json")) { Console.WriteLine("Brak pliku."); return; }
            var studenci = JsonSerializer.Deserialize<List<Student>>(File.ReadAllText("studenci.json"));
            studenci.ForEach(s => Console.WriteLine(s));
        }

        static void Zadanie8()
        {
            var studenci = PrzygotujStudentow();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
            using (FileStream fs = new FileStream("studenci.xml", FileMode.Create))
            {
                serializer.Serialize(fs, studenci);
            }
            Console.WriteLine("Zapisano studenci.xml");
        }

        static void Zadanie9()
        {
            if (!File.Exists("studenci.xml")) { Console.WriteLine("Brak pliku."); return; }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
            using (FileStream fs = new FileStream("studenci.xml", FileMode.Open))
            {
                var studenci = (List<Student>)serializer.Deserialize(fs);
                studenci.ForEach(s => Console.WriteLine(s));
            }
        }

        static List<Student> PrzygotujStudentow()
        {
            return new List<Student>
            {
                new Student("Jan", "Kowalski", new List<int>{3, 4, 5}),
                new Student("Anna", "Nowak", new List<int>{5, 5, 4})
            };
        }

        // --- ZADANIA 10-12: CSV (Iris.csv) ---

        // ZADANIE 10: Odczyt i wypisanie
        static void Zadanie10()
        {
            string[] linie = File.ReadAllLines("Iris.csv");

            Console.WriteLine("--- Dane z Iris.csv ---");
            // i=0 to nagłówek, i=1 to dane
            for (int i = 0; i < linie.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(linie[i])) continue;

                // Formatujemy tabulatorem dla czytelności
                string[] kolumny = linie[i].Split(',');
                Console.WriteLine(string.Join("\t", kolumny));
            }
        }

        // ZADANIE 11: Obliczanie średniej dla kolumn numerycznych
        static void Zadanie11()
        {
            var linie = File.ReadAllLines("Iris.csv");

            double sumaSepalLen = 0, sumaSepalWid = 0, sumaPetalLen = 0, sumaPetalWid = 0;
            int licznik = 0;

            // Zaczynamy od i = 1, żeby pominąć nagłówek
            for (int i = 1; i < linie.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(linie[i])) continue;

                var kol = linie[i].Split(',');

                // Parsujemy z InvariantCulture (kropka jako separator dziesiętny)
                if (kol.Length >= 4)
                {
                    sumaSepalLen += double.Parse(kol[0], CultureInfo.InvariantCulture);
                    sumaSepalWid += double.Parse(kol[1], CultureInfo.InvariantCulture);
                    sumaPetalLen += double.Parse(kol[2], CultureInfo.InvariantCulture);
                    sumaPetalWid += double.Parse(kol[3], CultureInfo.InvariantCulture);
                    licznik++;
                }
            }

            if (licznik > 0)
            {
                Console.WriteLine("--- Średnie wartości ---");
                Console.WriteLine($"Sepal Length: {sumaSepalLen / licznik:F2}");
                Console.WriteLine($"Sepal Width:  {sumaSepalWid / licznik:F2}");
                Console.WriteLine($"Petal Length: {sumaPetalLen / licznik:F2}");
                Console.WriteLine($"Petal Width:  {sumaPetalWid / licznik:F2}");
            }
            else
            {
                Console.WriteLine("Nie znaleziono danych do obliczeń.");
            }
        }

        // ZADANIE 12: Filtrowanie (sepal length < 5) i zapis wybranych kolumn
        static void Zadanie12()
        {
            var linie = File.ReadAllLines("Iris.csv");
            List<string> wynik = new List<string>();

            // Dodajemy nagłówek w nowym formacie (zgodnie z poleceniem: sepal len, sepal wid, class)
            wynik.Add("sepal length,sepal width,class");

            for (int i = 1; i < linie.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(linie[i])) continue;

                var kol = linie[i].Split(',');

                // Sprawdzamy czy mamy wystarczająco kolumn
                if (kol.Length >= 5)
                {
                    double sepalLen = double.Parse(kol[0], CultureInfo.InvariantCulture);

                    // Warunek filtracji: sepal length < 5
                    if (sepalLen < 5.0)
                    {
                        // Wybieramy kolumny: [0], [1] oraz [4] (class)
                        string nowaLinia = $"{kol[0]},{kol[1]},{kol[4]}";
                        wynik.Add(nowaLinia);
                    }
                }
            }

            File.WriteAllLines("iris_filtered.csv", wynik);
            Console.WriteLine($"Utworzono plik 'iris_filtered.csv'. Zapisano {wynik.Count - 1} rekordów.");
        }
    }
}