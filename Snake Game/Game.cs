using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Snake_Game
{
    class Game
    {
        private const int length = 30;
        public int Length { get { return length; } }
        
        private int row;
        private int col;
        public Snake snake { get;private set; }
        public Point food { get;private set; } 
        private Rectangle rect;
        public Rectangle Rect { get { return rect; } }
        public Direction currentDirection { get;private set; }
        public bool GameOver;
        public int Score { get { return snake.Body.Count - 5; } }
        Random random=new Random();

        public Game(Rectangle rect)
        {
            snake = new Snake(new Point(0, 0),Length, Direction.Up);
            currentDirection = Direction.Right;
            GameOver = false;
            this.rect = rect;
            col=rect.Width/Length;
            row=rect.Height/Length;
            food = new Point(random.Next(col) * Length , random.Next(row) * Length );
        }

        public static Point DirectionLocation(Point location, Direction direction, int length)
        {
            int xIncrease = 0;
            int yIncrease = 0;
            switch (direction)
            {
                case Direction.Up:
                    yIncrease = -length;
                    break;
                case Direction.Down:
                    yIncrease = length;
                    break;
                case Direction.Left:
                    xIncrease = -length;
                    break;
                case Direction.Right:
                    xIncrease = length;
                    break;
                default: break;
            }
            int newX = location.X + xIncrease;
            int newY = location.Y + yIncrease;
            return new Point(newX, newY);
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Blue, food.X + 5, food.Y + 5, Length - 10, Length - 10);
            snake.Draw(g);
        }

        public void Move(Direction direction)
        {
            currentDirection=direction;
            if (Safe(direction))
                if(HasFood(direction))
                {
                    List<Point> empty = new List<Point>();
                    for (int i = 0; i < col; i++)
                        for (int j = 0; j < row; j++)
                        {
                            Point here = new Point(i * Length, j * Length);
                            bool emptyHere = true;
                            foreach (Point body in snake.Body)
                                if (here == body)
                                {
                                    emptyHere = false;
                                    break;
                                }
                            if (emptyHere)
                                empty.Add(here);
                        }
                    food = empty[random.Next(empty.Count)];
                    snake.Move(currentDirection, true);
                }
                else
                    snake.Move(direction,false);
            else
                GameOver=true;
        }

        public void Move()
        {
            Move(currentDirection);
        }

        public bool Safe(Direction direction)
        {
            return Safe(snake.Body[0], direction, rect, length, snake.Body);
        }

        public static bool Safe(Point location, Direction direction, Rectangle rect, int length, List<Point> snakeBody)
        {
            Point target = DirectionLocation(location, direction, length);
            for (int i = 0; i < snakeBody.Count - 1; i++)
                if (target == snakeBody[i])
                    return false;

            if (target.X < 0 || target.Y < 0 || target.Y > rect.Height - length || target.X > rect.Width - length)
                return false;
            return true;
        }

        private bool HasFood(Direction direction)
        {
            Point target = DirectionLocation(snake.Body[0], direction, Length);
            if (food == target)
                return true;
            return false;
        }

        
    }
}
