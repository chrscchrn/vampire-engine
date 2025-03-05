using Microsoft.Xna.Framework;
/*using Microsoft.Xna.Framework.Graphics;*/
/*using System;*/

namespace vampire;

public class Circle : Collider
{
    public Circle(int radius)
    {
        Radius = radius;
    }

    private int Radius;

    public int Diameter
    {
        get => Radius * 2;
        set => Radius = value / 2;
    }

    public override int Width
    {
        get => Radius * 2;
        set => Radius = value / 2;
    }

    public override int Height
    {
        get => Radius * 2;
        set => Radius = value / 2;
    }

    public override int Top
    {
        get => (int)Position.Y - Radius;
        set => Position = new Vector2(Position.X, value + Radius);
    }

    public override int Bottom
    {
        get => (int)Position.Y + Height;
        set => Position = new Vector2(Position.X, value - Radius);
    }

    public override int Left
    {
        get => (int)Position.X - Radius;
        set => Position = new Vector2(Radius + value, Position.Y);
    }

    public override int Right
    {
        get => (int)Position.X + Radius;
        set => Position = new Vector2(Radius - value, Position.Y);
    }

    // make a hollow circle drawer
    public override void DebugRender()
    {
        Draw.Circle(Position, (float)Radius, Color.Red);
    }

    public override bool Collide(Box other)
    {
        throw new System.NotImplementedException();
    }

    public override bool Collide(TileCollider other)
    {
        throw new System.NotImplementedException();
    }

    public override bool Collide(Circle other)
    {
        throw new System.NotImplementedException();
    }
}
