using MathGL.Renderables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.UI
{
    class UIComponent
    {
        protected UIObject parent;

        public void SetComponentParent(UIObject parent)
        {
            this.parent = parent;
        }
    }
}
