using Labb3_SchoolDb_Alex.Methods;

namespace Labb3_SchoolDb_Alex.Menu;

public class Interface
{
    // här vill jag att min meny ska ligga för mitt program
    public static void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Välkommen till skolapplikationen!\n Var god välj ett alternativ:");
            Console.WriteLine("1. Hämta alla elever");
            Console.WriteLine("2. Hämta alla elever i en viss klass");
            Console.WriteLine("3. Lägg till ny personal");
            Console.WriteLine("4. Avsluta programmet");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    StudentMethods.GetAllStudents();         
                    break;
                case "2":
                    StudentMethods.GetStudentsByClass();
                    break;
                case "3":
                    EmployeeMethods.AddNewEmployee();
                    break;
                case "4":
                    Console.WriteLine("Avslutar programmet.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Felaktigt val. Försök igen.");
                    break;
            }
        }
    }
}
