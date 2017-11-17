using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Snake_Game
{
    class Snake
    {
        private int bodyLength;

        public List<Point> Body { get;private set; }
        public Snake(Point location,int length,Direction direction)
        {
            this.bodyLength = length;
            Body = new List<Point>();

            int xIncrease=0;
            int yIncrease=0;
            switch (direction)
            {
                case Direction.Up:
                    yIncrease = bodyLength;
                    break;
                case Direction.Down:
                    yIncrease = -bodyLength;
                    break;
                case Direction.Left:
                    xIncrease = bodyLength;
                    break;
                case Direction.Right:
                    xIncrease = -bodyLength;
                    break;
                default: break;
            }
            for (int i = 0; i < 5; i++)//增加5节身体
            {
                Body.Add(new Point(location.X + i*xIncrease, location.Y + i*yIncrease));
            }
        }

        

        public void Move(Direction direction,bool hasFood)
        {
            if (!hasFood)
            {
                for (int i = Body.Count - 1; i > 0; i--)
                {
                    Body[i] = Body[i - 1];
                }

                Body[0] = Game.DirectionLocation(Body[0], direction, bodyLength);
            }
            else
            {
                Body.Insert(0, Game.DirectionLocation(Body[0], direction, bodyLength));
            }
        }


        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Green,Body[0].X+2, Body[0].Y+2, bodyLength-4, bodyLength-4);
            for (int i = 1; i < Body.Count; i++)
            {
                g.FillRectangle(Brushes.Yellow, Body[i].X+2, Body[i].Y+2, bodyLength-4, bodyLength-4);
            }
        }
    }
}
