using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Utility.Drawing.Animation;
using Utility.Drawing;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.Reflection;
using Fclp;
using Newtonsoft.Json;

namespace Utility
{
    //singleton to store common data
    public static partial class CONTENT_MANAGER
    {
        public static Game GameInstance;
        public static Camera camera;
        public static string rootPath;

        #region resources
        public static ContentManager Content;

        public static SpriteBatch spriteBatch;

        public static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();

        public static Dictionary<string, Texture2D> Sprites = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SpriteSheetMap> SpriteSheets = new Dictionary<string, SpriteSheetMap>();

        public static Dictionary<string, Effect> Shaders = new Dictionary<string, Effect>();

        public static Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();

        public static Dictionary<string, AnimatedEntity> AnimationEntities = new Dictionary<string, AnimatedEntity>();

        /// <summary>
        /// load font, all font is put inside folder font
        /// </summary>
        /// <param name="fontList">fonts to load</param>
        public static void LoadFonts(params string[] fontList)
        {
            foreach (var font in fontList)
            {
                Fonts.Add(font, Content.Load<SpriteFont>(string.Format(@"font\{0}", font)));
            }
        }

        /// <summary>
        /// load sprite, all sprite is put inside folder sprite
        /// </summary>
        /// <param name="shaders">sprite to load</param>
        public static void LoadShaders(params string[] shaders)
        {
            foreach (var shader in shaders)
            {
                Shaders.Add(shader, Content.Load<Effect>(string.Format(@"shader\{0}", shader)));
            }
        }

        /// <summary>
        /// load sprite, all sprite is put inside folder sprite
        /// </summary>
        /// <param name="sprites">sprite to load</param>
        public static void LoadSprites(params string[] sprites)
        {
            foreach (var sprite in sprites)
            {
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
        public static SpriteSheetMap LoadSpriteSheet(string spriteSheet, int width = 0, int height = 0)
        {
            var spm = new SpriteSheetMap(spriteSheet, Content.Load<Texture2D>(string.Format(@"sprite\{0}", spriteSheet)), width, height);
            SpriteSheets.Add(spriteSheet, spm);
            return spm;
        }

        /// <summary>
        /// load in a spritesheet and an animationdata to create a animation
        /// </summary>
        /// <param name="spritesheet">animation's spritesheet</param>
        /// <param name="animationData">animation definition</param>
        public static void LoadAnimation(string animName, string spritesheet, string animationData)
        {
            var animEntity = JsonConvert.DeserializeObject<AnimatedEntity>(animationData);
            if (!Sprites.ContainsKey(spritesheet))
            {
                LoadSprites(spritesheet);
            }
            animEntity.LoadContent(Sprites[spritesheet]);

            AnimationEntities.Add(animName, animEntity);
        }


        public static void LoadSound(params string[] soundlist)
        {
            //menu_select = Content.Load<SoundEffect>(@"sound\sfx\menu_select");
            foreach (var sound in soundlist)
            {
                Sounds.Add(sound, Content.Load<SoundEffect>(string.Format(@"sound\{0}", sound)));
            }
        }

        #endregion

        private static bool IsSpriteBatchBegin = false;

        public static void BeginSpriteBatch(this SpriteBatch spriteBatch, SpriteSortMode spriteSortMode = SpriteSortMode.FrontToBack)
        {
            if (IsSpriteBatchBegin)
            {
                EndSpriteBatch(spriteBatch);
            }
            spriteBatch.Begin(spriteSortMode);
            IsSpriteBatchBegin = true;
        }
        public static void BeginSpriteBatchWithCamera(this SpriteBatch spriteBatch, Camera camera, SpriteSortMode spriteSortMode = SpriteSortMode.FrontToBack)
        {
            if (IsSpriteBatchBegin)
            {
                EndSpriteBatch(spriteBatch);
            }
            spriteBatch.Begin(spriteSortMode, transformMatrix: camera.TransformMatrix);
            IsSpriteBatchBegin = true;
        }

        public static void EndSpriteBatch(this SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            IsSpriteBatchBegin = false;
        }

        public static string MapName { get; internal set; } = null;

        public static void ParseArguments(string[] args)
        {
            var p = new FluentCommandLineParser();
            p.Setup<string>('m', "map")
                .Callback(x => MapName = x);
            p.Parse(args);
        }

        public static string LocalRootPath;

        //todo make internal messagebox
        public static void ShowMessageBox(object message)
        {

        }

        //todo make internal messagebox
        public static string ShowPromptBox(object message)
        {
            return string.Empty;
        }


        private static InputState _inputState;
        public static InputState CurrentInputState
        {
            get
            {
                return _inputState;
            }
            set
            {
                LastInputState = _inputState;
                _inputState = value;
            }
        }
        public static InputState LastInputState { get; private set; }

        public static void ShowFPS(GameTime gameTime)
        {
            int frameRate = (int)(1 / gameTime.ElapsedGameTime.TotalSeconds);
            spriteBatch.DrawString(Fonts["default"], frameRate.ToString(), new Vector2(0, 0), Color.Black);
        }
    }
}
