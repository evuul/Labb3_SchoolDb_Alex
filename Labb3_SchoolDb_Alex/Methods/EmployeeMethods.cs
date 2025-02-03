using Labb3_SchoolDb_Alex.Context;
using Labb3_SchoolDb_Alex.Migrations;

namespace Labb3_SchoolDb_Alex.Methods;

public class EmployeeMethods
{
    public static void AddNewEmployee()
    {
        try
        {
            // create a new instance of the DbContext
            using (var context = new MyDbContext())
            {
                string firstName, lastName;

                // ask user for first and last name with validation
                do
                {
                    Console.WriteLine("Ange Förnamn:");
                    firstName = Console.ReadLine()?.Trim();
                } while (string.IsNullOrEmpty(firstName));

                do
                {
                    Console.WriteLine("Ange Efternamn:");
                    lastName = Console.ReadLine()?.Trim();
                } while (string.IsNullOrEmpty(lastName));

                // get all professions from the database
                var professions = context.Professions.ToList();
                
                if (professions.Count == 0)
                {
                    Console.WriteLine("Inga roller finns i systemet. Lägg till roller först.");
                    return;
                }

                Console.WriteLine("Välj en roll från listan nedan:");
                foreach (var profession in professions)
                {
                    Console.WriteLine($"{profession.ProfessionId}. {profession.ProfessionName}");
                }

                // makes sure the user enters a valid profession ID
                int professionId;
                while (true)
                {
                    Console.WriteLine("Ange rollens ID:");
                    var input = Console.ReadLine()?.Trim();

                    if (int.TryParse(input, out professionId) &&
                        professions.Any(p => p.ProfessionId == professionId))
                    {
                        break; // exit the loop if the input is valid
                    }
                    Console.WriteLine("Ogiltigt val. Vänligen försök igen.");
                }

                // create a new Employee object and add it to the database
                var newEmployee = new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    ProfessionId = professionId
                };

                context.Employees.Add(newEmployee);
                context.SaveChanges();

                Console.WriteLine($"Den nya anställda {firstName} {lastName} har lagts till i systemet.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel inträffade vid sparandet av den nya anställda: {ex.Message}");
        }
    }
}