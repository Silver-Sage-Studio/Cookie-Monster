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
       protected Vector2 speed;

       protected int CollisonXoffset;
       protected int CollisonYoffset;

       protected int collisionRadius;


       public Vector2 Speed
       {
           get
           {
               return speed;
           }
           set
           {
               speed = value;
           }
       }


       public int CollisionRadius
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

       public Rectangle GetCollisionRect
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


       public DrawAble(SpriteAnimation spriteAnimation,
           Vector2 location,
           Vector2 speed,
           int collisionRadius)
       {
           this.spriteAnimation = spriteAnimation;
           this.location = location;
           this.speed = speed;
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
           this.speed = speed;
           this.collisionRadius = collisionRadius;
           this.CollisonXoffset = collisionXoffset;
           this.CollisonYoffset = collisionYoffset;
       }

       public virtual void Update(GameTime gameTime)
       {
           Movement(gameTime);
           spriteAnimation.Update(gameTime, location);
       }


       public virtual void Draw(SpriteBatch spriteBatch)
       {
           spriteAnimation.Draw(spriteBatch);
       }

       public abstract void Movement(GameTime gameTime);


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
       
    }
}
