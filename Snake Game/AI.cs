using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Snake_Game
{
    class AI
    {
        private Game game;
        

        public AI(Game game)
        {
            this.game = game;
        }

        public void Move()
        {
            double distance=Distance(game.snake.Body[0],game.food);
            double tailDistace = Distance(game.snake.Body[0], game.snake.Body[game.snake.Body.Count-1]);
            Dictionary<Direction, bool> findTails = new Dictionary<Direction, bool>();
            for (int i = 0; i < 4; i++)
            {
                bool findTail = FindTail((Direction)i);
                findTails.Add((Direction)i, findTail);
            }
            for (int i = 0; i < 4; i++)//能缩短距离也能找到尾巴远离尾巴
			{
                Point target=Game.DirectionLocation(game.snake.Body[0], (Direction)i, game.Length);
                double distance2 = Distance(target , game.food);
                double tailDistance2 = Distance(target , game.snake.Body[game.snake.Body.Count - 1]);
                if (game.Safe((Direction)i)&& findTails[(Direction)i] && distance2 < distance&&tailDistance2>tailDistace)
                {
                    game.Move((Direction)i);
                    return;
                }
                
			}
            for (int i = 0; i < 4; i++)//能缩短距离也能找到尾巴靠近尾巴
            {
                Point target = Game.DirectionLocation(game.snake.Body[0], (Direction)i, game.Length);
                double distance2 = Distance(target, game.food);
                if (game.Safe((Direction)i) && findTails[(Direction)i] && distance2 < distance)
                {
                    game.Move((Direction)i);
                    return;
                }

            }
            for (int i = 0; i < 4; i++)//不能缩短距离但是能找到尾巴远离尾巴
            {
                Point target=Game.DirectionLocation(game.snake.Body[0], (Direction)i, game.Length);
                double distance2 = Distance(target, game.food);
                double tailDistance2 = Distance(target, game.snake.Body[game.snake.Body.Count - 1]);
                if (game.Safe((Direction)i) && findTails[(Direction)i] && tailDistance2 > tailDistace)
                {
                    game.Move((Direction)i);
                    return;
                }
            }
            for (int i = 0; i < 4; i++)//不能缩短距离但是能找到尾巴靠近尾巴
            {
                Point target = Game.DirectionLocation(game.snake.Body[0], (Direction)i, game.Length);
                double tailDistance2 = Distance(target, game.snake.Body[game.snake.Body.Count - 1]);
                if (game.Safe((Direction)i) && findTails[(Direction)i])
                {
                    game.Move((Direction)i);
                    return;
                }
            }
            for (int i = 0; i < 4; i++)//安全
                if (game.Safe((Direction)i))
                {
                    game.Move((Direction)i);
                    return;
                }
            game.Move(game.currentDirection);
        }

        public static double Distance(Point p1,Point p2)
        {
            return Math.Pow(Math.Pow(p1.X-p2.X,2)+Math.Pow(p1.Y-p2.Y,2),0.5);
        }

        private bool FindTail(Direction direction)//看从目标方向能否找到尾巴
        {
            
            //target 是蛇头向目标方向移动的第一个点
            Point target = Game.DirectionLocation(game.snake.Body[0], direction, game.Length);
            //检查过的点
            List<Point> Checked = new List<Point>();
            //待检查的点
            Queue<Point> ToChecked = new Queue<Point>();
            ToChecked.Enqueue(target);
            while(ToChecked.Count>0)//队列中还有点时
            {
                target = ToChecked.Dequeue();//出队
                Checked.Add(target);
                if (target == game.snake.Body[game.snake.Body.Count - 1])//如果检查点就是蛇尾，返回true
                    return true;
                for (int i = 0; i < 4; i++)//否则，检查四个方向，如果有没检查过的安全点，则加入队列
                    if (Game.Safe(target, (Direction)i, game.Rect, game.Length, game.snake.Body))
                    {
                        Point newTarget = Game.DirectionLocation(target, (Direction)i, game.Length);
                        if(!Checked.Contains(newTarget)&&!ToChecked.Contains(newTarget))
                            ToChecked.Enqueue(newTarget);
                    }
            }
            return false;
        }
    }
}
