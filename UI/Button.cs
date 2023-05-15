using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace MathGL.UI
{
    class Button : UIComponent
    {
        public Button()
        {
            Program.GetWindow().MouseDown += Pressed;
            Program.GetWindow().MouseUp += Released;
        }

        protected bool buttonPressed = false;
        private void Pressed(MouseButtonEventArgs e)
        {
            if(e.Button == MouseButton.Button1)
            {
                if (parent.MouseHovering())
                {
                    buttonPressed = true;
                }
            }
        }

        private void Released(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Button1)
            {
                buttonPressed = false;
            }
        }
    }
}
