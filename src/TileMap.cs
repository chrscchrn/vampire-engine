using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace vampire;

public class TileMap : Component
{
    // tile map globals
    protected Dictionary<Vector2, int> tileMap = new();

    // tile set globals
    protected List<Rectangle> textureStore = new();
    private int tileSize;

    // tile image globals
    public Texture2D textureAtlas;

    public TileMap(string tileMapFile)
    {
        LoadTileMapData(tileMapFile);
    }

    public TileMap(string tileMapFile, bool addColliders)
    {
        LoadTileMapData(tileMapFile);
        if (addColliders)
            CreateTileMapColliders();
    }

    public void LoadTileMapData(string tileMapFile)
    {
        XmlDocument doc = new();
        try
        {
            doc.Load(Engine.Instance.ContentDirectory + tileMapFile + @".tmx");
        }
        catch (XmlException e)
        {
            throw new XmlException("TileMap file could not be loaded.\n" + e);
        }

        XmlNode mapNode = doc.GetElementsByTagName("map")[0];
        if (!int.TryParse(mapNode.Attributes["width"].Value, out int width))
            throw new XmlException("width not able to be processed in Tilemap.cs");
        if (!int.TryParse(mapNode.Attributes["height"].Value, out int height))
            throw new XmlException("height not able to be processed in Tilemap.cs");

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

        XmlNode tileMapTileSetNode = doc.GetElementsByTagName("tileset")[0];
        string tileSetFile =
            tileMapTileSetNode.Attributes["source"].Value
            ?? throw new XmlException(
                "TileSet path cannot be found or wasn't loaded properly"
            );

        // tile set data
        XmlDocument tileSetDoc = new();
        try
        {
            tileSetDoc.Load(Engine.Instance.ContentDirectory + tileSetFile);
        }
        catch (XmlException e)
        {
            throw new XmlException("TileSet File cannot be loaded.\n" + e);
        }

        XmlNode tileSetNode = tileSetDoc.GetElementsByTagName("tileset")[0];
        if (!int.TryParse(tileSetNode.Attributes["tilewidth"].Value, out tileSize))
            throw new XmlException("tilesize not able to be processed in Tilemap.cs");
        if (!int.TryParse(tileSetNode.Attributes["tilewidth"].Value, out int tileCount))
            throw new XmlException("tile count not able to be processed in Tilemap.cs");
        if (!int.TryParse(tileSetNode.Attributes["tilewidth"].Value, out int columns))
            throw new XmlException("columns not able to be processed in Tilemap.cs");

        for (int i = 0; i < (tileCount + columns - 1) / columns; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                textureStore.Add(
                    new Rectangle(j * tileSize, i * tileSize, tileSize, tileSize)
                );
            }
        }

        XmlNode imageNode = tileSetDoc.GetElementsByTagName("image")[0];
        string imageFile =
            imageNode.Attributes["source"].Value
            ?? throw new XmlException("Tile Image data cannot be parsed.");

        try
        {
            textureAtlas = Engine.Instance.Content.Load<Texture2D>(
                imageFile.Split('.')[0]
            );
        }
        catch (XmlException e)
        {
            throw new XmlException("File does not exist is filename is incorrect.\n" + e);
        }
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
            Engine.Instance._spriteBatch.Draw(textureAtlas, dest, src, Color.White);
        }
    }

    public void CreateTileMapColliders() { }
}
