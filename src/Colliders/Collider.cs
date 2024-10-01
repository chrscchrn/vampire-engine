using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace vampire;

public abstract class Collider
{

    public Entity Entity { get; set; }
    public Vector2 RelativePosition { get; set; }
    public Vector2 AbsolutePosition { get; set; }

    internal virtual void Added(Entity entity)
    {
        Entity = entity;
    }

    internal virtual void Removed(Entity entity)
    {
        Entity = null;
    }

    public abstract void DebugRender();
    public abstract int Width { get; set; }
    public abstract int Height { get; set; }
    public abstract int Top { get; set; }
    public abstract int Bottom { get; set; }
    public abstract int Left { get; set; }
    public abstract int Right { get; set; }
    public abstract bool Check(Vector2 at, Entity other);
    public abstract bool Check(Vector2 at, IEnumerable<Entity> others);

    public Vector2 Size
    {
        get => new Vector2(Width, Height);
    }

    public Rectangle Bounds
    {
        get => new Rectangle(Left, Right, Width, Height);
    }
}
