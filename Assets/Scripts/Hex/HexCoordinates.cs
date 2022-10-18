using HexPathfinding;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class HexCoordinates
{
    [SerializeField]
    int x, z;
    float height;

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

    public float Height 
    {
        get 
        {
            return height;
        }
    }

    public int Y { get { return -X - Z; } }

    public HexCoordinates(int x, int z, float height = 0.0f)
    {
        this.x = x;
        this.z = z;
        this.height = height;  
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Z.ToString() + "), (Height: " + Height + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Z.ToString();
    }

    public Vector3 ToWorldSpace() 
    {
        Vector3 position;
        position.x = X * (HexMetrics.innerRadius * 2f) + (HexMetrics.innerRadius * Z);
        position.y = Height * 8;
        position.z = Z * (HexMetrics.outerRadius * 1.5f);
        return position; 
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

    public override bool Equals(System.Object obj)
    {
        if (obj == null) { return false; }

        HexCoordinates hexCoordinatesObj = obj as HexCoordinates;
        if ((this.X == hexCoordinatesObj.X) && (this.Z == hexCoordinatesObj.Z))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return this.X.GetHashCode() + this.Z.GetHashCode();
    }
}