using Microsoft.Xna.Framework;

namespace vampire;

public class Solid : Entity
{
    // one movement function, no collision delegates, bc they dont collide with other geometry (guaranteed movement)
    // Any Actors we meet along the way will need to be dealt with to uphold our law of no Solid-Actor overlap.
    //

    public Solid(Vector2 at)
        : base(at) { }

    public Solid() { }

    public void Move(float x, float y) { }
}
