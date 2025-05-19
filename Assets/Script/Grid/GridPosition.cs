using Unity.VisualScripting;

public struct GridPosition
{
    int x;
    int z;
    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
    public override string ToString()
    {
        return $"x = {x},z = {z}";
    }


}

