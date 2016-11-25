using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TentativeTitle;
using Forms = System.Windows.Forms;

namespace MapEditor
{
    class Editor
    {
        private const string FILE_TYPE_FILTER = ".map files (*.map)|*.map";

        public static Map CurrentMap { get; private set; }

        private static ContentManager _contentLoader;
        private static Texture2D _selectionRect;
        private static Vector2 _mapPosition; 
        private static Vector2 _mousePos;
        private static int _speed = 5;
        private static bool _mapMovementReverse = true;
        // private static Point _selectionTilePos = Point.Zero;
        private static int _selectionX = 0;
        private static int _selectionY = 0;
        private static Point _selectionPos = Point.Zero;
        private static int _lastScroll = 0;
        private static float _zoom = 1;
        private static Texture2D _mapTexture;
        private static Texture2D _collisionTexture;

        private static Texture2D _toolBarTexture;
        private const float _toolBarZoom = 1.5f;
        private static Texture2D _toolbarScroll;
        private static Vector2 _toolbarScrollPos;
        private static Vector2 _toolbarPos;
        private static float _toolbarScrollWidth = 200;
        private static bool _isToolbarScrolling = false;

        private static Button _buttonToolsFillBucket;

        private static bool _toolsFillBucketTextureSelected = false;
        private static Vector2 _toolsFillBucketPos = new Vector2(Game1.Width - 32, 100 - 32);


        //-------------------------------| Rectangle Fill and UndoManager |----------------------------
        private static UndoManager _undoManager;
        
        private static Button   _buttonToolsFillRectangle;

        private static bool     _toolsFillRectangleTextureSelected = false;
        private static Vector2  _toolsFillRectanglePos = new Vector2(Game1.Width - 64, 100 - 32);
        //---------------------------------------------------------------------------------------------


        private static int _tileSelectionID = 0;
        public static int CurrentTileSelectionID { get; private set; } = 0;
        public static int SelectionTileID { get; private set; } = 0;

        public static int CurrentEditorRow { get; private set; } = 0;

        public static bool ShowCollision { get; private set; }
        public static bool ShowTriggers { get; private set; }

        public static int GetMapWidth() { return CurrentMap.Width; }
        public static int GetMapHeight() { return CurrentMap.Height; }

        public void Load(ContentManager content)
        {
            _contentLoader = content;
            _toolBarTexture = ShapeGenerator.GenerateRectangle(Game1.Width, 100, new Color(50, 50, 50));
            _buttonToolsFillBucket = new Button(content, "Tools/fillBucket");
            _buttonToolsFillBucket.SetEvent(ToggleFillBucket);
            _buttonToolsFillBucket.Position = _toolsFillBucketPos;


            //-----| Fill stuff |------------------
            _buttonToolsFillRectangle = new Button(content, "Tools/fillRectangle");
            _buttonToolsFillRectangle.SetEvent(ToggleFillRectangle);
            _buttonToolsFillRectangle.Position = _toolsFillRectanglePos;
            //-------------------------------------

            CreateNewMap();
            DefaultValues();
        }

        private void CreateNewMap()
        {
            CurrentMap = new Map(_contentLoader, "tileset1", 30, 30, 32);
        }

        private void OpenTextures()
        {
            _mapTexture = _contentLoader.Load<Texture2D>(CurrentMap.TextureMap.TileTexture);
            CurrentMap.TextureMap.SetTileDimensions( _mapTexture.Width, _mapTexture.Height);
            if ((_mapTexture.Width * _toolBarZoom) > Game1.Width)
            {
                float scrollRatio = (Game1.Width / (_mapTexture.Width * _toolBarZoom));
                _toolbarScrollWidth = Game1.Width * scrollRatio;
                if (_toolbarScrollWidth < 10)
                {
                    _toolbarScrollWidth = 10;
                }
            }
            else
            {
                _toolbarScrollWidth = Game1.Width;
            }
            _toolbarScroll = ShapeGenerator.GenerateRectangle((int) _toolbarScrollWidth, 16, Color.White);
            _toolbarScrollPos = Vector2.Zero;
            _toolbarPos = Vector2.Zero;
            _tileSelectionID = 0;
        }

        private void DefaultValues()
        {
            OpenTextures();
            _selectionRect = ShapeGenerator.GenerateBorder(CurrentMap.TextureMap.TileWidth, CurrentMap.TextureMap.TileHeight, 2, Color.White);
            _collisionTexture = ShapeGenerator.GenerateRectangle(CurrentMap.TextureMap.TileWidth, CurrentMap.TextureMap.TileHeight, Color.White);
            _mapPosition = Vector2.Zero;
            // _selectionTilePos = Point.Zero;
            _selectionX = 0;
            _selectionY = 0;
            CurrentEditorRow = 0;
            EditorDraw.CurrentMap = CurrentMap;
            _zoom = 1f;
        }

        public void Unload()
        {
            _collisionTexture.Dispose();
            _selectionRect.Dispose();
            _toolbarScroll.Dispose();
            _buttonToolsFillBucket.Dispose();
            //-----------------------
            _buttonToolsFillRectangle.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            _buttonToolsFillBucket.Update();

            //-----------------------
            _buttonToolsFillRectangle.Update();

            ParseKeys();
            ParseMouse();
        }

        private void SwitchTextures()
        {
            TextureMap previousTexture = new TextureMap(CurrentMap.TextureMap);
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter Texture.", "Change TileMap");
            if (input != null && input != "")
            {
                // CurrentMap.TextureMap.TileTexture = input;
                CurrentMap.TextureMap.SetTileTexture(input, 0, 0);
                try
                {
                    OpenTextures();
                }
                catch (Exception ex)
                {
                    Forms.MessageBox.Show("Could not open texture: " + ex.Message);

                    CurrentMap.TextureMap = previousTexture;
                    OpenTextures();
                }
            }
        }

        private void ChangeMapDimensions()
        {
            string inputWidth = Microsoft.VisualBasic.Interaction.InputBox("Enter Map Width", "Change Map Dimensions");
            string inputHeight = Microsoft.VisualBasic.Interaction.InputBox("Enter Map Height", "Change Map Dimensions");
            int width = CurrentMap.Width;
            int height = CurrentMap.Height;
            bool invalid = false;

            if (inputWidth != null && inputWidth != "")
            {
                if (int.TryParse(inputWidth, out width))
                {
                    if (width < 1)
                        invalid = true;
                }
                else
                {
                    invalid = true;
                }
            }
            if (inputHeight != null & inputHeight != "")
            {
                if (int.TryParse(inputHeight, out height))
                {
                    if (height < 1)
                        invalid = true;
                }
                else
                {
                    invalid = true;
                }
            }
            if (invalid)
                Forms.MessageBox.Show("Invalid width/height!");
            else
                CurrentMap.ChangeDimensions(width, height);
        }

        private void OpenMap()
        {
            using (Forms.OpenFileDialog fd = new Forms.OpenFileDialog())
            {
                fd.Title = "Open a Map...";
                fd.Filter = FILE_TYPE_FILTER;
                if (fd.ShowDialog() == Forms.DialogResult.OK)
                {
                    try
                    {
                        string file = fd.FileName;
                        CurrentMap = FileReadWriter.ReadFromBinary<Map>(file);
                        DefaultValues();
                    }
                    catch (Exception ex)
                    {
                        Forms.MessageBox.Show("Could not read file: " + ex.Message);
                    }
                }
            }
        }

        private void SaveMap()
        {
            using (Forms.SaveFileDialog fd = new Forms.SaveFileDialog())
            {
                fd.Title = "Save a Map...";
                fd.Filter = FILE_TYPE_FILTER;
                if (fd.ShowDialog() == Forms.DialogResult.OK)
                {
                    string file = fd.FileName;
                    FileReadWriter.WriteToBinary(CurrentMap, file);
                }

            }
        }

        private void NewMap()
        {
            Forms.DialogResult result = Forms.MessageBox.Show("Make a new map? Unsaved changes will be lost!", "New Map", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == Forms.DialogResult.Yes)
            {
                CreateNewMap();
                DefaultValues();
            }
        }

        //---------------------------------------------
        //private void Undo()
        //{
        //    //_undoManager.Undo();
        //}

        private void ParseKeys()
        {
            KeyboardState state = Keyboard.GetState();
            // Control is Down
            if (state.IsKeyDown(Keys.LeftControl))
            {
                if (state.IsKeyDown(Keys.T))
                {
                    SwitchTextures();
                }

                if (state.IsKeyDown(Keys.A))
                {
                    ChangeMapDimensions();
                }

                if (state.IsKeyDown(Keys.O))
                {
                    OpenMap();
                }

                if (state.IsKeyDown(Keys.S))
                {
                    SaveMap();
                }

                if (state.IsKeyDown(Keys.N))
                {
                    NewMap();
                }

                //---------| UNDO |
                //if (state.IsKeyDown(Keys.Z))
                //{
                //}

            }
            else
            {
                float speed = (_mapMovementReverse ? -1 : 1) * _speed;
                if (state.IsKeyDown(Keys.A))
                {
                    _mapPosition.X -= speed;
                    _selectionPos.X -= (int) speed;
                }
                if (state.IsKeyDown(Keys.D))
                {
                    _mapPosition.X += speed;
                    _selectionPos.X += (int) speed;
                }
                if (state.IsKeyDown(Keys.W))
                {
                    _mapPosition.Y -= speed;
                    _selectionPos.Y -= (int) speed;
                }
                if (state.IsKeyDown(Keys.S))
                {
                    _mapPosition.Y += speed;
                    _selectionPos.Y += (int) speed;
                }
                if (state.IsKeyDown(Keys.OemTilde))
                {
                    EditorDraw.PlaceTile(_tileSelectionID, _selectionX, _selectionY, ShowCollision);
                }
                if (state.IsKeyDown(Keys.R))
                {
                    _zoom = 1f;
                    _mapPosition = Vector2.Zero;
                }
                if (KeyboardInput.CheckIsPressed(Keys.P))
                {
                    ShowCollision = !ShowCollision;
                }
                if (KeyboardInput.CheckIsPressed(Keys.O))
                {
                    ShowTriggers = !ShowTriggers;
                }

                if (KeyboardInput.CheckIsPressed(Keys.Q))
                {
                    if (_tileSelectionID > 0)
                    {
                        _tileSelectionID--;
                        CurrentTileSelectionID--;
                    }
                }
                if (KeyboardInput.CheckIsPressed(Keys.E))
                {
                    if (_tileSelectionID < CurrentMap.TextureMap.GetTilesPerRow() - 1)
                    {
                        _tileSelectionID++;
                        CurrentTileSelectionID++;
                    }
                }
            }

            
        }

        private void ToggleFillBucket()
        {
            _toolsFillBucketTextureSelected = !_toolsFillBucketTextureSelected;
        }

        //-------------------------------------------------------------------------
        private void ToggleFillRectangle()
        {
            _toolsFillRectangleTextureSelected = !_toolsFillRectangleTextureSelected;
        }
        //-------------------------------------------------------------------------


        public void ParseMouse()
        {
            _mousePos = MouseInput.LastPos;
            bool inToolbar = (_mousePos.Y > 0 && _mousePos.Y < 100) && (_mousePos.X > 0 && _mousePos.Y < Game1.Width);

            if (MouseInput.CheckLeftPressed())
            {
                if (_mousePos.X > 0 && _mousePos.X < Game1.Width)
                {
                    if (inToolbar)
                    {
                        for (int i = 0; i < CurrentMap.TextureMap.GetTilesPerRow(); i++)
                        {
                            Vector2 pos = new Vector2(((i * 32) * _toolBarZoom), 20);
                            pos.X += (Game1.Width - _toolbarPos.X) - Game1.Width;
                            Vector2 bottomRight = new Vector2(pos.X + (CurrentMap.TextureMap.TileWidth * _toolBarZoom), pos.Y + (CurrentMap.TextureMap.TileHeight * _toolBarZoom));
                            if (_mousePos.X > pos.X && _mousePos.X < bottomRight.X && _mousePos.Y > pos.Y && _mousePos.Y < bottomRight.Y)
                            {
                                _tileSelectionID = i;
                                CurrentTileSelectionID = _tileSelectionID + (CurrentMap.TextureMap.GetTilesPerRow() * CurrentEditorRow);
                            }
                        }
                    }
                }

                if (_mousePos.Y > 100 && _mousePos.Y < Game1.Height)
                {
                    if (_toolsFillRectangleTextureSelected) //------------------------- 
                    {
                        
                        if (EditorDraw.FirstRectFillTileSelected)
                        {
                            EditorDraw.ExecuteRectangleFill(CurrentTileSelectionID, _selectionX, _selectionY);
                            EditorDraw.ClearToolsRectSelection();
                        }
                        else
                        {
                            EditorDraw.SelectTile(_selectionX, _selectionY);
                        }

                    }

                }


            }
            else if (MouseInput.CheckLeftDown())
            {
                if (_isToolbarScrolling)
                {
                    _toolbarScrollPos.X = MouseInput.LastPos.X - (_toolbarScrollWidth / 2);

                    if (_toolbarScrollPos.X < 0)
                        _toolbarScrollPos.X = 0;
                    else if (_toolbarScrollPos.X > Game1.Width - _toolbarScrollWidth)
                    {
                        _toolbarScrollPos.X = Game1.Width - (_toolbarScrollWidth);
                    }


                    float posRatio = _toolbarScrollPos.X / Game1.Width;
                    _toolbarPos.X = posRatio * Game1.Width * _toolBarZoom * 1.6f;
                }
                else if (inToolbar)
                {
                    if (_mousePos.Y < 16)
                    {
                        _isToolbarScrolling = true;

                    }
                }
                else if (_mousePos.Y > 100 && _mousePos.Y < Game1.Height)
                {
                    if (_toolsFillBucketTextureSelected)
                    {
                        EditorDraw.FillTile(CurrentTileSelectionID, _selectionX, _selectionY, ShowCollision);
                    }
                    else if (!_toolsFillRectangleTextureSelected)
                    {
                        EditorDraw.PlaceTile(CurrentTileSelectionID, _selectionX, _selectionY, ShowCollision);
                    }

                }


            }
            else if (MouseInput.CheckRightDown())
            {
                if (_mousePos.Y > 100 && _mousePos.Y < Game1.Height)
                {
                    EditorDraw.PlaceTile(0, _selectionX, _selectionY);
                }
            }
            else
            {
                if (_isToolbarScrolling)
                {
                    _isToolbarScrolling = false;
                }
            }

            int currentScroll = MouseInput.ScrollValue;
            int scrollDif = currentScroll - _lastScroll;

            if (inToolbar)
            {
                if (scrollDif < 0)
                {
                    if (CurrentEditorRow < CurrentMap.TextureMap.GetTilesPerColumn() - 1)
                    {
                        CurrentTileSelectionID += CurrentMap.TextureMap.GetTilesPerRow();
                        CurrentEditorRow++;
                    }
                }
                else if (scrollDif > 0)
                {
                    if (CurrentEditorRow > 0)
                    {
                        CurrentEditorRow--;
                        CurrentTileSelectionID -= CurrentMap.TextureMap.GetTilesPerRow();
                    }
                }
            }
            else
            {

                if (scrollDif < 0)
                {
                    _zoom -= 0.1f;
                    if (_zoom <= 0f)
                        _zoom = 0.1f;
                }
                else if (scrollDif > 0)
                    _zoom += 0.1f;
            }


            _lastScroll = currentScroll;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            DrawMap(spriteBatch);
            spriteBatch.Draw(_selectionRect, _selectionPos.ToVector2(), null, Color.White, 0f, Vector2.Zero, _zoom, SpriteEffects.None, 0f);
            DrawBrushes(spriteBatch);
        }

        private void DrawMap(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < CurrentMap.Width; x++)
            {
                for (int y = 0; y < CurrentMap.Height; y++)
                {
                    int textureTileWidth = CurrentMap.TextureMap.TileWidth;
                    int textureTileHeight = CurrentMap.TextureMap.TileHeight;
                    Vector2 position = new Vector2(x * textureTileWidth * _zoom, y * textureTileHeight * _zoom) + _mapPosition;

                    if ((_mousePos.X > position.X && _mousePos.X < position.X + textureTileWidth * _zoom) &&
                        _mousePos.Y > position.Y && _mousePos.Y < position.Y + textureTileHeight * _zoom)
                    {
                        _selectionX = x;
                        _selectionY = y;
                        // _selectionTilePos = new Point(x, y);
                        _selectionPos = position.ToPoint();
                        SelectionTileID = CurrentMap.Tiles[x, y].ID;
                    }

                    // if outside screen don't draw
                    if ((position.X + textureTileWidth * _zoom > 0 && position.X < Game1.Width) &&
                       (position.Y + textureTileHeight * _zoom > 100 && position.Y < Game1.Height))
                    {
                        spriteBatch.Draw(_mapTexture, position, CurrentMap.GetTileOnSheet(CurrentMap.Tiles[x, y].ID), Color.White, 0f, Vector2.Zero, _zoom, SpriteEffects.None, 0f);
                        if (ShowCollision)
                        {
                            if (CurrentMap.Tiles[x, y].Collidable)
                                spriteBatch.Draw(_collisionTexture, position, null, new Color(206, 39, 167) * 0.6f, 0f, Vector2.Zero, _zoom, SpriteEffects.None, 0f);
                        }
                        if (ShowTriggers)
                        {
                            if (CurrentMap.Tiles[x, y].Collidable)
                                spriteBatch.Draw(_collisionTexture, position, null, new Color(211, 107, 107) * 0.6f, 0f, Vector2.Zero, _zoom, SpriteEffects.None, 0f);
                        }
                    }
                }
            }
        }

        private void DrawBrushes(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_toolBarTexture, Vector2.Zero, new Color(255, 255, 255, 1.0f));
            spriteBatch.Draw(_toolbarScroll, _toolbarScrollPos, new Color(100, 100, 100));

            for (int i = 0; i < CurrentMap.TextureMap.GetTilesPerRow(); i++)
            {
                Vector2 pos = new Vector2(((i * 32) * _toolBarZoom), 20);
                pos.X += (Game1.Width - _toolbarPos.X) - Game1.Width;
                Vector2 bottomRight = new Vector2(pos.X + (CurrentMap.TextureMap.TileWidth * _toolBarZoom), pos.Y + (CurrentMap.TextureMap.TileHeight * _toolBarZoom));

                spriteBatch.Draw(_mapTexture, pos, CurrentMap.GetTileOnSheet(i + (CurrentMap.TextureMap.GetTilesPerRow() * CurrentEditorRow)), Color.White, 0f, Vector2.Zero, _toolBarZoom, SpriteEffects.None, 0f);

                if (_mousePos.Y > 0 && _mousePos.Y < 100)
                {
                    if (_mousePos.X >= pos.X && _mousePos.X < bottomRight.X && _mousePos.Y >= pos.Y && _mousePos.Y < bottomRight.Y)
                    {
                        spriteBatch.Draw(_selectionRect, pos, null, new Color(200, 100, 100) * 0.8f, 0f, Vector2.Zero, _toolBarZoom, SpriteEffects.None, 0f);
                    }
                }
                if (i == _tileSelectionID)
                {
                    spriteBatch.Draw(_selectionRect, pos, null, Color.White, 0f, Vector2.Zero, _toolBarZoom, SpriteEffects.None, 0f);
                }

            }

            // spriteBatch.Draw(_toolsFillBucketTexture, _toolsFillBucketPos, Color.White);
            _buttonToolsFillBucket.Draw(spriteBatch);
            _buttonToolsFillRectangle.Draw(spriteBatch);
            if (_toolsFillBucketTextureSelected)
                spriteBatch.Draw(_selectionRect, _toolsFillBucketPos, Color.White);
            if (_toolsFillRectangleTextureSelected)
                spriteBatch.Draw(_selectionRect, _toolsFillRectanglePos, Color.White);

        }

    }
}
