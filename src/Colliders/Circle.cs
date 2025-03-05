using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace vampire;

public class Circle : Collider
{
  public Circle(int radius)
  {
    Radius = radius;
  }

  private int Radius;

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
    get => (int)Position.Y - (int)Radius;
    set => Position = new Vector2(Position.X, value + Radius);
  }

  public override int Bottom
  {
    get => (int)Position.Y + Height;
    set => Position = new(Position.X, value - Height);
  }

  public override int Left
  {
    get => (int)Position.X - Radius;
    set => Position = new(Position.X + value, Position.Y);
  }

  public override int Right
  {
    get => (int)Position.X + Radius;
    set => Position = new(Position.X - value, Position.Y);
  }


  // make a hollow rect drawer
  public override void DebugRender()
  {
    Texture2D texture = new(Engine.Instance.GraphicsDevice, Width, Height);
    Color[] colors = new Color[Width * Height];

    for (int y = -Radius; y <= Radius; y++)
      for (int x = -Radius; x <= Radius; x++)
        if (Math.Sqrt(x * x + y * y) <= Radius)
          colors[x * Radius + y] = Color.Cyan;

    texture.SetData(colors);
    Engine.Instance._spriteBatch.Draw(texture, Bounds, Color.Red);
  }

  public override bool Collide(Box other)
  {
    throw new System.NotImplementedException();
  }

  public override bool Collide(TileCollider other)
  {
    throw new System.NotImplementedException();
  }
}
