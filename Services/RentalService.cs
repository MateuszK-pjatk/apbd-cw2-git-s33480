using Proj1.Models.Users;
using Proj1.Models.Equipments;
using Proj1.Models.Rentals;

namespace Proj1.Services;

public class RentalService
{
    private List<Rental> _rentals = new List<Rental>();
    
    private const decimal DailyPenaltyRate = 5.0m;

    public void RentEquipment(User user, Equipment equipment, int days)
    {
        if (equipment.IsAvailable == false)
        {
            throw new Exception($"Sprzet {equipment.Name} jest obecnie niedostepny");
        }

        int activeRentalsCount = 0;
        foreach (var rental in _rentals)
        {
            if (rental.RentedBy.Id == user.Id && rental.ReturnDate == null)
            {
                activeRentalsCount++;
            }
        }

        if (user is Student && activeRentalsCount >= 2)
        {
            throw new Exception("Student moze miec maksyamlnie 2 aktywne wypozyczenia");
        }
        
        if (user is Employee && activeRentalsCount >= 5)
        {
            throw new Exception("Pracownik moze miec maksyamlnie 5 aktywnych wypozyczen");
        }

        Rental newRental = new Rental(user, equipment, days);
        equipment.IsAvailable = false;
        _rentals.Add(newRental);
    }

    public void ReturnEquipment(Equipment equipment)
    {
        Rental activeRental = null;

        foreach (var rental in _rentals)
        {
            if (rental.RentedItem.Id == equipment.Id && rental.ReturnDate == null)
            {
                activeRental = rental;
                break;
            }
        }

        if (activeRental == null)
        {
            throw new Exception($"Sprzet {equipment.Name} nie jest obecnie wypozyczony");
        }
        
        activeRental.ReturnDate = DateTime.Now;

        if (activeRental.ReturnDate > activeRental.DueDate)
        {
            TimeSpan delay = activeRental.ReturnDate.Value - activeRental.DueDate;
            
            int daysLate = (int)Math.Ceiling(delay.TotalDays);

            if (daysLate > 0)
            {
                activeRental.PenaltyFee = daysLate * DailyPenaltyRate;
            }
        }
        
        equipment.IsAvailable = true;
    }

    public List<Rental> GetAllRentals()
    {
        return _rentals;
    }

    public List<Rental> GetActiveRentalsForUser(User user)
    {
        List<Rental> userRentals = new List<Rental>();
        foreach (var rental in _rentals)
        {
            if (rental.RentedBy.Id == user.Id && rental.ReturnDate == null)
            {
                userRentals.Add(rental);
            }
        }
        return userRentals;
    }
    
    public List<Rental> GetOverdueRentals()
    {
        List<Rental> overdueRentals = new List<Rental>();
        foreach (var rental in _rentals)
        {
            if (rental.ReturnDate == null && DateTime.Now > rental.DueDate)
            {
                overdueRentals.Add(rental);
            }
        }
        return overdueRentals;
    }
}