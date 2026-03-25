namespace Proj1.Models.Equipments;

public class Camera : Equipment
{
    public int Megapixels { get; set; }
    public bool HasOpticalZoom { get; set; }

    public Camera(string name, int megapixels, bool hasOpticalZoom) : base(name)
    {
        Megapixels = megapixels;
        HasOpticalZoom = hasOpticalZoom;
    }
}