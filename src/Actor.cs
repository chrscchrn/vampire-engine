using System;
using Microsoft.Xna.Framework;

namespace vampire;

public class Actor : Entity
{
    // Actors are physics objects, such as players, arrows, monsters, treasure chests, etc.
    // Anything that has to move and interact with the level geometry is an Actor.
    // Actors and Solids never interact (for now)
    // Actors don’t have any concept of their own velocity, acceleration, or gravity.

    public float yRemainder = 0f;
    public float xRemainder = 0f;

    public Actor() { }

    public override void Update()
    {
        base.Update();
    }

    public virtual void IsRiding(Solid solid) { }

    public virtual void Squish() { }

    public void MoveX(float amount, Action onCollide)
    {
        xRemainder += amount;
        int move = (int)Math.Round(xRemainder);
        if (move != 0)
        {
            xRemainder -= move;
            int sign = Sign(move);
            while (move != 0)
            {
                if (!CollideCheck<Solid>(Position + new Vector2(sign, 0)))
                {
                    Position.X += sign;
                    move -= sign;
                }
                else
                {
                    if (onCollide != null)
                        onCollide();
                    break;
                }
            }
        }
    }

    public void MoveY(float amount, Action onCollide)
    {
        yRemainder += amount;
        int move = (int)Math.Round(yRemainder);
        if (move != 0)
        {
            yRemainder -= move;
            int sign = Sign(move);
            while (move != 0)
            {
                if (!CollideCheck<Solid>(Position + new Vector2(0, sign)))
                {
                    Position.Y += sign;
                    move -= sign;
                }
                else
                {
                    if (onCollide != null)
                        onCollide();
                    break;
                }
            }
        }
    }

    public int Sign(int num)
    {
        if (num > 0)
        {
            return 1;
        }
        else if (num < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
