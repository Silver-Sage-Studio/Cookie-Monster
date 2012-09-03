﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;

namespace Sage_Engine
{
    public abstract class Humonoid : DrawAble
    {
        protected int health;
        protected float mana;
        protected float manaRegen;
        protected Vector2 OldLocation;
        protected DirectionFacing directionFacing;
      

        public Humonoid(SpriteAnimation spriteAnimation,
           Vector2 location,
           Vector2 speed,
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
        #endregion
    }
}