using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct HexCoordinates
{
    [SerializeField]
    private int x, z;

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Z
    {
        get
        {
            return z;
        }
    }

    public int Y { get { return -X - Z; } }

    public HexCoordinates(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public static HexCoordinates FromOffsetCoordinates(int x, int z)
    {
        return new HexCoordinates(x - z / 2, z);
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Z.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Z.ToString();
    }

    public static HexCoordinates operator +(HexCoordinates hex1, HexCoordinates hex2)
    {
        HexCoordinates newHex = new HexCoordinates(hex1.X + hex2.X, hex1.Z + hex2.Z);
        return newHex;
    }

    public static bool operator ==(HexCoordinates hex1, HexCoordinates hex2) 
    {
        if ((hex1.X == hex2.X) && (hex1.Z == hex2.Z))
        {
            return true;
        }
        return false;
    }

    public static bool operator !=(HexCoordinates hex1, HexCoordinates hex2)
    {
        if ((hex1.X == hex2.X) && (hex1.Z == hex2.Z))
        {
            return false;
        }
        return true;
    }
}