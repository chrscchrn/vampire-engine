using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace vampire;

public class TileMap : Component
{
    // tile map globals
    private Dictionary<Vector2, int> tileMap = new();
    private string tileSetFile;

    // tile set globals
    private List<Rectangle> textureStore = new();
    private string imageFile;
    private int tileSize;

    // tile image globals
    public Texture2D textureAtlas;

    public TileMap(string tileMapFile)
    {
        LoadTileMapData(tileMapFile);
        LoadTileSetData(tileSetFile);
        LoadTileImageData(imageFile);
    }

    public void LoadTileMapData(string tileMapFile)
    {
        XmlDocument doc = new();
        doc.Load(Engine.Instance.ContentDirectory + tileMapFile + @".tmx");
        XmlNode mapNode = doc.GetElementsByTagName("map")[0];

        if (!int.TryParse(mapNode.Attributes["width"].Value, out int width))
            throw new XmlException(
                "width not able to be processed in Tilemap.cs"
            );
        if (!int.TryParse(mapNode.Attributes["height"].Value, out int height))
            throw new XmlException(
                "height not able to be processed in Tilemap.cs"
            );

        // Dictionary<Vector2, int> result = new();
        string[] str = Regex.Replace(mapNode.InnerText, @"\s+", "").Split(",");
        int counter = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (!int.TryParse(str[counter], out int value))
                    throw new XmlException(
                        "MapNode data is not be able to be parsed. Tilemap.cs"
                    );
                if (value > 0)
                {
                    tileMap[new Vector2(x, y)] = value;
                }
                counter++;
            }
        }

        XmlNode tileSetNode = doc.GetElementsByTagName("tileset")[0];
        var tileSetAttribute = tileSetNode.Attributes["source"];
        if (tileSetAttribute == null)
        {
            throw new XmlException(
                "TileSet path cannot be found or wasn't loaded properly"
            );
        }
        else
        {
            tileSetFile = tileSetAttribute.Value;
        }
    }

    public void LoadTileSetData(string tileSetFile)
    {
        XmlDocument doc = new();
        doc.Load(Engine.Instance.ContentDirectory + tileSetFile);
        XmlNode tileSetNode = doc.GetElementsByTagName("tileset")[0];
        if (
            !int.TryParse(
                tileSetNode.Attributes["tilewidth"].Value,
                out tileSize
            )
        )
            throw new XmlException(
                "tilesize not able to be processed in Tilemap.cs"
            );
        if (
            !int.TryParse(
                tileSetNode.Attributes["tilewidth"].Value,
                out int tileCount
            )
        )
            throw new XmlException(
                "tile count not able to be processed in Tilemap.cs"
            );
        if (
            !int.TryParse(
                tileSetNode.Attributes["tilewidth"].Value,
                out int columns
            )
        )
            throw new XmlException(
                "columns not able to be processed in Tilemap.cs"
            );
        for (int i = 0; i < (tileCount + columns - 1) / columns; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                textureStore.Add(
                    new Rectangle(
                        j * tileSize,
                        i * tileSize,
                        tileSize,
                        tileSize
                    )
                );
            }
        }
        XmlNode imageNode = doc.GetElementsByTagName("image")[0];
        imageFile = imageNode.Attributes["source"].Value;
    }

    public void LoadTileImageData(string tileImage)
    {
        if (tileImage == null)
            throw new XmlException("tile image not loaded");
        textureAtlas = Engine.Instance.Content.Load<Texture2D>(
            tileImage.Split('.')[0]
        ); // png
    }

    public override void Render()
    {
        foreach (var item in tileMap)
        {
            Rectangle dest =
                new(
                    (int)item.Key.X * tileSize,
                    (int)item.Key.Y * tileSize,
                    tileSize,
                    tileSize
                );
            Rectangle src = textureStore[item.Value - 1];
            Engine.Instance._spriteBatch.Draw(
                textureAtlas,
                dest,
                src,
                Color.White
            );
        }
    }
}
