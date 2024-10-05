using System.Collections.Generic;
using Microsoft.Xna.Framework;
using vampire;

public static class Collide
{

    // entity vs entity
    public static bool Check(Entity entityOne, Entity entityTwo)
    {
        if (entityOne == null || entityTwo == null)
            return false;
        else
        {
            bool ans = ((entityOne != entityTwo) && entityOne.Collider.Collide(entityTwo));
            return ans;
        }

    }

    public static bool Check(Entity a, Vector2 at, Entity b)
    {
        Vector2 old = a.Position;
        a.Position = at;
        bool ans = Check(a, b);
        a.Position = old;
        return ans;
    }

    public static bool Check(Entity a, Vector2 pos, IEnumerable<Entity> entities)
    {
        foreach (Entity b in entities)
            if (Check(a, pos, b))
                return true;
        return false;
    }

    public static bool Check(Entity a, IEnumerable<Entity> entities)
    {
        foreach (Entity b in entities)
            if (Check(a, b))
                return true;
        return false;
    }
}
