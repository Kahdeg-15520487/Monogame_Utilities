﻿using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Utilities.Drawing.Animation;
using Utilities.Drawing;

namespace Utilities {
	//singleton to store common data
	public static partial class CONTENT_MANAGER {
		public static Game gameInstance;
		public static Camera camera;
		public static string rootPath;

		#region resources
		public static ContentManager Content;

		public static SpriteBatch spriteBatch;

		public static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();

		public static Dictionary<string, Texture2D> Sprites = new Dictionary<string, Texture2D>();
		public static Dictionary<string, SpriteSheetMap> SpriteSheets = new Dictionary<string, SpriteSheetMap>();

		public static Dictionary<string, AnimatedEntity> animationEntities;
		public static Dictionary<string, Texture2D> animationSheets;
		public static List<Animation> animationTypes;

		/// <summary>
		/// load font, all font is put inside folder font
		/// </summary>
		/// <param name="fontList">fonts to load</param>
		public static void LoadFont(params string[] fontList) {
			foreach (var font in fontList) {
				Fonts.Add(font, Content.Load<SpriteFont>(string.Format(@"font\{0}", font)));
			}
		}

		/// <summary>
		/// load sprite, all sprite is put inside folder sprite
		/// </summary>
		/// <param name="sprites">sprite to load</param>
		public static void LoadSprites(params string[] sprites) {
			foreach (var sprite in sprites) {
				Sprites.Add(sprite, Content.Load<Texture2D>(string.Format(@"sprite\{0}", sprite)));
			}
		}

		/// <summary>
		/// load in a spritesheet with given <paramref name="width"/> and <paramref name="height"/> of a sprite<para/>
		/// all spritesheet is put inside folder sprite
		/// </summary>
		/// <param name="spriteSheet">spritesheet to load</param>
		/// <param name="width">width of a sprite</param>
		/// <param name="height">height of a sprite</param>
		public static SpriteSheetMap LoadSpriteSheet(string spriteSheet, int width = 0, int height = 0) {
			var spm = new SpriteSheetMap(spriteSheet, Content.Load<Texture2D>(string.Format(@"sprite\{0}", spriteSheet)), width, height);
			SpriteSheets.Add(spriteSheet, spm);
			return spm;
		}

		public static void LoadAnimationContent(params string[] animationList) {
			//string delimit = "Yellow";
			animationEntities = new Dictionary<string, AnimatedEntity>();
			animationSheets = new Dictionary<string, Texture2D>();
			animationTypes = new List<Animation>();
		}


		public static void LoadSound(params string[] soundlist) {
			//menu_select = Content.Load<SoundEffect>(@"sound\sfx\menu_select");
		}

		#endregion

		private static InputState _inputState;
		public static InputState CurrentInputState {
			get {
				return _inputState;
			}
			set {
				LastInputState = _inputState;
				_inputState = value;
			}
		}
		public static InputState LastInputState { get; private set; }

		public static void ShowFPS(GameTime gameTime) {
			int frameRate = (int)(1 / gameTime.ElapsedGameTime.TotalSeconds);
			spriteBatch.DrawString(Fonts["defaultFont"], frameRate.ToString(), new Vector2(0, 0), Color.Black);
		}
	}
}
