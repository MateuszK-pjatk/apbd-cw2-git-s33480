namespace Proj1.Models.Equipment;

public class Laptop : Equipment
{
    public string Processor { get; set; }
    public int RamSizeGB { get; set; }

    public Laptop(string name, string processor, int ramSizeGB) : base(name)
    {
        Processor = processor;
        RamSizeGB = ramSizeGB;
    }
}