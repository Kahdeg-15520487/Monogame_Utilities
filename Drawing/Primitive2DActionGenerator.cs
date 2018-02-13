﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Drawing {
	public static class Primitive2DActionGenerator {


		#region Private Members

		private static readonly Dictionary<String, List<Vector2>> circleCache = new Dictionary<string, List<Vector2>>();
		//private static readonly Dictionary<String, List<Vector2>> arcCache = new Dictionary<string, List<Vector2>>();
		private static Texture2D pixel;

		#endregion


		#region Private Methods

		public static void CreateThePixel(SpriteBatch spriteBatch) {
			pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });
		}


		/// <summary>
		/// Draws a list of connecting points
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// /// <param name="position">Where to position the points</param>
		/// <param name="points">The points to connect with lines</param>
		/// <param name="color">The color to use</param>
		/// <param name="thickness">The thickness of the lines</param>
		private static Action<SpriteBatch> DrawPoints(Vector2 position, List<Vector2> points, Color color, float thickness) {
			return (sp) => {
				if (points.Count < 2)
					return;

				for (int i = 1; i < points.Count; i++) {
					DrawLine(points[i - 1] + position, points[i] + position, color, thickness)(sp);
				}
			};
		}


		/// <summary>
		/// Creates a list of vectors that represents a circle
		/// </summary>
		/// <param name="radius">The radius of the circle</param>
		/// <param name="sides">The number of sides to generate</param>
		/// <returns>A list of vectors that, if connected, will create a circle</returns>
		private static List<Vector2> CreateCircle(double radius, int sides) {
			// Look for a cached version of this circle
			String circleKey = radius + "x" + sides;
			if (circleCache.ContainsKey(circleKey)) {
				return circleCache[circleKey];
			}

			List<Vector2> vectors = new List<Vector2>();

			const double max = 2.0 * Math.PI;
			double step = max / sides;

			for (double theta = 0.0; theta < max; theta += step) {
				vectors.Add(new Vector2((float)(radius * Math.Cos(theta)), (float)(radius * Math.Sin(theta))));
			}

			// then add the first vector again so it's a complete loop
			vectors.Add(new Vector2((float)(radius * Math.Cos(0)), (float)(radius * Math.Sin(0))));

			// Cache this circle so that it can be quickly drawn next time
			circleCache.Add(circleKey, vectors);

			return vectors;
		}


		/// <summary>
		/// Creates a list of vectors that represents an arc
		/// </summary>
		/// <param name="radius">The radius of the arc</param>
		/// <param name="sides">The number of sides to generate in the circle that this will cut out from</param>
		/// <param name="startingAngle">The starting angle of arc, 0 being to the east, increasing as you go clockwise</param>
		/// <param name="radians">The radians to draw, clockwise from the starting angle</param>
		/// <returns>A list of vectors that, if connected, will create an arc</returns>
		private static List<Vector2> CreateArc(float radius, int sides, float startingAngle, float radians) {
			List<Vector2> points = new List<Vector2>();
			points.AddRange(CreateCircle(radius, sides));
			points.RemoveAt(points.Count - 1); // remove the last point because it's a duplicate of the first

			// The circle starts at (radius, 0)
			double curAngle = 0.0;
			double anglePerSide = MathHelper.TwoPi / sides;

			// "Rotate" to the starting point
			while ((curAngle + (anglePerSide / 2.0)) < startingAngle) {
				curAngle += anglePerSide;

				// move the first point to the end
				points.Add(points[0]);
				points.RemoveAt(0);
			}

			// Add the first point, just in case we make a full circle
			points.Add(points[0]);

			// Now remove the points at the end of the circle to create the arc
			int sidesInArc = (int)((radians / anglePerSide) + 0.5);
			points.RemoveRange(sidesInArc + 1, points.Count - sidesInArc - 1);

			return points;
		}

		#endregion


		#region FillRectangle

		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="rect">The rectangle to draw</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static Action<SpriteBatch> FillRectangle(this Rectangle rect, Color color) {
			return (sp) => {
				// Simply use the function already there
				sp.Draw(pixel, rect, color);
			};
		}


		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="rect">The rectangle to draw</param>
		/// <param name="color">The color to draw the rectangle in</param>
		/// <param name="angle">The angle in radians to draw the rectangle at</param>
		public static Action<SpriteBatch> FillRectangle(this Rectangle rect, Color color, float angle) {
			return (sp) => {
				sp.Draw(pixel, rect, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
			};
		}


		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="location">Where to draw</param>
		/// <param name="size">The size of the rectangle</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static Action<SpriteBatch> FillRectangle(this Vector2 location, Vector2 size, Color color) {
			return (sp) => {
				FillRectangle(location, size, color, 0.0f)(sp);
			};
		}


		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="location">Where to draw</param>
		/// <param name="size">The size of the rectangle</param>
		/// <param name="angle">The angle in radians to draw the rectangle at</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static Action<SpriteBatch> FillRectangle(this Vector2 location, Vector2 size, Color color, float angle) {
			return (sp) => {
				// stretch the pixel between the two vectors
				sp.Draw(pixel,
								 location,
								 null,
								 color,
								 angle,
								 Vector2.Zero,
								 size,
								 SpriteEffects.None,
								 0);
			};
		}


		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="x">The X coord of the left side</param>
		/// <param name="y">The Y coord of the upper side</param>
		/// <param name="w">Width</param>
		/// <param name="h">Height</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static Action<SpriteBatch> FillRectangle(this float x, float y, float w, float h, Color color) {
			return (sp) => {
				FillRectangle(new Vector2(x, y), new Vector2(w, h), color, 0.0f)(sp);
			};
		}


		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="x">The X coord of the left side</param>
		/// <param name="y">The Y coord of the upper side</param>
		/// <param name="w">Width</param>
		/// <param name="h">Height</param>
		/// <param name="color">The color to draw the rectangle in</param>
		/// <param name="angle">The angle of the rectangle in radians</param>
		public static Action<SpriteBatch> FillRectangle(this float x, float y, float w, float h, Color color, float angle) {
			return (sp) => {
				FillRectangle(new Vector2(x, y), new Vector2(w, h), color, angle)(sp);
			};
		}

		#endregion


		#region DrawRectangle

		/// <summary>
		/// Draws a rectangle with the thickness provided
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="rect">The rectangle to draw</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static Action<SpriteBatch> DrawRectangle(this Rectangle rect, Color color) {
			return (sp) => {
				DrawRectangle(rect, color, 1.0f)(sp);
			};
		}


		/// <summary>
		/// Draws a rectangle with the thickness provided
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="rect">The rectangle to draw</param>
		/// <param name="color">The color to draw the rectangle in</param>
		/// <param name="thickness">The thickness of the lines</param>
		public static Action<SpriteBatch> DrawRectangle(this Rectangle rect, Color color, float thickness) {

			// TODO: Handle rotations
			// TODO: Figure out the pattern for the offsets required and then handle it in the line instead of here
			return (sp) => {
				DrawLine(new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Y), color, thickness)(sp); // top
				DrawLine(new Vector2(rect.X + 1f, rect.Y), new Vector2(rect.X + 1f, rect.Bottom + thickness), color, thickness)(sp); // left
				DrawLine(new Vector2(rect.X, rect.Bottom), new Vector2(rect.Right, rect.Bottom), color, thickness)(sp); // bottom
				DrawLine(new Vector2(rect.Right + 1f, rect.Y), new Vector2(rect.Right + 1f, rect.Bottom + thickness), color, thickness)(sp); // right
			};
		}


		/// <summary>
		/// Draws a rectangle with the thickness provided
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="location">Where to draw</param>
		/// <param name="size">The size of the rectangle</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static Action<SpriteBatch> DrawRectangle(this Vector2 location, Vector2 size, Color color) {
			return (sp) => {
				DrawRectangle(new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, 1.0f)(sp);
			};
		}


		/// <summary>
		/// Draws a rectangle with the thickness provided
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="location">Where to draw</param>
		/// <param name="size">The size of the rectangle</param>
		/// <param name="color">The color to draw the rectangle in</param>
		/// <param name="thickness">The thickness of the line</param>
		public static Action<SpriteBatch> DrawRectangle(this Vector2 location, Vector2 size, Color color, float thickness) {
			return (sp) => {
				DrawRectangle(new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, thickness)(sp);
			};
		}

		#endregion


		#region DrawLine

		/// <summary>
		/// Draws a line from point1 to point2 with an offset
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="x1">The X coord of the first point</param>
		/// <param name="y1">The Y coord of the first point</param>
		/// <param name="x2">The X coord of the second point</param>
		/// <param name="y2">The Y coord of the second point</param>
		/// <param name="color">The color to use</param>
		public static Action<SpriteBatch> DrawLine(this float x1, float y1, float x2, float y2, Color color) {
			return (sp) => {
				DrawLine(new Vector2(x1, y1), new Vector2(x2, y2), color, 1.0f)(sp);
			};
		}


		/// <summary>
		/// Draws a line from point1 to point2 with an offset
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="x1">The X coord of the first point</param>
		/// <param name="y1">The Y coord of the first point</param>
		/// <param name="x2">The X coord of the second point</param>
		/// <param name="y2">The Y coord of the second point</param>
		/// <param name="color">The color to use</param>
		/// <param name="thickness">The thickness of the line</param>
		public static Action<SpriteBatch> DrawLine(this float x1, float y1, float x2, float y2, Color color, float thickness) {
			return (sp) => {
				DrawLine(new Vector2(x1, y1), new Vector2(x2, y2), color, thickness)(sp);
			};
		}


		/// <summary>
		/// Draws a line from point1 to point2 with an offset
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="point1">The first point</param>
		/// <param name="point2">The second point</param>
		/// <param name="color">The color to use</param>
		public static Action<SpriteBatch> DrawLine(this Vector2 point1, Vector2 point2, Color color) {
			return (sp) => {
				DrawLine(point1, point2, color, 1.0f)(sp);
			};
		}


		/// <summary>
		/// Draws a line from point1 to point2 with an offset
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="point1">The first point</param>
		/// <param name="point2">The second point</param>
		/// <param name="color">The color to use</param>
		/// <param name="thickness">The thickness of the line</param>
		public static Action<SpriteBatch> DrawLine(this Vector2 point1, Vector2 point2, Color color, float thickness) {
			return (sp) => {
				// calculate the distance between the two vectors
				float distance = Vector2.Distance(point1, point2);

				// calculate the angle between the two vectors
				float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

				DrawLine(point1, distance, angle, color, thickness)(sp);
			};
		}


		/// <summary>
		/// Draws a line from point1 to point2 with an offset
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="point">The starting point</param>
		/// <param name="length">The length of the line</param>
		/// <param name="angle">The angle of this line from the starting point in radians</param>
		/// <param name="color">The color to use</param>
		public static Action<SpriteBatch> DrawLine(this Vector2 point, float length, float angle, Color color) {
			return (sp) => {
				DrawLine(point, length, angle, color, 1.0f)(sp);
			};
		}


		/// <summary>
		/// Draws a line from point1 to point2 with an offset
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="point">The starting point</param>
		/// <param name="length">The length of the line</param>
		/// <param name="angle">The angle of this line from the starting point</param>
		/// <param name="color">The color to use</param>
		/// <param name="thickness">The thickness of the line</param>
		public static Action<SpriteBatch> DrawLine(this Vector2 point, float length, float angle, Color color, float thickness) {
			return (sp) => {
				// stretch the pixel between the two vectors
				sp.Draw(pixel,
								 point,
								 null,
								 color,
								 angle,
								 Vector2.Zero,
								 new Vector2(length, thickness),
								 SpriteEffects.None,
								 0);
			};
		}

		#endregion
	}

}
