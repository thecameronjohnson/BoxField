using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BoxField
{
    class Box
    {
        //need colour eventually (done)
        public int x, y, size;
        public Color colour;

        public Box (int _x, int _y, int _size, Color _colour)
        {
            x = _x;
            y = _y;
            size = _size;
            colour = _colour;
        }

        //need a move method
        public void Move(int speed)
        {
            y += speed;
        }

        public void Move(int speed, string direction)
        {
            if (direction == "right")
            {
                x += speed;
            }
            else if (direction == "left")
            {
                x -= speed;
            }
        }

        public bool Collision(Box b)
        {
            Rectangle rec1 = new Rectangle(b.x, b.y, b.size, b.size);
            Rectangle rec2 = new Rectangle(x, y, size, size);

            if (rec1.IntersectsWith(rec2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
