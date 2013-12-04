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
        Rectangle mainFrame = new Rectangle(0, 0, 1024, 768);
        Rectangle bgFrame = new Rectangle(0, 0, 2048, 768);
        
        Texture2D background;
        MoveableObject obj;
        MoveableObject obj2;
        MoveableObject obj3;
        Texture2D objTex;
        
        SpriteFont Arial;
        int updateCounter = 0;

        // List of all GameObjects

        List<GameObject> GObjects = new List<GameObject>();

        // Instanz
        Player player = new Player();

        public Game1()
        {
            // open a window 800x600
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
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
            background = Content.Load<Texture2D>("landscape_2048x768");

            player.loadPlayer(this.graphics, this.Content);


            objTex = new Texture2D(graphics.GraphicsDevice, 64, 64);
            objTex = Content.Load<Texture2D>("object");


          
            #region testLevel

            GObjects.Add(new GameObject(objTex,new Rectangle(100,400,20,200)));
            GObjects.Add(new GameObject(objTex, new Rectangle(180, 400, 20, 200)));

            GObjects.Add(new GameObject(objTex, new Rectangle(600, 700, 200, 10)));

            GObjects.Add(new GameObject(objTex, new Rectangle(400, 600, 200, 10)));

            GObjects.Add(new GameObject(objTex, new Rectangle(600, 500, 200, 10)));

            GObjects.Add(new GameObject(objTex, new Rectangle(300, 400, 200, 10)));

            GObjects.Add(new GameObject(objTex,new Rectangle(0,718,1024,50)));

            GObjects.Add(new MoveableObject(0, 10, objTex, new Rectangle(120,560,60,40),0));
            GObjects.Add(new MoveableObject(0, 9, objTex, new Rectangle(120, 520, 60, 40), 0));
            GObjects.Add(new MoveableObject(0, 8, objTex, new Rectangle(120, 480, 60, 40), 0));
            GObjects.Add(new MoveableObject(0, 7, objTex, new Rectangle(120, 440, 60, 40), 0));
            GObjects.Add(new MoveableObject(0, 6, objTex, new Rectangle(120, 400, 60, 40), 0));

            GObjects.Add(new MoveableObject(10, 0, objTex, new Rectangle(100, 600, 100, 64), Keys.W, Keys.S, Keys.A, Keys.D, 0));
            #endregion


            GObjects.Add(player);


            Arial = Content.Load<SpriteFont>("Arial");

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
            updateCounter++;
            KeyboardState kbstate = Keyboard.GetState();

            // Allows the game to exit
            if (kbstate.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

           

            foreach (GameObject go in GObjects)
            {
                go.Move(gameTime, kbstate, mainFrame, GObjects);
            }



            //if (player.rectangle.X >= 700)
            //{
            //    foreach (GameObject obj in GObjects)
            //    {
            //        obj.rectangle.X -= player.movementSpeed;
            //    }

            //    bgFrame.X -= player.movementSpeed;
            //}

            //if (player.rectangle.X <= 100)
            //{
            //    foreach (GameObject obj in GObjects)
            //    {
            //        obj.rectangle.X += player.movementSpeed;
            //    }

            //    bgFrame.X += player.movementSpeed;
            //}

            Scrolling.Scroll(player,  GObjects,ref bgFrame, mainFrame);

         

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            KeyboardState kbstate = Keyboard.GetState();
            spriteBatch.Begin();

            spriteBatch.Draw(background,bgFrame,Color.White);


            foreach (GameObject gobject in GObjects)
            {
                gobject.Draw(ref spriteBatch);
            }

            spriteBatch.DrawString(Arial, "Ausgangsposition: " + player.jumpRelative, new Vector2(1, 1), Color.Violet);
            spriteBatch.DrawString(Arial, "Sprunghoehe: " + player.jumpHeight, new Vector2(1, 30), Color.Violet);
            spriteBatch.DrawString(Arial, "Springt: " + player.jumping, new Vector2(1, 60), Color.Violet);
            spriteBatch.DrawString(Arial, "Updates: " + updateCounter, new Vector2(1, 90), Color.Violet);
            spriteBatch.DrawString(Arial, "Height: " + player.rectangle.Bottom, new Vector2(1, 120), Color.Violet);

            for (int i = 0; i < kbstate.GetPressedKeys().Count(); i++)
            {
                spriteBatch.DrawString(Arial, kbstate.GetPressedKeys()[i].ToString(), new Vector2(1, 150 + i * 30), Color.Violet);
            }

                spriteBatch.End();

            base.Draw(gameTime);
        }

   


    }
}
