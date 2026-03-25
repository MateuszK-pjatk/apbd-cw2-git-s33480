namespace Proj1.Models.Equipments;

public abstract class Equipment
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsAvailable { get; set; }

    protected Equipment(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        IsAvailable = true;
    }
}