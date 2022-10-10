using YamlDotNet.Serialization;



public class HexCellAbstract
{
    public string type { get; set; }
    public HexCoordinates coordinates { get; set; }

    public HexCellAbstract(string type, HexCoordinates coordinates) 
    {
        this.type = type;
        this.coordinates = coordinates;
    }
}