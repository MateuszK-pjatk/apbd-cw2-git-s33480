using Proj1.Models.Users;
using Proj1.Models.Equipments;

namespace Proj1.Models.Rentals;

public class Rental
{
    public Guid Id { get; set; }
    public User RentedBy { get; set; }
    public Equipment RentedItem { get; private set; }
    public DateTime RentalDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal PenaltyFee { get; set; }

    public Rental(User user, Equipment equipment, int daysToRent)
    {
        Id = Guid.NewGuid();
        RentedBy = user;
        RentedItem = equipment;
        RentalDate = DateTime.Now;
        DueDate = RentalDate.AddDays(daysToRent);
        PenaltyFee = 0;
    }
    
}