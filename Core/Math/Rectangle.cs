using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIVES
{
    [Serializable]
    public class Rectangle
    {
        public int width { get; private set; }
        public int height { get; private set; }

        public Rectangle() { }
        public Rectangle(int width, int height) : this()
        {
            this.width = width;
            this.height = height;
        }
        
        // TODO: check if calculation of height is correct (used cartesian quadrant IV)
        public Rectangle(Point upperLeft, Point lowerRight)
        {
            width = lowerRight.x - upperLeft.x;
            height = lowerRight.y - upperLeft.y;
        }

        public override string ToString()
        {
            return "rectangle(" + width + " x " + height + ")";
        }
    }
}
