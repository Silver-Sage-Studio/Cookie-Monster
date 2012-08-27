using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Sage_Engine
{
    // Need to look into Making Dynamic Enums , Will make working with this class alot easier.
   public class SpriteAnimation
    {
        private Texture2D texture;
        string currentAnimation;
        Vector2 location;
        bool animating = true;
        Dictionary<string, FrameAnimation> animations = new Dictionary<string, FrameAnimation>();
        float rotation = 0;


        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value % MathHelper.TwoPi;
            }
        }

        public string CurrentAnimationName
        {
            set
            {
                if (animations.ContainsKey(value))
                {
                    currentAnimation = value;
                }
            }

            get
            {
                return currentAnimation;
            }
        }

        public bool isAnimating
        {
            get
            {
                return this.animating;
            }

            set
            {
                this.animating = value;
            }
        }

        public FrameAnimation CurrentAnimation
        {
            get
            {
                if (animations.Count == 0)
                {
                    return null;
                }
                else
                {
                    if (CurrentAnimationName == null)
                    {
                        string[] keys = new string[animations.Count];
                        animations.Keys.CopyTo(keys, 0);
                        CurrentAnimationName = keys[0];
                    }
                    return animations[currentAnimation];
                }
            }
        }

        public SpriteAnimation(Texture2D texture)
        {
            this.texture = texture;
            location = Vector2.Zero;
        }

        public void Update(GameTime gameTime, Vector2 location)
        {
            if (!isAnimating)
                return;

            this.location = location;
            FrameAnimation anim = CurrentAnimation;
            if (anim == null)
            {
                return;
            }
            anim.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            FrameAnimation anim = CurrentAnimation;
            if (anim == null)
            {
                return;
            }

            //Might not be wise to put Begin here for everysprite but for now lets leave it here and test.
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, Camera.TransFormMatrix);
            spriteBatch.Draw(texture, 
                new Rectangle(
                    (int)location.X, (int)location.Y, CurrentAnimation.CurrentRect.Width, CurrentAnimation.CurrentRect.Height),
                    anim.CurrentRect, Color.White, Rotation,
                new Vector2(CurrentAnimation.CurrentRect.Width / 2, CurrentAnimation.CurrentRect.Height / 2), SpriteEffects.None, 0);
            ///Some Test this Code and tell me.
        }

       /// <summary>
       /// Copy Constructer
       /// </summary>
       /// <param name="anim"></param>
        public SpriteAnimation(SpriteAnimation anim)
        {
            texture = anim.texture;
            currentAnimation = anim.currentAnimation;
            location = anim.location;
            animating = true;

            foreach (KeyValuePair<string, FrameAnimation> s in anim.animations)
                animations.Add(s.Key, s.Value.Clone());

        }


       /// <summary>
       /// Use this to Add more frame Animation's for this sprite.
       /// </summary>
       /// <param name="Key"></param>
       /// <param name="Animation"></param>
        public void AddAnimations(string Key, FrameAnimation Animation)
        {
            if(Animation != null)
                animations.Add(Key, Animation);
        }



    }
}
