using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map2 = TentativeTitle.Maps;


namespace MapEditor
{

    public struct EditorDrawTile
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public EditorDrawTile(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }
    }

    class EditorDraw
    {
        public static Map2.Map CurrentMap { get; set; }


        //-------------------- Stored tile
        private static EditorDrawTile? _firstRectangleSelection = null;
        public static bool FirstRectFillTileSelected { get; private set; } = false;
        //--------------------

        
        public static void ClearToolsRectSelection()
        {
            _firstRectangleSelection = null;
            FirstRectFillTileSelected = false;
        }
        

        // public void PlaceTile(int xID, int yID, bool collidable = false)
        // {
        //     CurrentMap.Tiles[_selectionX, _selectionY].X = xID;
        //     CurrentMap.Tiles[_selectionX, _selectionY].Y = yID;
        //     CurrentMap.Tiles[_selectionX, _selectionY].Collidable = collidable;
        // }
        // 
        // public void PlaceTile(int xID, bool collidable = false)
        // {
        //     CurrentMap.Tiles[_selectionX, _selectionY].X = xID;
        //     CurrentMap.Tiles[_selectionX, _selectionY].Collidable = collidable;
        // }

        public static void SelectTile(int x, int y)
        {
            ClearToolsRectSelection();
            _firstRectangleSelection = new EditorDrawTile(x, y);
            FirstRectFillTileSelected = true;
        }

        public static void ExecuteRectangleFill(int xID, int x, int y)
        {
            if (_firstRectangleSelection != null)
            {
                int fromX;
                int toX;
                int fromY;
                int toY;

                if (_firstRectangleSelection.Value.X <= x)
                {
                    fromX = _firstRectangleSelection.Value.X;
                    toX = x;
                }
                else
                {
                    fromX = x;
                    toX = _firstRectangleSelection.Value.X;
                }

                if (_firstRectangleSelection.Value.Y <= y)
                {
                    fromY = _firstRectangleSelection.Value.Y;
                    toY = y;
                }
                else
                {
                    fromY = y;
                    toY = _firstRectangleSelection.Value.Y;
                }

                for (int xt = fromX; xt <= toX; xt++)
                {
                    for (int yt = fromY; yt <= toY; yt++)
                    {
                        PlaceTile(xID, xt, yt);
                    }
                }
            }
            ClearToolsRectSelection();
        }

        public static void PlaceTile(int xID, int selectX, int selectY, bool collidable = false)
        {
            CurrentMap.Tiles[selectX, selectY].ID = xID;
            CurrentMap.Tiles[selectX, selectY].Collidable = collidable;
        }

        public static void PlaceTile(int xID, Point selectPos, bool collidable = false)
        {
            CurrentMap.Tiles[selectPos.X, selectPos.Y].ID = xID;
            CurrentMap.Tiles[selectPos.X, selectPos.Y].Collidable = collidable;
        }

        public static void FillTile(int xID, Point selectPos, bool collidable = false)
        {
            FillTileOperation(xID, CurrentMap.Tiles[selectPos.Y, selectPos.Y].ID, selectPos, collidable);
        }

        public static void FillTile(int xID, int selectionX, int selectionY, bool collidable = false)
        {
            FillTileOperation(xID, CurrentMap.Tiles[selectionX, selectionY].ID, selectionX, selectionY, collidable);
        }

        private static void FillTileOperation(int xID, int replaceID, Point selectPos, bool collidable)
        {
            Queue<Point> q = new Queue<Point>();
            if (CurrentMap.Tiles[selectPos.X, selectPos.Y].ID == replaceID)
            {
                // Point point = new Point(selectPos.X, selectPos.Y);
                q.Enqueue(selectPos);

                // foreach (Point N in q)
                while (q.Count > 0)
                {
                    Point N = q.Dequeue();
                    Point w = N;
                    Point e = N;

                    while (w.X - 1 >= 0)
                    {
                        if (CurrentMap.Tiles[w.X - 1, w.Y].ID == replaceID)
                            w.X += -1;
                        else
                            break;
                    }
                    while (e.X + 1 < CurrentMap.Width)
                    {
                        if (CurrentMap.Tiles[e.X + 1, w.Y].ID == replaceID)
                            e.X += 1;
                        else
                            break;
                    }

                    for (Point n = w; n.X <= e.X; n.X++)
                    {
                        PlaceTile(xID, n.X, n.Y, collidable);
                        if (n.Y - 1 >= 0)
                        {
                            if (CurrentMap.Tiles[n.X, n.Y - 1].ID != xID && CurrentMap.Tiles[n.X, n.Y - 1].ID == replaceID)
                            {
                                Point north = new Point(n.X, n.Y - 1);
                                q.Enqueue(north);
                            }
                        }
                        if (n.Y + 1 < CurrentMap.Height)
                        {
                            if (CurrentMap.Tiles[n.X, n.Y + 1].ID != xID && CurrentMap.Tiles[n.X, n.Y + 1].ID == replaceID)
                            {
                                Point south = new Point(n.X, n.Y + 1);
                                q.Enqueue(south);
                            }
                        }

                    }
                }
            }
        }

        private static void FillTileOperation(int xID, int replaceID, int selectX, int selectY, bool collidable)
        {
            Queue<Point> q = new Queue<Point>();
            if (CurrentMap.Tiles[selectX, selectY].ID == replaceID)
            {
                Point point = new Point(selectX, selectY);
                q.Enqueue(point);

                // foreach (Point N in q)
                while (q.Count > 0)
                {
                    Point N = q.Dequeue();
                    Point w = N;
                    Point e = N;

                    while (w.X - 1 >= 0)
                    {
                        if (CurrentMap.Tiles[w.X - 1, w.Y].ID == replaceID)
                            w.X += -1;
                        else
                            break;
                    }
                    while (e.X + 1 < CurrentMap.Width)
                    {
                        if (CurrentMap.Tiles[e.X + 1, w.Y].ID == replaceID)
                            e.X += 1;
                        else
                            break;
                    }

                    for (Point n = w; n.X <= e.X; n.X++)
                    {
                        PlaceTile(xID, n.X, n.Y, collidable);
                        if (n.Y - 1 >= 0)
                        {
                            if (CurrentMap.Tiles[n.X, n.Y - 1].ID != xID && CurrentMap.Tiles[n.X, n.Y - 1].ID == replaceID)
                            {
                                Point north = new Point(n.X, n.Y - 1);
                                q.Enqueue(north);
                            }
                        }
                        if (n.Y + 1 < CurrentMap.Height)
                        {
                            if (CurrentMap.Tiles[n.X, n.Y + 1].ID != xID && CurrentMap.Tiles[n.X, n.Y + 1].ID == replaceID)
                            {
                                Point south = new Point(n.X, n.Y + 1);
                                q.Enqueue(south);
                            }
                        }

                    }
                }
            }
        }

        public static bool CheckTile(int checkID, int X, int Y)
        {
            return CurrentMap.Tiles[X, Y].ID == checkID;
        }

        public static bool CheckPlaceTile(int checkID, int X, int Y, bool collidable)
        {
            bool output = false;
            if (X >= 0 && X < CurrentMap.Width && Y >= 0 && Y < CurrentMap.Height)
            {
                if (CurrentMap.Tiles[X, Y].ID == checkID)
                {
                    CurrentMap.Tiles[X, Y].ID = checkID;
                    CurrentMap.Tiles[X, Y].Collidable = collidable;
                    output = true;
                }
            }
            return output;
        }
    }
}
