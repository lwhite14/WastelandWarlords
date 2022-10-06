public interface Map
{
    public HexCellAbstract[] GetCells();

    public HexCoordinates GetBottomLeftCoords();
    public HexCoordinates GetBottomRightCoords();
    public HexCoordinates GetTopLeftCoords();
    public HexCoordinates GetTopRightCoords();


    public int GetWidth();
    public int GetHeight();
}

