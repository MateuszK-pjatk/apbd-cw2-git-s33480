using Proj1.Models.Users;
using Proj1.Models.Equipments;
using Proj1.Services;

namespace Proj1;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- SYSTEM WYPOŻYCZALNI SPRZĘTU ---");
        
        RentalService rentalService = new RentalService();
        
        Laptop laptop = new Laptop("Dell XPS 15", "Intel i7", 16);
        Projector projector = new Projector("Epson 4K", "3840x2160", true);
        Camera camera = new Camera("Sony A7 III", 24, true);
        
        Student student = new Student("Jan", "Kowalski");
        Employee employee = new Employee("Anna", "Nowak");

        Console.WriteLine("\n=== 1. POPRAWNE WYPOŻYCZENIA ===");
        rentalService.RentEquipment(student, laptop, 3);
        Console.WriteLine($"[SUKCES] Student {student.FirstName} wypożyczył: {laptop.Name}");

        rentalService.RentEquipment(employee, projector, 5);
        Console.WriteLine($"[SUKCES] Pracownik {employee.FirstName} wypożyczył: {projector.Name}");

        Console.WriteLine("\n=== 2. PRÓBA WYKONANIA NIEPOPRAWNEJ OPERACJI ===");
        try
        {
            Console.WriteLine($"Próbujemy wypożyczyć {laptop.Name}, który jest już u studenta...");
            rentalService.RentEquipment(employee, laptop, 2);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BŁĄD SYSTEMU]: {ex.Message}");
        }

        Console.WriteLine("\n=== 3. ZWROT W TERMINIE ===");
        rentalService.ReturnEquipment(laptop);
        Console.WriteLine($"[SUKCES] {laptop.Name} został zwrócony na czas.");

        Console.WriteLine("\n=== 4. ZWROT OPÓŹNIONY (Z KARĄ) ===");
        DateTime futureDate = DateTime.Now.AddDays(10);
        rentalService.ReturnEquipment(projector, futureDate);
        
        foreach (var rental in rentalService.GetAllRentals())
        {
            if (rental.RentedItem.Id == projector.Id && rental.PenaltyFee > 0)
            {
                Console.WriteLine($"[KARA] Zwrócono po terminie: {projector.Name}. Naliczona kara: {rental.PenaltyFee} PLN");
            }
        }

        Console.WriteLine("\n=== 5. RAPORT KOŃCOWY ===");
        var allRentals = rentalService.GetAllRentals();
        Console.WriteLine($"Całkowita liczba zarejestrowanych wypożyczeń w historii: {allRentals.Count}");
        
        Console.WriteLine("Aktualnie dostępny sprzęt w magazynie:");
        if (laptop.IsAvailable) Console.WriteLine($"- {laptop.Name}");
        if (projector.IsAvailable) Console.WriteLine($"- {projector.Name}");
        if (camera.IsAvailable) Console.WriteLine($"- {camera.Name}");

        Console.WriteLine("\nKoniec prezentacji. Naciśnij dowolny klawisz...");
        Console.ReadKey();
    }
}