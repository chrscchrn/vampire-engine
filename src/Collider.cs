using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace vampire;

public class Collider : Component
{
    // Component Props:
    // Entity
    // isActive
    // Entity.Position is available
    public Vector2 Position
    {
        get => Entity.Position + Offset;
        set => Offset = Entity.Position - value;
    }
    public Vector2 Offset = new(0, 0);

    public int Height { get; private set; }
    public int Width { get; private set; }

    public Collider(Vector2 size, Vector2 offset)
    {
        Size = size;
        Offset = offset;
    }

    public Collider(Vector2 size)
    {
        Size = size;
    }

    public Collider() { }

    // public void Collide() { }

    public Vector2 Size
    {
        get => new(Width, Height);
        set
        {
            Width = (int)value.X;
            Height = (int)value.Y;
        }
    }

    public Rectangle Rectangle
    {
        get =>
            new(
                (int)Position.X + (int)Offset.X,
                (int)Position.Y + (int)Offset.Y,
                Width,
                Height
            );
    }

    public int Left() => (int)Entity.Position.X + (int)Offset.X;

    public int Right() => (int)Entity.Position.X + Width - 1 + (int)Offset.X;

    public int Top() => (int)Entity.Position.Y + (int)Offset.Y;

    public int Bottom() => (int)Entity.Position.Y + Height - 1 + (int)Offset.Y;

    public override void Render()
    {
        Texture2D texture = new(Engine.Instance.GraphicsDevice, Width, Height);
        Color[] colors = new Color[Width * Height];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (
                    i < Width / Height
                    || (i * Height + j > Width * Height - Width)
                    || ((i * Height + j + 1) % Width == 0)
                    || ((i * Height + j) % Width == 0)
                )
                    colors[i * Height + j] = new Color(new Vector4(10, 79, 79, 0.01f));
            }
        }
        texture.SetData(colors);
        Engine.Instance._spriteBatch.Draw(texture, Rectangle, Color.Red);
    }

    public bool Check(Vector2 at, Collider other)
    {
        return at.X < other.Position.X + other.Width
            && at.X + Width > other.Position.X
            && at.Y < other.Position.Y + other.Height
            && at.Y + Height > other.Position.Y;
    }

    public bool Check(Vector2 at, IEnumerable<Collider> others)
    {
        foreach (Collider col in others)
            if (Check(at, col))
                return true;
        return false;
    }

    public bool Check(Vector2 at, Entity entity)
    {
        foreach (Collider col in entity.ColliderTracker)
            if (Check(at, col))
                return true;
        return false;
    }

    public bool Check(Vector2 at, IEnumerable<Entity> entities)
    {
        foreach (Entity entity in entities)
            if (Check(at, entity))
                return true;
        return false;
    }
}
