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
   public class SpriteAnimation
    {
        private Texture2D texture;
        string currentAnimation;
        Vector2 location;
        bool animating = true;
        Dictionary<string, FrameAnimation> animations = new Dictionary<string, FrameAnimation>();


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

            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, Camera.TransFormMatrix);
            spriteBatch.Draw(texture, location, anim.CurrentRect, Color.White); // Havent Tester this code yet. Someone Test and get back to me
            spriteBatch.End();
        }

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
