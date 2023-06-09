﻿using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.UI
{
    class Draggable : Button
    {
        private float width, height;

        public Draggable(float width, float height)
        {
            this.width = width;
            this.height = height;

            Program.GetWindow().MouseMove += Move;
        }

        private void Move(MouseMoveEventArgs e)
        {
            Vector2 delta = e.Delta;
            if (buttonPressed)
            {
                parent.transform.position += new Vector3(delta.X * 2 / Program.GetWindow().Size.X, 
                    -2 * delta.Y / Program.GetWindow().Size.Y, 0);

                if (parent.transform.position.X < 0)
                    parent.transform.position.X = 0;

                if (parent.transform.position.X + width > Program.GetWindow().Size.X)
                    parent.transform.position.X = Program.GetWindow().Size.X - width;

                if (parent.transform.position.Y + parent.transform.scale.Y - height < 0)
                    parent.transform.position.Y = height - parent.transform.scale.Y;

                if (parent.transform.position.Y + parent.transform.scale.Y > Program.GetWindow().Size.Y)
                    parent.transform.position.Y = Program.GetWindow().Size.Y - parent.transform.scale.Y;
            }
        }
    }
}
