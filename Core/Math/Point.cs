using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIVES
{
    [Serializable]
    public class Point
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public Point() { }
        public Point(int x, int y) : this()
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return "Point(" + x + "," + y + ")";
        }
    }
}
