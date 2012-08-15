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
    public class spriteAnimation
    {
        public Texture2D texture;
        string currentAnimation;
        Vector2 location;
        bool animating = true;
        public Dictionary<string, framAnimation> animations = new Dictionary<string, framAnimation>();

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

        public framAnimation CurrentAnimation
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

        public spriteAnimation(Texture2D texture)
        {
            this.texture = texture;
            location = Vector2.Zero;
        }

        public void Update(GameTime gameTime, Vector2 location)
        {
            this.location = location;
            framAnimation anim = CurrentAnimation;
            if (anim == null)
            {
                return;
            }
            anim.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            framAnimation anim = CurrentAnimation;
            if (anim == null)
            {
                return;
            }


            spriteBatch.Draw(texture, location, anim.CurrentRect, Color.White);
        }

        public spriteAnimation(spriteAnimation anim)
        {
            texture = anim.texture;
            currentAnimation = anim.currentAnimation;
            location = anim.location;
            animating = true;

            foreach (KeyValuePair<string, framAnimation> s in anim.animations)
                animations.Add(s.Key, s.Value.Clone());

        }



    }
}
