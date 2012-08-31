﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sage_Engine
{

    public enum DirectionFacing
    {
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        Left,
        Upleft,
    }

   public abstract class DrawAble
   {
       #region Variables
       protected SpriteAnimation spriteAnimation;
       protected Vector2 location; //Location is now always in world Co-ordinates.
       protected Vector2 direction;
       protected float speed;
       protected float rotation;

       protected int CollisonXoffset;
       protected int CollisonYoffset;

       protected int collisionRadius;
       #endregion   
       
       #region Properties


       /// <summary>
       /// Speed given is pixels moved per second instead of the conventional Pixels moved per frame.
       /// This gives more constant and accurate scaling from frame to frame.(speed cannot be set to a negative number.
       /// </summary>
       /// 
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

       /// <summary>
       /// Get And Set Collision Radius
       /// </summary>
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
       /// <summary>
       /// Get Collision Rectangle
       /// </summary>
       public virtual Rectangle GetCollisionRect
       {
           get
           {
               return new Rectangle((int)location.X + CollisonXoffset, (int)location.Y - CollisonYoffset,
                   spriteAnimation.CurrentAnimation.CurrentRect.Width - (CollisonXoffset * 2),
                   spriteAnimation.CurrentAnimation.CurrentRect.Height - (CollisonYoffset * 2));
           }
       }
       /// <summary>
       /// REturn the center of the sprite
       /// </summary>
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

       #endregion

       #region Constructors
       /// <summary>
       /// Pass a SpriteAnimation Object , vector2 Location , vector2 Speed , int CollisionRadius
       /// </summary>
       /// <param name="spriteAnimation"></param>
       /// <param name="location"></param>
       /// <param name="speed"></param>
       /// <param name="collisionRadius"></param>
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
       /// <summary>
       /// OverLoad Constructor with spriteAnimation , vector2 speed , vector2 speed , int collisionRadius , int collisionXOffset , int collsionsYOffset
       /// </summary>
       /// <param name="spriteAnimation"></param>
       /// <param name="location"></param>
       /// <param name="speed"></param>
       /// <param name="collisionRadius"></param>
       /// <param name="collisionXoffset"></param>
       /// <param name="collisionYoffset"></param>
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
       #endregion

       #region Logic
       /// <summary>
       /// update
       /// </summary>
       /// <param name="gameTime"></param>
       public virtual void Update(GameTime gameTime)
       {
           Movement(gameTime);
           spriteAnimation.Rotation = rotation;
           spriteAnimation.Update(gameTime, location);

       }
       #endregion

       #region Draw

       /// <summary>
       /// Drawing code
       /// </summary>
       /// <param name="spriteBatch"></param>
       public virtual void Draw(SpriteBatch spriteBatch)
       {
           spriteAnimation.Draw(spriteBatch);
       }
       #endregion

       #region Helper Methods

       /// <summary>
       /// Movement method
       /// </summary>
       /// <param name="gameTime"></param>
       public virtual void Movement(GameTime gameTime)
       {
           float elaspedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
           location += direction * (speed * elaspedTime);
           
       }
       /// <summary>
       /// Create a new rectangle 
       /// </summary>
       /// <returns></returns>
       public Rectangle CurrentSpriteRect()
       {
           return spriteAnimation.CurrentAnimation.CurrentRect;
       }

       #endregion

      
       #region AnimationCode
       /// <summary>
       /// Add items to the Dicionary of Animations
       /// </summary>
       /// <param name="Key"></param>
       /// <param name="Animation"></param>
       public void AddAnimations(string Key, FrameAnimation Animation)
       {
           spriteAnimation.AddAnimations(Key, Animation);
       }

       /// <summary>
       /// Switch Animations
       /// </summary>
       /// <param name="Key"></param>
       public void SwitchAnimations(string Key)
       {
           spriteAnimation.CurrentAnimationName = Key;
       }

       /// <summary>
       /// Stop animations
       /// </summary>
       public void StopAnimating()
       {
           spriteAnimation.isAnimating = false;
       }

       /// <summary>
       /// Start Animation
       /// </summary>
       public void StartAnimating()
       {
           spriteAnimation.isAnimating = true;
       }
       #endregion
      

       
    }
}
