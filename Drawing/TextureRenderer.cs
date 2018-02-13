using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Utility.Drawing {
	public static class TextureRenderer {
		public static Texture2D Render(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 size, float scale = 1f, params Tuple<Vector2, Texture2D>[] textureList) {
			RenderTarget2D result = new RenderTarget2D(graphicsDevice, (int)size.X, (int)size.Y);
			graphicsDevice.SetRenderTarget(result);
			graphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();

			foreach (var sprite in textureList) {
				spriteBatch.Draw(sprite.Item2, sprite.Item1, Color.White);
			}

			spriteBatch.End();
			graphicsDevice.SetRenderTarget(null);
			return result;
		}

		public static Texture2D Render(Action<SpriteBatch> drawer, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 size, float scale = 1f) {
			RenderTarget2D result = new RenderTarget2D(graphicsDevice, (int)size.X, (int)size.Y);
			graphicsDevice.SetRenderTarget(result);
			graphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();

			drawer(spriteBatch);

			spriteBatch.End();
			graphicsDevice.SetRenderTarget(null);
			return result;
		}
	}
}