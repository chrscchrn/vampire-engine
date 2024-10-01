using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace vampire;

public class SpriteRenderer : Component
{
    public Texture2D texture;

    private bool _flipX = false;

    public bool flipX
    {
        get => _flipX;
        set
        {
            if (value != _flipX)
            {
                FlipX();
                _flipX = value;
            }
        }
    }

    public SpriteRenderer(Color color, int width, int height)
    {
        Color[] colors = new Color[width * height];
        for (int i = 0; i < width * height - 1; i++)
            colors[i] = color;
        texture = new Texture2D(Engine.Instance.GraphicsDevice, width, height);
        texture.SetData(colors);
    }

    public SpriteRenderer(string fileName)
    {
        texture = Engine.Instance.Content.Load<Texture2D>(fileName);
    }

    public SpriteRenderer(Texture2D texture)
    {
        this.texture = texture;
    }

    public SpriteRenderer() { }

    public override void Start()
    {
        base.Start();
    }

    public override void Update() { }

    public override void Render()
    {
        Engine.Instance._spriteBatch.Draw(
            texture,
            Entity.Position,
            new Rectangle(0, 0, texture.Width, texture.Height),
            Color.White
        );
    }

    public Texture2D FromFile(string fileName)
    {
        texture = Engine.Instance.Content.Load<Texture2D>(fileName);
        return texture;
    }

    public void SetTexture(Texture2D newtexture)
    {
        texture = newtexture;
    }

    private void FlipX()
    {
        Color[] colors = new Color[texture.Width * texture.Height];
        texture.GetData(colors);
        Color[] newColors = new Color[texture.Height * texture.Width];
        for (int i = 0; i < texture.Height; i++)
        {
            for (int j = 0; j < texture.Width; j++)
            {
                newColors[texture.Width * i + texture.Width - j - 1] = colors[
                    texture.Width * i + j
                ];
            }
        }
        texture.SetData(newColors);
    }
}
