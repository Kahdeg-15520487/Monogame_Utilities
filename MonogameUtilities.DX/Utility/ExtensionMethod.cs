using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Utility
{
    public static class ExtensionMethod
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static int Clamp(this float value, int max, int min)
        {
            int v = (int)value;
            return v >= max ? max : v <= min ? min : v;
        }

        public static int Clamp(this int value, int max, int min)
        {
            return value >= max ? max : value <= min ? min : value;
        }

        public static bool Between(this int value, int max, int min)
        {
            return value < max && value > min;
        }

        public static bool Betweene(this int value, int max, int min)
        {
            return value <= max && value >= min;
        }

        public static T Next<T>(this T src, int count = 1) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + (count < 0 ? 0 : count);
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        public static T Previous<T>(this T src, int count = 1) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) - (count < 0 ? 0 : count);
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        public static Point GetNearbyPoint(this Point p, Direction d)
        {
            Point output = new Point(p.X, p.Y);
            switch (d)
            {
                case Direction.NorthWest:
                    output = new Point(p.X - 1, p.Y - 1);
                    break;
                case Direction.North:
                    output = new Point(p.X, p.Y - 1);
                    break;
                case Direction.NorthEast:
                    output = new Point(p.X + 1, p.Y - 1);
                    break;
                case Direction.West:
                    output = new Point(p.X - 1, p.Y);
                    break;
                case Direction.East:
                    output = new Point(p.X + 1, p.Y);
                    break;
                case Direction.SouthWest:
                    output = new Point(p.X - 1, p.Y + 1);
                    break;
                case Direction.South:
                    output = new Point(p.X, p.Y + 1);
                    break;
                case Direction.SouthEast:
                    output = new Point(p.X + 1, p.Y + 1);
                    break;
                default:
                    break;
            }
            return output;
        }

        public static string toString(this Point p)
        {
            return string.Format("{0}:{1}", p.X, p.Y);
        }

        public static Point Parse(this string str)
        {
            var data = str.Split(':');
            int x, y;

            if (int.TryParse(data[0], out x) && int.TryParse(data[1], out y))
            {
                return new Point(x, y);
            }
            else
            {
                return Point.Zero;
            }
        }

        public static bool TryParse(this string str, out Point p)
        {
            var data = str.Split(':');
            int x, y;
            bool result = false;

            if (int.TryParse(data[0], out x) && int.TryParse(data[1], out y))
            {
                p = new Point(x, y);
                return true;
            }
            else
            {
                p = Point.Zero;
                return result;
            }
        }

        public static Direction GetDirectionFromPointAtoPointB(this Vector2 dir, Vector2 pos)
        {
            return GetDirectionFromPointAtoPointB(pos.ToPoint(), dir.ToPoint());
        }

        public static Direction GetDirectionFromPointAtoPointB(this Point pA, Point pB)
        {
            int deltaX = pA.X - pB.X;
            int deltaY = pA.Y - pB.Y;

            bool isLeft = false;
            bool isRight = false;
            bool isUp = false;
            bool isDown = false;

            if (deltaX > 0)
            {
                isLeft = true;
            }
            else
            {
                if (deltaX < 0)
                {
                    isRight = true;
                }
            }

            if (deltaY > 0)
            {
                isUp = true;
            }
            else
            {
                if (deltaY < 0)
                {
                    isDown = true;
                }
            }

            if (isLeft && isUp)
            {
                return Direction.NorthWest;
            }

            if (isRight && isUp)
            {
                return Direction.NorthEast;
            }

            if (isLeft && isDown)
            {
                return Direction.SouthWest;
            }

            if (isRight && isDown)
            {
                return Direction.SouthEast;
            }

            if (isLeft)
            {
                return Direction.West;
            }

            if (isRight)
            {
                return Direction.East;
            }

            if (isUp)
            {
                return Direction.North;
            }

            if (isDown)
            {
                return Direction.South;
            }

            return Direction.Center;
        }

        public static bool DistanceToOtherLessThan(this Point p, Point other, float MaxDistance)
        {
            return ((p.X - other.X) * (p.X - other.X) + (p.Y - other.Y) * (p.Y - other.Y)) < MaxDistance * MaxDistance;
        }

        public static double DistanceToOther(this Point p, Point other, bool isManhattan = false)
        {
            return isManhattan ? Math.Abs(p.X - other.X) + Math.Abs(p.Y - other.Y) : Math.Sqrt((p.X - other.X) * (p.X - other.X) + (p.Y - other.Y) * (p.Y - other.Y));
        }

        public static Vector2 SwitchAxis(this Vector2 vt)
        {
            return new Vector2(vt.Y, vt.X);
        }

        public static Point SwitchAxis(this Point pt)
        {
            return new Point(pt.Y, pt.X);
        }
    }
}
