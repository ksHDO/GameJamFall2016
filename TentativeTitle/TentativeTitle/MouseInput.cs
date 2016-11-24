using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TentativeTitle
{
    class MouseInput
    {
        public static Vector2 LastPos { get; private set; }
        public static Vector2 DPos { get; private set; }
        public static int ScrollValue { get; private set; }
        private static bool leftWasDown;
        private static bool leftIsDown;
        private static bool rightWasDown;
        private static bool rightIsDown;

        public static void Update()
        {
            MouseState mouseState = Mouse.GetState();
            UpdatePress(mouseState);
            UpdatePosition(mouseState);
            UpdateScroll(mouseState);
        }

        public static bool CheckLeftDown()
        {
            return leftIsDown;
        }
        public static bool CheckRightDown()
        {
            return rightIsDown;
        }

        public static bool CheckLeftPressed()
        {
            return leftIsDown && !leftWasDown;
        }
        public static bool CheckLeftReleased()
        {
            return !leftIsDown && leftWasDown;
        }

        public static bool CheckRightPressed()
        {
            return rightIsDown && !rightWasDown;
        }
        public static bool CheckRightReleased()
        {
            return !rightIsDown && rightWasDown;
        }

        private static void UpdatePosition(MouseState mouseState)
        {
            Vector2 curPos = mouseState.Position.ToVector2();
            DPos = curPos - LastPos;
            LastPos = curPos;
        }
        private static void UpdatePress(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!leftWasDown && leftIsDown)
                    leftWasDown = true;
                leftIsDown = true;
            }
            else if (mouseState.LeftButton == ButtonState.Released)
            {
                if (leftWasDown)
                    leftWasDown = false;
                leftIsDown = false;
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                if (!rightWasDown && rightIsDown)
                    rightWasDown = true;
                rightIsDown = true;

            }
            else if (mouseState.RightButton == ButtonState.Released)
            {
                if (rightWasDown)
                    rightWasDown = false;
                rightIsDown = false;

            }
        }

        private static void UpdateScroll(MouseState mouseState)
        {
            ScrollValue = mouseState.ScrollWheelValue;
        }
    }
}
