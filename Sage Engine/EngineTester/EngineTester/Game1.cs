using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Sage_Engine;

namespace CookieMonsterGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TileMap Map;

        int[,] layer1 = new int[,]
        {
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3}
        };

        int[,] layer2 = new int[,]
        {
            {0, 1, 0, 0,0,0,2,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,2,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,2,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,2,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,2,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 2,2,2,2,2,2,2,2,2,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,2,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,2,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,2,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3},
            {0, 1, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,3,3}
        };

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
  
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Components.Add(new InputHandler(this));
            base.Initialize();
            Map = new TileMap();
            Map.Addlayer(new TileLayer(layer1));
            //Map.Addlayer(new TileLayer(layer2));

            foreach (TileLayer layer in Map.Layers)
            {
                layer.AddTexture(Content.Load<Texture2D>("Tiles/texture0"));
                layer.AddTexture(Content.Load<Texture2D>("Tiles/texture1"));
                layer.AddTexture(Content.Load<Texture2D>("Tiles/texture2"));
                layer.AddTexture(Content.Load<Texture2D>("Tiles/texture8"));
            }

            Camera.Initialise(GraphicsDevice);
           
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
           
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Vector2 pos = Camera.Position;

            if (InputHandler.KeyDown(Keys.S))
            {
                pos.Y += 2;
            }
            if (InputHandler.KeyDown(Keys.W))
            {
                pos.Y -= 2;
            }
            if (InputHandler.KeyDown(Keys.A))
            {
                pos.X -= 2;
            }
            if (InputHandler.KeyDown(Keys.D))
            {
                pos.X += 2;
            }
            // TODO: Add your update logic here

            Camera.Position = pos;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Map.Draw(spriteBatch);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
