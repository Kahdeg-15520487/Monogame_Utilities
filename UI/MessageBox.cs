using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Utility.Drawing;

namespace Utility.UI {
	public class MessageBox : UIObject {
		Button button;
		Label label;
		private Texture2D background;

		public override float Depth {
			get => depth;
			set {
				depth = value;
				button.Depth = depth;
				label.Depth = depth;
			}
		}

		private string defaultPrompt;
		private string defaultButtonText;

		public MessageBox(Point position, string prompt = null, string buttontext = null) {
			//the msgbox is gonna be 200 x 100
			Position = position;

			defaultPrompt = prompt ?? string.Empty;
			defaultButtonText = buttontext ?? string.Empty;

			//the button gonna be 50x30
			button = new Button(defaultPrompt, new Point(position.X + 75, position.Y + 100), new Vector2(50, 30), CONTENT_MANAGER.Fonts["default"]);
			label = new Label(defaultButtonText, position, new Vector2(50, 30), CONTENT_MANAGER.Fonts["default"], 1f);
			background = TextureRenderer.Render(Primitive2DActionGenerator.FillRectangle(new Rectangle(Point.Zero, new Point(200, 100)), Color.LightGray), CONTENT_MANAGER.spriteBatch, CONTENT_MANAGER.gameInstance.GraphicsDevice, new Vector2(200, 100));

			depth = LayerDepth.GuiBackground;
			button.Depth = LayerDepth.GuiLower;
			label.Depth = LayerDepth.GuiLower;

			button.MouseClick += Button_MouseClick;
		}

		private void Button_MouseClick(object sender, UIEventArgs e) {
			ButtonPressed?.Invoke(this, e);
		}

		public void Show(string prompt = null, string buttontext = null) {
			label.Text = prompt ?? defaultPrompt;
			button.Text = buttontext ?? defaultButtonText;

			isVisible = true;
			button.IsVisible = IsVisible;
			label.IsVisible = IsVisible;
		}

		public void Hide() {
			isVisible = false;
			button.IsVisible = IsVisible;
			label.IsVisible = IsVisible;
		}

		public override void Update(GameTime gameTime, InputState inputState, InputState lastInputState) {
			button.Update(gameTime, inputState, lastInputState);
			label.Update(gameTime, inputState, lastInputState);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			spriteBatch.Draw(background, new Rectangle(Position, new Point(200, 100)), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth);
			button.Draw(spriteBatch, gameTime);
			label.Draw(spriteBatch, gameTime);
			//draw the border
			//draw the prompt
			//draw button
		}

		public event EventHandler<UIEventArgs> ButtonPressed;
		protected virtual void OnButtonPressed(object sender, UIEventArgs e) {
			ButtonPressed?.Invoke(sender, e);
		}
	}
}
