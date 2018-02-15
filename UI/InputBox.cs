using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Utility.Drawing;

namespace Utility.UI {
	public class InputBox : UIObject {
		StringBuilder textBuffer = new StringBuilder();

		/// <summary>
		/// The current text in the buffer. Assign text to this will clear the buffer.
		/// </summary>
		public virtual string Text {
			get {
				return textBuffer.ToString();
			}
			set {
				textBuffer.Clear();
				textBuffer.Append(value);
			}
		}
		public override Vector2 Size {
			get {
				return base.Size;
			}

			set {
				base.Size = value;
			}
		}

		/// <summary>
		/// Speed of flicker (change between "" and "|")
		/// </summary>       
		private const int speed_flick = 25;
		/// <summary>
		/// Increase until equal to speed_flicker and change isCursor_flicker value then reset to 0
		/// </summary>
		private int temp_speed_flicker = 0;
		/// <summary>
		/// Use to change between "" and "|"
		/// </summary>
		private bool isCursor_flicker = false;

		private Texture2D rectTexture = null;

		public Color CaretColor { get; set; } = Color.DarkGray;
		public int CursorPosition { get; set; }
		public List<char> ignoreCharacter;
		private int maxTextLength;
		private int textSpacing;

		public InputBox(string text, Point position, Vector2? size, SpriteFont font, Color foregroundColor, Color backgroundColor) {
			Text = text;
			Position = position;
			if (size != null) {
				Size = size.Value;
			}
			else {
				//todo auto calculate rect's size
			}
			Font = font;
			ForegroundColor = foregroundColor;
			BackgroundColor = backgroundColor;
			CursorPosition = 0;
			maxTextLength = FindMaxTextLength();
			textSpacing = rect.Width / maxTextLength;
			ignoreCharacter = new List<char>();

			rectTexture = TextureRenderer.Render(Primitive2DActionGenerator.DrawRectangle(new Rectangle(0, 0, rect.Width, rect.Height), backgroundColor), CONTENT_MANAGER.spriteBatch, new Vector2(rect.Width, rect.Height), Vector2.Zero, backgroundColor);

			CONTENT_MANAGER.gameInstance.Window.TextInput += TextInputHandler;
		}

		private void TextInputHandler(object sender, TextInputEventArgs e) {
			if (isFocused) {
				if (Font.Characters.Contains(e.Character) && !ignoreCharacter.Contains(e.Character)) {
					if (textBuffer.Length < maxTextLength) {
						textBuffer.Append(e.Character);
						CursorPosition++;
					}
				}
			}
		}

		private int FindMaxTextLength() {
			string teststr = "A";
			while (Font.MeasureString(teststr).X < rect.Width) {
				teststr += "A";
			}
			return teststr.Length;
		}

		public override void Update(GameTime gameTime, InputState inputState, InputState lastInputState) {
			base.Update(gameTime, inputState, lastInputState);

			if (isFocused == true) {
				temp_speed_flicker += 1;
				if (temp_speed_flicker == speed_flick) {
					isCursor_flicker = !isCursor_flicker;
					temp_speed_flicker = 0;
				}

				if (HelperFunction.IsKeyPress(Keys.Back)) {
					if (textBuffer.Length > 0) {
						if (Font.MeasureString(textBuffer).X > rect.Width) {
							textBuffer.Remove(textBuffer.Length - 1, 1);
							return;
						}
						textBuffer.Remove((CursorPosition - 1).Clamp(textBuffer.Length == 0 ? 0 : textBuffer.Length - 1, 0), 1);
						CursorPosition--;
					}
				}
			}
		}

		public void Clear() {
			textBuffer.Clear();
			CursorPosition = 0;
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			spriteBatch.DrawString(Font, textBuffer, rect.Location.ToVector2(), ForegroundColor, Rotation, origin, scale, SpriteEffects.None, LayerDepth.GuiLower);

			//Draw text caret
			spriteBatch.DrawString(Font, IsFocused ? isCursor_flicker ? "" : "|" : "|", rect.Location.ToVector2() + new Vector2(CursorPosition * textSpacing - 5, -2), CaretColor);

			spriteBatch.Draw(rectTexture, rect.Location.ToVector2(), null, Color.White);
		}
	}
}