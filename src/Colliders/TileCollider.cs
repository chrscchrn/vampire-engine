
/*using System.Collections.Generic;*/
using System.Collections.Generic;
using Microsoft.Xna.Framework;
/*using Microsoft.Xna.Framework.Graphics;*/

namespace vampire;
// only needs to be the size of the screen resolution before scaling
// 320 x 180
// again... this is preloaded before the scene is running
// it should not move, it should not change
// there are a ton of simple/efficient ways to check collisions
// so if I'm confused, the answer is on a simpler plane than I am 
// thinking at

public class TileCollider : Collider
{

    public TileCollider(Vector2 size)
    {
        Height = (int)size.Y;
        Width = (int)size.X;

    }

    private int height;
    private int width;

    public override int Height
    {
        set => height = value;
        get => height;
    }

    public override int Width
    {
        get => width;
        set => width = value;
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
        throw new System.NotImplementedException();
    }

    public override bool Check(Vector2 at, Entity other)
    {
        throw new System.NotImplementedException();
    }

    public override bool Check(Vector2 at, IEnumerable<Entity> others)
    {
        throw new System.NotImplementedException();
    }
}
