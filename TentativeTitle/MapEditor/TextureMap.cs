using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    [Serializable]
    class TextureMap
    {
        // public Texture2D Texture { get; private set; }
        public string TileTexture { get; private set; }
        public int TextureWidth { get; set; }
        public int TextureHeight { get; set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }

        public TextureMap(TextureMap map)
        {
            TileTexture = map.TileTexture;
            TextureWidth = map.TextureHeight;
            TileWidth = map.TileWidth;
            TileHeight = map.TileHeight;
        }

        public TextureMap (string tileTexture, int width, int height, int tileWidth, int tileHeight)
        {
            SetTileTexture(tileTexture, width, height);
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }
        public TextureMap (string tileTexture, int tileWidthHeight)
        {
            TileTexture = tileTexture;
            TileWidth = tileWidthHeight;
            TileHeight = tileWidthHeight;
        }

        public void SetTileTexture(string tileTexture, int textureWidth, int textureHeight)
        {
            TileTexture = tileTexture;
            SetTileDimensions(textureWidth, textureHeight);
        }

        public void SetTileDimensions(int textureWidth, int textureHeight)
        {
            TextureWidth = textureWidth;
            TextureHeight = textureHeight;
        }

        public Rectangle GetTileOnSheet(int id)
        {
            // Calculate stuff

            /*
            Width = 32.
            
                ||   0 |  32 |  64 |  96 | 128 |
                ++-----+-----+-----+-----+-----+
                ++-----+-----+-----+-----+-----+
              0 ||   0 |   1 |   2 |   3 |   4 |
             32 ||   5 |   6 |   7 |   8 |   9 |
             64 ||  10 |  11 |  12 |  13 |  14 |
             96 ||  15 |  16 |  17 |  18 |  19 |
            128 ||  20 |  21 |  22 |  23 |  24 |
            160 ||  25 |  26 |  27 |  28 |  29 |
            */
            // TextureMap.
            // Tiles[xTile, yTile].X; // id

            /*
                Rectangle tile = new Rectangle(
                CurrentMap.Tiles[x, y].X * textureTileWidth,
                CurrentMap.Tiles[x, y].Y * textureTileHeight,
                textureTileWidth,
                textureTileHeight
            );
            */
            return new Rectangle(
                GetXID(id) * TileWidth,
                GetYID(id) * TileHeight,
                TileWidth,
                TileHeight
            ); ;
        }

        public int GetTilesPerRow()
        {
            return TextureWidth / TileWidth;
        }

        public int GetTilesPerColumn()
        {
            return TextureHeight / TileHeight;
        }

        public int GetXID(int id)
        {
            // return the x position on the spritesheet from the id.
            // if there are 5 tiles on a sheet and id is 3 ... 3
            // if there are 5 tiles on a sheet and id is 7 ... 2
            return id % GetTilesPerRow();
        }
        public int GetYID(int id)
        {
            // If there are 5 tiles on a sheet and id is 3 ... 0
            // If there are 5 tiles on a sheet and id is 7 ... 1
            return id / GetTilesPerRow();
        }

    }
}
