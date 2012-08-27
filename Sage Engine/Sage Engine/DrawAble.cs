using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sage_Engine
{
   public abstract class DrawAble 
    {
       protected SpriteAnimation spriteAnimation;
       protected Vector2 location; //Location is now always in world Co-ordinates.
       protected Vector2 direction;
       protected float speed;
       protected float rotation;

       protected int CollisonXoffset;
       protected int CollisonYoffset;

       protected int collisionRadius;

       /// <summary>
       /// Speed given is pixels moved per second instead of the conventional Pixels moved per frame.
       /// This gives more constant and accurate scaling from frame to frame.(speed cannot be set to a negative number.
       /// </summary>
       public float Speed
       {
           get
           {
               return speed;
           }
           set
           {
               speed = MathHelper.Clamp(value, 0, 50);
           }
       }

       /// <summary>
       /// This Gives the direction vector the sprite is traveling in.
       /// Will be normalised.(set speed to scale).
       /// </summary>
       public Vector2 Direction
       {
           get
           {
               return direction;
           }
           set
           {
               direction = value;
               if(direction != Vector2.Zero)
                    direction.Normalize();
           }
       }


       public virtual int CollisionRadius
       {
           get
           {
               return collisionRadius;
           }
           set
           {
               collisionRadius = value;
           }
       }

       public virtual Rectangle GetCollisionRect
       {
           get
           {
               return new Rectangle((int)location.X + CollisonXoffset, (int)location.Y - CollisonYoffset,
                   spriteAnimation.CurrentAnimation.CurrentRect.Width - (CollisonXoffset * 2),
                   spriteAnimation.CurrentAnimation.CurrentRect.Height - (CollisonYoffset * 2));
           }
       }

       public Vector2 CenterofSprite
       {
           get
           {
               return new Vector2(location.X + (spriteAnimation.CurrentAnimation.CurrentRect.Width / 2),
                   location.Y - (spriteAnimation.CurrentAnimation.CurrentRect.Height / 2));
           }
           set
           {
               location = new Vector2 (value.X - (spriteAnimation.CurrentAnimation.CurrentRect.Width / 2),
                   value.Y + (spriteAnimation.CurrentAnimation.CurrentRect.Height / 2));
           }
       }

       /// <summary>
       /// Bottom center of sprite, Relative to the top left corner of the sprite.
       /// </summary>
       public Vector2 BaseOfSprite
       {
           get
           {
               return new Vector2(
                   location.Y + spriteAnimation.CurrentAnimation.CurrentRect.Height,
                   location.X + (spriteAnimation.CurrentAnimation.CurrentRect.Width / 2));
           }
       }


       public DrawAble(SpriteAnimation spriteAnimation,
           Vector2 location,
           Vector2 speed,
           int collisionRadius)
       {
           this.spriteAnimation = spriteAnimation;
           this.location = location;
           this.direction = speed;
           this.collisionRadius = collisionRadius;

       }

       public DrawAble(SpriteAnimation spriteAnimation,
           Vector2 location,
           Vector2 speed,
           int collisionRadius,
           int collisionXoffset,
           int collisionYoffset)
       {
           this.spriteAnimation = spriteAnimation;
           this.location = location;
           this.direction = speed;
           this.collisionRadius = collisionRadius;
           this.CollisonXoffset = collisionXoffset;
           this.CollisonYoffset = collisionYoffset;
       }

       public virtual void Update(GameTime gameTime)
       {
           Movement(gameTime);
           spriteAnimation.Rotation = rotation;
           spriteAnimation.Update(gameTime, location);

       }


       public virtual void Draw(SpriteBatch spriteBatch)
       {
           spriteAnimation.Draw(spriteBatch);
       }

       public virtual void Movement(GameTime gameTime)
       {
           float elaspedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
           location += direction * (speed * elaspedTime);
           
       }


       public void AddAnimations(string Key, FrameAnimation Animation)
       {
           spriteAnimation.AddAnimations(Key, Animation);
       }


       public void SwitchAnimations(string Key)
       {
           spriteAnimation.CurrentAnimationName = Key;
       }

       public void StopAnimating()
       {
           spriteAnimation.isAnimating = false;
       }

       public void StartAnimating()
       {
           spriteAnimation.isAnimating = true;
       }

       public Rectangle CurrentSpriteRect()
       {
           return spriteAnimation.CurrentAnimation.CurrentRect;
       }
    }
}
