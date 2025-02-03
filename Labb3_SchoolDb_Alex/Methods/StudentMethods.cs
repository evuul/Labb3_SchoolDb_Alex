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
                string sortChoice, orderChoice;
                
                do
                {
                    Console.WriteLine("Vill du sortera på:\n1. Förnamn\n2. Efternamn");
                    sortChoice = Console.ReadLine();

                    if (sortChoice != "1" && sortChoice != "2")
                    {
                        Console.WriteLine("Felaktig inmatning. Vänligen ange ett giltligt val.");
                    }
                } while (sortChoice != "1" && sortChoice != "2");

                do
                {
                    Console.WriteLine("Sorteringsordning:\n1. Stigande\n2. Fallande");
                    orderChoice = Console.ReadLine();

                    if (orderChoice != "1" && orderChoice != "2")
                    {
                        Console.WriteLine("Felaktig inmatning. Vänligen ange ett giltligt val.");
                    }
                } while (orderChoice != "1" && orderChoice != "2");

                // Get all students as a IQueryable to be able to sort them on a database level
                var students = context.Students.AsQueryable();

                // Sort the students based on the user's choice with ternary operators
                students = (sortChoice == "1" ? 
                    (orderChoice == "1" ? students.OrderBy(s => s.FirstName) 
                        : students.OrderByDescending(s => s.FirstName)) :
                    (orderChoice == "1" ? students.OrderBy(s => s.LastName) 
                        : students.OrderByDescending(s => s.LastName)));
                
                string sortFieldText = sortChoice == "1" ? "förnamn" : "efternamn";
                string sortOrderText = orderChoice == "1" ? "stigande" : "fallande";
                Console.WriteLine($"\nLista över elever sorterad efter {sortFieldText} ({sortOrderText}):");
                
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
