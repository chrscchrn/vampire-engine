using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace vampire;

public class Collider
{

    public int Height { get; private set; }
    public int Width { get; private set; }

    public Vector2 Offset { get; set; }
    public Entity Entity { get; set; }
    public Vector2 Position
    {
        get => Entity.Position + Offset;
    }

    public Collider(Vector2 size, Vector2 offset)
    {
        Size = size;
        Offset = offset;
    }

    public Collider(Vector2 size)
    {
        Size = size;
        Offset = Vector2.Zero;
    }

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
            new((int)Position.X, (int)Position.Y, Width, Height);
    }

    public int Left() => (int)Position.X;

    public int Right() => (int)Position.X + Width - 1;

    public int Top() => (int)Position.Y;

    public int Bottom() => (int)Position.Y + Height - 1;

    public void DebugRender()
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
                    colors[i * Height + j] = new Color(
                        new Vector4(10, 79, 79, 0.01f)
                    );
            }
        }
        texture.SetData(colors);
        Engine.Instance._spriteBatch.Draw(texture, Rectangle, Color.Red);
    }

    public bool Check(Vector2 at, Entity other)
    {
        return at.X < other.Position.X + other.Collider.Width
            && at.X + Width > other.Position.X
            && at.Y < other.Position.Y + other.Collider.Height
            && at.Y + Height > other.Position.Y;
    }

    public bool Check(Vector2 at, IEnumerable<Entity> others)
    {
        foreach (Entity other in others)
            if (Check(at, other))
                return true;
        return false;
    }

    public void Collide() { }

}
