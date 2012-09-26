using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
        UpLeft,
    }

    public abstract class Humonoid : DrawAble
    {
        protected int health;
        protected float mana;
        protected float manaRegen;
        protected Vector2 OldLocation;
        protected DirectionFacing directionFacing;
        protected SpellManager spellManager;
      
        public Humonoid(SpriteAnimation spriteAnimation,
           Vector2 location,
           float speed,
           int collisionRadius)
            : base(spriteAnimation, location, speed, collisionRadius)
        { 
        }

        #region Properties
        public int Health
        {
            get { return this.health; }
            set
            {
                if (health + value <= 100)
                {
                    health += value;
                }
                else
                {
                    health = 100;
                }
            }
        }

        public float Mana
        {
            get { return this.mana; }
            set
            {
                if (mana + value <= 100)
                {
                    mana += value;
                }
                else
                {
                    health = 100;
                }
            }
        }
        #endregion

        #region HelperCode
        
        protected int manaSeconds;
        protected void manaTicks(GameTime gameTime)
        {
            manaSeconds += gameTime.ElapsedGameTime.Milliseconds;
            if (manaSeconds >= 1000)
            {
                manaSeconds = 0;
                if (manaRegen + mana <= 100)
                {
                    mana += manaRegen;
                }
                else
                {
                    mana = 100;
                }
            }
        }

        protected void CalculateDirectionFacing(Vector2 DirectionToFace)
        {

            Vector2 location = CenterofSprite;

            if ((location != null) && (DirectionToFace != null))
            {
                float Angle = (float)Math.Atan2(( DirectionToFace.Y- location.Y ), (DirectionToFace.X-location.X ));

                Angle = MathHelper.ToDegrees(Angle);
               
                if (InputHandler.KeyDown(Keys.Space)) {
                    Console.WriteLine(Angle);
                    Console.WriteLine(location);

                }

                if (Angle > -45 && Angle < 0)
                {
                    if (Angle < -22.5)
                    {
                        directionFacing = DirectionFacing.UpLeft;
                    }
                    else
                    {
                        directionFacing = DirectionFacing.Left;
                    }
                  
                }
                else if (Angle > -90 && Angle < -45)
                {
                    if (Angle < -67.5)
                    {
                        directionFacing = DirectionFacing.Up;
                    }
                    else
                    {
                        directionFacing = DirectionFacing.UpLeft;
                    }
                }
                else if (Angle > -135 && Angle < -90)
                {
                    if (Angle < -112.5)
                    {
                        directionFacing = DirectionFacing.UpRight;
                    }
                    else
                    {
                        directionFacing = DirectionFacing.Up;
                    }

                }
                else if (Angle > -180 && Angle < -135)
                {

                    if (Angle < -157.5)
                    {
                        directionFacing = DirectionFacing.Right;
                    }
                    else
                    {
                        directionFacing = DirectionFacing.UpRight;
                    }
                }

                else if (Angle > 135 && Angle < 180)
                {
                    if (Angle > 157.5)
                    {
                        directionFacing = DirectionFacing.Right;
                    }
                    else
                    {
                        directionFacing = DirectionFacing.DownRight;
                    }
                }
                else if (Angle > 90 && Angle < 135)
                {
                    if (Angle > 112.5)
                    {
                        directionFacing = DirectionFacing.DownRight;
                    }
                    else
                    {
                        directionFacing = DirectionFacing.Down;
                    }
                }
                else if (Angle > 45 && Angle < 90)
                {
                    if (Angle > 67.5)
                    {
                        directionFacing = DirectionFacing.Down;
                    }
                    else
                    {
                        directionFacing = DirectionFacing.DownLeft;
                    }

                }
                else if (Angle > 0 && Angle < 45)
                {

                    if (Angle > 22.5)
                    {
                        directionFacing = DirectionFacing.DownLeft;
                    }
                    else
                    {
                        directionFacing = DirectionFacing.Left;
                    }
                }
            }
        }


        public void SwitchAnimationToFaceDirection()
        {
            switch(directionFacing)
            {
                case DirectionFacing.Up:
                    SwitchAnimations("Up");
                    break;
                case DirectionFacing.Down:
                    SwitchAnimations("Down");
                    break;
                case DirectionFacing.Left:
                    SwitchAnimations("Left");
                    break;
                case DirectionFacing.Right:
                    SwitchAnimations("Right");
                    break;
                case DirectionFacing.UpLeft:
                    SwitchAnimations("UpLeft");
                    break;
                case DirectionFacing.UpRight:
                    SwitchAnimations("UpRight");
                    break;
                case DirectionFacing.DownLeft:
                    SwitchAnimations("DownLeft");
                    break;
                case DirectionFacing.DownRight:
                    SwitchAnimations("DownRight");
                    break;
            }
        }
        #endregion
    }
}
