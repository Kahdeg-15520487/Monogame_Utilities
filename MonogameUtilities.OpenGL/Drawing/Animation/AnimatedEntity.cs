using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Utility.Drawing.Animation
{
    public enum AnimatedEntityState
    {
        Playing,
        Stopped,
        Paused
    }

    public class AnimatedEntity
    {
        #region Fields

        // Holds all animations this entity can play
        private Dictionary<string, Animation> animations;

        // The animation we are currently playing
        private Animation currentAnimation;

        // The texture that contains all of our frames
        private Texture2D spriteSheet;

        // Positon of the sprite in our world
        private Vector2 position;

        // Tells SpriteBatch where to center our texture
        private Vector2 origin;

        // The rotation of our sprite
        private float rotation;

        // The scale of our sprite
        private float scale;

        private float depth;
        private AnimatedEntityState state;

        // Tells SpriteBatch how to flip our texture
        private SpriteEffects flipEffect;

        // Tells SpriteBatch what color to tint our texture with
        private Color tintColor;

        #endregion

        #region Properties

        public Dictionary<string, Animation> Animations { get => animations; }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public SpriteEffects FlipEffect
        {
            get { return flipEffect; }
            set { flipEffect = value; }
        }
        public string CurntAnimationName
        {
            get { return currentAnimation.Name; }
        }
        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }
        public bool IsPlaying
        {
            get { return state == AnimatedEntityState.Playing; }
        }

        #endregion

        #region Constructor

        public AnimatedEntity()
        {
            animations = new Dictionary<string, Animation>();
            spriteSheet = null;
            position = Vector2.Zero;
            origin = Vector2.Zero;
            rotation = 0;
            scale = 1;
            depth = 0f;// LayerDepth.BackGround;
            flipEffect = SpriteEffects.None;
            tintColor = Color.White;
        }
        public AnimatedEntity(Vector2 position, Vector2 origin, Color? tintColor, float depth, float scale = 1)
        {
            //Initialize the Dictionary
            animations = new Dictionary<string, Animation>();
            spriteSheet = null;
            this.origin = origin;
            rotation = 0;
            this.depth = depth;
            flipEffect = SpriteEffects.None;

            this.position = position;
            this.scale = scale;
            this.tintColor = tintColor ?? Color.White;

            //If the scale is less than 0 we wont see the texture get drawn
            if (scale <= 0)
                scale = 0.1f;
        }
        public AnimatedEntity(AnimatedEntity source)
        {
            animations = new Dictionary<string, Animation>();
            spriteSheet = source.spriteSheet;
            origin = source.origin;
            rotation = source.rotation;
            depth = source.depth;
            flipEffect = source.flipEffect;

            position = source.position;
            scale = source.scale;
            tintColor = source.tintColor;

            //If the scale is less than 0 we wont see the texture get drawn
            if (scale <= 0)
                scale = 0.1f;

            foreach (var anim in source.animations.Values)
            {
                animations.Add(anim.Name, new Animation(anim));
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Loads the sprite sheet texture
        /// </summary>
        /// <param name="content">ContentManager to load from</param>
        /// <param name="textureAssetName">The asset name of our texture</param>
        public void LoadContent(ContentManager content, string textureAssetName)
        {
            //Load our sprite sheet texture
            spriteSheet = content.Load<Texture2D>(textureAssetName);
        }
        /// <summary>
        /// Loads the sprite sheet texture
        /// </summary>
        /// <param name="spriteSheet">spritesheet to be load</param>
        public void LoadContent(Texture2D spriteSheet)
        {
            //Set our sprite sheet texture
            this.spriteSheet = spriteSheet;
        }
        /// <summary>
        /// Loads the sprite sheet texture and assign keyframe
        /// </summary>
        /// <param name="spriteSheetMap">spriteSheetMap to be load from</param>
        public void LoadContent(SpriteSheetMap spriteSheetMap, bool shouldLoop, float fps)
        {
            spriteSheet = spriteSheetMap.SpriteSheet;
            Animation animation = new Animation(spriteSheetMap.SpriteSheetName, shouldLoop, fps, null);
            foreach (var rect in spriteSheetMap.SpriteRect)
            {
                animation.AddKeyFrame(rect);
            }
            AddAnimation(animation);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Helper method to add Animations to this entity
        /// </summary>
        /// <param name="animation">The Animation we want to add</param>
        public void AddAnimation(Animation animation)
        {
            // Is this Animation already in the Dictionary?
            if (!animations.ContainsKey(animation.Name))
            {
                // If not we can safely add it
                animations.Add(animation.Name, animation);
            }
            else
            {
                // Otherwise we tell are computer to yell at us
                //log stuff
                //CONTENT_MANAGER.Log("duplicate animation : " + animation.Name);
            }
        }
        public void AddAnimation(params Animation[] anims)
        {
            foreach (Animation animation in anims)
            {
                // Is this Animation already in the Dictionary?
                if (!animations.ContainsKey(animation.Name))
                {
                    // If not we can safely add it
                    animations.Add(animation.Name, animation);
                }
                else
                {
                    // Otherwise we tell are computer to yell at us
                    //log stuff
                    //CONTENT_MANAGER.Log("duplicate animation : " + animation.Name);
                }
            }
        }
        public void AddAnimation(List<Animation> anims)
        {
            foreach (Animation animation in anims)
            {
                // Is this Animation already in the Dictionary?
                if (!animations.ContainsKey(animation.Name))
                {
                    // If not we can safely add it
                    animations.Add(animation.Name, animation);
                }
                else
                {
                    // Otherwise we tell are computer to yell at us
                    //log stuff
                    //CONTENT_MANAGER.Log("duplicate animation : " + animation.Name);
                }
            }
        }

        public bool ContainAnimation(string animationName)
        {
            return animations.ContainsKey(animationName);
        }

        public void RemoveAnimation(string animationName)
        {
            if (animations.ContainsKey(animationName))
            {
                animations.Remove(animationName);
            }
            else
            {
                //CONTENT_MANAGER.Log("animation doesn't exist : " + animationName);
            }
        }

        /// <summary>
        /// Tells this entity to play an specific animation
        /// </summary>
        /// <param name="key">The name of the animation you want to play</param>
        public void PlayAnimation(string key)
        {
            if (string.IsNullOrEmpty(key) || !animations.ContainsKey(key))
            {
                //CONTENT_MANAGER.Log(key + "not found");
                return;
            }

            //TODO find the meaning of the following code

            if (currentAnimation != null)
            {
                if (currentAnimation.Name == key)
                {
                    if (animations.ContainsKey(key))
                    {
                        return;
                    }
                }
            }

            state = AnimatedEntityState.Playing;

            currentAnimation = animations[key];
            currentAnimation.Reset();
        }

        public void StopAnimation()
        {
            state = AnimatedEntityState.Stopped;
        }

        public void ContinueAnimation()
        {
            state = AnimatedEntityState.Playing;
        }

        public void PauseAnimation()
        {
            state = AnimatedEntityState.Paused;
        }

        #endregion

        #region Update

        /// <summary>
        /// We call this method to Update our animation each frame
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            if (currentAnimation != null && state == AnimatedEntityState.Playing)
            {
                //2 câu lệnh sau sẽ làm cho origin của animation ở chính giữa khung hình.
                //vì nguyên cái game chạy theo origin là vector2.zero nên bỏ
                //origin.X = currentAnimation.CurntKeyFrame.Width / 2;
                //origin.Y = currentAnimation.CurntKeyFrame.Height / 2;

                currentAnimation.Update(gameTime);

                if (currentAnimation.IsComplete)
                {
                    if (!string.IsNullOrEmpty(currentAnimation.TransitionKey))
                    {
                        PlayAnimation(currentAnimation.TransitionKey);
                    }
                    else
                    {
                        state = AnimatedEntityState.Stopped;
                    }
                }
            }
        }

        #endregion

        #region Draw

        /// <summary>
        /// Draws the AnimatedEntity
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        /// <param name="spriteBatch">The SpriteBatch object we will use to draw</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentAnimation != null && state != AnimatedEntityState.Stopped)
            {
                spriteBatch.Draw(spriteSheet, position, currentAnimation.CurntKeyFrame.GetRekt, tintColor,
                    rotation, origin, scale, flipEffect, depth);
            }
        }

        /// <summary>
        /// Draws the AnimatedEntity
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        /// <param name="spriteBatch">The SpriteBatch object we will use to draw</param>
        /// <param name="depth">The depth offset</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, float depth)
        {
            if (currentAnimation != null && state != AnimatedEntityState.Stopped)
            {
                spriteBatch.Draw(spriteSheet, position, currentAnimation.CurntKeyFrame.GetRekt, tintColor,
                    rotation, origin, scale, flipEffect, depth);
            }
        }

        #endregion

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (string key in animations.Keys)
            {
                result.Append(key);
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }
    }


}
