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
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
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

            player.loadPlayer(this.graphics, this.Content);


            objTex = new Texture2D(graphics.GraphicsDevice, 64, 64);
            objTex = Content.Load<Texture2D>("object");


          
            #region testLevel

            GObjects.Add(new GameObject(objTex,new Rectangle(100,200,20,200)));
            GObjects.Add(new GameObject(objTex, new Rectangle(180, 200, 20, 200)));

            GObjects.Add(new GameObject(objTex, new Rectangle(600, 500, 200, 10)));

            GObjects.Add(new GameObject(objTex, new Rectangle(400, 400, 200, 10)));

            GObjects.Add(new GameObject(objTex, new Rectangle(600, 300, 200, 10)));

            GObjects.Add(new GameObject(objTex, new Rectangle(300, 200, 200, 10)));

            GObjects.Add(new MoveableObject(0, 10, objTex, new Rectangle(120,360,60,40),0));
            GObjects.Add(new MoveableObject(0, 9, objTex, new Rectangle(120, 320, 60, 40), 0));
            GObjects.Add(new MoveableObject(0, 8, objTex, new Rectangle(120, 280, 60, 40), 0));
            GObjects.Add(new MoveableObject(0, 7, objTex, new Rectangle(120, 240, 60, 40), 0));
            GObjects.Add(new MoveableObject(0, 6, objTex, new Rectangle(120, 200, 60, 40), 0));

            GObjects.Add(new MoveableObject(10, 0, objTex, new Rectangle(100, 400, 100, 64), Keys.W, Keys.S, Keys.A, Keys.D, 0));
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

            spriteBatch.Draw(background,mainFrame,Color.White);


            foreach (GameObject gobject in GObjects)
            {
                gobject.Draw(ref spriteBatch);
            }

            spriteBatch.DrawString(Arial, "Ausgangsposition: " + player.jumpRelative, new Vector2(1, 1), Color.White);
            spriteBatch.DrawString(Arial, "Sprunghoehe: " + player.jumpHeight, new Vector2(1, 30), Color.White);
            spriteBatch.DrawString(Arial, "Springt: " + player.jumping, new Vector2(1, 60), Color.White);
            spriteBatch.DrawString(Arial, "Updates: " + updateCounter, new Vector2(1, 90), Color.White);

            for (int i = 0; i < kbstate.GetPressedKeys().Count(); i++)
            {
                spriteBatch.DrawString(Arial, kbstate.GetPressedKeys()[i].ToString(), new Vector2(1, 120 + i*30), Color.White);
            }

                spriteBatch.End();

            base.Draw(gameTime);
        }

   


    }
}
