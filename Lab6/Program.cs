using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient; // Zadanie e

namespace Laboratorium6
{
    // Zadanie f
    public class Student
    {
        public int StudentId { get; set; }
        public string Imie { get; set; } = "";
        public string Nazwisko { get; set; } = "";
        public List<Ocena> Oceny { get; set; } = new();
    }

    public class Ocena
    {
        public int OcenaId { get; set; }
        public double Wartosc { get; set; }
        public string Przedmiot { get; set; } = "";
        public int StudentId { get; set; }
    }

    class Program
    {
        // Zadanie g
        static void Main(string[] args)
        {
           
            string connectionString =
                "Data Source=10.200.2.28;" +
                "Initial Catalog=studenci_72420;" +
                "Integrated Security=True;" +
                "Encrypt=True;" +
                "TrustServerCertificate=True";

            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Połączono z bazą.");

                // Testowanie zadań:
                Console.WriteLine("\n--- Zadanie 4: Lista studentów ---");
                WyswietlWszystkichStudentow(connection);

                Console.WriteLine("\n--- Zadanie 5: Student o ID 1 ---");
                WyswietlStudentaPoId(connection, 1);

                Console.WriteLine("\n--- Zadanie 7: Dodawanie studenta ---");
                AddStudent(connection, new Student { Imie = "Adam", Nazwisko = "Nowak" });

                Console.WriteLine("\n--- Zadanie 8: Dodawanie oceny ---");
                AddOcena(connection, new Ocena { Wartosc = 4.5, Przedmiot = "Programowanie", StudentId = 1 });

                Console.WriteLine("\n--- Zadanie 9: Usuwanie Geografii ---");
                UsunOcenyZGeografii(connection);

                Console.WriteLine("\n--- Zadanie 10: Aktualizacja oceny ---");
                AktualizujOcene(connection, 1, 5.0);

                Console.WriteLine("\n--- Zadanie 6: Studenci z ocenami ---");
                var lista = PobierzStudentowZOcenami(connection);
                foreach (var s in lista)
                {
                    Console.WriteLine($"{s.Imie} {s.Nazwisko}:");
                    foreach (var o in s.Oceny) Console.WriteLine($"  - {o.Przedmiot}: {o.Wartosc}");
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Wystąpił błąd: " + exc.Message);
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey();
        }

        // Zadanie 4
        static void WyswietlWszystkichStudentow(SqlConnection connection)
        {
            string sql = "SELECT student_id, imie, nazwisko FROM student";
            using SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["student_id"]}: {reader["imie"]} {reader["nazwisko"]}");
            }
        }

        // Zadanie 5
        static void WyswietlStudentaPoId(SqlConnection connection, int id)
        {
            string sql = "SELECT imie, nazwisko FROM student WHERE student_id = @id";
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine($"Znaleziono: {reader["imie"]} {reader["nazwisko"]}");
            }
            else Console.WriteLine("Nie znaleziono studenta o podanym ID.");
        }

        // Zadanie 6
        static List<Student> PobierzStudentowZOcenami(SqlConnection connection)
        {
            var studenciMap = new Dictionary<int, Student>();
            string sql = @"SELECT s.student_id, s.imie, s.nazwisko, o.ocena_id, o.wartosc, o.przedmiot 
                           FROM student s 
                           LEFT JOIN ocena o ON s.student_id = o.student_id";

            using SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int sId = (int)reader["student_id"];
                if (!studenciMap.ContainsKey(sId))
                {
                    studenciMap[sId] = new Student
                    {
                        StudentId = sId,
                        Imie = reader["imie"].ToString(),
                        Nazwisko = reader["nazwisko"].ToString()
                    };
                }

                if (reader["ocena_id"] != DBNull.Value)
                {
                    studenciMap[sId].Oceny.Add(new Ocena
                    {
                        OcenaId = (int)reader["ocena_id"],
                        Wartosc = Convert.ToDouble(reader["wartosc"]),
                        Przedmiot = reader["przedmiot"].ToString(),
                        StudentId = sId
                    });
                }
            }
            return new List<Student>(studenciMap.Values);
        }

        // Zadanie 7
        static void AddStudent(SqlConnection connection, Student student)
        {
            string sql = "INSERT INTO student (imie, nazwisko) VALUES (@imie, @nazwisko)";
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@imie", student.Imie);
            command.Parameters.AddWithValue("@nazwisko", student.Nazwisko);
            command.ExecuteNonQuery();
            Console.WriteLine("Dodano studenta.");
        }

        // Zadanie 8
        static void AddOcena(SqlConnection connection, Ocena ocena)
        {
            if (!CzyOcenaPrawidlowa(ocena.Wartosc))
            {
                Console.WriteLine($"Błąd: Wartość {ocena.Wartosc} jest niedozwolona.");
                return;
            }

            string sql = "INSERT INTO ocena (wartosc, przedmiot, student_id) VALUES (@w, @p, @sid)";
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@w", ocena.Wartosc);
            command.Parameters.AddWithValue("@p", ocena.Przedmiot);
            command.Parameters.AddWithValue("@sid", ocena.StudentId);
            command.ExecuteNonQuery();
            Console.WriteLine("Dodano ocenę.");
        }

        // Zadanie 9
        static void UsunOcenyZGeografii(SqlConnection connection)
        {
            string sql = "DELETE FROM ocena WHERE przedmiot = 'Geografia'";
            using SqlCommand command = new SqlCommand(sql, connection);
            int rows = command.ExecuteNonQuery();
            Console.WriteLine($"Usunięto {rows} ocen z Geografii.");
        }

        // Zadanie 10
        static void AktualizujOcene(SqlConnection connection, int ocenaId, double nowaWartosc)
        {
            if (!CzyOcenaPrawidlowa(nowaWartosc))
            {
                Console.WriteLine("Błąd: Nieprawidłowa wartość oceny.");
                return;
            }

            string sql = "UPDATE ocena SET wartosc = @w WHERE ocena_id = @id";
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@w", nowaWartosc);
            command.Parameters.AddWithValue("@id", ocenaId);
            command.ExecuteNonQuery();
            Console.WriteLine("Zaktualizowano ocenę.");
        }

        // Pomocnicza funkcja do walidacji (Zadanie 8 i 10)
        private static bool CzyOcenaPrawidlowa(double v)
        {
            
            if (v < 2.0 || v > 5.0 || v == 2.5) return false;
            if ((v * 10) % 5 != 0) return false;
            return true;
        }
    }
}