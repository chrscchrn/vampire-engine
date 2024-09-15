namespace vampire.physics;


public class SpatialHash
{

    int _cellSize;

    float _inverseCellSize;

    public SpatialHash(int cellSize)
    {
        _cellSize = cellSize;
        _inverseCellSize = 1f / _cellSize;
    }

    /// <summary>
    /// adds the object to the SpatialHash
    /// </summary>
    /// <param name="collider">Object.</param>
    public void Register(Collider collider)
    {

    }

    /// <summary>
    /// adds the object to the SpatialHash
    /// </summary>
    /// <param name="collider">Object.</param>
    public void Remove(Collider collider)
    {

    }


}
