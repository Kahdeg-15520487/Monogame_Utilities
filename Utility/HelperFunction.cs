using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Utility
{
    public static class HelperFunction
    {
        public static void Make2Int(long ll, out int left, out int right)
        {
            left = (int)(ll & uint.MaxValue);
            right = (int)(ll >> 32);
        }

        public static void MakeLong(int left, int right, out long result)
        {
            result = left;
            result = (result << 32);
            result = result | (uint)right;
        }

        /// <summary>
        /// convert angle from degree to radian. use to ease process of drawing sprite
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static float DegreeToRadian(float angle)
        {
            return (float)(Math.PI * angle / 180.0f);
        }

        /// <summary>
        /// convert angle from radian to degree. use to ease process of drawing sprite
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static float RadianToDegree(float angle)
        {
            return (float)(angle * (180.0f / Math.PI));
        }

        public static Vector2 DirectionVector(Vector2 first, Vector2 second)
        {
            return second - first;
        }

        /// <summary>
        /// calculate the angle of the vector in radians
        /// </summary>
        public static float AngleOfVector(Vector2 vt)
        {
            return (float)Math.Atan2(vt.Y, vt.X);
        }

        /// <summary>
        /// Rotate a vector an angle
        /// </summary>
        /// <param name="vt"></param>
        /// <param name="degrees">angle in radian</param>
        /// <returns></returns>
        public static Vector2 RotateVector(Vector2 vt, float degrees)
        {
            var result = new Vector2()
            {
                X = vt.X * (float)Math.Cos(degrees) - vt.Y * (float)Math.Sin(degrees),
                Y = vt.X * (float)Math.Sin(degrees) + vt.Y * (float)Math.Cos(degrees),
            };
            return result;
        }

        public static bool IsKeyDown(Keys k)
        {
            return CONTENT_MANAGER.CurrentInputState.keyboardState.IsKeyDown(k);
        }

        public static bool IsKeyDown(params Keys[] ks)
        {
            return ks.All(IsKeyDown);
        }

        public static bool IsKeyPress(Keys k)
        {
            return CONTENT_MANAGER.CurrentInputState.keyboardState.IsKeyUp(k) && CONTENT_MANAGER.LastInputState.keyboardState.IsKeyDown(k);
        }

        public static bool IsKeyPress(params Keys[] ks)
        {
            return ks.All(IsKeyPress);
        }

        public static bool IsLeftMousePressed()
        {
            return CONTENT_MANAGER.CurrentInputState.mouseState.LeftButton == ButtonState.Released && CONTENT_MANAGER.LastInputState.mouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool IsRightMousePressed()
        {
            return CONTENT_MANAGER.CurrentInputState.mouseState.RightButton == ButtonState.Released && CONTENT_MANAGER.LastInputState.mouseState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// A -> B -> C
        /// </summary>
        /// <param name="pA"></param>
        /// <param name="pB"></param>
        /// <param name="pC"></param>
        /// <returns></returns>
        public static Direction GetIntersectionDir(Point pA, Point pB, Point pC)
        {
            var indir = pB.GetDirectionFromPointAtoPointB(pA);
            var outdir = pB.GetDirectionFromPointAtoPointB(pC);

            Direction result = Direction.Void;

            switch (indir)
            {
                case Direction.North:
                    switch (outdir)
                    {
                        case Direction.West:
                            result = Direction.NorthWest;
                            break;
                        case Direction.East:
                            result = Direction.NorthEast;
                            break;
                        case Direction.South:
                            result = Direction.South;
                            break;
                        default:
                            break;
                    }
                    break;

                case Direction.South:
                    switch (outdir)
                    {
                        case Direction.West:
                            result = Direction.SouthWest;
                            break;
                        case Direction.East:
                            result = Direction.SouthEast;
                            break;
                        case Direction.North:
                            result = Direction.South;
                            break;
                        default:
                            break;
                    }
                    break;

                case Direction.West:
                    switch (outdir)
                    {
                        case Direction.North:
                            result = Direction.NorthWest;
                            break;
                        case Direction.South:
                            result = Direction.SouthWest;
                            break;
                        case Direction.East:
                            result = Direction.East;
                            break;
                        default:
                            break;
                    }
                    break;

                case Direction.East:
                    switch (outdir)
                    {
                        case Direction.North:
                            result = Direction.NorthEast;
                            break;
                        case Direction.South:
                            result = Direction.SouthEast;
                            break;
                        case Direction.West:
                            result = Direction.East;
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }

            return result;
        }

        public static Point OffsetPoint(Rectangle root, Rectangle p, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center, VerticalAlignment verticalAlignment = VerticalAlignment.Center)
        {
            return Point.Zero;
        }

        /// <summary>
        /// for comapring double value
        /// </summary>
        /// <param name="a">the first value to compare</param>
        /// <param name="b">the second value to compare</param>
        /// <param name="epsilon">the minimum different</param>
        /// <returns></returns>
        public static bool NearlyEqual(double a, double b, double epsilon)
        {
            double absA = Math.Abs(a);
            double absB = Math.Abs(b);
            double diff = Math.Abs(a - b);

            if (a == b)
            { // shortcut, handles infinities
                return true;
            }
            else if (a == 0 || b == 0 || diff < Double.Epsilon)
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < epsilon;
            }
            else
            { // use relative error
                return diff / (absA + absB) < epsilon;
            }
        }

        /// <summary>
        /// for comapring float value
        /// </summary>
        /// <param name="a">the first value to compare</param>
        /// <param name="b">the second value to compare</param>
        /// <param name="epsilon">the minimum different</param>
        /// <returns></returns>
        public static bool NearlyEqual(float a, float b, float epsilon)
        {
            float absA = Math.Abs(a);
            float absB = Math.Abs(b);
            float diff = Math.Abs(a - b);

            if (a == b)
            { // shortcut, handles infinities
                return true;
            }
            else if (a == 0 || b == 0 || diff < float.Epsilon)
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < epsilon;
            }
            else
            { // use relative error
                return diff / (absA + absB) < epsilon;
            }
        }

        /// <summary>
        /// for comapring Vector2
        /// </summary>
        /// <param name="a">the first Vector2 to compare</param>
        /// <param name="b">the second Vector2 to compare</param>
        /// <param name="epsilon">the minimum different</param>
        /// <returns></returns>
        public static bool NearlyEqual(Vector2 a, Vector2 b, float epsilon)
        {
            return NearlyEqual(a.X, b.X, epsilon) && NearlyEqual(a.Y, b.Y, epsilon);
        }

        public static T[,] Make2DArray<T>(T[] input, int height, int width)
        {
            T[,] output = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                }
            }
            return output;
        }

        public static T[] Make1DArray<T>(T[,] input)
        {
            int width = input.GetLength(0);
            int height = input.GetLength(1);
            T[] output = new T[height * width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i * width + j] = input[i, j];
                }
            }
            return output;
        }

        public static T[] Make1DArray<T>(T[][] input)
        {
            int height = input.GetLength(0);
            int width = input[0].GetLength(0);
            T[] output = new T[height * width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i * width + j] = input[i][j];
                }
            }
            return output;
        }

        public static T[,] ConvertArrayOfArrayTo2DArray<T>(T[][] input)
        {
            int height = input.GetLength(0);
            int width = input[0].GetLength(0);
            T[,] output = new T[width, height];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i][j];
                }
            }

            return output;
        }
    }

}
