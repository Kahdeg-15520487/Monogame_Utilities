using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Utility.Drawing;

namespace Utility.UI {
	public class MessageBox : UIObject {
		Button middleButton;
		Button leftButton;
		Button rightButton;
		Label label;
		private Texture2D background;

		public override float Depth {
			get => depth;
			set {
				depth = value;
				middleButton.Depth = depth;
				label.Depth = depth;
			}
		}

		private string defaultPrompt;
		private string defaultButtonText;

		public MessageBox(Point position, string prompt = null, string buttontext = null) {
			//the msgbox is gonna be 240 x 100
			Position = position;

			defaultPrompt = prompt ?? string.Empty;
			defaultButtonText = buttontext ?? string.Empty;

			//the button gonna be 80x30
			middleButton = new Button(defaultButtonText, new Point(position.X + 80, position.Y + 100), new Vector2(80, 30), CONTENT_MANAGER.Fonts["default"]);
			leftButton = new Button(defaultButtonText, new Point(position.X, position.Y + 100), new Vector2(80, 30), CONTENT_MANAGER.Fonts["default"]) {
				IsVisible = false
			};
			rightButton = new Button(defaultButtonText, new Point(position.X + 160, position.Y + 100), new Vector2(80, 30), CONTENT_MANAGER.Fonts["default"]) {
				IsVisible = false
			};

			label = new Label(defaultPrompt, position, new Vector2(50, 30), CONTENT_MANAGER.Fonts["default"], 1f);
			background = TextureRenderer.Render(Primitive2DActionGenerator.FillRectangle(new Rectangle(Point.Zero, new Point(240, 100)), Color.LightGray), CONTENT_MANAGER.spriteBatch, new Vector2(240, 100), Vector2.Zero, Color.White);

			depth = LayerDepth.GuiBackground;
			middleButton.Depth = LayerDepth.GuiLower;
			label.Depth = LayerDepth.GuiLower;

			middleButton.MouseClick += (o, e) => MiddleButtonPressed?.Invoke(this, e);
			leftButton.MouseClick += (o, e) => LeftButtonPressed?.Invoke(this, e);
			rightButton.MouseClick += (o, e) => RightButtonPressed?.Invoke(this, e);
		}

		public void Show(string prompt = null, string buttontext = null, string leftbtn = null, string rightbtn = null) {
			label.Text = prompt ?? defaultPrompt;
			middleButton.Text = buttontext ?? defaultButtonText;

			if (!string.IsNullOrEmpty(leftbtn)) {
				leftButton.IsVisible = true;
				leftButton.Text = leftbtn;
			}
			else {
				leftButton.IsVisible = false;
			}
			if (!string.IsNullOrEmpty(rightbtn)) {
				rightButton.IsVisible = true;
				rightButton.Text = rightbtn;
			}
			else {
				rightButton.IsVisible = false;
			}

			isVisible = true;
			middleButton.IsVisible = IsVisible;
			label.IsVisible = IsVisible;
		}

		public void Hide() {
			isVisible = false;
			middleButton.IsVisible = IsVisible;
			label.IsVisible = IsVisible;

			leftButton.IsVisible = false;
			rightButton.IsVisible = false;
		}

		public override void Update(GameTime gameTime, InputState inputState, InputState lastInputState) {
			middleButton.Update(gameTime, inputState, lastInputState);
			if (leftButton.IsVisible) {
				leftButton.Update(gameTime, inputState, lastInputState);
			}
			if (rightButton.IsVisible) {
				rightButton.Update(gameTime, inputState, lastInputState);
			}
			label.Update(gameTime, inputState, lastInputState);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			spriteBatch.Draw(background, new Rectangle(Position, new Point(240, 100)), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth);
			middleButton.Draw(spriteBatch, gameTime);
			label.Draw(spriteBatch, gameTime);

			if (leftButton.IsVisible) {
				leftButton.Draw(spriteBatch, gameTime);
			}
			if (rightButton.IsVisible) {
				rightButton.Draw(spriteBatch, gameTime);
			}
			//draw the border
			//draw the prompt
			//draw button
		}

		public event EventHandler<UIEventArgs> MiddleButtonPressed;
		public event EventHandler<UIEventArgs> LeftButtonPressed;
		public event EventHandler<UIEventArgs> RightButtonPressed;
		public event EventHandler<UIEventArgs> MessageboxClicked;
		protected virtual void OnButtonPressed(object sender, UIEventArgs e) {
			MiddleButtonPressed?.Invoke(sender, e);
		}
		protected virtual void OnLeftButtonPressed(object sender, UIEventArgs e) {
			LeftButtonPressed?.Invoke(sender, e);
		}
		protected virtual void OnRightButtonPressed(object sender, UIEventArgs e) {
			RightButtonPressed?.Invoke(sender, e);
		}
		protected virtual void OnMessageboxClicked(object sender, UIEventArgs e) {
			MessageboxClicked?.Invoke(sender, e);
		}
	}
}
