using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Utility.Drawing
{
    public static class TextureRenderer
    {
        /// <summary>
        /// Render an array of texture on to a texture
        /// </summary>
        /// <param name="size">the normal size of the target texture before apply scaling</param>
        /// <param name="scale">the scale of the final texture</param>
        /// <param name="textureList">list of textures and its position on the target texture before apply scaling</param>
        /// <returns></returns>
        public static Texture2D Render(SpriteBatch spriteBatch, Vector2 size, float scale = 1f, params Tuple<Vector2, Texture2D>[] textureList)
        {
            var graphicsDevice = spriteBatch.GraphicsDevice;
            RenderTarget2D result = new RenderTarget2D(graphicsDevice, (int)size.X, (int)size.Y);
            graphicsDevice.SetRenderTarget(result);
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            foreach (var sprite in textureList)
            {
                spriteBatch.Draw(sprite.Item2, sprite.Item1, Color.White);
            }

            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);

            if (scale == 1)
            {
                return result;
            }

            RenderTarget2D finalresult = new RenderTarget2D(graphicsDevice, (int)(size.X / scale), (int)(size.Y / scale));
            graphicsDevice.SetRenderTarget(finalresult);
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            spriteBatch.Draw(result, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);

            return finalresult;
        }

        /// <summary>
        /// Take a drawer method and draw it on to a texture
        /// </summary>
        /// <param name="drawer">the drawer method, only take in one parameter SpriteBatch</param>
        /// <param name="size">the normal size of the target texture before apply scaling</param>
        /// <param name="scale">the scale of the final texture</param>
        /// <returns></returns>
        public static Texture2D Render(SpriteBatch spriteBatch, Vector2 size, Vector2 offset, Color background, float scale = 1f, params Action<SpriteBatch>[] drawerDelegates)
        {
            GraphicsDevice graphicsDevice = spriteBatch.GraphicsDevice;
            RenderTarget2D result = new RenderTarget2D(graphicsDevice, (int)size.X, (int)size.Y);
            graphicsDevice.SetRenderTarget(result);
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            foreach (var dd in drawerDelegates)
            {
                dd.Invoke(spriteBatch);
            }

            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);


            if (scale == 1)
            {
                return result;
            }

            RenderTarget2D finalresult = new RenderTarget2D(graphicsDevice, (int)(size.X / scale), (int)(size.Y / scale));
            graphicsDevice.SetRenderTarget(finalresult);
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            spriteBatch.Draw(result, offset, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);

            return finalresult;
        }
    }
}