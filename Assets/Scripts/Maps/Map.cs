public interface Map
{
    public HexCellAbstract[] GetCells();

    public HexCoordinates GetBottomLeftCoords();
    public HexCoordinates GetBottomRightCoords();
    public HexCoordinates GetTopLeftCoords();
    public HexCoordinates GetTopRightCoords();
}

