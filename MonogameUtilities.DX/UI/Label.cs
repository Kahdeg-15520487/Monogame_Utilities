﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utility.Drawing;

namespace Utility.UI {
	public class Label : UIObject {

		public virtual bool AutoSize {
			get;
			set;
		}

		protected string text;
		public virtual string Text {
			get {
				return text;
			}
			set {
				text = string.IsNullOrEmpty(value) ? "" : value;
			}
		}

		public override Point Position {
			get {
				return base.Position;
			}
			set {
				base.Position = value;
				//background.rectangle.Location = value;
				//border.rectangle.Location = value;
			}
		}

		public override Vector2 Size {
			get {
				return base.Size;
			}
			set {
				base.Size = value;
				//background.rectangle.Size = value.ToPoint();
				//border.rectangle.Size = value.ToPoint();
			}
		}
		/// <summary>
		/// Offset of the text inside the button
		/// </summary>
		public Vector2 Origin {
			get { return origin; }
			set { origin = value; }
		}

		public override float Depth { get; set; } = LayerDepth.GuiUpper;

		public Label() {

		}

		public Label(string text, Point position, Vector2 size, SpriteFont font) {
			//Init();
			Text = text;
			Position = position;
			Size = size;
			Font = font;
			origin = new Vector2(rect.X, rect.Y) + Size / 4;
		}

		public Label(string text, Point position, Vector2? size, SpriteFont font, float _scale) {
			//Init();
			Text = text;
			Position = position;
			if (size != null) {
				Size = size.Value;
			}
			else {
				Size = font.MeasureString(text);
				origin = position.ToVector2();
			}
			Font = font;
			Scale = _scale;
		}

		private void Init() {
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			spriteBatch.DrawString(Font ?? CONTENT_MANAGER.Fonts["defaultFont"], (string.IsNullOrEmpty(text)) ? "" : text, Position.ToVector2() - origin, ForegroundColor, Rotation, Vector2.Zero, scale, SpriteEffects.None, Depth);
			DrawingHelper.DrawRectangle(rect, BackgroundColor, true);
			DrawingHelper.DrawRectangle(rect, BorderColor, false);
		}
	}
}
