using System.Collections.Generic;
using Proj1.Models.Users;
using Proj1.Models.Equipments;
using Proj1.Models.Rentals;

namespace Proj1.Services;

public class RentalService
{
    private List<Rental> _rentals = new List<Rental>();

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
}