using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace vampire;

public class Box : Collider
{
    public Box(Vector2 size)
    {
        Height = (int)size.Y;
        Width = (int)size.X;
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
        get => (int)RelativePosition.X;
        set => RelativePosition = new(RelativePosition.X, value);
    }

    public override int Bottom
    {
        get => (int)RelativePosition.X + Height;
        set => RelativePosition = new(RelativePosition.X, value - Height);
    }

    public override int Left
    {
        get => (int)RelativePosition.Y;
        set => RelativePosition = new(value, RelativePosition.Y);
    }

    public override int Right
    {
        get => (int)RelativePosition.Y + Width;
        set => RelativePosition = new(value - Width, RelativePosition.Y);
    }



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

    public override bool Check(Vector2 at, Entity other)
    {
        // check the type of Collider
        if (other.Collider is Box)
        {
            return at.X < other.Position.X + other.Collider.Width
                && at.X + Width > other.Position.X
                && at.Y < other.Position.Y + other.Collider.Height
                && at.Y + Height > other.Position.Y;
        }
        else if (other.Collider is TileCollider)
        {
            /*return other.Collider.Check(Entity, at);*/
            // need a switch or something to organize the diff types of cols
        }
        else
        {
            throw new System.NotImplementedException();
        }
    }

    public override bool Check(Vector2 at, IEnumerable<Entity> others)
    {
        foreach (Entity other in others)
            if (Check(at, other))
                return true;
        return false;
    }
}
