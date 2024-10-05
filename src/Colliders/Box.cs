using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace vampire;

public class Box : Collider
{
    public Box(Vector2 size)
    {
        Width = (int)size.X;
        Height = (int)size.Y;
    }

    private int width;
    private int height;

    public override int Width
    {
        get => width;
        set => width = value;
    }

    public override int Height
    {
        get => height;
        set => height = value;
    }

    public override int Top
    {
        get => (int)Position.Y;
        set => Position = new(Position.Y, value);
    }

    public override int Bottom
    {
        get => (int)Position.Y + Height;
        set => Position = new(Position.Y, value - Height);
    }

    public override int Left
    {
        get => (int)Position.X;
        set => Position = new(value, Position.X);
    }

    public override int Right
    {
        get => (int)Position.X + Width;
        set => Position = new(value - Width, Position.X);
    }



    // make a hollow rect drawer
    public override void DebugRender()
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
                {

                    colors[i * Height + j] = new Color(
                        new Vector4(10, 79, 79, 0.01f)
                        );
                }
            }
        }
        texture.SetData(colors);
        Engine.Instance._spriteBatch.Draw(texture, Bounds, Color.Red);
    }

    public override bool Collide(Box other)
    {
        return Left < other.Right
            && Right > other.Left
            && Top < other.Bottom
            && Bottom > other.Top;
    }

    public override bool Collide(TileCollider other)
    {
        return other.Collide(this);
    }
}
