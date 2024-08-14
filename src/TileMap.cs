using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace vampire;

public class TileMap : Component
{
    public Dictionary<Vector2, int> tileMap = new();
    public List<Rectangle> textureStore;
    public Texture2D textureAtlas;
    public int tileSize;

    public TileMap(string filePath)
    {
        Dictionary<Vector2, int> result = new();
        XmlDocument doc = new ();
        doc.Load(Engine.Instance.ContentDirectory + filePath + @".tmx");
        XmlNode mapNode = doc.GetElementsByTagName("map")[0];

        if (!int.TryParse(mapNode.Attributes["width"].Value, out int width))
            throw new XmlException("width not able to be processed in Tilemap.cs");
        if (!int.TryParse(mapNode.Attributes["height"].Value, out int height))
            throw new XmlException("height not able to be processed in Tilemap.cs");
        if (!int.TryParse(mapNode.Attributes["tilewidth"].Value, out tileSize))
            throw new XmlException("tilesize not able to be processed in Tilemap.cs");

        string[] str = Regex.Replace(mapNode.InnerText, @"\s+", "").Split(",");
        int counter = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (int.TryParse(str[counter], out int value))
                    throw new XmlException("MapNode data is not be able to be parsed. Tilemap.cs");
                if (value > 0)
                {
                    result[new Vector2(x, y)] = value;
                }
                counter++;
            }
        }

        tileMap = result;
        textureStore = new()
        {
            new Rectangle(0, 0, tileSize, tileSize),
            new Rectangle(8, 0, tileSize, tileSize)
        };
        textureAtlas = Engine.Instance.Content.Load<Texture2D>("tiles-image"); // png
    }

    public override void Render()
    {
        foreach (var item in tileMap)
        {
            Rectangle dest =
                new((int)item.Key.X * tileSize, (int)item.Key.Y * tileSize, tileSize, tileSize);
            Rectangle src = textureStore[item.Value - 1];
            Engine.Instance._spriteBatch.Draw(textureAtlas, dest, src, Color.White);
        }
    }
}
