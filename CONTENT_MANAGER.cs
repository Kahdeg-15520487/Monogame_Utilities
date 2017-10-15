using System.IO;
using System.Collections.Generic;
using System.Reflection;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Utilities.Drawing.Animation;

namespace Utilities
{
    //singleton to store common data
    public static partial class CONTENT_MANAGER
    {
        public static Game gameInstance;

        #region resources
        public static ContentManager Content;

        #region sprite
        public static SpriteBatch spriteBatch;

        public static SpriteFont defaultfont;

        public static Texture2D spriteSheet;
        public static Texture2D UIspriteSheet;

        #region animation sprite sheet
        public static Dictionary<string, AnimatedEntity> animationEntities;
        public static Dictionary<string, Texture2D> animationSheets;
        public static List<Animation> animationTypes;
        #endregion

        public static void LoadContent()
        {
            LoadFont();

            LoadSpriteSheet();

            LoadAnimationContent();

            LoadSound();
        }

        private static void LoadFont()
        {
            defaultfont = Content.Load<SpriteFont>(@"font\default_font");
        }

        private static void LoadSpriteSheet()
        {
            //spriteSheet = Content.Load<Texture2D>(@"sprite\sprite_sheet");
            //UIspriteSheet = Content.Load<Texture2D>(@"sprite\ui_sprite_sheet");
        }

        private static void LoadAnimationContent()
        {
            //string delimit = "Yellow";
            CONTENT_MANAGER.animationEntities = new Dictionary<string, AnimatedEntity>();
            CONTENT_MANAGER.animationSheets = new Dictionary<string, Texture2D>();
            CONTENT_MANAGER.animationTypes = new List<Animation>();
        }

        #endregion

        #region sound

        private static void LoadSound()
        {
            //menu_select = Content.Load<SoundEffect>(@"sound\sfx\menu_select");
        }

        #endregion
        #endregion

        public static RasterizerState antialiasing = new RasterizerState { MultiSampleAntiAlias = true };

        private static InputState _inputState;
        public static InputState currentInputState
        {
            get
            {
                return _inputState;
            }
            set
            {
                lastInputState = _inputState;
                _inputState = value;
            }
        }
        public static InputState lastInputState { get; private set; }

        public static void ShowFPS(GameTime gameTime)
        {
            int frameRate = (int)(1 / gameTime.ElapsedGameTime.TotalSeconds);
            spriteBatch.DrawString(defaultfont, frameRate.ToString(), new Vector2(0, 0), Color.Black);
        }
    }
}
