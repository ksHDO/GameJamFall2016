using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TentativeTitle;

namespace MapEditor
{
    class EditorText
    {
        private static SpriteFont _fontEditor;
        private static Texture2D _textBackground;
        private static int _fontHeight;
        public static void Load(ContentManager content)
        {
            _fontEditor = content.Load<SpriteFont>("EditorFont");
            _textBackground = ShapeGenerator.GenerateRectangle(200, 200, Color.Black);
            _fontHeight = (int) _fontEditor.MeasureString("Aq").X;
        }

        public static void Draw(SpriteBatch batch)
        {
            int startDraw = 100;
            batch.Draw(_textBackground, new Vector2(0, startDraw), Color.Black * 0.4f);

            batch.DrawString(_fontEditor, "Tileset: " + Editor.CurrentMap.TextureMap.TileTexture, new Vector2(10, startDraw), Color.White);
            batch.DrawString(_fontEditor, "Dimensions: " + Editor.GetMapWidth().ToString() + "x" + Editor.GetMapHeight().ToString(), new Vector2(10, startDraw + _fontHeight), Color.White);
            batch.DrawString(_fontEditor, "Selector Tile ID: " + Editor.SelectionTileID.ToString(), new Vector2(10, startDraw + (2 * _fontHeight)), Color.White);
            batch.DrawString(_fontEditor, "Selection Tile ID: " + Editor.CurrentTileSelectionID.ToString(), new Vector2(10, startDraw + (3 * _fontHeight)), Color.White);
            if (Editor.ShowCollision)
            {
                batch.DrawString(_fontEditor, "Collision On", new Vector2(10, startDraw + (4 * _fontHeight)), Color.White);
            }


            batch.DrawString(_fontEditor, "Row " + (Editor.CurrentEditorRow + 1).ToString() + " of " + (Editor.CurrentMap.TextureMap.GetTilesPerColumn()), new Vector2(40, 70), Color.White);

        }
    }
}
