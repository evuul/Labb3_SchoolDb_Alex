using Labb3_SchoolDb_Alex.Context;

namespace Labb3_SchoolDb_Alex.Methods;

public class StudentMethods
{
public static void GetAllStudents()
{
    try
    {
        using (var context = new MyDbContext())
        {
            string sortChoice;
            string orderChoice;
            
            // Säkerställa att användaren bara kan mata in 1 eller 2 för sorteringsval
            while (true)
            {
                Console.WriteLine("Vill du sortera på:");
                Console.WriteLine("1. Förnamn");
                Console.WriteLine("2. Efternamn");
                sortChoice = Console.ReadLine();

                if (sortChoice == "1" || sortChoice == "2")
                    break;

                Console.WriteLine("Felaktig inmatning. Vänligen ange 1 eller 2.");
            }

            // Säkerställa att användaren bara kan mata in 1 eller 2 för sorteringsordning
            while (true)
            {
                Console.WriteLine("Sorteringsordning:");
                Console.WriteLine("1. Stigande");
                Console.WriteLine("2. Fallande");
                orderChoice = Console.ReadLine();

                if (orderChoice == "1" || orderChoice == "2")
                    break;

                Console.WriteLine("Felaktig inmatning. Vänligen ange 1 eller 2.");
            }

            var students = context.Students.AsQueryable();

            if (sortChoice == "1")
            {
                students = orderChoice == "1"
                    ? students.OrderBy(s => s.FirstName)
                    : students.OrderByDescending(s => s.FirstName);
            }
            else
            {
                students = orderChoice == "1"
                    ? students.OrderBy(s => s.LastName)
                    : students.OrderByDescending(s => s.LastName);
            }

            Console.WriteLine("\nLista över elever:");
            foreach (var student in students.ToList())
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}");
            }

            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn.");
            Console.ReadKey();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ett fel inträffade när eleverna hämtades. Försök igen senare.");
        Console.WriteLine($"Felsökningsinformation: {ex.Message}");
    }
}
    
    public static void GetStudentsByClass()
    {
        try
        {
            using (var context = new MyDbContext())
            {
                var classes = context.Classes.ToList();
                if (!classes.Any())
                {
                    Console.WriteLine("Det finns inga klasser att välja på.");
                    return;
                }

                int choice = 0;
                bool validChoice = false;

                while (!validChoice)
                {
                    Console.WriteLine("Lista över alla klasser på skolan:");
                    foreach (var c in classes)
                    {
                        Console.WriteLine($"{c.ClassId}. {c.ClassName}");
                    }

                    Console.WriteLine("Ange numret på den klass som du vill se elever för:");
                    if (int.TryParse(Console.ReadLine(), out choice) &&
                        classes.Any(c => c.ClassId == choice))
                    {
                        validChoice = true; // Användaren har gjort ett korrekt val.
                    }
                    else
                    {
                        Console.WriteLine("Felaktig inmatning. Ange ett giltigt klassnummer från listan.");
                    }
                }

                // Hämtar elever i den valda klassen
                var students = context.Students.Where(s => s.ClassId == choice).ToList();
                if (!students.Any())
                {
                    Console.WriteLine("Det finns inga elever i den valda klassen.");
                    return;
                }

                foreach (var student in students)
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName}");
                }
                Console.WriteLine("Tryck på valfri tangent för att återgå till menyn.");
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ett fel inträffade när eleverna hämtades. Försök igen senare.");
            Console.WriteLine($"Felsökningsinformation: {ex.Message}");
        }
    }
    
    
}
