using Microsoft.Xna.Framework;

namespace vampire;

public abstract class Collider
{

    public Entity Entity { get; set; }
    public Vector2 Position
    {
        get
        {
            return Entity.Position + Offset;
        }
        set
        {
            throw new System.Exception("Don't set colliders position, use offset bc I am lazy");
        }
    }
    public Vector2 Offset = Vector2.Zero;

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
    public abstract bool Collide(Box box);
    public abstract bool Collide(TileCollider tileCollider);

    public bool Collide(Entity entity)
    {
        return Collide(entity.Collider);
    }

    public bool Collide(Collider collider)
    {
        if (collider is Box)
        {
            return Collide(collider as Box);
        }
        else if (collider is TileCollider)
        {
            return Collide(collider as TileCollider);
        }
        else
        {
            throw new System.Exception("Collisions against the collider type are not allowed");
        }
    }

    public Vector2 Size
    {
        get => new Vector2(Width, Height);
    }

    public Rectangle Bounds
    {
        get => new Rectangle(Left, Top, Width, Height);
    }
}
