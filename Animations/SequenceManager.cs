using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Animations
{
    class SequenceManager
    {
        public static List<Animation> animations = new();

        private static int queueIndex = 0;
        public static void PlayNext()
        {
            animations[queueIndex].Play();
            queueIndex++;
        }
    }
}
