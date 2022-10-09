using YamlDotNet.Serialization;



public class HexCellAbstract
{
    public string type { get; set; }
    public HexCoordinates coordinates { get; set; }
    public float height { get; set; }

    public HexCellAbstract(string type, HexCoordinates coordinates, float height) 
    {
        this.type = type;
        this.coordinates = coordinates;
        this.height = height;
    }
}