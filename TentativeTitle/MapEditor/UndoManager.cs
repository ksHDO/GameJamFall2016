using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{

    public struct UndoInfo
    {

        public int[,] _tiles;
       
        public UndoInfo(int x, int y)
        {
            _tiles = new int[x, y];
        }


    }

    class UndoManager
    {
       
        int _width = 0;
        int _height = 0;
        //int[,,] _previousStates;
        List<UndoInfo> _undoStates;
        List<UndoInfo> _redoStates;

        public UndoManager(int w, int h)
        {
            _width = w;
            _height = h;
            // _previousStates = new int[MAX_UNDOS, _width, _height];
           // _undoStates.

        }

        public void Clear()
        {
           // _previousStates = new int[MAX_UNDOS, _width, _height];
        }

    }
}
