using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_Programmer._224_DetectingFourSidedFigures
{
    public class DetectingFourSidedFigures
    {
        private char[][] grid;

        public int Count(string input)
        {
            grid = input.Split('\n')
                .Where(row => row.Contains('-') || row.Contains('|') || row.Contains('+'))
                .Select(row => row.ToCharArray())
                .ToArray();

            var corners = new List<Point> { };
            for (int i = 0; i < grid.Length; i++)
            {
                var row = grid[i];
                for (int j = 0; j < row.Length; j++)
                {
                    if (row[j] == '+') corners.Add(new Point(j, i));
                }
            }

            return corners.Sum(corner => GetRects(
                new Point(corner.X + 1, corner.Y),
                Direction.Right,
                new Point(corner.X, corner.Y)));
        }

        private int GetRects(Point point, Direction direction, Point origin)
        {
            var right = new Point(point.X + 1, point.Y);
            var down = new Point(point.X, point.Y + 1);
            var left = new Point(point.X - 1, point.Y);
            var up = new Point(point.X, point.Y - 1);

            if (!point.IsWithinBounds(grid)) return 0;
            if (point.X < origin.X || point.Y < origin.Y) return 0;
            if (GetCharAt(point) == '+' && point.Equals(origin)) return 1;
            if (direction == Direction.Right)
            {
                if (GetCharAt(point) == '+') return
                    GetRects(right, Direction.Right, origin) +
                    GetRects(down, Direction.Down, origin);
                if (GetCharAt(point) == '-') return
                    GetRects(right, Direction.Right, origin);
                return 0;
            }
            else if (direction == Direction.Down)
            {
                if (GetCharAt(point) == '+') return
                    GetRects(down, Direction.Down, origin) +
                    GetRects(left, Direction.Left, origin);
                if (GetCharAt(point) == '|') return
                    GetRects(down, Direction.Down, origin);
                return 0;
            }
            else if (direction == Direction.Left)
            {
                if (GetCharAt(point) == '+') return
                    GetRects(left, Direction.Left, origin) +
                    GetRects(up, Direction.Up, origin);
                if (GetCharAt(point) == '-') return
                    GetRects(left, Direction.Left, origin);
                return 0;
            }
            else
            {
                if (GetCharAt(point) == '|' || GetCharAt(point) == '+') return
                    GetRects(up, Direction.Up, origin);
                return 0;
            }
        }

        private char GetCharAt(Point point)
        {
            return grid[point.Y][point.X];
        }
    }

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsWithinBounds (char[][] grid)
        {
            return (Y > -1 && Y < grid.Count())
                && (X > -1 && X < grid.ElementAt(Y).Count());
        }

        public bool Equals(Point point)
        {
            return point.X == X && point.Y == Y;
        }
    }

    enum Direction
    {
        Right,
        Down,
        Left,
        Up
    }
}
