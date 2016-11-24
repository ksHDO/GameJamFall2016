using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TentativeTitle
{
    class KeyboardInput
    {
        struct KeyKeys
        {
            public Keys Key;
            public bool WasDown;
            public bool IsDown;

            public KeyKeys(Keys key)
            {
                Key = key;
                WasDown = false;
                IsDown = false;
            }
        }

        private const int MAX_SIZE = 128;

        private static KeyKeys[] _keys = new KeyKeys[MAX_SIZE];
        private static int _keySize = 0;

        public static void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();
            UpdateKeys(keyboard);
        }

        public static bool CheckIsKeyDown(Keys key)
        {
            KeyboardState keyboard = Keyboard.GetState();
            return keyboard.IsKeyDown(key);
        }
        public static bool CheckIsPressed(Keys key)
        {
            bool output = false;
            for (int i = 0; i < _keySize; i++)
            {
                if (_keys[i].Key == key)
                {
                    if (_keys[i].IsDown && !_keys[i].WasDown)
                        output = true;
                }

            }
            return output;
        }
        public static bool CheckIsReleased(Keys key)
        {
            bool output = false;
            for (int i = 0; i < _keySize; i++)
            {
                if (_keys[i].Key == key)
                {
                    if (!_keys[i].IsDown && _keys[i].WasDown)
                    {
                        output = true;
                    }
                }
            }
            return output;
        }

        public static void AddKey(Keys key)
        {
            if (!CheckKeyExists(key))
            {
                _keys[_keySize++] = new KeyKeys(key);
            }
        }

        public static void AddKey(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                AddKey(key);
            }
        }

        public static bool CheckKeyExists(Keys key)
        {
            bool output = false;
            for (int i = 0; i < _keySize; i++)
            {
                if (_keys[i].Key == key)
                {
                    output = true;
                }
            }
            return output;
        }

        private static void UpdateKeys(KeyboardState state)
        {
            for (int i = 0; i < _keySize; i++)
            {
                if (state.IsKeyDown(_keys[i].Key))
                {
                    if (!_keys[i].WasDown && _keys[i].IsDown)
                        _keys[i].WasDown = true;
                    _keys[i].IsDown = true;
                }
                else
                {
                    if (_keys[i].WasDown)
                        _keys[i].WasDown = false;
                    _keys[i].IsDown = false;
                }
            }
        }

    }
}
