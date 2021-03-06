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
        Texture2D nakedplayer;
        

        TileMap Map;
        Player player;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
  
            
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
            //Map.Addlayer(new TileLayer(layer2));

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
             Dictionary<int, string> Textures;
             Map = TileMap.ReadInMap(Content.RootDirectory + "/Map1.map", out Textures);
             string[] Strings = new string[Textures.Values.Count];
             Textures.Values.CopyTo(Strings, 0);
             foreach (TileLayer layer in Map.Layers)
             {
                 layer.AddTextures(Content, Strings);
             }

            nakedplayer = Content.Load<Texture2D>("Sprites/8_way_large");
            SpriteAnimation animation = new SpriteAnimation(nakedplayer);
            FrameAnimation Down = new FrameAnimation(10, nakedplayer.Width / 4, nakedplayer.Height / 2, 1, (nakedplayer.Width / 4)*0, 0, new Point(0, 0));
            FrameAnimation DownLeft = new FrameAnimation(10, nakedplayer.Width / 4, nakedplayer.Height / 2, 1, (nakedplayer.Width / 4) * 1, 0, new Point(0, 0));
            FrameAnimation Left = new FrameAnimation(10, nakedplayer.Width / 4, nakedplayer.Height / 2, 1, (nakedplayer.Width / 4) * 2, 0, new Point(0, 0));
            FrameAnimation UpLeft = new FrameAnimation(10, nakedplayer.Width / 4, nakedplayer.Height / 2, 1, (nakedplayer.Width / 4) * 3, 0, new Point(0, 0));
            FrameAnimation Up = new FrameAnimation(10, nakedplayer.Width / 4, nakedplayer.Height / 2, 1, (nakedplayer.Width / 4) * 0, (nakedplayer.Height / 2), new Point(0, 0));
            FrameAnimation UpRight = new FrameAnimation(10, nakedplayer.Width / 4, nakedplayer.Height / 2, 1, (nakedplayer.Width / 4 )* 1, (nakedplayer.Height / 2), new Point(0, 0));
            FrameAnimation Right = new FrameAnimation(10, nakedplayer.Width / 4, nakedplayer.Height / 2, 1, (nakedplayer.Width / 4) * 2, (nakedplayer.Height / 2), new Point(0, 0));
            FrameAnimation DownRight = new FrameAnimation(10, nakedplayer.Width / 4, nakedplayer.Height / 2, 1, (nakedplayer.Width / 4) * 3, (nakedplayer.Height / 2), new Point(0, 0));

            animation.AddAnimations("Down", Down);
            animation.AddAnimations("DownRight", DownLeft);
            animation.AddAnimations("Right", Left);
            animation.AddAnimations("UpRight", UpLeft);
            animation.AddAnimations("Up", Up);
            animation.AddAnimations("UpLeft", UpRight);
            animation.AddAnimations("Left", Right);
            animation.AddAnimations("DownLeft", DownRight);

            player = new Player(animation, new Vector2(200,200), 30f, 0);

            
            
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

            if (InputHandler.KeyDown(Keys.Up))
            {
                pos.Y += 2;
            }
            if (InputHandler.KeyDown(Keys.Down))
            {
                pos.Y -= 2;
            }
            if (InputHandler.KeyDown(Keys.Left))
            {
                pos.X -= 2;
            }
            if (InputHandler.KeyDown(Keys.Right))
            {
                pos.X += 2;
            }

            Camera.Position = pos;
            player.Update(gameTime);

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
            player.Draw(spriteBatch);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
