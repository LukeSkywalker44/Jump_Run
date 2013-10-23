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
using Jump_n_Run.classes;
namespace Jump_n_Run
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle mainFrame = new Rectangle(0, 0, 800, 600);
        Texture2D background;
        MoveableObject obj;
        Texture2D objTex;


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
            background = new Texture2D(graphics.GraphicsDevice, 800,600);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("800x600_Pacman");

            objTex = new Texture2D(graphics.GraphicsDevice, 64, 64);
            objTex = Content.Load<Texture2D>("object");
            obj = new MoveableObject(10, objTex, new Rectangle(100, 100, 64, 64));

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

            KeyboardState kbstate = Keyboard.GetState();

            // Allows the game to exit
            if (kbstate.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (kbstate.IsKeyDown(Keys.W))
            {
               if(!Collision(mainFrame,obj.rectangle,obj.movementSpeed))
                obj.rectangle.Y -= obj.movementSpeed;
            }
            if (kbstate.IsKeyDown(Keys.A))
            {
                if (!Collision(mainFrame, obj.rectangle, obj.movementSpeed))
                obj.rectangle.X -= obj.movementSpeed;
            }
            if (kbstate.IsKeyDown(Keys.S))
            {
                if (!Collision(mainFrame, obj.rectangle, obj.movementSpeed))
                obj.rectangle.Y += obj.movementSpeed;
            }
            if (kbstate.IsKeyDown(Keys.D))
            {
                if (!Collision(mainFrame, obj.rectangle, obj.movementSpeed))
                obj.rectangle.X += obj.movementSpeed;
            }
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background,mainFrame,Color.White);
            spriteBatch.Draw(obj.Texture, obj.rectangle, Color.White);
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }

        static bool Collision(Rectangle bounds, Rectangle moveable, int moveSpeed)
        {
            bool result1;
            bool result2;
            bool result3;
            bool result4;

            Rectangle futureRect1 = new Rectangle(moveable.X + moveSpeed ,moveable.Y, moveable.Width, moveable.Height);
            Rectangle futureRect2 = new Rectangle(moveable.X - moveSpeed ,moveable.Y, moveable.Width, moveable.Height);
            Rectangle futureRect3 = new Rectangle(moveable.X , moveable.Y + moveSpeed , moveable.Width, moveable.Height);
            Rectangle futureRect4 = new Rectangle(moveable.X , moveable.Y - moveSpeed , moveable.Width, moveable.Height);

            bounds.Contains(ref futureRect1, out result1);
            bounds.Contains(ref futureRect2, out result2);
            bounds.Contains(ref futureRect3, out result3);
            bounds.Contains(ref futureRect3, out result4);


            return !(result1 & result2 & result3 & result4);
        }
    }
}
