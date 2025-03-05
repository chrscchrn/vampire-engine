using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace vampire;

public static class Draw
{
    public static SpriteBatch SpriteBatch;

    public static Texture2D Pixel;

    public static void Initialize(GraphicsDevice GraphicsDevice, SpriteBatch spriteBatch)
    {
        SpriteBatch = spriteBatch;
        // Create pixel for debug drawing
        Pixel = new Texture2D(GraphicsDevice, 1, 1);
        var color = new Color[1];
        color[0] = Color.Red;
        Pixel.SetData<Color>(color);
    }

    public static void Circle(Vector2 position, float radius, Color color)
    {

        Vector2 last = Vector2.UnitX * radius;
        Vector2 lastP = last.Perpendicular();
        for (int i = 0; i < radius * 2; i++)
        {
            Vector2 at = Calc.AngleToVector(i * MathHelper.PiOver2 / radius * 2, radius);
            Vector2 atP = at.Perpendicular();


            Console.WriteLine(at.ToString() + " | " + atP.ToString());

            Draw.Line(position + last, position + at, color);
            Draw.Line(position - last, position - at, color);
            Draw.Line(position + lastP, position + atP, color);
            Draw.Line(position - lastP, position - atP, color);

            last = at;
            lastP = atP;
        }
    }

    public static void Line(Vector2 start, Vector2 end, Color color)
    {
        LineAngle(start, Calc.Angle(start, end), Vector2.Distance(start, end), color);
    }

    public static void LineAngle(Vector2 start, float angle, float length, Color color)
    {
        SpriteBatch.Draw(Pixel, start, Pixel.Bounds, color, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
    }
}
