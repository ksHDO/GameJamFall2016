using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
   [Serializable]
   class Map
    {
        private const int MAX_WIDTH = 500;
        private const int MAX_HEIGHT = 500;

        public TextureMap TextureMap { get; set; }
        // private Texture2D _texture;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Tile[,] Tiles { get; set; }
        // public Trigger[] Triggers { get; set; }
        // private Vector2 _mapPosition;


        private Map(ContentManager content, int width, int height, int spriteWidthHeight)
        {
            Width = width;
            Height = height;
        }
        
        public Map(ContentManager content, string tileSet, int width, int height, int spriteWidthHeight) : this(content,width, height, spriteWidthHeight)
        {
            TextureMap = new TextureMap(tileSet, spriteWidthHeight);
            Load(content);
        }

        public Map(ContentManager content, string tileSet, int tileWidth, int tileHeight, int width, int height, int spriteWidthHeight) : this(content, width, height, spriteWidthHeight)
        {
            TextureMap = new TextureMap(tileSet, tileWidth, tileHeight, spriteWidthHeight, spriteWidthHeight);
        }

        // public GetTextureWidthHeight()
        // {
        //     
        // }

        public void Load(ContentManager content)
        {
            // _mapPosition = new Vector2(50, 50);
            // Texture2D texture = content.Load<Texture2D>("tileset1");
            // TextureMap = new TextureMap(_texture, 160, 32, 32, 32);
            Tiles = new Tile[Width, Height];
            Tiles = InitializeTiles(Tiles, Width, Height);
        }

        public void ChangeDimensions(int width, int height)
        {
            Tile[,] oldTiles = Tiles;
            Tiles = new Tile[width, height];
            Tiles = InitializeTiles(Tiles, width, height);


            int widthOrWidth = (width > Width ? Width : width);
            int heightOrHeight = (height > Height ? Height : height);
            for (int x = 0; x < widthOrWidth; x++)
            {
                for (int y = 0; y < heightOrHeight; y++)
                {
                    Tiles[x, y] = oldTiles[x, y];
                }
            }
            Width = width;
            Height = height;
        }

        private Tile[,] InitializeTiles(Tile[,] tiles, int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = new Tile();
                    tiles[x, y].ID = 0;
                    // tiles[x, y].Y = 0;
                    tiles[x, y].Collidable = false;
                }
            }
            return tiles;
        }

        public Rectangle GetTileOnSheet(int id)
        {
            return TextureMap.GetTileOnSheet(id);
        }
    }
}
