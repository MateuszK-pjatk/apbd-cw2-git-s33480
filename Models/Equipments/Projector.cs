namespace Proj1.Models.Equipments;

public class Projector : Equipment
{
    public string Resolution { get; set; }
    public bool HasHdmi  { get; set; }

    public Projector(string name, string resolution, bool hasHdmi) : base(name)
    {
        Resolution = resolution;
        HasHdmi = hasHdmi;
    }
}